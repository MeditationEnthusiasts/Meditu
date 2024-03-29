//
// Meditu - A way to track Meditation Sessions.
// Copyright (C) 2017-2022 Meditation Enthusiasts.
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published
// by the Free Software Foundation, either version 3 of the License, or
// any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
//

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using LiteDB;
using SethCS.Collections;
using SethCS.Exceptions;
using SethCS.Extensions;

namespace Meditu.Api
{
    /// <summary>
    /// This class keeps track of all the logs.
    /// 
    /// The BaseClass is a list of logs in the order from which the start time is,
    /// where the most current is index 0, and the earliest is
    /// the last element in the list.
    /// </summary>
    /// <remarks>
    /// There are properties in this class considered "shortcut properties".
    /// These are properties that require a lot of CPU time to figure out,
    /// so the are cached.  These values are kept even after the underlying list is cleared via
    /// <see cref="ClearCache"/>.  They are recalculated when <see cref="Refresh"/> is called,
    /// and are reset when <see cref="ResetState"/> is called.
    /// </remarks>
    public class LogBook : BaseCloningReadOnlyList<Log>, IDisposable
    {
        // ---------------- Fields ----------------

        /// <summary>
        /// Table of logs whose key is the guid of the logs.
        /// Useful for quick lookups to see if a log exists.
        /// </summary>
        private readonly Dictionary<Guid, Log> logTable;

        private readonly LiteDatabase db;

        private readonly ILiteCollection<Log> col;

        // -------- Shortcut Fields --------

        private readonly List<int> startTimeBucket;
        private readonly Dictionary<string, int> techniques;

        // ---------------- Constructor ----------------

        public LogBook( string dbLocation ) :
            base( new List<Log>() )
        {
            this.logTable = new Dictionary<Guid, Log>();

            this.startTimeBucket = new List<int>( 24 );
            for( int i = 0; i < 24; ++i )
            {
                this.startTimeBucket.Add( 0 );
            }

            this.StartTimeBucket = this.startTimeBucket.AsReadOnly();

            this.techniques = new Dictionary<string, int>();
            this.Techniques = new ReadOnlyDictionary<string, int>( this.techniques );

            this.ResetStateNoLock();

            var connectionString = new ConnectionString
            {
                Upgrade = true,
                Filename = dbLocation,
                Connection = ConnectionType.Shared // <- So multiple threads can access it without issue.
            };

            this.db = new LiteDatabase( connectionString );

            // Use UTC for everything in the back-end to keep our sanity.
            // When presenting to the front-end, the front-end will handle
            // converting date strings to use the timezone
            // configured in the settings.
            this.db.Pragma( "UTC_DATE", true );
            this.col = this.db.GetCollection<Log>();
        }

        // ---------------- Properties ----------------

        /// <summary>
        /// Returns a copy of the log at the specified GUID.
        /// </summary>
        /// <exception cref="KeyNotFoundException">If there is no log at the GUID.</exception>
        public Log this[Guid g]
        {
            get
            {
                lock( this.list )
                {
                    return this.logTable[g].Clone();
                }
            }
        }

        // -------- Shortcut Properties --------

        /// <summary>
        /// Read-only list of start times.
        /// This is a list whose length is 24.
        /// Each index is the hour in which a session started.
        /// So if index 0 has 2 in it, then there were 2 sessions that started
        /// between 12AM - 1AM.
        /// 
        /// Note: Like our <see cref="Log"/> object, this is based on UTC time.
        /// </summary>
        public IReadOnlyList<int> StartTimeBucket { get; private set; }

        /// <summary>
        /// Dictionary of techniques where key is the technique used and
        /// value is the number of times the technique was used.
        /// 
        /// The key will ALWAYS be in all lower-case, even if the user wrote it in all caps.
        /// - Whitespace on the ends will be stripped.
        /// - Multiple whitespace characters in a row will be reduced to one.
        /// 
        /// The value is the number of times the technique was used.
        /// </summary>
        public IReadOnlyDictionary<string, int> Techniques { get; private set; }

        /// <summary>
        /// The total time of all the logs.
        /// </summary>
        public TimeSpan TotalTime { get; private set; }

        /// <summary>
        /// The Log that contains the longest time
        /// </summary>
        public Log? LongestSession { get; private set; }

        public TimeSpan LongestTime => LongestSession?.Duration ?? TimeSpan.Zero;

        /// <summary>
        /// The total number of sessions.
        /// Note that this is different than <see cref="BaseCloningReadOnlyList{T}.Count"/>.
        /// That is the number of elements in the cache.  This is the total number of sessions.
        /// </summary>
        public int TotalSessions { get; private set; }

        // ---------------- Functions ----------------

        public void Dispose()
        {
            this.db?.Dispose();
            GC.SuppressFinalize( this );
        }

        public Task AsyncRefresh()
        {
            return Task.Run( () => this.Refresh() );
        }

        /// <summary>
        /// Loads all the logs from the database and puts them into cache.
        /// All of the "shortcut" properties are reset and updated as well.
        /// </summary>
        public void Refresh()
        {
            if( this.db == null )
            {
                throw new InvalidOperationException( "Database is not open!  Database must be open!" );
            }

            lock( this.list )
            {
                this.RefreshNoLock();
            }
        }

        public Log? TryGetLog( Guid guid )
        {
            lock( this.list )
            {
                if( this.logTable.ContainsKey( guid ) == false )
                {
                    return null;
                }
                else
                {
                    return this.logTable[guid].Clone();
                }
            }
        }

        /// <summary>
        /// Clears the list (might save memory), but will NOT clear any of the "shortcut" properties.
        /// </summary>
        public void ClearCache()
        {
            lock( this.list )
            {
                this.ClearCacheNoLock();
            }
        }

        /// <summary>
        /// Imports the given log into the database.
        /// - If the passed in log's GUID already exists, and the Edit Time is earlier
        ///   than the saved version, nothing happens.
        /// - If the passed in log's GUID already exists, but the Edit Time is later
        ///   than the saved version, the log in the database gets overwritten with this log.
        /// - If the passed in log's GUID exits in cache, but does not exist in the database,
        ///   the log will be added to the database.
        /// </summary>
        /// <returns>
        /// True if the log was imported, false if the log was not imported.
        /// This returns false if the log already exists.
        /// </returns>
        public bool ImportLog( Log log )
        {
            log.Validate();
            log = log.Clone();

            lock( this.list )
            {
                if ( this.logTable.ContainsKey( log.Guid ) )
                {
                    if ( this.logTable[log.Guid].EditTime >= log.EditTime )
                    {
                        return false;
                    }
                }

                Log savedLog = this.col.FindOne( l => l.Guid == log.Guid );
                if ( savedLog == null )
                {
                    this.col.Insert( log );
                    this.AddLogNoLock( log );
                }
                else
                {
                    log.Id = savedLog.Id;
                    this.col.Update( log );
                    this.logTable[log.Guid] = log;
                    this.UpdateShortcutProperties( log );
                }

                this.SortCache();

                return true;
            }
        }

        /// <summary>
        /// Adds the given log to the database.
        /// </summary>
        /// <param name="log">
        /// The log to add to the database.
        /// The GUID will be updated and returned.
        /// </param>
        /// <returns>The GUID of the log.</returns>
        public Guid AddLogToDb( Log log )
        {
            log.Validate();

            log = log.Clone();
            log.Guid = Guid.NewGuid();

            lock( this.list )
            {
                this.col.Insert( log );
                this.AddLogNoLock( log );

                this.SortCache();
            }

            return log.Guid;
        }

        public void EditLog( Guid id, EditLogSettings settings )
        {
            if( settings.TryValidate( out string errorString ) == false )
            {
                throw new ValidationException( errorString );
            }

            lock( this.list )
            {
                if( this.logTable.ContainsKey( id ) == false )
                {
                    throw new KeyNotFoundException(
                        $"Can not find log with ID {id}, can not edit."
                    );
                }

                Log log = this.logTable[id];
                log.EditLog( settings );
                this.col.Update( log );

                // Need to rebuild our cache.
                // There's probably a more efficent way to do this.
                this.RefreshNoLock();
            }
        }

        /// <summary>
        /// Tries to delete the log at the given ID from
        /// the database. 
        /// </summary>
        /// <returns>The log that was deleted.  Null if no log of the ID exists.</returns>
        public Log? DeleteLog( Guid id )
        {
            lock( this.list )
            {
                if( this.logTable.ContainsKey( id ) == false )
                {
                    return null;
                }

                Log log = this.logTable[id];
                this.col.DeleteMany( l => l.Guid == id );

                // Need to do a full refresh as our "shortcuts" all change.
                // A lot of CPU for deleting a log.
                // Maybe we'll optimize later.
                RefreshNoLock();

                return log;
            }
        }

        /// <summary>
        /// Clears the list AND resets the "shortcut" properties.
        /// </summary>
        public void ResetState()
        {
            lock( this.list )
            {
                this.ResetStateNoLock();
            }
        }

        protected override Log CloneInstructions( Log original )
        {
            return original.Clone();
        }

        private void SortCache()
        {
            // Sort by date.
            // Calling OrderByDecending and then clear on a list will clear the IOrderedEnumerable
            // that is returned by OrderByDecending.  So, we need to create a new list >_>.
            // There needs to be a better way to do this...

            List<Log> logs = new List<Log>( this.list.OrderByDescending( l => l.StartTime ) );
            this.list.Clear();
            foreach( Log l in logs )
            {
                this.list.Add( l );
            }
        }

        private void AddLogNoLock( Log log )
        {
            this.list.Add( log );
            this.logTable[log.Guid] = log;
            this.UpdateShortcutProperties( log );
        }

        private void ClearCacheNoLock()
        {
            this.list.Clear();
            this.logTable.Clear();
        }

        private void ResetStateNoLock()
        {
            this.ClearCacheNoLock();

            for( int i = 0; i < this.startTimeBucket.Count; ++i )
            {
                this.startTimeBucket[i] = 0;
            }

            this.techniques.Clear();

            this.TotalSessions = 0;
            this.TotalTime = TimeSpan.Zero;
            this.LongestSession = null;
        }

        private void RefreshNoLock()
        {
            this.ResetStateNoLock();
            IOrderedEnumerable<Log> logs = col.FindAll().OrderByDescending( l => l.StartTime );

            foreach( Log log in logs )
            {
                this.AddLogNoLock( log );
            }
        }

        private void UpdateShortcutProperties( Log log )
        {
            ++this.TotalSessions;

            this.TotalTime += log.Duration;
            if( log.Duration > this.LongestTime )
            {
                this.LongestSession = log;
            }

            ++this.startTimeBucket[log.StartTime.Hour];

            string techniqueKey = log.Technique.ToLower(); // Already Trimed in Log.
            techniqueKey = techniqueKey.NormalizeWhiteSpace();

            if( this.techniques.ContainsKey( techniqueKey ) == false )
            {
                this.techniques[techniqueKey] = 0;
            }

            ++this.techniques[techniqueKey];
        }
    }

    public static class LogBookExtensions
    {
        // ---------------- Fields ----------------

        internal const string XmlElementName = "logbook";

        /// <summary>
        /// Version 1:
        ///     Original version.  Everything was an attribute.
        ///     Times were in local time.
        ///
        /// Version 2:
        ///     Everything is an element.
        ///     Times are in UTC time.
        /// </summary>
        internal const int XmlVersion = 2;

        internal const string VersionAttributeName = "version";

        // ---------------- Functions ----------------

        public static XmlDocument ToXml( this LogBook logbook )
        {
            XmlDocument doc = new XmlDocument();

            // Create declaration.
            XmlDeclaration dec = doc.CreateXmlDeclaration( "1.0", "UTF-8", null );

            // Add declaration to document.
            XmlElement? root = doc.DocumentElement;
            doc.InsertBefore( dec, root );

            XmlElement logbookNode = doc.CreateElement( XmlElementName );
            {
                XmlAttribute versionAttribute = doc.CreateAttribute( VersionAttributeName );
                versionAttribute.Value = XmlVersion.ToString();
                logbookNode.Attributes.Append( versionAttribute );
            }

            List<Log> logList = logbook.ToList();

            foreach( Log log in logList )
            {
                log.ToXml( doc, logbookNode );
            }

            doc.InsertAfter( logbookNode, dec );

            return doc;
        }

        /// <summary>
        /// Converts the <see cref="LogBook.StartTimeBucket"/>
        /// from UTC to local time.
        /// </summary>
        /// <returns>
        /// A dictionary whose key is the local time's hour,
        /// and value is the number of start times that started
        /// within that hour.
        /// </returns>
        public static IDictionary<int, int> StartTimesToLocalTime(
            this LogBook logbook,
            DateTimeSettings dateTimeSettings
        )
        {
            var dic = new Dictionary<int, int>();

            for( int i = 0; i < logbook.StartTimeBucket.Count; ++i )
            {
                DateTime date = new DateTime( 2022, 6, 13, i, 0, 0, DateTimeKind.Utc );
                date = date.ToTimeZoneTime( dateTimeSettings );
                dic[date.Hour] = logbook.StartTimeBucket[i];
            }

            return dic;
        }

        /// <returns>
        /// -1 if no sessions are recorded, otherwise
        /// returns the hour that contains the most sessions
        /// in UTC.
        /// </returns>
        public static int GetFavoriteStartTimeUtc(
            this LogBook logbook
        )
        {
            if( logbook.TotalSessions == 0 )
            {
                return -1;
            }

            // Must use ToList() as IReadOnlyList doesn't implement IndexOf.
            return logbook.StartTimeBucket.ToList().IndexOf( logbook.StartTimeBucket.Max() );
        }

        /// <returns>
        /// -1 if no sessions are recorded, otherwise
        /// returns the hour that contains the most sessions
        /// in local time.
        /// </returns>
        public static int GetFavoriteStartTimeInLocalTime(
            this LogBook logbook,
            DateTimeSettings dateTimeSettings
        )
        {
            int utcHour = GetFavoriteStartTimeUtc( logbook );
            if( utcHour == -1 )
            {
                return -1;
            }

            DateTime date = new DateTime( 2022, 6, 13, utcHour, 0, 0, DateTimeKind.Utc );
            date = date.ToTimeZoneTime( dateTimeSettings );
            return date.Hour;
        }
    }
}

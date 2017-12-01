//
// Meditation Logger.
// Copyright (C) 2017  Seth Hendrick.
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using SethCS.Collections;
using SethCS.Extensions;

namespace MeditationLogger.Api
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
        private Dictionary<Guid, Log> logTable;

        private LiteDatabase db;

        private LiteCollection<Log> col;

        // -------- Shortcut Fields --------

        private List<int> startTimeBucket;
        private Dictionary<string, int> techniques;

        // ---------------- Constructor ----------------

        public LogBook() :
            base( new List<Log>() )
        {
            this.logTable = new Dictionary<Guid, Log>();

            this.startTimeBucket = new List<int>( 24 );
            for( int i = 0; i < 24; ++i )
            {
                this.startTimeBucket[i] = 0;
            }

            this.StartTimeBucket = this.startTimeBucket.AsReadOnly();

            this.techniques = new Dictionary<string, int>();
            this.Techniques = new ReadOnlyDictionary<string, int>( this.techniques );

            this.ResetStateNoLock();
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
        /// Note: Like our <see cref="Log"/> object, this is based on UTC.
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
        /// The longest time of all the logs.
        /// </summary>
        public TimeSpan LongestTime { get; private set; }

        /// <summary>
        /// The total number of sessions.
        /// Note that this is different than <see cref="BaseCloningReadOnlyList{T}.Count"/>.
        /// That is the number of elements in the cache.  This is the total number of sessions.
        /// </summary>
        public int TotalSessions { get; private set; }

        // ---------------- Functions ----------------

        public void OpenDb( string dbLocation )
        {
            if( this.db != null )
            {
                throw new InvalidOperationException( "Database is already open!" );
            }

            this.db = new LiteDatabase( dbLocation );
            this.col = this.db.GetCollection<Log>( "logs" );
        }

        public void Dispose()
        {
            this.db?.Dispose();
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
                this.ResetStateNoLock();
                IOrderedEnumerable<Log> logs = col.FindAll().OrderBy( l => l.StartTime );
                foreach( Log log in logs )
                {
                    this.AddLogNoLock( log );
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
                BsonValue id = this.col.Insert( log );
                this.AddLogNoLock( log );
            }

            return log.Guid;
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

            this.startTimeBucket.Clear();
            this.techniques.Clear();

            this.TotalSessions = 0;
            this.TotalTime = TimeSpan.Zero;
            this.LongestTime = TimeSpan.Zero;
        }

        private void UpdateShortcutProperties( Log log )
        {
            ++this.TotalSessions;

            this.TotalTime += log.Duration;
            if( log.Duration > this.LongestTime )
            {
                this.LongestTime = log.Duration;
            }

            ++this.startTimeBucket[log.StartTime.Hour];

            string techniqueKey = log.Technique.Trim();
            techniqueKey = techniqueKey.NormalizeWhiteSpace();

            if( this.techniques.ContainsKey( techniqueKey ) )
            {
                ++this.techniques[techniqueKey];
            }
            else
            {
                this.techniques[techniqueKey] = 0;
            }
        }
    }
}

//
// Meditu - A way to track Meditation Sessions.
// Copyright (C) 2017-2021 Seth Hendrick.
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
using System.Globalization;
using System.Text;
using System.Xml;
using LiteDB;
using SethCS.Exceptions;
using SethCS.Extensions;

namespace Meditu.Api
{
    /// <summary>
    /// This class represents a specific instance
    /// of an event logged.
    /// </summary>
    public class Log : IEquatable<Log>
    {
        // ---------------- Fields ----------------

        private string comments;
        private string technique;

        // ---------------- Constructor ----------------

        /// <summary>
        /// The maximum time we support.
        /// Mainly here for unit testing purposes.
        /// The MaxTime is able to fit in a sqlite database.  .Net's DateTime.MaxValue doesn't always fit in it.
        /// On Linux, sqlite seems to insert it as 0.
        /// Another example is here: http://stackoverflow.com/questions/6127123/net-datetime-maxvalue-is-different-once-it-is-stored-in-database
        /// 
        /// Yeah, we're not using SQLite, but maybe someday we'll support multiple databases?
        /// </summary>
        public static readonly DateTime MaxTime = new DateTime( 5000, 1, 1, 0, 0, 0 ).ToUniversalTime(); // Year 5000 is good enough.

        /// <summary>
        /// Constructor.  Set everything to default values.
        /// </summary>
        public Log()
        {
            // Fun fact!  DateTime.MinValue seems to return local time, not UTC time.
            this.EndTime = DateTime.MinValue;

            // Make start time ahead of end time,
            // this will make the log in an invalid state, as the
            // start time is ahead of the end time which is not allowed.
            this.StartTime = MaxTime;

            this.Guid = Guid.NewGuid();

            this.EditTime = DateTime.MinValue;
            this.comments = string.Empty;
            this.technique = string.Empty;
            this.Latitude = null;
            this.Longitude = null;
        }

        // ---------------- Properties ----------------

        /// <summary>
        /// The unique ID for databases.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// When the session starts
        /// The time is in local time
        /// (database saves to UTC, and retrieves to local time).
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// When the session ends
        /// The time is in local time.
        /// (database saves to UTC, and retrieves to local time).
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// How long the session lasted.
        /// </summary>
        [BsonIgnore]
        public TimeSpan Duration
        {
            get
            {
                return EndTime - StartTime;
            }
        }

        /// <summary>
        /// Unique GUID for the log objet.
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// The last time this log was edited.
        /// The time is in local time.
        /// (database saves to UTC, and retrieves to local time).
        /// </summary>
        public DateTime EditTime { get; set; }

        /// <summary>
        /// The comments the user wrote about the session.
        /// </summary>
        public string Comments
        {
            get
            {
                return this.comments;
            }
            set
            {
                // By default, LiteDB calls trim on strings when saving to the database.
                // We should do the same.
                this.comments = ( value ?? string.Empty ).Trim();
            }
        }

        /// <summary>
        /// The Technique of the session.
        /// </summary>
        public string Technique
        {
            get
            {
                return this.technique;
            }
            set
            {
                // By default, LiteDB calls trim on strings when saving to the database.
                // We should do the same.
                this.technique = ( value ?? string.Empty ).Trim();
            }
        }

        /// <summary>
        /// The latitude of where the session took place.
        /// null if no location specified.
        /// </summary>
        public decimal? Latitude { get; set; }

        /// <summary>
        /// The longitude of where the session took place.
        /// null if no location specified.
        /// </summary>
        public decimal? Longitude { get; set; }

        // ---------------- Functions ----------------

        /// <summary>
        /// Returns true if all properties EXCEPT For GUID and ID match.
        /// </summary>
        public override bool Equals( object? obj )
        {
            return Equals( obj as Log );
        }

        public bool Equals( Log? other )
        {
            if( other == null )
            {
                return false;
            }

            return
                ( this.StartTime == other.StartTime ) &&
                ( this.EndTime == other.EndTime ) &&
                ( this.EditTime == other.EditTime ) &&
                ( this.Comments == other.Comments ) &&
                ( this.Technique == other.Technique ) &&
                ( this.Latitude == other.Latitude ) &&
                ( this.Longitude == other.Longitude );
        }

        public override int GetHashCode()
        {
            return
                this.StartTime.GetHashCode() +
                this.EndTime.GetHashCode() +
                this.EditTime.GetHashCode() +
                ( ( this.Comments != null ) ? this.Comments.GetHashCode() : 0 ) +
                ( ( this.Technique != null ) ? this.Technique.GetHashCode() : 0 ) +
                ( this.Latitude.HasValue ? this.Latitude.Value.GetHashCode() : 0 ) +
                ( this.Longitude.HasValue ? this.Longitude.Value.GetHashCode() : 0 );
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine( "Id: " + this.Id );
            builder.AppendLine( "Guid: " + this.Guid );
            builder.AppendLine( "Start Time: " + this.StartTime.ToString() );
            builder.AppendLine( "End Time: " + this.EndTime.ToString() );
            builder.AppendLine( "Edit Time: " + this.EditTime.ToString() );
            builder.AppendLine( "Comments: " + ( this.Comments ?? "[null]" ) );
            builder.AppendLine( "Technique: " + ( this.Technique ?? "[null]" ) );
            builder.AppendLine( "Latitude: " + ( this.Latitude.HasValue ? this.Latitude.Value.ToString() : "[null]" ) );
            builder.AppendLine( "Longitude: " + ( this.Longitude.HasValue ? this.Longitude.Value.ToString() : "[null]" ) );

            return builder.ToString();
        }

        /// <summary>
        /// Creates a deep copy of this class.
        /// </summary>
        public Log Clone()
        {
            return (Log)this.MemberwiseClone();
        }
    }

    internal static class LogExtensions
    {
        // ---------------- Fields ----------------

        public const string XmlElementName = "log";

        // ---------------- Functions ----------------

        public static void Validate( this Log log )
        {
            bool success = true;
            StringBuilder errorString = new StringBuilder();
            errorString.AppendLine( "Validation errors found with Log object." );

            if( log.Id < 0 )
            {
                success = false;
                errorString.AppendLine( "\t-ID can not be zero or less" );
            }

            if( log.EndTime < log.StartTime )
            {
                success = false;
                errorString.AppendLine( "\t-Log End Time is less than the start time." );
            }

            if( log.Comments == null )
            {
                success = false;
                errorString.AppendLine( "\t-Comments can not be null" );
            }

            if( log.Technique == null )
            {
                success = false;
                errorString.AppendLine( "\t-Technique can not be null" );
            }

            if( ( log.Latitude == null ) && ( log.Longitude != null ) )
            {
                success = false;
                errorString.AppendLine( "\t-Latitude set on log, but not longitude" );
            }

            if( ( log.Longitude == null ) && ( log.Latitude != null ) )
            {
                success = false;
                errorString.AppendLine( "\t-Longitude set on log, but not latitude" );
            }

            if( success == false )
            {
                throw new ValidationException( errorString.ToString() );
            }
        }

        public static void FromXml( this Log log, XmlNode node )
        {
            if( XmlElementName.EqualsIgnoreCase( node.Name ) == false )
            {
                throw new ArgumentException(
                    "Node name must be " + XmlElementName,
                    nameof( node )
                );
            }

            if( node.Attributes is null )
            {
                return;
            }

            foreach( XmlAttribute attr in node.Attributes )
            {
                if( string.IsNullOrWhiteSpace( attr.Name ) )
                {
                    continue;
                }
                else if( "guid".EqualsIgnoreCase( attr.Name ) )
                {
                    log.Guid = Guid.Parse( attr.Value );
                }
                else if( "edittime".EqualsIgnoreCase( attr.Name ) )
                {
                    log.EditTime = DateTime.ParseExact(
                        attr.Value,
                        DateTimeExtensions.TimeStampFormatString,
                        CultureInfo.InvariantCulture
                    );
                }
                else if( "starttime".EqualsIgnoreCase( attr.Name ) )
                {
                    log.StartTime = DateTime.ParseExact(
                        attr.Value,
                        DateTimeExtensions.TimeStampFormatString,
                        CultureInfo.InvariantCulture
                    );
                }
                else if( "endtime".EqualsIgnoreCase( attr.Name ) )
                {
                    log.EndTime = DateTime.ParseExact(
                        attr.Value,
                        DateTimeExtensions.TimeStampFormatString,
                        CultureInfo.InvariantCulture
                    );
                }
                else if( "technique".EqualsIgnoreCase( attr.Name ) )
                {
                    log.Technique = attr.Value;
                }
                else if( "comments".EqualsIgnoreCase( attr.Name ) )
                {
                    log.Comments = attr.Value;
                }
                else if( "latitude".EqualsIgnoreCase( attr.Name ) )
                {
                    if( string.IsNullOrEmpty( attr.Value ) )
                    {
                        log.Latitude = null;
                    }
                    else
                    {
                        log.Latitude = decimal.Parse( attr.Value );
                    }
                }
                else if( "longitude".EqualsIgnoreCase( attr.Name ) )
                {
                    if( string.IsNullOrEmpty( attr.Value ) )
                    {
                        log.Longitude = null;
                    }
                    else
                    {
                        log.Longitude = decimal.Parse( attr.Value );
                    }
                }
            }
        }

        /// <summary>
        /// Appends this Log to the given XML document.
        /// </summary>
        public static void ToXml( this Log log, XmlDocument doc, XmlNode parentNode )
        {
            log.Validate();

            XmlNode logNode = doc.CreateElement( XmlElementName );

            // Add attributes
            if( logNode.Attributes is null )
            {
                throw new InvalidOperationException(
                    "Somehow, the attributes is null"
                );
            }

            {
                XmlAttribute attr = doc.CreateAttribute( "Guid" );
                attr.Value = log.Guid.ToString();
                logNode.Attributes.Append( attr );
            }

            {
                XmlAttribute attr = doc.CreateAttribute( "EditTime" );
                attr.Value = log.EditTime.ToTimeStampString();
                logNode.Attributes.Append( attr );
            }

            {
                XmlAttribute attr = doc.CreateAttribute( "StartTime" );
                attr.Value = log.StartTime.ToTimeStampString();
                logNode.Attributes.Append( attr );
            }

            {
                XmlAttribute attr = doc.CreateAttribute( "EndTime" );
                attr.Value = log.EndTime.ToTimeStampString();
                logNode.Attributes.Append( attr );
            }

            {
                XmlAttribute attr = doc.CreateAttribute( "Comments" );
                attr.Value = log.Comments;
                logNode.Attributes.Append( attr );
            }

            {
                XmlAttribute attr = doc.CreateAttribute( "Technique" );
                attr.Value = log.Technique;
                logNode.Attributes.Append( attr );
            }

            {
                XmlAttribute attr = doc.CreateAttribute( "Latitude" );
                attr.Value = log.Latitude.HasValue ? log.Latitude.ToString() : string.Empty;
                logNode.Attributes.Append( attr );
            }

            {
                XmlAttribute attr = doc.CreateAttribute( "Longitude" );
                attr.Value = log.Longitude.HasValue ? log.Longitude.ToString() : string.Empty;
                logNode.Attributes.Append( attr );
            }

            parentNode.AppendChild( logNode );
        }
    }
}

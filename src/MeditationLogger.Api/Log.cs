﻿//
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
using System.Text;
using System.Xml;
using LiteDB;
using SethCS.Exceptions;
using SethCS.Extensions;

namespace MeditationLogger.Api
{
    /// <summary>
    /// This class represents a specific instance
    /// of an event logged.
    /// </summary>
    public class Log
    {
        // ---------------- Fields ----------------

        public const string XmlElementName = "log";

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

        public static Log FromXml( XmlNode node )
        {
            if( node.Name.ToLower() != XmlElementName )
            {
                throw new ArgumentException( "Node name must be " + XmlElementName, nameof( node ) );
            }

            Log log = new Log();

            foreach( XmlAttribute attr in node.Attributes )
            {
                switch( attr.Name.ToLower() )
                {
                    case "guid":
                        log.Guid = Guid.Parse( attr.Value );
                        break;

                    case "edittime":
                        log.EditTime = DateTime.Parse( attr.Value );
                        break;

                    case "starttime":
                        log.StartTime = DateTime.Parse( attr.Value );
                        break;

                    case "endtime":
                        log.EndTime = DateTime.Parse( attr.Value );
                        break;

                    case "technique":
                        log.Technique = attr.Value;
                        break;

                    case "comments":
                        log.Comments = attr.Value;
                        break;

                    case "latitude":
                        if( string.IsNullOrEmpty( attr.Value ) )
                        {
                            log.Latitude = null;
                        }
                        else
                        {
                            log.Latitude = decimal.Parse( attr.Value );
                        }
                        break;

                    case "longitude":
                        if( string.IsNullOrEmpty( attr.Value ) )
                        {
                            log.Longitude = null;
                        }
                        else
                        {
                            log.Longitude = decimal.Parse( attr.Value );
                        }
                        break;

                }
            }

            log.Validate();
            return log;
        }

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
            this.Comments = string.Empty;
            this.Technique = string.Empty;
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
                ArgumentChecker.IsNotNull( value, nameof( Comments ) );

                // By default, LiteDB calls trim on strings when saving to the database.
                // We should do the same.
                this.comments = value.Trim();
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
                ArgumentChecker.IsNotNull( value, nameof( Technique ) );

                // By default, LiteDB calls trim on strings when saving to the database.
                // We should do the same.
                this.technique = value.Trim();
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
        /// Ensures this Log object is valid.
        /// </summary>
        public void Validate()
        {
            bool success = true;
            StringBuilder errorString = new StringBuilder();
            errorString.AppendLine( "Validation errors found with Log object." );

            if( this.Id < 0 )
            {
                success = false;
                errorString.AppendLine( "\t-ID can not be zero or less" );
            }

            if( this.EndTime < this.StartTime )
            {
                success = false;
                errorString.AppendLine( "\t-Log End Time is less than the start time." );
            }

            if( this.Comments == null )
            {
                success = false;
                errorString.AppendLine( "\t-Comments can not be null" );
            }

            if( this.Technique == null )
            {
                success = false;
                errorString.AppendLine( "\t-Technique can not be null" );
            }

            if( ( this.Latitude == null ) && ( this.Longitude != null ) )
            {
                success = false;
                errorString.AppendLine( "\t-Latitude set on log, but not longitude" );
            }

            if( ( this.Longitude == null ) && ( this.Latitude != null ) )
            {
                success = false;
                errorString.AppendLine( "\t-Longitude set on log, but not latitude" );
            }

            if( success == false )
            {
                throw new ValidationException( errorString.ToString() );
            }
        }

        /// <summary>
        /// Returns true if all properties EXCEPT For GUID and ID match.
        /// </summary>
        /// <param name="obj">A <see cref="Log"/> or <see cref="IReadOnlyLog"/> object.</param>
        public override bool Equals( object obj )
        {
            Log other = obj as Log;
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

        /// <summary>
        /// Appends this Log to the given XML document.
        /// </summary>
        public void ToXml( XmlDocument doc, XmlNode parentNode )
        {
            this.Validate();

            XmlNode logNode = doc.CreateElement( XmlElementName );

            // Add attributes

            {
                XmlAttribute attr = doc.CreateAttribute( "Guid" );
                attr.Value = this.Guid.ToString();
                logNode.Attributes.Append( attr );
            }

            {
                XmlAttribute attr = doc.CreateAttribute( "EditTime" );
                attr.Value = this.EditTime.ToTimeStampString();
                logNode.Attributes.Append( attr );
            }

            {
                XmlAttribute attr = doc.CreateAttribute( "StartTime" );
                attr.Value = this.StartTime.ToTimeStampString();
                logNode.Attributes.Append( attr );
            }

            {
                XmlAttribute attr = doc.CreateAttribute( "EndTime" );
                attr.Value = this.EndTime.ToTimeStampString();
                logNode.Attributes.Append( attr );
            }

            {
                XmlAttribute attr = doc.CreateAttribute( "Comments" );
                attr.Value = this.Comments;
                logNode.Attributes.Append( attr );
            }

            {
                XmlAttribute attr = doc.CreateAttribute( "Technique" );
                attr.Value = this.Technique;
                logNode.Attributes.Append( attr );
            }

            {
                XmlAttribute attr = doc.CreateAttribute( "Latitude" );
                attr.Value = this.Latitude.HasValue ? this.Latitude.ToString() : string.Empty;
                logNode.Attributes.Append( attr );
            }

            {
                XmlAttribute attr = doc.CreateAttribute( "Longitude" );
                attr.Value = this.Longitude.HasValue ? this.Longitude.ToString() : string.Empty;
                logNode.Attributes.Append( attr );
            }

            parentNode.AppendChild( logNode );
        }
    }
}

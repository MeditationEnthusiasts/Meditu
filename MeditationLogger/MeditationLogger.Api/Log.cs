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
using System.Text;
using SethCS.Exceptions;

namespace MeditationLogger.Api
{
    /// <summary>
    /// This class represents a specific instance
    /// of an event logged.
    /// </summary>
    public class Log
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
            this.Id = 0;

            // Fun fact!  DateTime.MinValue seems to return local time, not UTC time.
            this.EndTime = DateTime.MinValue.ToUniversalTime();

            // Make start time ahead of end time,
            // this will make the log in an invalid state, as the
            // start time is ahead of the end time which is not allowed.
            this.StartTime = MaxTime;

            this.Guid = Guid.NewGuid();

            this.EditTime = DateTime.MinValue.ToUniversalTime();
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
        /// (UTC, the UI must convert it to local time).
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// When the session ends
        /// (UTC, the UI must convert it to local time).
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// How long the session lasted.
        /// </summary>
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
        /// (UTC, the UI must convert it to local time).
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
                errorString.AppendLine( "\t-Longitude set on long, but not latitude" );
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
    }
}

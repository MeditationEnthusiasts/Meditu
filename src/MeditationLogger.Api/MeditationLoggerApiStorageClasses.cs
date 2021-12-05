//
// MeditationLogger - A way to track Meditation Sessions.
// Copyright (C) 2017-2021 Seth Hendrick
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
using System.Text;
using SethCS.Exceptions;

namespace MeditationLogger.Api
{
    /// <summary>
    /// Parameters for starting a session.
    /// </summary>
    public struct StartSessionParams
    {
        // ---------------- Properties ----------------

        /// <summary>
        /// How long the session should be.
        /// Null for no time limit (User MUST stop the timer themselves).
        /// </summary>
        public TimeSpan? Duration { get; set; }

        // ---------------- Functions ----------------
    }

    /// <summary>
    /// Parameters for saving a session.
    /// </summary>
    public struct SaveSessionParams
    {
        // ---------------- Properties ----------------

        /// <summary>
        /// The technique of the session.
        /// </summary>
        public string Technique { get; set; }

        /// <summary>
        /// Comments about the session.
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// The latitude of where the session took place.
        /// Null for none.
        /// </summary>
        public decimal? Latitude { get; set; }

        /// <summary>
        /// The longitude of where the session took place.
        /// Null for none.
        /// </summary>
        public decimal? Longitude { get; set; }

        // -------- Functions --------

        public void Validate()
        {
            bool success = true;
            StringBuilder errorString = new StringBuilder();
            errorString.AppendLine( "Validation errors found with Log object." );

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

            if( ( this.Longitude == null ) && ( this.Latitude != null ) )
            {
                success = false;
                errorString.AppendLine( "\t-Longitude set, but not latitude" );
            }

            if( ( this.Latitude == null ) && ( this.Longitude != null ) )
            {
                success = false;
                errorString.AppendLine( "\t-Latitude set, but not longitude" );
            }

            if( success == false )
            {
                throw new ValidationException( errorString.ToString() );
            }
        }
    }
}

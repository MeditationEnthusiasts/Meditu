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

namespace MeditationLogger.Api
{
    /// <summary>
    /// The API to Meditation Logger.
    /// </summary>
    public class MeditationLoggerApi : IDisposable
    {
        // ---------------- Fields ----------------

        private Log currentSession;

        private ApiState currentState;

        // ---------------- Constructor ----------------

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbPath">The path to the database file.</param>
        public MeditationLoggerApi( string dbPath )
        {
            this.currentSession = null;
            this.currentState = ApiState.Idle;
            this.LogBook = new LogBook();
            this.LogBook.OpenDb( dbPath );
        }

        // ---------------- Properties ----------------

        /// <summary>
        /// Returns a COPY of the current session.       
        /// </summary>
        /// <exception cref="InvalidOperationException">If a session is not started yet.</exception>
        public Log CurrentSession
        {
            get
            {
                if( this.currentSession == null )
                {
                    throw new InvalidOperationException( "No current session, did you start one?" );
                }

                return this.currentSession.Clone();
            }
        }

        public LogBook LogBook { get; private set; }

        // ---------------- Functions ----------------

        /// <summary>
        /// Starts a session.
        /// </summary>
        /// <exception cref="InvalidOperationException">If we are already started.</exception>
        public void Start( StartSessionParams startParams )
        {
            if( this.currentState != ApiState.Idle )
            {
                throw new InvalidOperationException( "Already started!" );
            }

            this.currentSession = new Log();
            this.currentSession.StartTime = DateTime.UtcNow;
        }

        /// <summary>
        /// Stops the current session.
        /// </summary>
        /// <exception cref="InvalidOperationException">If we are not started.</exception>
        public void Stop()
        {
            if( this.currentState != ApiState.Started )
            {
                throw new InvalidOperationException( "Session not started, can not stop." );
            }

            this.currentSession.EndTime = DateTime.UtcNow;
        }

        /// <summary>
        /// Saves the current session.
        /// Current session returns to null.
        /// </summary>
        /// <exception cref="InvalidOperationException">If there is no current session, or we are not stopped.</exception>
        public void SaveSession( SaveSessionParams saveParams )
        {
            if( this.currentState != ApiState.Stopped )
            {
                throw new InvalidOperationException( "Session not started, can not save." );
            }
            saveParams.Validate();

            // Save Session!
            this.currentSession.EditTime = DateTime.UtcNow;
            this.currentSession.Latitude = saveParams.Latitude;
            this.currentSession.Longitude = saveParams.Longitude;
            this.currentSession.Technique = saveParams.Technique;
            this.currentSession.Comments = saveParams.Comments;

            this.LogBook.AddLogToDb( this.currentSession );

            this.currentSession = null;
            this.currentState = ApiState.Idle;
        }

        public void CancelSession()
        {
            if( this.currentState != ApiState.Stopped )
            {
                throw new InvalidOperationException( "Session not started, can not clear." );
            }

            this.currentSession = null;
            this.currentState = ApiState.Idle;
        }

        public void Dispose()
        {
            this.LogBook?.Dispose();
        }

        // ---------------- Helper Classes ----------------

        enum ApiState
        {
            Idle,
            Started,
            Stopped
        }
    }
}

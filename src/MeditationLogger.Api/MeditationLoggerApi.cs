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

        private Session currentSession;

        private ApiState currentState;

        private object currentStateLock;

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

            this.currentStateLock = new object();
        }

        // ---------------- Properties ----------------

        /// <summary>
        /// Returns a COPY of the current session.       
        /// </summary>
        /// <exception cref="InvalidOperationException">If a session is not started yet.</exception>
        public Session CurrentSession
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

        public ApiState CurrentState
        {
            get
            {
                lock( this.currentStateLock )
                {
                    return this.currentState;
                }
            }
            private set
            {
                lock( this.currentStateLock )
                {
                    this.currentState = value;
                }
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
            if( this.CurrentState != ApiState.Idle )
            {
                throw new InvalidOperationException( "Already started!" );
            }

            this.currentSession = new Session();
            this.currentSession.SessionDuration = startParams.Duration;
            this.currentSession.Log.StartTime = DateTime.Now;
            this.CurrentState = ApiState.Started;
        }

        /// <summary>
        /// Stops the current session.
        /// </summary>
        /// <exception cref="InvalidOperationException">If we are not started.</exception>
        public void Stop()
        {
            if( this.CurrentState != ApiState.Started )
            {
                throw new InvalidOperationException( "Session not started, can not stop." );
            }

            this.currentSession.Log.EndTime = DateTime.Now;
            this.CurrentState = ApiState.Stopped;
        }

        /// <summary>
        /// Saves the current session.
        /// Current session returns to null.
        /// </summary>
        /// <exception cref="InvalidOperationException">If there is no current session, or we are not stopped.</exception>
        public void SaveSession( SaveSessionParams saveParams )
        {
            if( this.CurrentState != ApiState.Stopped )
            {
                throw new InvalidOperationException( "Session not started, can not save." );
            }
            saveParams.Validate();

            // Save Session!
            this.currentSession.Log.EditTime = DateTime.Now;
            this.currentSession.Log.Latitude = saveParams.Latitude;
            this.currentSession.Log.Longitude = saveParams.Longitude;
            this.currentSession.Log.Technique = saveParams.Technique;
            this.currentSession.Log.Comments = saveParams.Comments;

            this.LogBook.AddLogToDb( this.currentSession.Log );

            this.CancelSession();
        }

        public void CancelSession()
        {
            this.currentSession = null;
            this.currentState = ApiState.Idle;
        }

        public void Dispose()
        {
            this.LogBook?.Dispose();
        }

        // ---------------- Helper Classes ----------------
    }

    public enum ApiState
    {
        Idle,
        Started,
        Stopped
    }
}

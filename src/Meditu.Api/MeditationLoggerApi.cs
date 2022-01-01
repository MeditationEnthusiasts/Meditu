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
using System.IO;

namespace Meditu.Api
{
    /// <summary>
    /// The API to Meditation Logger.
    /// </summary>
    public sealed class MeditationLoggerApi : IDisposable, IApiModel
    {
        // ---------------- Fields ----------------

        private readonly string settingsFile;

        private Session? currentSession;

        private ApiState currentState;
        private readonly object currentStateLock;

        // ---------------- Constructor ----------------

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dbPath">The path to the database file.</param>
        /// <param name="settingsFile">Path to the settings file.</param>
        public MeditationLoggerApi( string dbPath, string settingsFile )
        {
            this.currentSession = null;
            this.currentState = ApiState.Idle;
            this.LogBook = new LogBook( dbPath );

            this.Settings = new SettingsManager();
            this.settingsFile = settingsFile;
            this.Settings.OnUpdated += Settings_OnUpdated;

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
        
        public SettingsManager Settings { get; private set; }

        IReadOnlySettingsManager IApiModel.Settings => this.Settings;

        // ---------------- Functions ----------------

        public void Init()
        {
            this.LoadSettings();
            this.LogBook.Refresh();
        }

        public void LoadSettings()
        {
            lock( this.settingsFile )
            {
                if( File.Exists( this.settingsFile ) )
                {
                    this.Settings.LoadXmlFromFile( this.settingsFile );
                }
            }
        }

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
            else if( this.currentSession is null )
            {
                throw new InvalidOperationException(
                    "Current Session is null"
                );
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
            else if( this.currentSession is null )
            {
                throw new InvalidOperationException(
                    "Current Session is null"
                );
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
            this.Settings.OnUpdated -= this.Settings_OnUpdated;
            this.LogBook?.Dispose();
            this.Settings.SaveXmlToFile( this.settingsFile );
        }

        private void Settings_OnUpdated()
        {
            lock( this.settingsFile )
            {
                this.Settings.SaveXmlToFile( this.settingsFile );
            }
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

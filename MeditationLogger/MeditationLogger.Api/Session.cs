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
    /// This is the current session information.
    /// </summary>
    public class Session
    {
        // ---------------- Constructor ----------------

        public Session()
        {
            this.Log = new Log();
        }

        // ---------------- Properties ----------------

        /// <summary>
        /// How long the session is supposed to go for.
        /// Null if forever (user must hit "stop").
        /// </summary>
        public TimeSpan? SessionDuration { get; set; }

        /// <summary>
        /// The session's log object.
        /// </summary>
        public Log Log { get; set; }

        /// <summary>
        /// Is our timer counting up forever (true), or is there
        /// a duration specified and we are counting down. (false)
        /// </summary>
        public bool IsCountingUp
        {
            get
            {
                return this.SessionDuration == null;
            }
        }

        public TimeSpan TimeRemaining
        {
            get
            {
                TimeSpan span;
                if ( this.IsCountingUp )
                {
                    DateTime now = DateTime.Now;
                    span = now - this.Log.StartTime;
                }
                else
                {
                    DateTime expectedCompleteTime = this.Log.StartTime + this.SessionDuration.Value;

                    span = expectedCompleteTime - DateTime.Now;
                }

                // Don't allow for negative time.
                if ( span < TimeSpan.Zero )
                {
                    span = TimeSpan.Zero;
                }

                return span;
            }
        }

        // ---------------- Functions ----------------

        /// <summary>
        /// Creates a deep-copy of this object.
        /// </summary>
        public Session Clone()
        {
            Session session = (Session)this.MemberwiseClone();
            session.Log = this.Log.Clone();

            return session;
        }
    }
}
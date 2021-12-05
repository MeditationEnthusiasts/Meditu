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
using MeditationLogger.Api;

namespace MeditationLogger.Gui
{
    /// <summary>
    /// The bridge to the API.
    /// </summary>
    public static class ApiBridge
    {
        /// <summary>
        /// Reference to the API instance.
        /// Returns null if <see cref="CreateInstance(string)"/> was never called.
        /// </summary>
        public static MeditationLoggerApi Instance { get; private set; }

        /// <summary>
        /// Creates the single instance of the bridge to the API.
        /// </summary>
        /// <exception cref="InvalidOperationException">Called if called when an instance already exists.</exception>
        public static void CreateInstance( string databaseLocation )
        {
            if( Instance != null )
            {
                throw new InvalidOperationException( "Instance already created!" );
            }
            Instance = new MeditationLoggerApi( databaseLocation );
        }

        /// <summary>
        /// Destroys the instance to the API.
        /// No-op if it was never created.
        /// </summary>
        public static void DestroyInstance()
        {
            Instance?.Dispose();
        }
    }
}

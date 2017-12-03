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

using ElectronNET.API;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using SethCS.Exceptions;

namespace MeditationLogger.Gui
{
    public class Program
    {
        public static void Main( string[] args )
        {
            IWebHost webHost = BuildWebHost( args );
            webHost.Run();

        }

        public static IWebHost BuildWebHost( string[] args )
        {
            ArgumentChecker.IsNotNull( args, nameof( args ) );
            return WebHost.CreateDefaultBuilder( args )
                .UseElectron( args )
                .UseStartup<Startup>()
                .Build();
        }
    }
}

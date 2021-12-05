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

#if CAKE
#else
using Cake.Common.Tools.DotNet;
using Cake.Common.Tools.DotNet.Build;
using Cake.Common.Tools.DotNet.MSBuild;
using Cake.Common.Tools.DotNetCore.MSBuild;
using Cake.Core;
using Cake.Core.IO;

namespace DevOps
{
#endif
    public static class MsBuildHelpers
    {
        /// <summary>
        /// Calls MSBuild to compile Chaskis
        /// </summary>
        /// <param name="configuration">The configuration to use (e.g. Debug, Release, etc.).</param>
        public static void DoMsBuild( ICakeContext context, FilePath sln, string configuration )
        {
            string versString = VersionInfo.Version.ToString( 3 );
            var msBuildSettings = new DotNetMSBuildSettings
            {
                WorkingDirectory = sln.GetDirectory().ToString()
            }
            .WithProperty( "Version", versString )
            .WithProperty( "AssemblyVersion", versString )
            .WithProperty( "FileVersion", versString )
            .SetMaxCpuCount( Environment.ProcessorCount )
            .SetConfiguration( configuration );

            var settings = new DotNetBuildSettings
            {
                MSBuildSettings = msBuildSettings
            };

            context.DotNetBuild( sln.ToString(), settings );
        }
    }

#if CAKE
#else
}
#endif

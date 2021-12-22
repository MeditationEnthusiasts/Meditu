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

#if CAKE
#else
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Build;
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
            DotNetCoreMSBuildSettings msBuildSettings = GetMsBuildSettings( configuration );
            msBuildSettings.WorkingDirectory = sln.GetDirectory().ToString();

            var settings = new DotNetCoreBuildSettings
            {
                MSBuildSettings = msBuildSettings
            };

            context.DotNetCoreBuild( sln.ToString(), settings );
        }

        public static DotNetCoreMSBuildSettings GetMsBuildSettings( string configuration )
        {
            string versString = VersionInfo.VersionString;
            var msBuildSettings = new DotNetCoreMSBuildSettings
            {
            }
            .WithProperty( "Version", versString )
            .WithProperty( "AssemblyVersion", versString )
            .WithProperty( "FileVersion", versString )
            .SetMaxCpuCount( Environment.ProcessorCount )
            .SetConfiguration( configuration );

            return msBuildSettings;
        }
    }

#if CAKE
#else
}
#endif

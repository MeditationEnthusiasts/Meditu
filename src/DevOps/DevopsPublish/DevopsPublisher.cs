//
// Meditu - A way to track Meditation Sessions.
// Copyright (C) 2017-2022 Meditation Enthusiasts.
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

using Cake.Common.IO;
using Cake.Common.Tools.DotNetCore;
using Cake.Common.Tools.DotNetCore.Publish;

namespace DevOps.DevopsPublish
{
    internal class DevopsPublisher
    {

        private readonly MeditationLogContext context;

        // ----------------- Constructor -----------------

        public DevopsPublisher( MeditationLogContext context )
        {
            this.context = context;
        }

        // ----------------- Functions -----------------

        /// <param name="publishDirName">
        /// The directory inside of <see cref="MeditationLogContext.DevopsDistributionPath"/>
        /// to place the published project.
        /// Set to null to have it be <paramref name="rid"/>.
        /// </param>
        public static void DevopsPublish( MeditationLogContext context, string rid, string? publishDirName = null )
        {
            var publisher = new DevopsPublisher( context );
            publisher.DevopsPublish( rid, publishDirName );
        }

        /// <param name="publishDirName">
        /// The directory inside of <see cref="MeditationLogContext.DevopsDistributionPath"/>
        /// to place the published project.
        /// Set to null to have it be <paramref name="rid"/>.
        /// </param>
        public void DevopsPublish( string rid, string? publishDirName = null )
        {
            const string configuration = "Release";

            var publishDir = context.DevopsDistributionPath.Combine( publishDirName ?? rid );

            context.EnsureDirectoryExists( publishDir );
            context.CleanDirectory( publishDir );

            var publishSettings = new DotNetCorePublishSettings
            {
                Configuration = configuration,
                OutputDirectory = publishDir,
                MSBuildSettings = MsBuildHelpers.GetMsBuildSettings( configuration ),

                // We'll be running in a docker image with the runtime, no need
                // to include the entire runtime.
                SelfContained = false,
                Runtime = rid
            };

            context.DotNetCorePublish(
                context.DevopsCsProj.ToString(),
                publishSettings
            );
        }
    }
}

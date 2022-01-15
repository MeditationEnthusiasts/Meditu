//
// Meditu - A way to track Meditation Sessions.
// Copyright (C) 2017-2022 Seth Hendrick.
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
using Cake.Core.IO;
using Cake.Frosting;

namespace DevOps.Docker
{
    [TaskName( "publish_for_docker" )]
    [TaskDescription( "Publishes everything needed for Docker before creating the Docker file." )]
    public sealed class PublishForDockerTask : DefaultTask
    {
        // ----------------- Functions -----------------
        public override void Run( MeditationLogContext context )
        {
            const string configuration = "Release";

            var publishDir = context.DockerPath.Combine( "bin" );

            context.EnsureDirectoryExists( publishDir );
            context.CleanDirectory( publishDir );

            var publishSettings = new DotNetCorePublishSettings
            {
                Configuration = configuration,
                OutputDirectory = publishDir,
                MSBuildSettings = MsBuildHelpers.GetMsBuildSettings( configuration ),

            };

            context.DotNetCorePublish(
                context.GuiCsProject.ToString(),
                publishSettings
            );

            // No need to inclue these files in the docker image,
            // may as well decrease the file size a bit.
            var filesToDelete = new FilePath[]
            {
                publishDir.CombineWithFilePath( "MeditationLogger.Gui.exe" ),
                publishDir.CombineWithFilePath( "MeditationLogger.Gui" )
            };
            foreach( FilePath path in filesToDelete )
            {
                if( context.FileExists( path ) )
                {
                    context.DeleteFile( path );
                }
            }
        }
    }
}

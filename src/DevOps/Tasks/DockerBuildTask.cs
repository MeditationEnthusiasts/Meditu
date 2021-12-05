//
// MeditationLogger - A way to track Meditation Sessions.
// Copyright (C) 2017-2021 Seth Hendrick
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

using Cake.Common;
using Cake.Core.IO;
using Cake.Frosting;

namespace DevOps.Tasks
{
    [TaskName( "docker_build" )]
    [TaskDescription( "Builds the Docker image for the currently running platform." )]
    public sealed class DockerBuildTask : DefaultTask
    {
        // ----------------- Fields -----------------

        private const string imageName = "xforever1313/meditationlogger";

        // ----------------- Functions -----------------

        public override void Run( MeditationLogContext context )
        {
            FilePath dockerFile = "server.dockerfile";

            {
                string arguments = $"build -t {imageName} -f {dockerFile} .";
                ProcessArgumentBuilder argumentsBuilder = ProcessArgumentBuilder.FromString( arguments );
                ProcessSettings settings = new ProcessSettings
                {
                    Arguments = argumentsBuilder,
                    WorkingDirectory = context.DockerPath
                };
                int exitCode = context.StartProcess( "docker", settings );
                if( exitCode != 0 )
                {
                    throw new ApplicationException(
                        "Error when running docker to build.  Got error: " + exitCode
                    );
                }
            }

            {
                string arguments = $"tag {imageName}:latest {imageName}:{VersionInfo.VersionString}";
                ProcessArgumentBuilder argumentsBuilder = ProcessArgumentBuilder.FromString( arguments );
                ProcessSettings settings = new ProcessSettings
                {
                    Arguments = argumentsBuilder
                };
                int exitCode = context.StartProcess( "docker", settings );
                if( exitCode != 0 )
                {
                    throw new ApplicationException(
                        "Error when running docker to tag.  Got error: " + exitCode
                    );
                }
            }
        }
    }
}

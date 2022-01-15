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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cake.Common;
using Cake.Core.IO;

namespace DevOps.Docker
{
    internal sealed class DockerBuilder
    {
        // ----------------- Fields -----------------

        private const string imageName = "xforever1313/meditu";

        private readonly MeditationLogContext context;

        // ----------------- Constructor -----------------

        public DockerBuilder( MeditationLogContext context )
        {
            this.context = context;
        }

        // ----------------- Functions -----------------

        public void Run( string platforms )
        {
            FilePath dockerFile = "server.dockerfile";

            {
                string arguments = $"buildx build --platform {platforms} --tag {imageName} -f {dockerFile} .";
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

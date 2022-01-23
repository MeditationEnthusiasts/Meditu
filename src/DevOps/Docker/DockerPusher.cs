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

using Cake.Common;
using Cake.Core.IO;

namespace DevOps.Docker
{
    internal sealed class DockerPusher
    {
        // ----------------- Fields -----------------

        private readonly MeditationLogContext context;

        // ----------------- Constructor -----------------

        public DockerPusher( MeditationLogContext context )
        {
            this.context = context;
        }

        // ----------------- Functions -----------------

        public static void PushPlatformImage( MeditationLogContext context, string platform )
        {
            var pusher = new DockerPusher( context );
            pusher.PushPlatformImage( platform );
        }

        public void PushPlatformImage( string platform )
        {
            string arguments = $"push {DockerConstants.GetPlatformImageName( platform )}";
            ProcessArgumentBuilder argumentsBuilder = ProcessArgumentBuilder.FromString( arguments );
            ProcessSettings settings = new ProcessSettings
            {
                Arguments = argumentsBuilder
            };

            int exitCode = context.StartProcess( "docker", settings );
            if( exitCode != 0 )
            {
                throw new ApplicationException(
                    $"Error when trying to push docker platform image {platform}.  Got error: " + exitCode
                );
            }
        }

        public static void PushManifest( MeditationLogContext context )
        {
            var pusher = new DockerPusher( context );
            pusher.PushManifest();
        }

        public void PushManifest()
        {
            foreach( string version in new string[] { "latest", VersionInfo.VersionString } )
            {
                string arguments = $"push {DockerConstants.ImageName}:{version}";
                ProcessArgumentBuilder argumentsBuilder = ProcessArgumentBuilder.FromString( arguments );
                ProcessSettings settings = new ProcessSettings
                {
                    Arguments = argumentsBuilder
                };

                int exitCode = context.StartProcess( "docker", settings );
                if( exitCode != 0 )
                {
                    throw new ApplicationException(
                        $"Error when trying to push docker manifest image.  Got error: " + exitCode
                    );
                }
            }
        }
    }
}

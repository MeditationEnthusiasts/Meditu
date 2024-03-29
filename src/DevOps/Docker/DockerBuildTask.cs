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
using Cake.Frosting;

namespace DevOps.Docker
{
    [TaskName( "build_docker_windows_x64" )]
    [TaskDescription( "Builds the Docker image for Windows." )]
    public sealed class BuildWindowsX64DockerTask : DefaultTask
    {
        // ----------------- Functions -----------------

        public override void Run( MeditationLogContext context )
        {
            DockerBuilder.BuildPlatformImage( context, DockerConstants.WinX64Platform );
        }
    }

    [TaskName( "build_docker_linux_x64" )]
    [TaskDescription( "Builds the Docker image for Linux x64." )]
    public sealed class BuildLinuxX64DockerTask : DefaultTask
    {
        // ----------------- Functions -----------------

        public override void Run( MeditationLogContext context )
        {
            DockerBuilder.BuildPlatformImage( context, DockerConstants.LinuxX64Platform );
        }
    }

    [TaskName( "build_docker_linux_arm32" )]
    [TaskDescription( "Builds the Docker image for Linux arm32." )]
    public sealed class BuildLinuxArm32DockerTask : DefaultTask
    {
        // ----------------- Functions -----------------

        public override void Run( MeditationLogContext context )
        {
            DockerBuilder.BuildPlatformImage( context, DockerConstants.LinuxArm32Platform );
        }
    }

    [TaskName( "build_docker_linux_arm64" )]
    [TaskDescription( "Builds the Docker image for Linux arm32." )]
    public sealed class BuildLinuxArm64DockerTask : DefaultTask
    {
        // ----------------- Functions -----------------

        public override void Run( MeditationLogContext context )
        {
            DockerBuilder.BuildPlatformImage( context, DockerConstants.LinuxArm64Platform );
        }
    }

    [TaskName( "build_docker_manifest" )]
    [TaskDescription(
        "Builds the Docker manifest image.  Assumes all platform images have been pushed."
    )]
    public sealed class BuildDockerManifestImage : DefaultTask
    {
        // ----------------- Functions -----------------

        public override void Run( MeditationLogContext context )
        {
            DockerBuilder.BuildManifestImage(
                context,
                DockerConstants.WinX64Platform,
                DockerConstants.LinuxX64Platform,
                DockerConstants.LinuxArm32Platform,
                DockerConstants.LinuxArm64Platform
            );
        }
    }
}

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

using Cake.Frosting;

namespace DevOps.Docker
{
    [TaskName( "push_docker_windows_x64" )]
    [TaskDescription( "Pushes the Windows docker image." )]
    public sealed class DockerPushWindowsX64Task : DefaultTask
    {
        // ---------------- Functions ----------------

        public override void Run( MeditationLogContext context )
        {
            DockerPusher.PushPlatformImage( context, DockerConstants.WinX64Platform );
        }
    }

    [TaskName( "push_docker_linux_x64" )]
    [TaskDescription( "Pushes the x64 Linux docker image." )]
    public sealed class DockerPushLinuxX64Task : DefaultTask
    {
        // ---------------- Functions ----------------

        public override void Run( MeditationLogContext context )
        {
            DockerPusher.PushPlatformImage( context, DockerConstants.LinuxX64Platform );
        }
    }

    [TaskName( "push_docker_linux_arm32" )]
    [TaskDescription( "Pushes the arm32 Linux docker image." )]
    public sealed class DockerPushLinuxArm32Task : DefaultTask
    {
        // ---------------- Functions ----------------

        public override void Run( MeditationLogContext context )
        {
            DockerPusher.PushPlatformImage( context, DockerConstants.LinuxArm32Platform );
        }
    }

    [TaskName( "push_docker_manifest" )]
    [TaskDescription( "Pushes the manifest docker image." )]
    public sealed class DockerPushManifest : DefaultTask
    {
        // ---------------- Functions ----------------

        public override void Run( MeditationLogContext context )
        {
            DockerPusher.PushManifest( context );
        }
    }
}

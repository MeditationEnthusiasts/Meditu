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

namespace DevOps.DevopsPublish
{
    [TaskName( "devops_publish_windows_x64" )]
    [TaskDescription( "Publishes the devops project so it can be used in pipelines without re-building" )]
    public sealed class WindowsX64DevopsPublishTask : DefaultTask
    {
        // ----------------- Functions -----------------

        public override void Run( MeditationLogContext context )
        {
            DevopsPublisher.DevopsPublish( context, "win-x64" );
        }
    }

    [TaskName( "devops_publish_linux_x64" )]
    [TaskDescription( "Publishes the devops project so it can be used in pipelines without re-building" )]
    public sealed class LinuxX64DevopsPublishTask : DefaultTask
    {
        // ----------------- Functions -----------------

        public override void Run( MeditationLogContext context )
        {
            DevopsPublisher.DevopsPublish( context, "linux-x64" );
        }
    }

    [TaskName( "devops_publish_linux_arm32" )]
    [TaskDescription( "Publishes the devops project so it can be used in pipelines without re-building" )]
    public sealed class LinuxarmDevopsPublishTask : DefaultTask
    {
        // ----------------- Functions -----------------

        public override void Run( MeditationLogContext context )
        {
            DevopsPublisher.DevopsPublish( context, "linux-arm", "linux-arm32" );
        }
    }
}

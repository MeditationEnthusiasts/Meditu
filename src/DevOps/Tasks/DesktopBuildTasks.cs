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

using Cake.Frosting;

namespace DevOps.Tasks
{
    [TaskName( "build_windows" )]
    [TaskDescription( "Builds the desktop application for Windows" )]
    public class WindowsBuildTask : DefaultTask
    {
        // ---------------- Functions ----------------

        public override void Run( MeditationLogContext context )
        {
            var electronBuilder = new ElectronBuilder( context );
            electronBuilder.Build( "win" );
        }
    }

    [TaskName( "build_linux" )]
    [TaskDescription( "Builds the desktop application for Linux" )]
    public class LinuxBuildTask : DefaultTask
    {
        // ---------------- Functions ----------------

        public override void Run( MeditationLogContext context )
        {
            var electronBuilder = new ElectronBuilder( context );
            electronBuilder.Build( "linux" );
        }
    }

    [TaskName( "build_mac" )]
    [TaskDescription( "Builds the desktop application for Linux" )]
    public class MacOsBuildTask : DefaultTask
    {
        // ---------------- Functions ----------------

        public override void Run( MeditationLogContext context )
        {
            var electronBuilder = new ElectronBuilder( context );
            electronBuilder.Build( "mac" );
        }
    }
}

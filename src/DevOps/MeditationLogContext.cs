//
// Meditation Logger.
// Copyright (C) 2017  Seth Hendrick.
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using Cake.Core;
using Cake.Core.IO;
using Cake.Frosting;

namespace DevOps
{
    public sealed class MeditationLogContext : FrostingContext
    {
        // ---------------- Constructor ----------------

        public MeditationLogContext( ICakeContext context ) :
            base( context )
        {
            this.RepoRoot = context.Environment.WorkingDirectory;
            this.SrcPath = this.RepoRoot.Combine( new DirectoryPath( "src" ) );
            this.SlnPath = this.SrcPath.CombineWithFilePath( new FilePath( "MeditationLogger.sln" ) );

#if DEBUG
            this.RunningRelease = false;
#else
            this.RunningRelease = true;
#endif
        }

        // ---------------- Properties ----------------

        public DirectoryPath RepoRoot { get; private set; }

        public DirectoryPath SrcPath { get; private set; }

        public FilePath SlnPath { get; private set; }

        public bool RunningRelease { get; set; }
    }
}

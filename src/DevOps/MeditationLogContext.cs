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

using Cake.Common.Diagnostics;
using Cake.Common.Security;
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
            this.SlnPath = this.SrcPath.CombineWithFilePath( new FilePath( "Meditu.sln" ) );
            this.DistributionPath = this.RepoRoot.Combine( new DirectoryPath( "dist" ) );
            this.DesktopDistributionPath = this.DistributionPath.Combine( new DirectoryPath( "desktop" ) );
            this.DevopsDistributionPath= this.DistributionPath.Combine( new DirectoryPath( "devops" ) );
            this.DockerPath = this.RepoRoot.Combine( new DirectoryPath( "Docker" ) );
            this.DevopsCsProj = this.SrcPath.CombineWithFilePath(
                new FilePath( "DevOps/DevOps.csproj" )
            );
            this.GuiCsProject = this.SrcPath.CombineWithFilePath(
                new FilePath( "Meditu.Gui/Meditu.Gui.csproj" )
            );
            this.ImageDirectory = this.GuiCsProject.GetDirectory().Combine(
                "wwwroot/static/img"
            );

#if DEBUG
            this.RunningRelease = false;
#else
            this.RunningRelease = true;
#endif
        }

        // ---------------- Properties ----------------

        public DirectoryPath RepoRoot { get; private set; }

        public DirectoryPath DistributionPath { get; private set; }

        public DirectoryPath DesktopDistributionPath { get; private set; }

        public DirectoryPath DevopsDistributionPath { get; private set; }

        public DirectoryPath SrcPath { get; private set; }

        public DirectoryPath DockerPath { get; private set; }

        public FilePath SlnPath { get; private set; }

        public FilePath DevopsCsProj { get; private set; }

        public FilePath GuiCsProject { get; private set; }

        public DirectoryPath ImageDirectory { get; private set; }

        public bool RunningRelease { get; set; }

        // ---------------- Functions ----------------
        public void GenerateSha256( FilePath source, FilePath output )
        {
            FileHash hash = this.CalculateFileHash( source, HashAlgorithm.SHA256 );

            string hashStr = hash.ToHex();
            File.WriteAllText( output.ToString(), hashStr );
            this.Information( "Hash for " + source.GetFilename() + ": " + hashStr );
        }
    }
}

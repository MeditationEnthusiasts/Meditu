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

using Cake.ArgumentBinder;
using Cake.Common.Diagnostics;
using Cake.Common.IO;
using Cake.Core.IO;

namespace DevOps
{
    internal sealed class ElectronBuilder
    {
        // ---------------- Fields ----------------

        private readonly MeditationLogContext context;

        // ---------------- Constructor ----------------

        public ElectronBuilder( MeditationLogContext context )
        {
            this.context = context;
        }

        // ---------------- Functions ----------------

        public void Build( string target )
        {
            Build( target, this.context.CreateFromArguments<ElectronBuilderArguments>() );
        }

        public void Build( string target, ElectronBuilderArguments userArgs )
        {
            FilePath projectPath = this.context.GuiCsProject;
            DirectoryPath workingDirectory = projectPath.GetDirectory();
            DirectoryPath outputDirectory = this.context.DistributionPath.Combine( new DirectoryPath( target ) );
            string medituExeName = projectPath.GetFilenameWithoutExtension().ToString();

            var arguments = ProcessArgumentBuilder.FromStrings(
                new string[]
                {
                    "build",
                    $"/target {target}",
                    $"/absolute-path {outputDirectory}",
                    $"/version {VersionInfo.VersionString}",
                    $"/p:Version={VersionInfo.VersionString}",
                    $"/p:AssemblyVersion={VersionInfo.VersionString}",
                    $"/p:FileVersion={VersionInfo.VersionString}"
                }
            );

            var processSettings = new ProcessSettings
            {
                Arguments = arguments,
                WorkingDirectory = workingDirectory
            };

            this.context.EnsureDirectoryExists( outputDirectory );
            this.context.CleanDirectory( outputDirectory );

            FilePath electronizeExe = userArgs.ElectronizePath ?? new FilePath( "electronize" );
            context.Information( $"Building desktop application for {target}" );

            using( IProcess proc = this.context.ProcessRunner.Start( electronizeExe, processSettings ) )
            {
                proc.WaitForExit();
                if( proc.GetExitCode() != 0 )
                {
                    throw new Exception( "electronize exited with exit code: " + proc.GetExitCode() );
                }
            }
        }

        private void CreateManifestFile( DirectoryPath workingDirectory )
        {
        }
    }
}

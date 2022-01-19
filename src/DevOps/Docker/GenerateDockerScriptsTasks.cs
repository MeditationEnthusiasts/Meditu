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
using Cake.ArgumentBinder;
using Cake.Common;
using Cake.Common.IO;
using Cake.Core.IO;
using Cake.Frosting;

namespace DevOps.Docker
{
    [TaskName( "generate_windows_docker_push_script" )]
    [TaskDescription( "Generates the Docker Push script for the deploy pipeline." )]
    public sealed class GenerateWindowsPushScriptTask : BaseGenerateScriptTask
    {
        // ---------------- Properties ----------------
     
        public override string Platform => DockerConstants.WinX64Platform;
    }

    [TaskName( "generate_linux_x64_docker_push_script" )]
    [TaskDescription( "Generates the Docker Push script for the deploy pipeline." )]
    public sealed class GenerateLinuxX64PushScriptTask : BaseGenerateScriptTask
    {
        // ---------------- Properties ----------------
     
        public override string Platform => DockerConstants.LinuxX64Platform;
    }

    [TaskName( "generate_linux_arm32_docker_push_script" )]
    [TaskDescription( "Generates the Docker Push script for the deploy pipeline." )]
    public sealed class GenerateLinuxArm32PushScriptTask : BaseGenerateScriptTask
    {
        // ---------------- Properties ----------------

        public override string Platform => DockerConstants.LinuxArm32Platform;
    }

    [TaskName( "generate_manifest_docker_push_script" )]
    [TaskDescription( "Generates the Docker manifest script for the deploy pipeline." )]
    public sealed class GenerateManifestPushScriptTask : DefaultTask
    {
        // ---------------- Properties ----------------
    }

    public abstract class BaseGenerateScriptTask : DefaultTask
    {
        // ---------------- Properties ----------------

        public abstract string Platform { get; }

        // ---------------- Functions ----------------

        public override void Run( MeditationLogContext context )
        {
            var args = context.CreateFromArguments<DockerScriptArguments>();

            DirectoryPath outputDir;
            if( args.OutputDirectory == null )
            {
                outputDir = context.DistributionPath.Combine( "scripts" );
            }
            else
            {
                outputDir = args.OutputDirectory;
            }

            outputDir = outputDir.Combine( this.Platform );
            FilePath outputPath = outputDir.CombineWithFilePath( this.Platform );

            context.EnsureDirectoryExists( outputDir );

            string fileContents = $"docker push {DockerConstants.ImageName}:{VersionInfo.VersionString}_{this.Platform}";
            System.IO.File.WriteAllText( outputPath.ToString(), fileContents );
        }
    }
}
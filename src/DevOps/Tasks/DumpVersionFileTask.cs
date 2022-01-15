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
using Cake.Frosting;

namespace DevOps.Tasks
{
    [TaskName( "dump_version" )]
    [TaskDescription( "Writes the version to the given file." )]
    public sealed class DumpVersionFileTask : DefaultTask
    {
        // ---------------- Functions ----------------

        public override void Run( MeditationLogContext context )
        {
            var args = context.CreateFromArguments<VersionDumpArguments>();

            context.Information( $"Writing version '{VersionInfo.VersionString}' to file '{args.OutputFile}'." );

            File.WriteAllText(
                args.OutputFile.ToString(),
                VersionInfo.VersionString
            );
        }
    }
}

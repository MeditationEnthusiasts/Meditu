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

using Cake.ArgumentBinder;
using Cake.Core.IO;

namespace DevOps.VersionDump
{
    public sealed class VersionDumpArguments
    {
        // ---------------- Fields ----------------

        /// <summary>
        /// Default to current directory.
        /// </summary>
        private const string defaultPath = ".";

        // ---------------- Constructor ----------------

        public VersionDumpArguments()
        {
            this.OutputFile = new FilePath( defaultPath );
        }

        // ---------------- Properties ----------------

        [FilePathArgument(
            "output",
            Description = "What file to write the version to.  Defaulted to the current working directory.",
            Required = false,
            DefaultValue = defaultPath
        )]
        public FilePath OutputFile { get; set; }
    }
}

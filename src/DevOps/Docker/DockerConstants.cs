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

using Meditu.Constants;

namespace DevOps.Docker
{
    internal static class DockerConstants
    {
        // ---------------- Fields ----------------

        internal const string ImageName = "xforever1313/meditu";

        internal const string WinX64Platform = "win-x64";

        internal const string LinuxX64Platform = "linux-x64";

        internal const string LinuxArm32Platform = "linux-arm32";

        // ---------------- Functions ----------------

        public static string GetPlatformImageName( string platform )
        {
            return $"{ImageName}:{MedituConstants.VersionString}_{platform}";
        }
    }
}
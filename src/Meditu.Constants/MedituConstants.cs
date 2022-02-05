﻿//
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

#if CAKE
#else
namespace Meditu.Constants
{
#endif
    public static class MedituConstants
    {
        public static readonly Version Version = new Version( 0, 4, 0 );

        public static readonly string VersionString = Version.ToString( 3 );

        public static readonly string CopyrightString = "Copyright (C) 2017-2022 Meditation Enthusiasts.";

        public static readonly string Website = "https://meditationenthusiasts.org";

        public static readonly string Github = "https://github.com/MeditationEnthusiasts/Meditu";

        public static readonly string IssueTracker = "https://github.com/MeditationEnthusiasts/Meditu/issues/";
    }
#if CAKE
#else
}
#endif
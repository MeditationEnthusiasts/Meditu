﻿//
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

using Cake.ArgumentBinder;

namespace DevOps
{
    internal class TestArguments
    {
        // ---------------- Fields ----------------

        public static readonly string CoverageFilter = "+[*]MeditationLogger*";

        // ---------------- Constructor ----------------

        public TestArguments()
        {
            this.RunWithCodeCoverage = false;
        }

        // ---------------- Properties ----------------

        [BooleanArgument(
            "code_coverage",
            Description = "Should we run this with code coverage?",
            DefaultValue = false
        )]
        public bool RunWithCodeCoverage { get; set; }
    }
}
//
// MeditationLogger - A way to track Meditation Sessions.
// Copyright (C) 2017-2021 Seth Hendrick
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

using Cake.Common.Solution;
using Cake.Core.IO;
using Cake.Frosting;
using Cake.LicenseHeaderUpdater;
using Seth.CakeLib;

namespace DevOps.Tasks
{
    [TaskName( "update_licenses" )]
    public class UpdateLicenseHeaderTask : DefaultTask
    {
        // ---------------- Fields ----------------

        const string currentLicense =
@"//
// MeditationLogger - A way to track Meditation Sessions.
// Copyright (C) 2017-2021 Seth Hendrick
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

";

        const string oldLicense =
@"^//
//\s*Meditation\s+Logger\.
//\s*Copyright\s+\(C\)\s+\d+\s+Seth Hendrick\.
//\s*
//\s*This\s+program\s+is\s+free\s+software:\s+you\s+can\s+redistribute\s+it\s+and/or\s+modify
//\s*it\s+under\s+the\s+terms\s+of\s+the\s+GNU\s+General\s+Public\s+License\s+as\s+published\s+by
//\s*the\s+Free\s+Software\s+Foundation,\s+either\s+version\s+3\s+of\s+the\s+License,\s+or
//\s*\(at\s+your\s+option\)\s+any\s+later\s+version\.
//\s*
//\s*This\s+program\s+is\s+distributed\s+in\s+the\s+hope\s+that\s+it\s+will\s+be\s+useful,
//\s*but\s+WITHOUT\s+ANY\s+WARRANTY;\s+without\s+even\s+the\s+implied\s+warranty\s+of
//\s*MERCHANTABILITY\s+or\s+FITNESS\s+FOR\s+A\s+PARTICULAR\s+PURPOSE\.\s+\s+See\s+the
//\s*GNU\s+General\s+Public\s+License\s+for\s+more\s+details\.
//\s*
//\s*You\s+should\s+have\s+received\s+a\s+copy\s+of\s+the\s+GNU\s+General\s+Public\s+License
//\s*along\s+with\s+this\s+program\.\s+\s+If\s+not,\s+see\s+<http://www\.gnu\.org/licenses/>\.
//[\n\r\s]*";

        // ---------------- Functions ----------------

        public override void Run( MeditationLogContext context )
        {
            var settings = new CakeLicenseHeaderUpdaterSettings
            {
                LicenseString = currentLicense,
                Threads = 0,
            };

            settings.OldHeaderRegexPatterns.Add( oldLicense );

            settings.FileFilter = SolutionProjectHelpers.DefaultCsFileFilter;

            var files = new List<FilePath>();

            context.PerformActionOnSolutionCsFiles(
                context.SlnPath,
                ( path ) => files.Add( path ),
                null,
                delegate ( SolutionProject slnProject )
                {
                    if( slnProject.Path.ToString().Contains( "SethCS", StringComparison.OrdinalIgnoreCase ) )
                    {
                        return false;
                    }

                    return true;
                }
            );

            context.UpdateLicenseHeaders( files, settings );
        }
    }
}

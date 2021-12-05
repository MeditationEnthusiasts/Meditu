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
//\s*Meditation Logger\.
//\s*Copyright (C) \d+\s+Seth Hendrick\.
//\s*
//\s*This program is free software: you can redistribute it and/or modify
//\s*it under the terms of the GNU General Public License as published by
//\s*the Free Software Foundation, either version 3 of the License, or
//\s*\(at your option\) any later version\.
//\s*
//\s*This program is distributed in the hope that it will be useful,
//\s*but WITHOUT ANY WARRANTY; without even the implied warranty of
//\s*MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE\.  See the
//\s*GNU General Public License for more details\.
//\s*
//\s*You should have received a copy of the GNU General Public License
//\s*along with this program\.  If not, see <http://www\.gnu\.org/licenses/>\.
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

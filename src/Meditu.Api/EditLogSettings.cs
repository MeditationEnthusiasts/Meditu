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
using System.Text;

namespace Meditu.Api
{
    /// <summary>
    /// Settings for editing a log.
    /// </summary>
    public record EditLogSettings(
        DateTime StartTime,
        DateTime EndTime,
        string Technique,
        string Comments,
        bool RemoveLocation
    );

    public static class EditLogSettingsExtensions
    {
        public static bool TryValidate( this EditLogSettings settings, out string errorString )
        {
            bool success = true;
            StringBuilder builder = new StringBuilder();

            if( settings.StartTime > settings.EndTime )
            {
                success = false;
                builder.AppendLine( "Start Time can not be greater than the End Time." );
            }

            errorString = builder.ToString();
            return success;
        }
    }
}

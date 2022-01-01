//
// Meditu - A way to track Meditation Sessions.
// Copyright (C) 2017-2021 Seth Hendrick.
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditu.Api
{
    public static class DateTimeFormatExtensions
    {
        // ---------------- Functions ----------------

        /// <summary>
        /// Reads in the settings, and returns a string representation
        /// of the <see cref="TimeSpan"/> based on the settings.
        /// </summary>
        public static string ToSettingsString( this TimeSpan timespan, IReadOnlyDateTimeSettings settings )
        {
            ArgumentNullException.ThrowIfNull( settings, nameof( settings ) );

            DurationFormat duration = settings.DurationFormat;
            DurationSeparator separator = settings.DurationSeparator;

            string formatString;
            if( duration == DurationFormat.HourMinuteSecond )
            {
                if( separator == DurationSeparator.LettersOnly )
                {
                    formatString = @"hh'H 'mm'M 'ss'S'";
                }
                else if( separator == DurationSeparator.ColonAndLetters )
                {
                    formatString = @"hh'H'\:mm'M'\:ss'S'";
                }
                else // ColonOnly is the default.
                {
                    formatString = @"hh\:mm\:ss";
                }
            }
            else // HourMinute (and any others) are the default.
            {
                if( separator == DurationSeparator.LettersOnly )
                {
                    formatString = @"hh'H 'mm'M'";
                }
                else if( separator == DurationSeparator.ColonAndLetters )
                {
                    formatString = @"hh'H'\:mm'M'";
                }
                else // ColonOnly is the default.
                {
                    formatString = @"hh\:mm";
                }
            }

            return timespan.ToString( formatString );
        }
    }
}

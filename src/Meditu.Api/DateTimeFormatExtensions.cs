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

namespace Meditu.Api
{
    public static class DateTimeFormatExtensions
    {
        // ---------------- Functions ----------------

        /// <summary>
        /// Reads in the settings and returns a string representation
        /// of the <see cref="DateTime"/> object based on the settings.
        /// </summary>
        public static string ToSettingsString( this DateTime date, IReadOnlyDateTimeSettings settings )
        {
            ArgumentNullException.ThrowIfNull( settings, nameof( settings ) );

            DateFormat dateFormat = settings.DateFormat;
            MonthFormat monthFormat = settings.MonthFormat;
            DateSeparatorFormat dateSepFormat = settings.DateSeparatorFormat;
            TimeFormat timeFormat = settings.TimeFormat;

            char dateSeparator;
            if( dateSepFormat == DateSeparatorFormat.Dashes )
            {
                dateSeparator = '-';
            }
            else // Slashes are the default.
            {
                dateSeparator = '/';
            }

            string monthFormatString;
            if( monthFormat == MonthFormat.ThreeLetters )
            {
                monthFormatString = "MMM";
            }
            else if( monthFormat == MonthFormat.FullMonth )
            {
                monthFormatString = "MMMM";
            }
            else // Number is the default.
            {
                monthFormatString = "MM";
            }

            string hourFormatString;
            string amPmFormatString;
            if( timeFormat == TimeFormat.Hour24 )
            {
                hourFormatString = "HH";
                amPmFormatString = string.Empty;
            }
            else // 12 hour is the default.
            {
                hourFormatString = "hh";
                amPmFormatString = " tt";
            }

            string timeFormatString = $"{hourFormatString}:mm{amPmFormatString}";
            string formatString;
            if( dateFormat == DateFormat.DayMonthYear )
            {
                formatString = $"dd{dateSeparator}{monthFormatString}{dateSeparator}yyyy {timeFormatString}";
            }
            else if( dateFormat == DateFormat.YearMonthDay )
            {
                formatString = $"yyyy{dateSeparator}{monthFormatString}{dateSeparator}dd {timeFormatString}";
            }
            else // Month/Day/Year is the default.
            {
                formatString = $"{monthFormatString}{dateSeparator}dd{dateSeparator}yyyy {timeFormatString}";
            }

            return date.ToString( formatString );
        }

        /// <summary>
        /// Reads in the settings, and returns a string representation
        /// of the <see cref="TimeSpan"/> object based on the settings.
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

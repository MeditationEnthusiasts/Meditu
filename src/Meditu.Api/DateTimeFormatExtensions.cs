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

using System;

namespace Meditu.Api
{
    public static class DateTimeFormatExtensions
    {
        // ---------------- Functions ----------------

        public static TimeZoneInfo GetTimeZoneInfo( this DateTimeSettings settings )
        {
            // If not specified, assume local timezone.
            if( string.IsNullOrWhiteSpace( settings.TimeZoneIdentifier ) )
            {
                return TimeZoneInfo.Local;
            }

            try
            {
                return TimeZoneInfo.FindSystemTimeZoneById( settings.TimeZoneIdentifier );
            }
            catch( TimeZoneNotFoundException )
            {
                // If we can't find the timezone, assume UTC.
                return TimeZoneInfo.Utc;
            }
        }

        public static DateTime ToTimeZoneTime( this DateTime date, DateTimeSettings settings )
        {
            TimeZoneInfo tz = settings.GetTimeZoneInfo();

            return TimeZoneInfo.ConvertTime(
                date,
                TimeZoneInfo.Utc,
                tz
            );
        }

        public static DateTime GetLocalDateTimeNow( this DateTimeSettings settings )
        {
            return DateTime.UtcNow.ToTimeZoneTime( settings );
        }

        // -------- ToSettingsString --------

        /// <summary>
        /// Reads in the settings and returns a string representation
        /// of the <see cref="DateTime"/> object based on the settings.
        /// </summary>
        public static string ToSettingsString( this DateTime date, DateTimeSettings settings )
        {
            ArgumentNullException.ThrowIfNull( settings, nameof( settings ) );

            date = date.ToTimeZoneTime( settings );

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

        public static string ToFormDateString( this DateTime date, DateTimeSettings settings )
        {
            date = date.ToTimeZoneTime( settings );

            return date.ToString( "s" );
        }

        public static string ToSettingsString( this TimeOnly time, DateTimeSettings settings )
        {
            string hourFormatString;
            string amPmFormatString;
            if( settings.TimeFormat == TimeFormat.Hour24 )
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
            return time.ToString( timeFormatString );
        }

        /// <summary>
        /// Reads in the settings, and returns a string representation
        /// of the <see cref="TimeSpan"/> object based on the settings.
        /// </summary>
        public static string ToSettingsString( this TimeSpan timespan, DateTimeSettings settings )
        {
            ArgumentNullException.ThrowIfNull( settings, nameof( settings ) );

            DurationFormat duration = settings.DurationFormat;
            DurationSeparator separator = settings.DurationSeparator;

            string formatString;
            if( duration == DurationFormat.HourMinuteSecond )
            {
                if( separator == DurationSeparator.LettersOnly )
                {
                    formatString = @"'H 'mm'M 'ss'S'";
                }
                else if( separator == DurationSeparator.ColonAndLetters )
                {
                    formatString = @"'H'\:mm'M'\:ss'S'";
                }
                else // ColonOnly is the default.
                {
                    formatString = @"\:mm\:ss";
                }
            }
            else // HourMinute (and any others) are the default.
            {
                if( separator == DurationSeparator.LettersOnly )
                {
                    formatString = @"'H 'mm'M'";
                }
                else if( separator == DurationSeparator.ColonAndLetters )
                {
                    formatString = @"'H'\:mm'M'";
                }
                else // ColonOnly is the default.
                {
                    formatString = @"\:mm";
                }
            }

            // For total hours, need to use the TotalHours property instead of using
            // a format string.  Otherwise, if the time is more than a day,
            // it won't report the correct number of hours.
            //
            // Need to Math.Floor it as well, otherwise if we have more than half an hour
            // in the minutes section, it will round up.
            return
                Math.Floor( timespan.TotalHours ).ToString( "00" ) +
                timespan.ToString( formatString );
        }

        // -------- ToLabelString --------

        public static string ToLabelString(
            this DateFormat format,
            DateTimeSettings currentSettings,
            DateTime sampleTime
        )
        {
            string label;
            if( format == DateFormat.DayMonthYear )
            {
                label = "Day Month Year";
            }
            else if( format == DateFormat.YearMonthDay )
            {
                label = "Year Month Day";
            }
            else // MonthDayYear is the default.
            {
                label = "Month Day Year";
            }

            DateTimeSettings settings = currentSettings with { DateFormat = format };

            return $"{label} ({sampleTime.ToSettingsString( settings )})";
        }

        public static string ToLabelString(
            this MonthFormat format,
            DateTimeSettings currentSettings,
            DateTime sampleTime
        )
        {
            string label;
            if( format == MonthFormat.ThreeLetters )
            {
                label = "Abbreviated";
            }
            else if( format == MonthFormat.FullMonth )
            {
                label = "Full Month";
            }
            else // Number is the default.
            {
                label = "Digit";
            }

            DateTimeSettings settings = currentSettings with { MonthFormat = format };

            return $"{label} ({sampleTime.ToSettingsString( settings )})";
        }

        public static string ToLabelString(
            this DateSeparatorFormat format,
            DateTimeSettings currentSettings,
            DateTime sampleTime
        )
        {
            string label;
            if( format == DateSeparatorFormat.Dashes )
            {
                label = "Dashes";
            }
            else // Slashes is the default.
            {
                label = "Slashes";
            }

            DateTimeSettings settings = currentSettings with { DateSeparatorFormat = format };

            return $"{label} ({sampleTime.ToSettingsString( settings )})";
        }

        public static string ToLabelString(
            this TimeFormat format,
            DateTimeSettings currentSettings,
            DateTime sampleTime
        )
        {
            string label;
            if( format == TimeFormat.Hour24 )
            {
                label = "24 Hour";
            }
            else // 12 Hour is the default.
            {
                label = "12 Hour";
            }

            DateTimeSettings settings = currentSettings with { TimeFormat = format };
            var time = TimeOnly.FromDateTime( sampleTime );

            return $"{label} ({time.ToSettingsString( settings )})";
        }

        public static string ToLabelString(
            this DurationFormat format,
            DateTimeSettings currentSettings,
            TimeSpan sampleTime
        )
        {
            string label;
            if( format == DurationFormat.HourMinuteSecond )
            {
                label = "Hour Minute Second";
            }
            else // Hour Minute is the default.
            {
                label = "Hour Minute";
            }

            DateTimeSettings settings = currentSettings with { DurationFormat = format };
            return $"{label} ({sampleTime.ToSettingsString( settings )})";
        }

        public static string ToLabelString(
            this DurationSeparator format,
            DateTimeSettings currentSettings,
            TimeSpan sampleTime
        )
        {
            string label;
            if( format == DurationSeparator.ColonAndLetters )
            {
                label = "Colons and Letters";
            }
            else if( format == DurationSeparator.LettersOnly )
            {
                label = "Letters Only";
            }
            else // Colons only is the default.
            {
                label = "Colons Only";
            }

            DateTimeSettings settings = currentSettings with { DurationSeparator = format };
            return $"{label} ({sampleTime.ToSettingsString( settings )})";
        }
    }
}

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

namespace Meditu.Api
{
    // ---------------- Date Format ----------------

    public enum DateFormat
    {
        /// <summary>
        /// Day/Month/Year
        /// </summary>
        DayMonthYear = 0,

        /// <summary>
        /// Month/Day/Year
        /// </summary>
        MonthDayYear = 1,

        /// <summary>
        /// Year/Day/Month
        /// </summary>
        YearMonthDay = 2
    }

    public enum MonthFormat
    {
        /// <summary>
        /// The month is the number.
        /// For example, June is 6.
        /// </summary>
        Number = 0,

        /// <summary>
        /// The month is three letters long.
        /// For example, June is "Jun".
        /// </summary>
        ThreeLetters = 1,

        /// <summary>
        /// The month is the full month name.
        /// For example, June is "June".
        /// </summary>
        FullMonth = 2
    }

    public enum DateSeparatorFormat
    {
        /// <summary>
        /// Date is separated by slashes.
        /// MM/DD/YYYY
        /// </summary>
        Slashes = 0,

        /// <summary>
        /// Date is separated by dashes.
        /// MM-DD-YYYY
        /// </summary>
        Dashes = 1
    }

    public enum TimeFormat
    {
        /// <summary>
        /// Show times in 12 hour format.
        /// </summary>
        Hour12 = 0,

        /// <summary>
        /// Show times in 24 hour format.
        /// </summary>
        Hour24 = 1
    }

    // ---------------- Duration Format ----------------

    public enum DurationFormat
    {
        /// <summary>
        /// Duration shows the hours and minutes.
        /// </summary>
        HourMinute = 0,

        /// <summary>
        /// Duration shows the hours, minutes, and seconds.
        /// </summary>
        HourMinuteSecond = 1
    }

    public enum DurationSeparator
    {
        /// <summary>
        /// Format is Hour:Minute:Second
        /// </summary>
        ColonOnly = 0,

        /// <summary>
        /// Format is hhH mmM ssS
        /// </summary>
        LettersOnly = 1,

        /// <summary>
        /// Format is hhH:mmM:ssS
        /// </summary>
        ColonAndLetters = 2
    }
}

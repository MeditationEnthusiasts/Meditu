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
    public enum DateFormat
    {
        /// <summary>
        /// Day/Month/Year
        /// </summary>
        DayMonthYear,

        /// <summary>
        /// Month/Day/Year
        /// </summary>
        MonthDayYear,

        /// <summary>
        /// Year/Day/Month
        /// </summary>
        YearMonthDay
    }

    public enum MonthFormat
    {
        /// <summary>
        /// The month is the number.
        /// For example, June is 6.
        /// </summary>
        Number,

        /// <summary>
        /// The month is three letters long.
        /// For example, June is "Jun".
        /// </summary>
        ThreeLetters,

        /// <summary>
        /// The month is the full month name.
        /// For example, June is "June".
        /// </summary>
        FullMonth
    }

    public enum TimeFormat
    {
        /// <summary>
        /// Show times in 12 hour format.
        /// </summary>
        Hour12,
        
        /// <summary>
        /// Show times in 24 hour format.
        /// </summary>
        Hour24
    }

    public enum DurationFormat
    {
        /// <summary>
        /// Duration shows the hours and minutes.
        /// </summary>
        HourMinute,

        /// <summary>
        /// Duration shows the hours, minutes, and seconds.
        /// </summary>
        HourMinuteSecond
    }

    public enum DurationSeparator
    {
        /// <summary>
        /// Format is Hour:Minute:Second
        /// </summary>
        ColonOnly,

        /// <summary>
        /// Format is hhH mmM ssS
        /// </summary>
        LettersOnly,

        /// <summary>
        /// Format is hhH:mmM:ssS
        /// </summary>
        ColonAndLetters
    }
}

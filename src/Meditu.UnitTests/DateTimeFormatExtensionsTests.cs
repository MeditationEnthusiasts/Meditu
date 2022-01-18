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
using Meditu.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Meditu.UnitTests
{
    [TestClass]
    public sealed class DateTimeFormatExtensionsTests
    {
        // ---------------- Tests ----------------

        // -------- DateTime Format Tests --------

        // ---- MDY ----

        // This block tests MDY, but also 12 hour vs 24 hour, and dashes vs slashes.

        [TestMethod]
        public void MDY_24Hr_NumberMonth_Dashes_SingleDigit_Test()
        {
            // Setup
            var date = new DateTime( 2020, 6, 3, 13, 2, 3, DateTimeKind.Utc );

            var settings = new DateTimeSettings
            {
                DateFormat = DateFormat.MonthDayYear,
                DateSeparatorFormat = DateSeparatorFormat.Dashes,
                MonthFormat = MonthFormat.Number,
                TimeFormat = TimeFormat.Hour24,
                TimeZoneIdentifier = "UTC"
            };

            // Act
            string format = date.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "06-03-2020 13:02", format );
        }

        [TestMethod]
        public void MDY_24Hr_NumberMonth_Dashes_SingleDigit_DifferentTimeZone_Test()
        {
            // Setup
            var date = new DateTime( 2020, 6, 3, 17, 2, 3, DateTimeKind.Utc );

            var settings = new DateTimeSettings
            {
                DateFormat = DateFormat.MonthDayYear,
                DateSeparatorFormat = DateSeparatorFormat.Dashes,
                MonthFormat = MonthFormat.Number,
                TimeFormat = TimeFormat.Hour24,
                TimeZoneIdentifier = "Eastern Standard Time"
            };

            // Act
            string format = date.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "06-03-2020 13:02", format );
        }

        [TestMethod]
        public void MDY_24Hr_NumberMonth_Dashes_DoubleDigit_Test()
        {
            // Setup
            var date = new DateTime( 2020, 12, 13, 11, 22, 23, DateTimeKind.Utc );

            var settings = new DateTimeSettings
            {
                DateFormat = DateFormat.MonthDayYear,
                DateSeparatorFormat = DateSeparatorFormat.Dashes,
                MonthFormat = MonthFormat.Number,
                TimeFormat = TimeFormat.Hour24,
                TimeZoneIdentifier = "UTC"
            };

            // Act
            string format = date.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "12-13-2020 11:22", format );
        }

        [TestMethod]
        public void MDY_12Hr_NumberMonth_Slashes_SingleDigit_Test()
        {
            // Setup
            var date = new DateTime( 2020, 6, 3, 13, 2, 3, DateTimeKind.Utc );

            var settings = new DateTimeSettings
            {
                DateFormat = DateFormat.MonthDayYear,
                DateSeparatorFormat = DateSeparatorFormat.Slashes,
                MonthFormat = MonthFormat.Number,
                TimeFormat = TimeFormat.Hour12,
                TimeZoneIdentifier = "UTC"
            };

            // Act
            string format = date.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "06/03/2020 01:02 PM", format );
        }

        [TestMethod]
        public void MDY_12Hr_NumberMonth_Slashes_DoubleDigit_Test()
        {
            // Setup
            var date = new DateTime( 2020, 12, 13, 11, 22, 23, DateTimeKind.Utc );

            var settings = new DateTimeSettings
            {
                DateFormat = DateFormat.MonthDayYear,
                DateSeparatorFormat = DateSeparatorFormat.Slashes,
                MonthFormat = MonthFormat.Number,
                TimeFormat = TimeFormat.Hour12,
                TimeZoneIdentifier = "UTC"
            };

            // Act
            string format = date.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "12/13/2020 11:22 AM", format );
        }

        // ---- DMY ----

        // This block tests DMY, but also tests the 3-number Month format as well.

        [TestMethod]
        public void DMY_12Hr_3LetterMonth_Slashes_SingleDigit_Test()
        {
            // Setup
            var date = new DateTime( 2020, 6, 3, 13, 2, 3, DateTimeKind.Utc );

            var settings = new DateTimeSettings
            {
                DateFormat = DateFormat.DayMonthYear,
                DateSeparatorFormat = DateSeparatorFormat.Slashes,
                MonthFormat = MonthFormat.ThreeLetters,
                TimeFormat = TimeFormat.Hour12,
                TimeZoneIdentifier = "UTC"
            };

            // Act
            string format = date.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "03/Jun/2020 01:02 PM", format );
        }

        [TestMethod]
        public void DMY_24Hr_3LetterMonth_Dashes_DoubleDigit_Test()
        {
            // Setup
            var date = new DateTime( 2020, 12, 13, 11, 22, 23, DateTimeKind.Utc );

            var settings = new DateTimeSettings
            {
                DateFormat = DateFormat.DayMonthYear,
                DateSeparatorFormat = DateSeparatorFormat.Dashes,
                MonthFormat = MonthFormat.ThreeLetters,
                TimeFormat = TimeFormat.Hour24,
                TimeZoneIdentifier = "UTC"
            };

            // Act
            string format = date.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "13-Dec-2020 11:22", format );
        }

        // ---- YMD ----

        // This block tests DMY, but also tests the 3-number Month format as well.

        [TestMethod]
        public void YMD_12Hr_FullMonth_Slashes_SingleDigit_Test()
        {
            // Setup
            var date = new DateTime( 2020, 6, 3, 1, 2, 3, DateTimeKind.Utc );

            var settings = new DateTimeSettings
            {
                DateFormat = DateFormat.YearMonthDay,
                DateSeparatorFormat = DateSeparatorFormat.Slashes,
                MonthFormat = MonthFormat.FullMonth,
                TimeFormat = TimeFormat.Hour12,
                TimeZoneIdentifier = "UTC"
            };

            // Act
            string format = date.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "2020/June/03 01:02 AM", format );
        }

        [TestMethod]
        public void YMD_24Hr_FullMonth_Dashes_DoubleDigit_Test()
        {
            // Setup
            var date = new DateTime( 2020, 12, 13, 11, 22, 23, DateTimeKind.Utc );

            var settings = new DateTimeSettings
            {
                DateFormat = DateFormat.YearMonthDay,
                DateSeparatorFormat = DateSeparatorFormat.Dashes,
                MonthFormat = MonthFormat.FullMonth,
                TimeFormat = TimeFormat.Hour24,
                TimeZoneIdentifier = "UTC"
            };

            // Act
            string format = date.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "2020-December-13 11:22", format );
        }

        // -------- TimeOnly Format Tests --------

        // ---- 12 Hour ----

        [TestMethod]
        public void TimeOnly_12Hour_SingleDigit_Test()
        {
            // Setup
            var time = new TimeOnly( 3, 8 );

            var settings = new DateTimeSettings
            {
                TimeFormat = TimeFormat.Hour12
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "03:08 AM", format );
        }

        [TestMethod]
        public void TimeOnly_12Hour_DoubleDigit_Test()
        {
            // Setup
            var time = new TimeOnly( 14, 18 );

            var settings = new DateTimeSettings
            {
                TimeFormat = TimeFormat.Hour12
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "02:18 PM", format );
        }

        // ---- 24 Hour ----

        [TestMethod]
        public void TimeOnly_24Hour_SingleDigit_Test()
        {
            // Setup
            var time = new TimeOnly( 3, 8 );

            var settings = new DateTimeSettings
            {
                TimeFormat = TimeFormat.Hour24
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "03:08", format );
        }

        [TestMethod]
        public void TimeOnly_24Hour_DoubleDigit_Test()
        {
            // Setup
            var time = new TimeOnly( 14, 18 );

            var settings = new DateTimeSettings
            {
                TimeFormat = TimeFormat.Hour24
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "14:18", format );
        }

        // -------- TimeSpan Format Tests --------

        // ---- HMS Letters Only ----

        [TestMethod]
        public void HMS_LettersOnly_Seconds_Test()
        {
            // Setup
            var time = TimeSpan.FromSeconds( 1 );

            var settings = new DateTimeSettings
            {
                DurationFormat = DurationFormat.HourMinuteSecond,
                DurationSeparator = DurationSeparator.LettersOnly
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "00H 00M 01S", format );
        }

        [TestMethod]
        public void HMS_LettersOnly_MinutesAndSeconds_Test()
        {
            // Setup
            var time = TimeSpan.FromSeconds( 121 );

            var settings = new DateTimeSettings
            {
                DurationFormat = DurationFormat.HourMinuteSecond,
                DurationSeparator = DurationSeparator.LettersOnly
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "00H 02M 01S", format );
        }

        [TestMethod]
        public void HMS_LettersOnly_HoursAndMinutesAndSeconds_Test()
        {
            // Setup
            var time = TimeSpan.FromSeconds( 7263 );

            var settings = new DateTimeSettings
            {
                DurationFormat = DurationFormat.HourMinuteSecond,
                DurationSeparator = DurationSeparator.LettersOnly
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "02H 01M 03S", format );
        }

        [TestMethod]
        public void HMS_LettersOnly_HoursAndMinutesAndSecondsGreaterThan10_Test()
        {
            // Setup
            var time = new TimeSpan( 11, 22, 23 );

            var settings = new DateTimeSettings
            {
                DurationFormat = DurationFormat.HourMinuteSecond,
                DurationSeparator = DurationSeparator.LettersOnly
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "11H 22M 23S", format );
        }

        // ---- HMS Colon And Letters ----

        [TestMethod]
        public void HMS_ColonAndLetters_Seconds_Test()
        {
            // Setup
            var time = TimeSpan.FromSeconds( 1 );

            var settings = new DateTimeSettings
            {
                DurationFormat = DurationFormat.HourMinuteSecond,
                DurationSeparator = DurationSeparator.ColonAndLetters
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "00H:00M:01S", format );
        }

        [TestMethod]
        public void HMS_ColonAndLetters_MinutesAndSeconds_Test()
        {
            // Setup
            var time = TimeSpan.FromSeconds( 121 );

            var settings = new DateTimeSettings
            {
                DurationFormat = DurationFormat.HourMinuteSecond,
                DurationSeparator = DurationSeparator.ColonAndLetters
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "00H:02M:01S", format );
        }

        [TestMethod]
        public void HMS_ColonAndLetters_HoursAndMinutesAndSeconds_Test()
        {
            // Setup
            var time = TimeSpan.FromSeconds( 7263 );

            var settings = new DateTimeSettings
            {
                DurationFormat = DurationFormat.HourMinuteSecond,
                DurationSeparator = DurationSeparator.ColonAndLetters
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "02H:01M:03S", format );
        }

        [TestMethod]
        public void HMS_ColonAndLetters_HoursAndMinutesAndSecondsGreaterThan10_Test()
        {
            // Setup
            var time = new TimeSpan( 11, 22, 23 );

            var settings = new DateTimeSettings
            {
                DurationFormat = DurationFormat.HourMinuteSecond,
                DurationSeparator = DurationSeparator.ColonAndLetters
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "11H:22M:23S", format );
        }

        // ---- HMS Colon Only ----

        [TestMethod]
        public void HMS_ColonOnly_Seconds_Test()
        {
            // Setup
            var time = TimeSpan.FromSeconds( 1 );

            var settings = new DateTimeSettings
            {
                DurationFormat = DurationFormat.HourMinuteSecond,
                DurationSeparator = DurationSeparator.ColonOnly
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "00:00:01", format );
        }

        [TestMethod]
        public void HMS_ColonOnly_MinutesAndSeconds_Test()
        {
            // Setup
            var time = TimeSpan.FromSeconds( 121 );

            var settings = new DateTimeSettings
            {
                DurationFormat = DurationFormat.HourMinuteSecond,
                DurationSeparator = DurationSeparator.ColonOnly
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "00:02:01", format );
        }

        [TestMethod]
        public void HMS_ColonOnly_HoursAndMinutesAndSeconds_Test()
        {
            // Setup
            var time = TimeSpan.FromSeconds( 7263 );

            var settings = new DateTimeSettings
            {
                DurationFormat = DurationFormat.HourMinuteSecond,
                DurationSeparator = DurationSeparator.ColonOnly
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "02:01:03", format );
        }

        [TestMethod]
        public void HMS_ColonOnly_HoursAndMinutesAndSecondsGreaterThan10_Test()
        {
            // Setup
            var time = new TimeSpan( 11, 22, 23 );

            var settings = new DateTimeSettings
            {
                DurationFormat = DurationFormat.HourMinuteSecond,
                DurationSeparator = DurationSeparator.ColonOnly
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "11:22:23", format );
        }

        [TestMethod]
        public void HMS_ColonOnly_MoreThanADay_Test()
        {
            // Setup
            var time = new TimeSpan( 48 + 11, 22, 23 );

            var settings = new DateTimeSettings
            {
                DurationFormat = DurationFormat.HourMinuteSecond,
                DurationSeparator = DurationSeparator.ColonOnly
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "59:22:23", format );
        }

        // ---- HMS Letters Only ----

        [TestMethod]
        public void HM_LettersOnly_Seconds_Test()
        {
            // Setup
            var time = TimeSpan.FromSeconds( 1 );

            var settings = new DateTimeSettings
            {
                DurationFormat = DurationFormat.HourMinute,
                DurationSeparator = DurationSeparator.LettersOnly
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "00H 00M", format );
        }

        [TestMethod]
        public void HM_LettersOnly_MinutesAndSeconds_Test()
        {
            // Setup
            var time = TimeSpan.FromSeconds( 121 );

            var settings = new DateTimeSettings
            {
                DurationFormat = DurationFormat.HourMinute,
                DurationSeparator = DurationSeparator.LettersOnly
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "00H 02M", format );
        }

        [TestMethod]
        public void HM_LettersOnly_HoursAndMinutesAndSeconds_Test()
        {
            // Setup
            var time = TimeSpan.FromSeconds( 7263 );

            var settings = new DateTimeSettings
            {
                DurationFormat = DurationFormat.HourMinute,
                DurationSeparator = DurationSeparator.LettersOnly
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "02H 01M", format );
        }

        [TestMethod]
        public void HM_LettersOnly_HoursAndMinutesAndSecondsGreaterThan10_Test()
        {
            // Setup
            var time = new TimeSpan( 11, 22, 23 );

            var settings = new DateTimeSettings
            {
                DurationFormat = DurationFormat.HourMinute,
                DurationSeparator = DurationSeparator.LettersOnly
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "11H 22M", format );
        }

        // ---- HMS Colon And Letters ----

        [TestMethod]
        public void HM_ColonAndLetters_Seconds_Test()
        {
            // Setup
            var time = TimeSpan.FromSeconds( 1 );

            var settings = new DateTimeSettings
            {
                DurationFormat = DurationFormat.HourMinute,
                DurationSeparator = DurationSeparator.ColonAndLetters
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "00H:00M", format );
        }

        [TestMethod]
        public void HM_ColonAndLetters_MinutesAndSeconds_Test()
        {
            // Setup
            var time = TimeSpan.FromSeconds( 121 );

            var settings = new DateTimeSettings
            {
                DurationFormat = DurationFormat.HourMinute,
                DurationSeparator = DurationSeparator.ColonAndLetters
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "00H:02M", format );
        }

        [TestMethod]
        public void HM_ColonAndLetters_HoursAndMinutesAndSeconds_Test()
        {
            // Setup
            var time = TimeSpan.FromSeconds( 7263 );

            var settings = new DateTimeSettings
            {
                DurationFormat = DurationFormat.HourMinute,
                DurationSeparator = DurationSeparator.ColonAndLetters
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "02H:01M", format );
        }

        [TestMethod]
        public void HM_ColonAndLetters_HoursAndMinutesAndSecondsGreaterThan10_Test()
        {
            // Setup
            var time = new TimeSpan( 11, 22, 23 );

            var settings = new DateTimeSettings
            {
                DurationFormat = DurationFormat.HourMinute,
                DurationSeparator = DurationSeparator.ColonAndLetters
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "11H:22M", format );
        }

        // ---- HMS Colon Only ----

        [TestMethod]
        public void HM_ColonOnly_Seconds_Test()
        {
            // Setup
            var time = TimeSpan.FromSeconds( 1 );

            var settings = new DateTimeSettings
            {
                DurationFormat = DurationFormat.HourMinute,
                DurationSeparator = DurationSeparator.ColonOnly
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "00:00", format );
        }

        [TestMethod]
        public void HM_ColonOnly_MinutesAndSeconds_Test()
        {
            // Setup
            var time = TimeSpan.FromSeconds( 121 );

            var settings = new DateTimeSettings
            {
                DurationFormat = DurationFormat.HourMinute,
                DurationSeparator = DurationSeparator.ColonOnly
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "00:02", format );
        }

        [TestMethod]
        public void HM_ColonOnly_HoursAndMinutesAndSeconds_Test()
        {
            // Setup
            var time = TimeSpan.FromSeconds( 7263 );

            var settings = new DateTimeSettings
            {
                DurationFormat = DurationFormat.HourMinute,
                DurationSeparator = DurationSeparator.ColonOnly
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "02:01", format );
        }

        [TestMethod]
        public void HM_ColonOnly_HoursAndMinutesAndSecondsGreaterThan10_Test()
        {
            // Setup
            var time = new TimeSpan( 11, 22, 23 );

            var settings = new DateTimeSettings
            {
                DurationFormat = DurationFormat.HourMinute,
                DurationSeparator = DurationSeparator.ColonOnly
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "11:22", format );
        }

        [TestMethod]
        public void HM_ColonOnly_MoreThanADay_Test()
        {
            // Setup
            var time = new TimeSpan( 24 + 10, 22, 23 );

            var settings = new DateTimeSettings
            {
                DurationFormat = DurationFormat.HourMinute,
                DurationSeparator = DurationSeparator.ColonOnly
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "34:22", format );
        }

        [TestMethod]
        public void HM_ColonOnly_MoreThanADayRoundUp_Test()
        {
            // Setup
            var time = new TimeSpan( 24 + 10, 59, 59 );

            var settings = new DateTimeSettings
            {
                DurationFormat = DurationFormat.HourMinute,
                DurationSeparator = DurationSeparator.ColonOnly
            };

            // Act
            string format = time.ToSettingsString( settings );

            // Check
            Assert.AreEqual( "34:59", format );
        }
    }
}

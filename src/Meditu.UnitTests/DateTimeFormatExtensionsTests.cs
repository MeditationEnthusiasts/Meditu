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
using Meditu.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Meditu.UnitTests
{
    [TestClass]
    public sealed class DateTimeFormatExtensionsTests
    {
        // ---------------- Tests ----------------

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
    }
}

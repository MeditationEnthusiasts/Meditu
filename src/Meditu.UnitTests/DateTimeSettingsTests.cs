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

using System.Xml.Linq;
using Meditu.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Meditu.UnitTests
{
    [TestClass]
    public sealed class DateTimeSettingsTests
    {
        // ---------------- Tests ----------------

        [TestMethod]
        public void DefaultSettingsTest()
        {
            // Setup

            // These are settings that (probably) most users will use,
            // at least in the US.
            var expected = new DateTimeSettings
            {
                DateFormat = DateFormat.MonthDayYear,
                DateSeparatorFormat = DateSeparatorFormat.Slashes,
                DurationFormat = DurationFormat.HourMinute,
                DurationSeparator = DurationSeparator.ColonOnly,
                MonthFormat = MonthFormat.Number,
                TimeFormat = TimeFormat.Hour12,
                TimeZoneIdentifier = string.Empty
            };

            // Act
            var actual = new DateTimeSettings();

            // Check
            Assert.AreEqual( expected, actual );
        }

        /// <remarks>
        /// *Technically* being a record type should
        /// mean this should just work.  However,
        /// I've never used record types before, so let's see what happens.
        /// </remarks>
        [TestMethod]
        public void EqualsTest()
        {
            var uut1 = new DateTimeSettings();
            var uut2 = new DateTimeSettings();

            this.EqualsSuccessTest( uut1, uut2 );

            // null or invalid types should be false.
            Assert.IsFalse( uut1.Equals( null ) );
            Assert.IsFalse( uut1.Equals( 1 ) );

            // Start changing things.
            uut2 = uut1 with { DateFormat = DateFormat.YearMonthDay };
            EqualsFailureTest( uut1, uut2 );

            uut2 = uut1 with { DateSeparatorFormat = DateSeparatorFormat.Dashes };
            EqualsFailureTest( uut1, uut2 );

            uut2 = uut1 with { DurationFormat = DurationFormat.HourMinuteSecond };
            EqualsFailureTest( uut1, uut2 );

            uut2 = uut1 with { DurationSeparator = DurationSeparator.LettersOnly };
            EqualsFailureTest( uut1, uut2 );

            uut2 = uut1 with { MonthFormat = MonthFormat.FullMonth };
            EqualsFailureTest( uut1, uut2 );

            uut2 = uut1 with { TimeFormat = TimeFormat.Hour24 };
            EqualsFailureTest( uut1, uut2 );

            uut2 = uut1 with { TimeZoneIdentifier = "UTC" };
            EqualsFailureTest( uut1, uut2 );
        }

        [TestMethod]
        public void XmlRoundTripTest()
        {
            // Setup
            var uut = new DateTimeSettings
            {
                DateFormat = DateFormat.YearMonthDay,
                DateSeparatorFormat = DateSeparatorFormat.Dashes,
                DurationFormat = DurationFormat.HourMinuteSecond,
                DurationSeparator = DurationSeparator.LettersOnly,
                MonthFormat = MonthFormat.FullMonth,
                TimeFormat = TimeFormat.Hour24,
                TimeZoneIdentifier = "UTC"
            };

            // Act
            var parent = new XElement( "Parent" );
            uut.ToXml( parent );
            DateTimeSettings actual = DateTimeSettingsExtensions.FromXml( parent );

            // Check
            EqualsSuccessTest( uut, actual );
        }

        /// <summary>
        /// If no settings node is specified,
        /// we'll just return default settings.
        /// </summary>
        [TestMethod]
        public void MissingXmlSettingsTest()
        {
            // Setup
            var expected = new DateTimeSettings();
            var parent = new XElement( "Parent" );

            // Act
            DateTimeSettings actual = DateTimeSettingsExtensions.FromXml( parent );

            // Check
            EqualsSuccessTest( expected, actual );
        }

        /// <summary>
        /// If no settings are specified,
        /// we'll just return default settings.
        /// </summary>
        [TestMethod]
        public void XmlDefaultSettingsTest()
        {
            // Setup
            var expected = new DateTimeSettings();
            var parent = new XElement(
                "Parent",
                new XElement( DateTimeSettingsExtensions.XmlElementName )
            );

            // Act
            DateTimeSettings actual = DateTimeSettingsExtensions.FromXml( parent );

            // Check
            EqualsSuccessTest( expected, actual );
        }

        // ---------------- Test Helpers ----------------

        private void EqualsSuccessTest( DateTimeSettings left, DateTimeSettings right )
        {
            Assert.IsTrue( left.Equals( right ) );
            Assert.IsTrue( right.Equals( left ) );
            Assert.IsTrue( left == right );
            Assert.IsTrue( right == left ); ;
            Assert.IsFalse( left != right );
            Assert.IsFalse( right != left ); ;
        }

        private void EqualsFailureTest( DateTimeSettings left, DateTimeSettings right )
        {
            Assert.IsFalse( left.Equals( right ) );
            Assert.IsFalse( right.Equals( left ) );
            Assert.IsFalse( left == right );
            Assert.IsFalse( right == left ); ;
            Assert.IsTrue( left != right );
            Assert.IsTrue( right != left ); ;
        }
    }
}

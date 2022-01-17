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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Meditu.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Meditu.UnitTests
{
    [TestClass]
    public sealed class LogBookTests
    {
        // ---------------- Fields ----------------

        private const string fileName = "test.db";

        private LogBook uut;

        private static Log log1;
        private static Log log2;
        private static Log log3;

        // ---------------- Setup / Teardown ----------------

        [ClassInitialize]
        public static void FixtureSetup( TestContext context )
        {
            // Log 1:
            // - 1 hour
            // - Earliest
            // - "a technique" for technique.
            // - No Comments
            // - No Location
            log1 = new Log();
            log1.StartTime = new DateTime( 1990, 1, 1, 0, 0, 0, DateTimeKind.Utc );
            log1.EndTime = log1.StartTime + new TimeSpan( 1, 0, 0 );
            log1.Technique = "A technique";
            log1.EditTime = log1.EndTime;
            log1.Latitude = null;
            log1.Longitude = null;
            log1.Validate();

            // Log 2:
            // - 1 hour
            // - Middle Time
            // - "a technique" for technique.
            // - Comments
            // - Location
            log2 = new Log();
            log2.StartTime = new DateTime( 1991, 1, 1, 0, 0, 0, DateTimeKind.Utc );
            log2.EndTime = log2.StartTime + new TimeSpan( 1, 0, 0 );

            // Same technique as long 1, but with different casing and whitespace.
            // Should be same 
            log2.Technique = "   A     Technique   ";
            log2.EditTime = log2.EndTime;
            log2.Latitude = 90; // <- North Pole
            log2.Longitude = 0;
            log2.Comments = "Some Comment" + Environment.NewLine + "Across several lines";
            log2.Validate();

            // Log 3:
            // - 30 minutes
            // - Latest
            // - No Technique
            // - Comments
            // - No Location
            log3 = new Log();
            log3.StartTime = new DateTime( 1992, 1, 1, 2, 0, 0, DateTimeKind.Utc );
            log3.EndTime = log3.StartTime + new TimeSpan( 0, 30, 0 );
            log3.EditTime = log3.EndTime;
            log3.Comments = "Some Comment.";
            log3.Validate();
        }

        [ClassCleanup]
        public static void FixtureTeardown()
        {
        }

        [TestInitialize]
        public void TestSetup()
        {
            this.DeleteFile();

            this.uut = new LogBook( fileName );
        }

        [TestCleanup]
        public void TestTeardown()
        {
            this.uut?.Dispose();
            this.DeleteFile();
        }

        // ---------------- Tests ----------------

        [TestMethod]
        public void AddReadLogTest()
        {
            // Add logs out of order.
            Guid l1Id = this.uut.AddLogToDb( log1 );
            Guid l3Id = this.uut.AddLogToDb( log3 );
            Guid l2Id = this.uut.AddLogToDb( log2 );

            Assert.AreEqual( log1, this.uut[l1Id] );
            Assert.AreEqual( log2, this.uut[l2Id] );
            Assert.AreEqual( log3, this.uut[l3Id] );

            this.CheckLogbook();

            this.uut.ClearCache();
            Assert.AreEqual( 0, this.uut.Count );
            this.CheckShortcutProperties();

            this.uut.Dispose();

            // Close / reopen.  Everything should still match.
            this.uut = new LogBook( fileName );
            Assert.AreEqual( 0, this.uut.Count );
            this.uut.Refresh();

            Assert.AreEqual( log1, this.uut[l1Id] );
            Assert.AreEqual( log2, this.uut[l2Id] );
            Assert.AreEqual( log3, this.uut[l3Id] );

            this.CheckLogbook();

            this.uut.ResetState();
            this.CheckForEmptyProperties();
        }

        [TestMethod]
        public void ToListTest()
        {
            // Setup
            Guid l1Id = this.uut.AddLogToDb( log1 );

            // Act
            IList<Log> list = this.uut.ToList();

            // Check
            Assert.AreEqual( 1, list.Count );
            Assert.AreNotSame( log1, list[0] );
            Assert.AreEqual( log1, list[0] );
        }

        [TestMethod]
        public void XmlRoundTripTest()
        {
            Guid l1Id = this.uut.AddLogToDb( log1 );
            Guid l3Id = this.uut.AddLogToDb( log3 );
            Guid l2Id = this.uut.AddLogToDb( log2 );

            XmlDocument doc = this.uut.ToXml();

            // Delete the logbook, and then import.
            this.uut.Dispose();
            DeleteFile();
            this.uut = new LogBook( fileName );

            IList<Log> logs = LogBookXmlLoader.ParseLogbookXml( doc );

            foreach( Log log in logs )
            {
                Assert.IsTrue( this.uut.ImportLog( log ) );
            }

            CheckLogbook();
        }

        // ---------------- Test Helpers ----------------

        private void CheckLogbook()
        {
            // Should be in order where most current is in slot 0.
            Assert.AreEqual( log1, this.uut[2] );
            Assert.AreEqual( log2, this.uut[1] );
            Assert.AreEqual( log3, this.uut[0] );

            // Check properties.
            this.CheckShortcutProperties();
        }

        private void CheckShortcutProperties()
        {
            Assert.AreEqual( 3, this.uut.TotalSessions );
            Assert.AreEqual( new TimeSpan( 1, 0, 0 ), this.uut.LongestTime );
            Assert.AreEqual( 2, this.uut.StartTimeBucket[0] ); // Started at midnight twice.
            Assert.AreEqual( 1, this.uut.StartTimeBucket[2] ); // Started at 2 once.
            Assert.AreEqual( 2, this.uut.Techniques["a technique"] ); // Twice some form of "a technique" was used.
            Assert.AreEqual( 1, this.uut.Techniques[string.Empty] ); // 1 no technique was used.
            Assert.AreEqual( new TimeSpan( 2, 30, 0 ), this.uut.TotalTime ); // 1.5 hours used.
        }

        private void CheckForEmptyProperties()
        {
            Assert.AreEqual( 0, this.uut.Count );
            Assert.AreEqual( 0, this.uut.TotalSessions );
            Assert.AreEqual( TimeSpan.Zero, this.uut.LongestTime );
            for( int i = 0; i < 23; ++i )
            {
                Assert.AreEqual( 0, this.uut.StartTimeBucket[i] );
            }

            Assert.AreEqual( 0, this.uut.Techniques.Count );
            Assert.AreEqual( TimeSpan.Zero, this.uut.TotalTime );
        }
        private void DeleteFile()
        {
            if( File.Exists( fileName ) )
            {
                File.Delete( fileName );
            }
        }
    }
}

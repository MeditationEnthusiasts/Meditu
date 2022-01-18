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
using Meditu.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Meditu.UnitTests
{
    [TestClass]
    public sealed class LogBookXmlLoaderTests
    {
        // ---------------- Tests ----------------

        /// <summary>
        /// If no version is specified, then we assume
        /// its version 1, which did not have a version.
        /// </summary>
        [TestMethod]
        public void NoVersionTest()
        {
            // Setup
            const string xmlString =
@"<?xml version=""1.0"" encoding=""utf-8""?>
<logbook>
  <log Guid=""00299e35-d8b8-4357-9e84-5d5a9e472e01"" EditTime=""2021-12-30T19:45:46.4050000-05:00"" StartTime=""2021-12-30T19:27:18.7370000-05:00"" EndTime=""2021-12-30T19:45:15.1720000-05:00"" Comments=""Okay session after being back on vacation."" Technique=""Focus on Breath"" Latitude="""" Longitude="""" />
  <log Guid=""73f2a1fa-fb37-4c55-902d-f0a17b58d6ba"" EditTime=""2021-12-24T14:32:17.5680000-05:00"" StartTime=""2021-12-24T14:18:52.8270000-05:00"" EndTime=""2021-12-24T14:31:53.3490000-05:00"" Comments=""Quick pre-travel session. Went okay."" Technique=""Focus on Breath"" Latitude="""" Longitude="""" />
</logbook>
";
            var log0 = new Log
            {
                Guid = Guid.Parse( "00299e35-d8b8-4357-9e84-5d5a9e472e01" ),
                Comments = "Okay session after being back on vacation.",
                EditTime = new DateTime( 2021, 12, 30, 19, 45, 46, 405, DateTimeKind.Local ),
                EndTime = new DateTime( 2021, 12, 30, 19, 45, 15, 172, DateTimeKind.Local ),
                StartTime = new DateTime( 2021, 12, 30, 19, 27, 18, 737, DateTimeKind.Local ),
                Latitude = null,
                Longitude = null,
                Technique = "Focus on Breath"
            };

            var log1 = new Log
            {
                Guid = Guid.Parse( "73f2a1fa-fb37-4c55-902d-f0a17b58d6ba" ),
                Comments = "Quick pre-travel session. Went okay.",
                EditTime = new DateTime( 2021, 12, 24, 14, 32, 17, 568, DateTimeKind.Local ),
                EndTime = new DateTime( 2021, 12, 24, 14, 31, 53, 349, DateTimeKind.Local ),
                StartTime = new DateTime( 2021, 12, 24, 14, 18, 52, 827, DateTimeKind.Local ),
                Latitude = null,
                Longitude = null,
                Technique = "Focus on Breath"
            };

            // Act
            IList<Log> logs = LogBookXmlLoader.ParseLogbookXmlString( xmlString );

            // Check
            Assert.AreEqual( 2, logs.Count );
            Assert.AreEqual( log0, logs[0] );
            Assert.AreEqual( log1, logs[1] );
        }

        [TestMethod]
        public void WithVersion1Test()
        {
            // Setup
            const string xmlString =
@"<?xml version=""1.0"" encoding=""utf-8""?>
<logbook version=""1"">
  <log Guid=""00299e35-d8b8-4357-9e84-5d5a9e472e01"" EditTime=""2021-12-30T19:45:46.4050000-05:00"" StartTime=""2021-12-30T19:27:18.7370000-05:00"" EndTime=""2021-12-30T19:45:15.1720000-05:00"" Comments=""Okay session after being back on vacation."" Technique=""Focus on Breath"" Latitude="""" Longitude="""" />
  <log Guid=""73f2a1fa-fb37-4c55-902d-f0a17b58d6ba"" EditTime=""2021-12-24T14:32:17.5680000-05:00"" StartTime=""2021-12-24T14:18:52.8270000-05:00"" EndTime=""2021-12-24T14:31:53.3490000-05:00"" Comments=""Quick pre-travel session. Went okay."" Technique=""Focus on Breath"" Latitude="""" Longitude="""" />
</logbook>
";
            var log0 = new Log
            {
                Guid = Guid.Parse( "00299e35-d8b8-4357-9e84-5d5a9e472e01" ),
                Comments = "Okay session after being back on vacation.",
                EditTime = new DateTime( 2021, 12, 30, 19, 45, 46, 405, DateTimeKind.Local ),
                EndTime = new DateTime( 2021, 12, 30, 19, 45, 15, 172, DateTimeKind.Local ),
                StartTime = new DateTime( 2021, 12, 30, 19, 27, 18, 737, DateTimeKind.Local ),
                Latitude = null,
                Longitude = null,
                Technique = "Focus on Breath"
            };

            var log1 = new Log
            {
                Guid = Guid.Parse( "73f2a1fa-fb37-4c55-902d-f0a17b58d6ba" ),
                Comments = "Quick pre-travel session. Went okay.",
                EditTime = new DateTime( 2021, 12, 24, 14, 32, 17, 568, DateTimeKind.Local ),
                EndTime = new DateTime( 2021, 12, 24, 14, 31, 53, 349, DateTimeKind.Local ),
                StartTime = new DateTime( 2021, 12, 24, 14, 18, 52, 827, DateTimeKind.Local ),
                Latitude = null,
                Longitude = null,
                Technique = "Focus on Breath"
            };

            // Act
            IList<Log> logs = LogBookXmlLoader.ParseLogbookXmlString( xmlString );

            // Check
            Assert.AreEqual( 2, logs.Count );
            Assert.AreEqual( log0, logs[0] );
            Assert.AreEqual( log1, logs[1] );
        }

        [TestMethod]
        public void Version2Test()
        {
            // Setup
            const string xmlString =
@"<?xml version=""1.0"" encoding=""utf-8""?>
<logbook version=""2"">
    <log>
        <Guid>24fb25ea-f91e-460e-b27d-4e570086fea5</Guid>
        <EditTime>2022-01-17T21:24:17.1780000Z</EditTime>
        <StartTime>2022-01-17T20:23:44.0000000Z</StartTime>
        <EndTime>2022-01-17T20:23:48.0000000Z</EndTime>
        <Comments>UTC Test</Comments>
        <Technique>UTC Test</Technique>
    </log>
    <log>
        <Guid>3f205902-8826-421f-b9ea-a67ebcd6afbb</Guid>
        <EditTime>2021-12-22T00:51:54.1610000Z</EditTime>
        <StartTime>2021-12-22T00:35:16.5480000Z</StartTime>
        <EndTime>2021-12-22T00:51:31.3800000Z</EndTime>
        <Comments>
        Quick session since it's late.
        </Comments>
        <Technique>Focus on Breath</Technique>
        <Latitude>42</Latitude>
        <Longitude>-73</Longitude>
    </log>
</logbook>
";
            var log0 = new Log
            {
                Guid = Guid.Parse( "24fb25ea-f91e-460e-b27d-4e570086fea5" ),
                Comments = "UTC Test",
                EditTime = new DateTime( 2022, 1, 17, 21, 24, 17, 178, DateTimeKind.Utc ),
                StartTime = new DateTime( 2022, 1, 17, 20, 23, 44, 0, DateTimeKind.Utc ),
                EndTime = new DateTime( 2022, 1, 17, 20, 23, 48, 0, DateTimeKind.Utc ),
                Latitude = null,
                Longitude = null,
                Technique = "UTC Test"
            };

            var log1 = new Log
            {
                Guid = Guid.Parse( "3f205902-8826-421f-b9ea-a67ebcd6afbb" ),
                Comments = "Quick session since it's late.",
                EditTime = new DateTime( 2021, 12, 22, 0, 51, 54, 161, DateTimeKind.Utc ),
                StartTime = new DateTime( 2021, 12, 22, 0, 35, 16, 548, DateTimeKind.Utc ),
                EndTime = new DateTime( 2021, 12, 22, 0, 51, 31, 380, DateTimeKind.Utc ),
                Latitude = 42,
                Longitude = -73,
                Technique = "Focus on Breath"
            };

            // Act
            IList<Log> logs = LogBookXmlLoader.ParseLogbookXmlString( xmlString );

            // Check
            Assert.AreEqual( 2, logs.Count );
            Assert.AreEqual( log0, logs[0] );
            Assert.AreEqual( log1, logs[1] );
        }
    }
}

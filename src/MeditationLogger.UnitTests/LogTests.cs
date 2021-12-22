//
// MeditationLogger - A way to track Meditation Sessions.
// Copyright (C) 2017-2021 Seth Hendrick
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
using System.Xml;
using Meditu.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SethCS.Exceptions;

namespace Meditu.UnitTests
{
    [TestClass]
    public class LogTests
    {
        // ---------------- Tests ----------------

        [TestMethod]
        public void CloneTest()
        {
            Log log1 = new Log();
            this.InitLog( log1 );

            Log clone = log1.Clone();
            Assert.AreEqual( log1, clone );
            Assert.AreNotSame( log1, clone );
        }

        [TestMethod]
        public void ValidateTests()
        {
            Log uut = new Log();
            this.InitLog( uut );

            // Should validate out the gate.
            uut.Validate();

            // Comments can not be null.
            Log badLog = uut.Clone();
            Assert.ThrowsException<ArgumentNullException>( () => { badLog.Comments = null; } );

            // Start time can not be after end time.
            badLog = uut.Clone();
            badLog.EndTime = badLog.StartTime - new TimeSpan( 1 );
            Assert.ThrowsException<ValidationException>( () => badLog.Validate() );

            // Id can not be -1.
            badLog = uut.Clone();
            badLog.Id = -1;
            Assert.ThrowsException<ValidationException>( () => badLog.Validate() );

            // Latitude can not be null, when longitude has a value.
            badLog = uut.Clone();
            badLog.Latitude = null;
            badLog.Longitude = 1;
            Assert.ThrowsException<ValidationException>( () => badLog.Validate() );

            // Longitude can not be null, when latitude has a value.
            badLog = uut.Clone();
            badLog.Latitude = 1;
            badLog.Longitude = null;
            Assert.ThrowsException<ValidationException>( () => badLog.Validate() );

            // Technique can not be null.
            badLog = uut.Clone();
            Assert.ThrowsException<ArgumentNullException>( () => { badLog.Technique = null; } );
        }

        [TestMethod]
        public void EqualsTest()
        {
            Log log1 = new Log();
            Log log2 = new Log();

            this.InitLog( log1 );
            this.InitLog( log2 );

            log2.Guid = log1.Guid;

            Assert.IsFalse( log1.Equals( null ) );
            Assert.IsFalse( log1.Equals( 1 ) );

            Assert.AreEqual( log1, log2 );
            Assert.AreEqual( log2, log1 );
            Assert.AreEqual( log1.GetHashCode(), log2.GetHashCode() );

            // Start changing things.
            log2.Comments = log1.Comments + "1";
            Assert.AreNotEqual( log1, log2 );
            Assert.AreNotEqual( log2, log1 );
            Assert.AreNotEqual( log1.GetHashCode(), log2.GetHashCode() );
            log2 = log1.Clone();

            log2.EditTime = log1.EditTime + new TimeSpan( 1 );
            Assert.AreNotEqual( log1, log2 );
            Assert.AreNotEqual( log2, log1 );
            Assert.AreNotEqual( log1.GetHashCode(), log2.GetHashCode() );
            log2 = log1.Clone();

            log2.EndTime = log1.EndTime + new TimeSpan( 1 );
            Assert.AreNotEqual( log1, log2 );
            Assert.AreNotEqual( log2, log1 );
            Assert.AreNotEqual( log1.GetHashCode(), log2.GetHashCode() );
            log2 = log1.Clone();

            // GUID not included in Equals check.
            log2.Guid = Guid.NewGuid();
            Assert.AreEqual( log1, log2 );
            Assert.AreEqual( log2, log1 );
            Assert.AreEqual( log1.GetHashCode(), log2.GetHashCode() );
            log2 = log1.Clone();

            // ID not included in Equals check.
            log2.Id = log1.Id + 1;
            Assert.AreEqual( log1, log2 );
            Assert.AreEqual( log2, log1 );
            Assert.AreEqual( log1.GetHashCode(), log2.GetHashCode() );
            log2 = log1.Clone();

            log2.Latitude = log1.Latitude + 1;
            Assert.AreNotEqual( log1, log2 );
            Assert.AreNotEqual( log2, log1 );
            Assert.AreNotEqual( log1.GetHashCode(), log2.GetHashCode() );
            log2 = log1.Clone();

            log2.Longitude = log1.Longitude + 1;
            Assert.AreNotEqual( log1, log2 );
            Assert.AreNotEqual( log2, log1 );
            Assert.AreNotEqual( log1.GetHashCode(), log2.GetHashCode() );
            log2 = log1.Clone();

            log2.StartTime = log1.StartTime + new TimeSpan( 1 );
            Assert.AreNotEqual( log1, log2 );
            Assert.AreNotEqual( log2, log1 );
            Assert.AreNotEqual( log1.GetHashCode(), log2.GetHashCode() );
            log2 = log1.Clone();

            log2.Technique = log1.Technique + "1";
            Assert.AreNotEqual( log1, log2 );
            Assert.AreNotEqual( log2, log1 );
            Assert.AreNotEqual( log1.GetHashCode(), log2.GetHashCode() );
            log2 = log1.Clone();
        }

        [TestMethod]
        public void TrimTest()
        {
            string whitespace = "       Hello There    " + Environment.NewLine;
            const string noWs = "Hello There";

            Log log = new Log();
            log.Comments = whitespace;
            log.Technique = whitespace;

            Assert.AreEqual( noWs, log.Comments );
            Assert.AreEqual( noWs, log.Technique );
        }

        [TestMethod]
        public void XmlTestWithOutLatitudeLongitude()
        {
            Log log = new Log();
            this.InitLog( log );
            log.Latitude = null;
            log.Longitude = null;

            XmlDocument doc = new XmlDocument();

            XmlNode rootNode = doc.CreateElement( "logbook" );

            log.ToXml( doc, rootNode );

            Log copiedLog = Log.FromXml( rootNode.FirstChild );

            Assert.AreEqual( log, copiedLog );
        }

        [TestMethod]
        public void XmlTestWithLatitudeLongitude()
        {
            Log log = new Log();
            this.InitLog( log );

            XmlDocument doc = new XmlDocument();

            XmlNode rootNode = doc.CreateElement( "logbook" );

            log.ToXml( doc, rootNode );

            Log copiedLog = Log.FromXml( rootNode.FirstChild );

            Assert.AreEqual( log, copiedLog );
        }

        // ---------------- Test Helpers ----------------

        private void InitLog( Log log )
        {
            log.Comments = "A Comment!";
            log.StartTime = new DateTime( 2017, 6, 13 );
            log.EndTime = log.StartTime + new TimeSpan( 1, 0, 0 );
            log.Technique = "A technique!";
            log.EditTime = log.StartTime;
            log.Id = 1;
            log.Guid = Guid.NewGuid();
            log.Latitude = 90;
            log.Longitude = 180;

            log.Validate();
        }
    }
}

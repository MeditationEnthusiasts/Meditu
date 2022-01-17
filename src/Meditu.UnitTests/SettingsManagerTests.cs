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

using Meditu.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Meditu.UnitTests
{
    [TestClass]
    public sealed class SettingsManagerTests
    {
        // ---------------- Tests ----------------

        [TestMethod]
        public void DefaultSettingsTest()
        {
            // Setup
            var expected = new SettingsManager
            {
                DateTimeSettings = new DateTimeSettings()
            };

            // Act
            var actual = new SettingsManager();

            // Check
            Assert.AreEqual( expected.DateTimeSettings, actual.DateTimeSettings );
        }
    }
}

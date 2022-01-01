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

using System.IO;
using Meditu.Api;
using Microsoft.AspNetCore.Mvc;

namespace Meditu.Gui.Controllers
{
    public class ExportController : Controller
    {
        // ---------------- Functions ----------------

        public IActionResult Index()
        {
            ViewData["Title"] = "Export to XML file";
            return View();
        }

        public IActionResult DownloadLogXml()
        {
            var xml = ApiBridge.Instance.LogBook.ToXml();
            using( var writer = new StringWriter() )
            {
                xml.Save( writer );
                return Content( writer.ToString(), "application/xml" );
            }
        }

        public IActionResult DownloadSettingsXml()
        {
            var xml = ApiBridge.Instance.Settings.SaveXmlAsXDoc();
            using( var writer = new StringWriter() )
            {
                xml.Save( writer );
                return Content( writer.ToString(), "application/xml" );
            }
        }
    }
}
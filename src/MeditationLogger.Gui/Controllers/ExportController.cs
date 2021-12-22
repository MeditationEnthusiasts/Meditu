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
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Meditu.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Meditu.Gui.Controllers
{
    public class ExportController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Export to XML file";
            return View();
        }

        public IActionResult DownloadXml()
        {
            XmlDocument xml = ApiBridge.Instance.LogBook.ToXml();
            using( StringWriter writer = new StringWriter() )
            {
                xml.Save( writer );
                return Content( writer.ToString(), "application/xml" );
            }
        }
    }
}
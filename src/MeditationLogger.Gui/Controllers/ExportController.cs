//
// Meditation Logger.
// Copyright (C) 2017  Seth Hendrick.
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using MeditationLogger.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeditationLogger.Gui.Controllers
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
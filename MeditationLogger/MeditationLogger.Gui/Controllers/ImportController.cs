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
using MeditationLogger.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeditationLogger.Gui.Controllers
{
    public class ImportController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Import from XML file";
            return View();
        }

        [HttpPost]
        public IActionResult UploadXml( List<IFormFile> files )
        {
            try
            {
                ViewData["Title"] = "Logs Imported";

                List<Log> logs = new List<Log>();
                foreach( IFormFile file in files )
                {
                    using( Stream stream = file.OpenReadStream() )
                    {
                        logs.AddRange( XmlLoader.ParseLogbookFromStream( stream ) );
                    }
                }

                return View( logs );
            }
            catch( Exception err )
            {
                return BadRequest( err.ToString() );
            }
        }
    }
}
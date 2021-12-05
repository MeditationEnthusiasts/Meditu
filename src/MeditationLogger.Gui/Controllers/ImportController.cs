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

                Dictionary<Log, string> logStatus = new Dictionary<Log, string>();
                foreach( Log log in logs )
                {
                    try
                    {
                        bool success = ApiBridge.Instance.LogBook.ImportLog( log );
                        if ( success )
                        {
                            logStatus.Add( log, "Log imported!" );
                        }
                        else
                        {
                            logStatus.Add( log, "Log already exists, not imported." );
                        }
                    }
                    catch( Exception e )
                    {
                        logStatus.Add( log, "Error, could not import: " + e.Message );
                    }
                }

                return View( logStatus );
            }
            catch( Exception err )
            {
                return BadRequest( err.ToString() );
            }
        }
    }
}
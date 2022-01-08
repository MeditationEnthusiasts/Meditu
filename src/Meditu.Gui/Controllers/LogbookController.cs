//
// Meditu - A way to track Meditation Sessions.
// Copyright (C) 2017-2022 Seth Hendrick.
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
using Meditu.Api;
using Meditu.Gui.Models;
using Microsoft.AspNetCore.Mvc;

namespace Meditu.Gui.Controllers
{
    /// <summary>
    /// The main home page of the app.
    /// </summary>
    public class LogbookController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Logbook";
            return View( ApiBridge.Instance );
        }

        public IActionResult Log( [FromRoute] string id )
        {
            MeditationLoggerApi api = ApiBridge.Instance;

            Log log;
            if( Guid.TryParse( id, out Guid guid ) )
            {
                log = api.LogBook.TryGetLog( guid );
            }
            else
            {
                log = null;
            }

            var model = new LogModel(
                Api: api,
                Log: log
            );

            ViewData["Title"] = log.ToTitleString( api.Settings.DateTimeSettings );
            return View( model );
        }

        public IActionResult MapView()
        {
            ViewData["Title"] = "The places you have meditated!";
            return View( ApiBridge.Instance );
        }

        public IActionResult GraphView()
        {
            ViewData["Title"] = "Logbook - Graph View";
            return View( ApiBridge.Instance );
        }

        public IActionResult CalendarView()
        {
            ViewData["Title"] = "Logbook - Calendar View";
            return View( ApiBridge.Instance );
        }
    }
}
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

using System.Collections.Generic;
using MeditationLogger.Api;
using Microsoft.AspNetCore.Mvc;

namespace MeditationLogger.Gui.Controllers
{
    /// <summary>
    /// The main home page of the app.
    /// </summary>
    public class LogbookController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Logbook";
            return View( ApiBridge.Instance.LogBook );
        }

        public IActionResult MapView()
        {
            ViewData["Title"] = "The places you have meditated!";
            return View( ApiBridge.Instance.LogBook );
        }
    }
}
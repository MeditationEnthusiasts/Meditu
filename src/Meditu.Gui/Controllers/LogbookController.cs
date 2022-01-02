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
            return View( ApiBridge.Instance.LogBook );
        }

        public IActionResult MapView()
        {
            ViewData["Title"] = "The places you have meditated!";
            return View( ApiBridge.Instance.LogBook );
        }

        public IActionResult GraphView()
        {
            ViewData["Title"] = "Logbook - Graph View";
            return View( ApiBridge.Instance.LogBook );
        }

        public IActionResult CalendarView()
        {
            ViewData["Title"] = "Logbook - Calendar View";
            return View( ApiBridge.Instance.LogBook );
        }
    }
}
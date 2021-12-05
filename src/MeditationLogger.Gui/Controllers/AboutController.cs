﻿//
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

using Microsoft.AspNetCore.Mvc;

namespace MeditationLogger.Gui.Controllers
{
    /// <summary>
    /// Controller for the about page.
    /// </summary>
    public class AboutController : Controller
    {
        // ---------------- Functions ----------------

        public IActionResult Index()
        {
            ViewData["Title"] = "About Meditation Logger";
            return View();
        }

        public IActionResult License()
        {
            ViewData["Title"] = "License Information";
            return View();
        }

        public IActionResult Credits()
        {
            ViewData["Title"] = "Credits";
            return View( CreditsInfo.AllCredits );
        }
    }
}
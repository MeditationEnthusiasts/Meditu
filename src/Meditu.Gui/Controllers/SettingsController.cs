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

using System;
using Meditu.Api;
using Meditu.Gui.Models;
using Microsoft.AspNetCore.Mvc;

namespace Meditu.Gui.Controllers
{
    public class SettingsController : Controller
    {
        // ---------------- Functions ----------------

        public IActionResult Index()
        {
            ViewData["Title"] = "Settings";

            var model = new SettingsModel(
                Api: ApiBridge.Instance,
                InfoMessage: TempData["info_message"]?.ToString() ?? string.Empty,
                ErrorMessage: TempData["error_message"]?.ToString() ?? string.Empty
            );
            return View( nameof( Index ), model );
        }

        [HttpPost]
        public IActionResult SetDateTimeSettings( DateTimeSettings settings )
        {
            try
            {
                SettingsManager settingsMgr = ApiBridge.Instance.Settings;
                settingsMgr.DateTimeSettings = settings;

                TempData["info_message"] = "Settings Saved.";
                return RedirectToAction( nameof( Index ) );
            }
            catch( Exception err )
            {
                TempData["error_message"] = err.Message;
                return RedirectToAction( nameof( Index ) );
            }
        }
    }
}

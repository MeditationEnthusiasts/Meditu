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
    public class MeditateController : Controller
    {
        // ---------------- Functions ----------------

        public IActionResult Index()
        {
            ViewData["Title"] = "Meditate!";

            Session session;
            if( ApiBridge.Instance.CurrentState != ApiState.Idle )
            {
                session = ApiBridge.Instance.CurrentSession;
            }
            else
            {
                session = null;
            }

            var model = new MeditateModel(
                Session: session,
                ApiState: ApiBridge.Instance.CurrentState,
                InfoMessage: this.TempData["info_message"]?.ToString() ?? string.Empty,
                ErrorMessage: this.TempData["error_message"]?.ToString() ?? string.Empty
            );

            return View( model );
        }

        [HttpPost]
        public IActionResult Start( int hour, int minute )
        {
            try
            {
                StartSessionParams sessionParams = new StartSessionParams();
                if ( ( hour <= 0 ) && ( minute <= 0 ) )
                {
                    sessionParams.Duration = null;
                }
                else
                {
                    sessionParams.Duration = new TimeSpan( hour, minute, 0 );
                }
                ApiBridge.Instance.Start( sessionParams );

                this.TempData["info_message"] = "Session started!";
            }
            catch( Exception e )
            {
                this.TempData["error_message"] = e.Message;
            }

            return RedirectToAction( nameof( Index ) );
        }

        [HttpPost]
        public IActionResult Stop()
        {
            try
            {
                ApiBridge.Instance.Stop();

                this.TempData["info_message"] = "Session completed!";
            }
            catch( Exception e )
            {
                this.TempData["error_message"] = e.Message;
            }

            return RedirectToAction( nameof( Index ) );
        }

        [HttpPost]
        public IActionResult Save(
            string technique,
            string comments,
            decimal? latitude,
            decimal? longitude
        )
        {
            try
            {
                SaveSessionParams saveSessionParams = new SaveSessionParams
                {
                    Comments = comments,
                    Technique = technique,
                    Latitude = latitude,
                    Longitude = longitude
                };

                ApiBridge.Instance.SaveSession( saveSessionParams );
                this.TempData["info_message"] = "Session saved successfully!";
            }
            catch( Exception e )
            {
                this.TempData["error_message"] = e.Message;
            }

            return RedirectToAction( nameof( Index ) );
        }

        [HttpPost]
        public IActionResult Cancel()
        {
            try
            {
                ApiBridge.Instance.CancelSession();
                this.TempData["info_message"] = "Session cancelled successfully!";
            }
            catch( Exception e )
            {
                this.TempData["error_message"] = e.Message;
            }

            return RedirectToAction( nameof( Index ) );
        }
    }
}
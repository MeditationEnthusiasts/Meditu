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
using System.Collections.Generic;
using Meditu.Api;
using Meditu.Gui.Models;
using Microsoft.AspNetCore.Mvc;

namespace Meditu.Gui.Controllers
{
    public class LogbookController : Controller
    {
        // ---------------- Fields ----------------

        private const string infoMessageKey = "info_message";
        private const string errorMessageKey = "error_message";

        // ---------------- Functions ----------------

        public IActionResult Index()
        {
            ViewData["Title"] = "Logbook";

            MeditationLoggerApi api = ApiBridge.Instance;

            var model = new LogBookModel(
                Api: api,
                InfoMessage: this.TempData[infoMessageKey]?.ToString() ?? string.Empty,
                ErrorMessage: this.TempData[errorMessageKey]?.ToString() ?? string.Empty
            );

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
                Log: log,
                InfoMessage: this.TempData[infoMessageKey]?.ToString() ?? string.Empty,
                ErrorMessage: this.TempData[errorMessageKey]?.ToString() ?? string.Empty
            );

            ViewData["Title"] = log.ToTitleString( api.Settings.DateTimeSettings );
            return View( model );
        }

        public IActionResult Edit( [FromRoute] string id )
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
                Log: log,
                InfoMessage: this.TempData[infoMessageKey]?.ToString() ?? string.Empty,
                ErrorMessage: this.TempData[errorMessageKey]?.ToString() ?? string.Empty
            );

            ViewData["Title"] = "Editing: " + log.ToTitleString( api.Settings.DateTimeSettings );
            return View( model );
        }

        [HttpPost]
        public IActionResult EditLog( [FromRoute] string id, [FromForm] EditLogSettings settings )
        {
            MeditationLoggerApi api = ApiBridge.Instance;

            try
            {
                if( Guid.TryParse( id, out Guid guid ) )
                {
                    api.LogBook.EditLog( guid, settings );
                    this.TempData[infoMessageKey] = "Log has been updated successfully.";
                }
                else
                {
                    this.TempData[errorMessageKey] = $"Invalid ID: {id}.";
                }
            }
            catch( KeyNotFoundException e )
            {
                this.TempData[errorMessageKey] = e.Message;
            }
            catch( Exception e )
            {
                this.TempData[errorMessageKey] = e.Message;
                // Some other exception while editing happened,
                // but we know the key, so return to the edit page.
                return Redirect( $"/LogBook/Edit/{id}" );
            }

            return Redirect( $"/LogBook/Log/{id}" );
        }

        [HttpPost] // <- Delete is not supported by forms.  So use a POST.
        public IActionResult DeleteLog( [FromRoute] string id )
        {
            MeditationLoggerApi api = ApiBridge.Instance;

            Log log = null;
            string errorString = string.Empty;
            try
            {
                if( Guid.TryParse( id, out Guid guid ) )
                {
                    log = api.LogBook.DeleteLog( guid );
                }

                if( log is null )
                {
                    this.TempData[errorMessageKey] = "Could not delete log, ID not found.";
                }
            }
            catch( Exception e )
            {
                this.TempData[errorMessageKey] = e.Message;
            }
            
            // When a log is deleted, we want to go back to the main
            // logbook page, as there is no log to redirect to.
            if( log is not null )
            {
                this.TempData[infoMessageKey] = $"Deleted log: {log.ToTitleString( api.Settings.DateTimeSettings )}";
            }

            return RedirectToAction( nameof( Index ) );
        }
    }
}
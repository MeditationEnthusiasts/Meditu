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
using MeditationLogger.Api;
using Microsoft.AspNetCore.Mvc;

namespace MeditationLogger.Gui.Controllers
{
    public class MeditateModel
    {
        /// <summary>
        /// Error message, if any.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Info message, if any.
        /// </summary>
        public string InfoMessage { get;set; }

        public Session Session{ get; set; }
        
        public ApiState ApiState { get; set; }
    }

    public class MeditateController : Controller
    {
        // ---------------- Functions ----------------

        public IActionResult Index()
        {
            ViewData["Title"] = "Meditate!";

            MeditateModel model = new MeditateModel();
            model.ApiState = ApiBridge.Instance.CurrentState;

            if ( model.ApiState != ApiState.Idle )
            {
                model.Session = ApiBridge.Instance.CurrentSession;
            }

            return View( model );
        }

        [HttpPost]
        public IActionResult Start( int hour, int minute )
        {
            MeditateModel model = new MeditateModel();

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

                model.InfoMessage = "Session started!";
                model.Session = ApiBridge.Instance.CurrentSession;
            }
            catch( Exception e )
            {
                model.Session = ApiBridge.Instance.CurrentSession;
                model.ErrorMessage = e.Message;
            }

            model.ApiState = ApiBridge.Instance.CurrentState;
            return View( "~/Views/Meditate/Index.cshtml", model );
        }

        [HttpPost]
        public IActionResult Stop()
        {
            MeditateModel model = new MeditateModel();

            try
            {
                ApiBridge.Instance.Stop();

                model.InfoMessage = "Session completed!";
                model.Session = ApiBridge.Instance.CurrentSession;
            }
            catch( Exception e )
            {
                model.ErrorMessage = e.Message;
            }

            model.ApiState = ApiBridge.Instance.CurrentState;
            return View( "~/Views/Meditate/Index.cshtml", model );
        }

        [HttpPost]
        public IActionResult Save( string technique, string comments, decimal? latitude, decimal? longitude )
        {
            MeditateModel model = new MeditateModel();
            try
            {
                SaveSessionParams saveSessionParams = new SaveSessionParams();
                saveSessionParams.Comments = comments;
                saveSessionParams.Technique = technique;
                saveSessionParams.Latitude = latitude;
                saveSessionParams.Longitude = longitude;
                
                ApiBridge.Instance.SaveSession( saveSessionParams );
                model.InfoMessage = "Session saved successfully!";
            }
            catch( Exception e )
            {
                model.ErrorMessage = e.Message;
            }

            model.ApiState = ApiBridge.Instance.CurrentState;
            return View( "~/Views/Meditate/Index.cshtml", model );
        }

        [HttpPost]
        public IActionResult Cancel()
        {
            MeditateModel model = new MeditateModel();

            try
            {
                ApiBridge.Instance.CancelSession();
                model.InfoMessage = "Session cancelled successfully!";
            }
            catch( Exception e )
            {
                model.ErrorMessage = e.Message;
            }

            model.ApiState = ApiBridge.Instance.CurrentState;
            return View( "~/Views/Meditate/Index.cshtml", model );
        }
    }
}
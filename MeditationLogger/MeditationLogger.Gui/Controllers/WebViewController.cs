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
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Mvc;
using SethCS.Exceptions;

namespace MeditationLogger.Gui.Controllers
{
    public class WebViewController : Controller
    {
        public IActionResult Index( string url, string original )
        {
            ViewData["original"] = HtmlEncoder.Default.Encode( original );
            ViewData["url"] = HtmlEncoder.Default.Encode( url );
            return View();
        }

        [HttpPost]
        public IActionResult Open( OpenInBrowserModel model )
        {
            string url = null;
            try
            {
                ArgumentChecker.IsNotNull( model, nameof( model ) );
                ArgumentChecker.IsNotNull( model.Url, nameof( model.Url ) );

                url = HtmlEncoder.Default.Encode( model.Url );

                OpenBrowser( url );
                return Ok( url + " should have opened in a browser." );
            }
            catch( Exception err )
            {
                return BadRequest( "Could not start process for url: " + ( url ?? "[null]" ) + Environment.NewLine + err.Message );
            }
        }

        /// <summary>
        /// Taken from here:
        /// https://stackoverflow.com/questions/38576854/how-do-i-launch-the-web-browser-after-starting-my-asp-net-core-application
        /// For multiplatfom targets, must use this function to open a browser.
        /// </summary>
        private static void OpenBrowser( string url )
        {
            if( RuntimeInformation.IsOSPlatform( OSPlatform.Windows ) )
            {
                Process.Start( new ProcessStartInfo( "cmd", $"/c start {url}" ) ); // Works ok on windows
            }
            else if( RuntimeInformation.IsOSPlatform( OSPlatform.Linux ) )
            {
                Process.Start( "xdg-open", url );  // Works ok on linux
            }
            else if( RuntimeInformation.IsOSPlatform( OSPlatform.OSX ) )
            {
                Process.Start( "open", url ); // Not tested
            }
            else
            {
                throw new PlatformNotSupportedException(
                    "Open in browser functionality not supported for this Operating System '" + Environment.OSVersion.ToString() + "'" );
            }
        }

        // ---------------- Helper Classes ----------------

        public class OpenInBrowserModel
        {
            /// <summary>
            /// URL to open in a browser.
            /// </summary>
            public string Url { get; set; }
        }
    }
}
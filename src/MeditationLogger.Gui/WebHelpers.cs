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

using ElectronNET.API;

namespace MeditationLogger.Gui
{
    /// <summary>
    /// This class has helper functions that are useful while rendering HTML.
    /// </summary>
    public static class WebHelpers
    {
        /// <summary>
        /// Use this function when we need to like to an external URL.
        /// If Electron is running, it will return the URL with what is needed
        /// for the web view.
        /// 
        /// If we are in a browser, its just a pass-through for the URL.
        /// </summary>
        /// <param name="url">The external URL</param>
        /// <param name="page">The page where the link was opened.</param>
        public static string ExternalUrl( string url, string page )
        {
            if( HybridSupport.IsElectronActive )
            {
                return string.Format(
                    "/WebView?url={0}&original={1}",
                    url,
                    page
                );
            }
            else
            {
                return url;
            }
        }
    }
}

//
// Meditu - A way to track Meditation Sessions.
// Copyright (C) 2017-2021 Seth Hendrick.
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

using Meditu.Api;

namespace Meditu.Gui.Models
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
        public string InfoMessage { get; set; }

        public Session Session { get; set; }

        public ApiState ApiState { get; set; }
    }

}

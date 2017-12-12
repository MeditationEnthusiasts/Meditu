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

function postRequest(url, formData)
{
    var request = new XMLHttpRequest();
    request.onload = function () {
        var status = request.status;
        var data = request.responseText;
        if (status !== 200) {
            alert(data);
        }
    };

    request.open("POST", url, true);
    // From https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/sending-html-form-data-part-1
    // application/x-www-form-urlencoded is the default content type.
    request.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    request.send(jQuery.param(formData));
}
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meditu.Api
{
    public sealed class DateTimeSettings : IReadOnlyDateTimeSettings
    {
        // ---------------- Constructor ----------------

        public DateTimeSettings()
        {
        }

        // ---------------- Properties ----------------

        public DateFormat DateFormat { get; set; }

        public MonthFormat MonthFormat { get; set; }

        public DateSeparatorFormat DateSeparatorFormat { get; set; }

        public TimeFormat TimeFormat { get; set; }

        public DurationFormat DurationFormat { get; set; }

        public DurationSeparator DurationSeparator { get; set; }
    }
}

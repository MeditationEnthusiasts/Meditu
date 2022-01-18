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

using System.Linq;
using System.Xml.Linq;
using SethCS.Extensions;

namespace Meditu.Api
{
    public sealed record DateTimeSettings
    {
        // ---------------- Constructor ----------------

        public DateTimeSettings()
        {
            this.TimeZoneIdentifier = string.Empty;
        }

        // ---------------- Properties ----------------

        public DateFormat DateFormat { get; init; }

        public MonthFormat MonthFormat { get; init; }

        public DateSeparatorFormat DateSeparatorFormat { get; init; }

        public TimeFormat TimeFormat { get; init; }

        public DurationFormat DurationFormat { get; init; }

        public DurationSeparator DurationSeparator { get; init; }

        /// <remarks>
        /// If this is null or empty string, assume
        /// local timezone.
        /// </remarks>
        public string TimeZoneIdentifier { get; init; }
    }

    internal static class DateTimeSettingsExtensions
    {
        // ---------------- Fields ----------------

        internal const string XmlElementName = "datetime";

        internal const int XmlVersion = 1;

        // ---------------- Functions ----------------

        public static void ToXml( this DateTimeSettings settings, XElement parentNode )
        {
            var element = new XElement(
                XmlElementName,
                new XAttribute( "version", XmlVersion ),
                // We'll save all formats as integers, so if we rename them in code,
                // we're not going to blow them up.
                new XElement( "dateformat", (int)settings.DateFormat ),
                new XElement( "monthformat", (int)settings.MonthFormat ),
                new XElement( "dateseparator", (int)settings.DateSeparatorFormat ),
                new XElement( "timeformat", (int)settings.TimeFormat ),
                new XElement( "durationformat", (int)settings.DurationFormat ),
                new XElement( "durationseparator", (int)settings.DurationSeparator ),
                new XElement( "timezone", settings.TimeZoneIdentifier ?? string.Empty )
            );

            parentNode.Add( element );
        }

        public static DateTimeSettings FromXml( XElement parentNode )
        {
            XElement? settingsNode = parentNode.Elements().FirstOrDefault(
                e => XmlElementName.EqualsIgnoreCase( e.Name.LocalName )
            );

            if( settingsNode == default )
            {
                return new DateTimeSettings();
            }

            // Default to the latest version.
            int version = XmlVersion;
            foreach( XAttribute attr in settingsNode.Attributes() )
            {
                string name = attr.Name.LocalName;
                if( string.IsNullOrEmpty( name ) )
                {
                    continue;
                }
                else if( "version".EqualsIgnoreCase( name ) )
                {
                    version = int.Parse( attr.Value );
                }
            }

            // Any setting that is somehow not specified will be set to defaulted settings.
            var settings = new DateTimeSettings();

            foreach( XElement child in settingsNode.Elements() )
            {
                string name = child.Name.LocalName;
                if( string.IsNullOrWhiteSpace( name ) )
                {
                    continue;
                }
                else if( "dateformat".EqualsIgnoreCase( name ) )
                {
                    settings = settings with { DateFormat = (DateFormat)int.Parse( child.Value ) };
                }
                else if( "monthformat".EqualsIgnoreCase( name ) )
                {
                    settings = settings with { MonthFormat = (MonthFormat)int.Parse( child.Value ) };
                }
                else if( "dateseparator".EqualsIgnoreCase( name ) )
                {
                    settings = settings with { DateSeparatorFormat = (DateSeparatorFormat)int.Parse( child.Value ) };
                }
                else if( "timeformat".EqualsIgnoreCase( name ) )
                {
                    settings = settings with { TimeFormat = (TimeFormat)int.Parse( child.Value ) };
                }
                else if( "durationformat".EqualsIgnoreCase( name ) )
                {
                    settings = settings with { DurationFormat = (DurationFormat)int.Parse( child.Value ) };
                }
                else if( "durationseparator".EqualsIgnoreCase( name ) )
                {
                    settings = settings with { DurationSeparator = (DurationSeparator)int.Parse( child.Value ) };
                }
                else if( "timezone".EqualsIgnoreCase( name ) )
                {
                    settings = settings with { TimeZoneIdentifier = child.Value };
                }
            }

            return settings;
        }
    }
}

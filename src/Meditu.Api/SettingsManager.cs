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
using System.Xml.Linq;
using SethCS.Extensions;

namespace Meditu.Api
{
    public interface IReadOnlySettingsManager
    {
        DateTimeSettings DateTimeSettings { get; }
    }

    public sealed class SettingsManager : IReadOnlySettingsManager
    {
        // ---------------- Events ----------------

        public event Action? OnUpdated;

        // ---------------- Fields ----------------

        private DateTimeSettings dateTimeSettings;

        // ---------------- Constructor ----------------

        public SettingsManager()
        {
            this.dateTimeSettings = new DateTimeSettings();
        }

        // ---------------- Properties ----------------

        public DateTimeSettings DateTimeSettings
        {
            get => this.dateTimeSettings;
            set
            {
                ArgumentNullException.ThrowIfNull( value, nameof( DateTimeSettings ) );
                if( value.Equals( this.dateTimeSettings ) )
                {
                    return;
                }
                this.dateTimeSettings = value;
                OnUpdated?.Invoke();
            }
        }
    }

    public static class SettingsManagerExtensions
    {
        // ---------------- Fields ----------------

        internal const string XmlElementName = "MedituConfig";

        private const int XmlVersion = 1;

        // ---------------- Functions ----------------

        public static XDocument SaveXmlAsXDoc( this SettingsManager settings )
        {
            var dec = new XDeclaration( "1.0", "utf-8", "yes" );
            var doc = new XDocument( dec );

            var root = new XElement( XmlElementName );
            doc.Add( root );
            root.Add(
                new XAttribute( "version", XmlVersion )
            );

            settings.DateTimeSettings.ToXml( root );

            return doc;
        }

        public static void SaveXmlToFile( this SettingsManager settings, string path )
        {
            XDocument doc = settings.SaveXmlAsXDoc();
            doc.Save( path );
        }

        public static void LoadXmlFromFile( this SettingsManager settings, string path )
        {
            LoadXmlFromXDoc( settings, XDocument.Load( path ) );
        }

        public static void LoadXmlFromString( this SettingsManager settings, string xmlString )
        {
            LoadXmlFromXDoc( settings, XDocument.Parse( xmlString ) );
        }

        public static void LoadXmlFromXDoc( this SettingsManager settings, XDocument doc )
        {
            ArgumentNullException.ThrowIfNull( settings, nameof( settings ) );
            ArgumentNullException.ThrowIfNull( doc, nameof( doc ) );

            var root = doc.Root;
            if( root == null )
            {
                throw new InvalidOperationException(
                    "Unable to parse settings file.  Root is null"
                );
            }
            else if( XmlElementName.EqualsIgnoreCase( root.Name.LocalName ) == false )
            {
                throw new ArgumentException(
                    $"Invalid XML file, root node is '{root.Name.LocalName}', but expected '{XmlElementName}'."
                );
            }

            int version = XmlVersion;
            foreach( XAttribute attr in root.Attributes() )
            {
                string name = attr.Name.LocalName;
                if( string.IsNullOrWhiteSpace( name ) )
                {
                    continue;
                }
                else if( "version".EqualsIgnoreCase( name ) )
                {
                    version = int.Parse( attr.Value );
                }
            }

            settings.DateTimeSettings = DateTimeSettingsExtensions.FromXml( root );
        }
    }
}

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
using System.IO;
using System.Xml;
using SethCS.Extensions;

namespace Meditu.Api
{
    public static class LogBookXmlLoader
    {
        public static IList<Log> ParseLogbookFromFile( string filePath )
        {
            XmlDocument doc = new XmlDocument();
            doc.Load( filePath );

            return ParseLogbookXml( doc );
        }

        public static IList<Log> ParseLogbookFromStream( Stream stream )
        {
            XmlDocument doc = new XmlDocument();
            doc.Load( stream );

            return ParseLogbookXml( doc );
        }

        public static IList<Log> ParseLogbookXmlString( string xml )
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml( xml );

            return ParseLogbookXml( doc );
        }

        public static IList<Log> ParseLogbookXml( XmlDocument doc )
        {
            if( doc.DocumentElement is null )
            {
                throw new InvalidOperationException(
                    "Somehow, our root node is null"
                );
            }

            XmlNode rootNode = doc.DocumentElement;

            if( LogBookExtensions.XmlElementName.EqualsIgnoreCase( rootNode?.Name ) == false )
            {
                throw new ArgumentException( "Rootnode must be named " + LogBookExtensions.XmlElementName );
            }

            // The first XML version did not have a version attribute.
            // Therefore, if there is no version attribute, assume its version 1.
            int version = 1;
            if( rootNode?.Attributes is not null )
            {
                foreach( XmlAttribute attribute in rootNode.Attributes )
                {
                    if( LogBookExtensions.VersionAttributeName.EqualsIgnoreCase( attribute.Name ) )
                    {
                        version = int.Parse( attribute.Value );
                    }
                }
            }

            List<Log> logs = new List<Log>();

            if( rootNode?.ChildNodes is not null )
            {
                foreach( XmlNode logNode in rootNode.ChildNodes )
                {
                    var log = new Log();
                    log.FromXml( logNode, version );
                    logs.Add( log );
                }
            }

            return logs;
        }
    }
}

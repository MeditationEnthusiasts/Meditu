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

namespace Meditu.Api
{
    public static class XmlLoader
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
            XmlNode rootNode = doc.DocumentElement;

            if( rootNode.Name.ToLower() != LogBook.XmlElementName )
            {
                throw new ArgumentException( "Rootnode must be named " + LogBook.XmlElementName );
            }

            List<Log> logs = new List<Log>();

            foreach( XmlNode logNode in rootNode.ChildNodes )
            {
                logs.Add( Log.FromXml( logNode ) );
            }

            return logs;
        }
    }
}

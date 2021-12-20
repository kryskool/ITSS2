// Implementation of the WWKS2 protocol.
// Copyright (C) 2020  Thomas Reth

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.

// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace Reth.Itss2.Serialization.Formats.Xml
{
    public class XmlSerializationSettings
    {
        private XmlSerializationSettings()
        {
        }

        private const StringComparison DefaultStringComparison = StringComparison.OrdinalIgnoreCase;

        private static readonly Lazy<XmlReaderSettings> readerSettings = new Lazy<XmlReaderSettings>(   () =>
                                                                                                        {
                                                                                                            XmlReaderSettings result = new XmlReaderSettings();
    
                                                                                                            result.Async = true;
                                                                                                            result.IgnoreWhitespace = false;
                                                                                                            result.IgnoreComments = true;
                                                                                                            
                                                                                                            return result;
                                                                                                        },
                                                                                                        LazyThreadSafetyMode.PublicationOnly    );

        private static readonly Lazy<XmlWriterSettings> writerSettings = new Lazy<XmlWriterSettings>(   () =>
                                                                                                        {
                                                                                                            XmlWriterSettings result = new XmlWriterSettings();
    
                                                                                                            result.Encoding = new UTF8Encoding( false );
                                                                                                            result.OmitXmlDeclaration = true;
                                                                                                            result.CheckCharacters = false;
                                                                                                            result.Async = true;

                                                                                                            return result;
                                                                                                        },
                                                                                                        LazyThreadSafetyMode.PublicationOnly    );

        public static readonly XmlRootAttribute EnvelopeRoot = new XmlRootAttribute( "WWKS" );
        public static readonly Encoding Encoding = Encoding.UTF8;
        
        public static StringComparison StringComparison => XmlSerializationSettings.DefaultStringComparison;
        
        internal static XmlReaderSettings ReaderSettings => XmlSerializationSettings.readerSettings.Value;
        internal static XmlWriterSettings WriterSettings => XmlSerializationSettings.writerSettings.Value;
    }
}

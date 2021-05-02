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

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Serialization.Formats.Xml;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Formats.Xml.Messages
{
    [TestClass]
    public abstract class XmlMessageTests
    {
        protected bool SerializeRequest( ( String Xml, IMessageEnvelope Object ) request )
        {
            XmlMessageParser parser = new XmlMessageParser( typeof( XmlSerializationProvider ) );

            String actualXml = parser.SerializeMessageEnvelope( request.Object );

            return XmlComparer.AreEqual( request.Xml, actualXml );
        }

        protected bool DeserializeRequest( ( String Xml, IMessageEnvelope Object ) request )
        {
            XmlMessageParser parser = new XmlMessageParser( typeof( XmlSerializationProvider ) );

            IMessageEnvelope actualObject = parser.DeserializeMessageEnvelope( request.Xml );

            return request.Object.Equals( actualObject );
        }
    }
}

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
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.KeepAlive;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Formats.Xml.Messages.KeepAliveDialog
{
    [TestClass]
    public class KeepAliveRequestEnvelopeDataContractTests:XmlMessageTests
    {
        public static ( String Xml, IMessageEnvelope Object ) Request
        {
            get
            {
                return (    $@" <WWKS Version=""2.0"" TimeStamp=""{ XmlMessageTests.Timestamp }"">
                                    <KeepAliveRequest Id=""{ XmlMessageTests.MessageId }"" Source=""{ XmlMessageTests.Source }"" Destination=""{ XmlMessageTests.Destination }"" />
                                </WWKS>",
                            new MessageEnvelope(    new KeepAliveRequest(   XmlMessageTests.MessageId,
                                                                            XmlMessageTests.Source,
                                                                            XmlMessageTests.Destination   ),
                                                    XmlMessageTests.Timestamp    ) );
            }
        }

        public static ( String Xml, IMessageEnvelope Object ) Response
        {
            get
            {
                return (    $@" <WWKS Version=""2.0"" TimeStamp=""{ XmlMessageTests.Timestamp }"">
                                    <KeepAliveResponse Id=""{ XmlMessageTests.MessageId }"" Source=""{ XmlMessageTests.Source }"" Destination=""{ XmlMessageTests.Destination }"" />
                                </WWKS>",
                            new MessageEnvelope(    new KeepAliveResponse(  XmlMessageTests.MessageId,
                                                                            XmlMessageTests.Source,
                                                                            XmlMessageTests.Destination   ),
                                                    XmlMessageTests.Timestamp    ) );
            }
        }

        [TestMethod]
        public void Serialize_Request_Succeeds()
        {
            bool result = base.SerializeMessage( KeepAliveRequestEnvelopeDataContractTests.Request );

            Assert.IsTrue( result );
        }

        [TestMethod]
        public void Serialize_Response_Succeeds()
        {
            bool result = base.SerializeMessage( KeepAliveRequestEnvelopeDataContractTests.Response );

            Assert.IsTrue( result );
        }

        [TestMethod]
        public void Deserialize_Request_Succeeds()
        {
            bool result = base.DeserializeMessage( KeepAliveRequestEnvelopeDataContractTests.Request );

            Assert.IsTrue( result );
        }

        [TestMethod]
        public void Deserialize_Response_Succeeds()
        {
            bool result = base.DeserializeMessage( KeepAliveRequestEnvelopeDataContractTests.Response );

            Assert.IsTrue( result );
        }
    }
}

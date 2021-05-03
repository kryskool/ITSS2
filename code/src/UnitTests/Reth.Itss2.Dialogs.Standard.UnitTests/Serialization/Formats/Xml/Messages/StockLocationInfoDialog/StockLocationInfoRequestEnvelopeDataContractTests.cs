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
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockLocationInfo;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Formats.Xml.Messages.StockLocationInfoDialog
{
    [TestClass]
    public class StockLocationInfoRequestEnvelopeDataContractTests:XmlMessageTests
    {
        public static ( String Xml, IMessageEnvelope Object ) Request
        {
            get
            {
                return (    $@" <WWKS Version=""2.0"" TimeStamp=""{ XmlMessageTests.Timestamp }"">
                                    <StockLocationInfoRequest Id=""{ XmlMessageTests.MessageId }"" Source=""{ XmlMessageTests.Source }"" Destination=""{ XmlMessageTests.Destination }"" />
                                </WWKS>",
                            new MessageEnvelope(    new StockLocationInfoRequest(   XmlMessageTests.MessageId,
                                                                                    XmlMessageTests.Source,
                                                                                    XmlMessageTests.Destination   ),
                                                    XmlMessageTests.Timestamp    ) );
            }
        }   

        public static ( String Xml, IMessageEnvelope Object ) Response
        {
            get
            {
                StockLocationId defaultLocationId = new StockLocationId( "4711" );
                StockLocationId specialLocationId = new StockLocationId( "4712" );

                String defaultLocationDescription = "Default";
                String specialLocationDescription = "Special";

                return (    $@" <WWKS Version=""2.0"" TimeStamp=""{ XmlMessageTests.Timestamp }"">
                                    <StockLocationInfoResponse Id=""{ XmlMessageTests.MessageId }"" Source=""{ XmlMessageTests.Source }"" Destination=""{ XmlMessageTests.Destination }"">
                                        <StockLocation Id=""{ defaultLocationId }"" Description=""{ defaultLocationDescription }"" />
                                        <StockLocation Id=""{ specialLocationId }"" Description=""{ specialLocationDescription }"" />
                                    </StockLocationInfoResponse>
                                </WWKS>",
                            new MessageEnvelope(    new StockLocationInfoResponse(  XmlMessageTests.MessageId,
                                                                                    XmlMessageTests.Source,
                                                                                    XmlMessageTests.Destination,
                                                                                    new StockLocation[]
                                                                                    {
                                                                                        new StockLocation( defaultLocationId, defaultLocationDescription ),
                                                                                        new StockLocation( specialLocationId, specialLocationDescription )
                                                                                    } ),
                                                    XmlMessageTests.Timestamp    ) );
            }
        }

        [TestMethod]
        public void Serialize_Request_Succeeds()
        {
            bool result = base.SerializeMessage( StockLocationInfoRequestEnvelopeDataContractTests.Request );

            Assert.IsTrue( result );
        }

        [TestMethod]
        public void Serialize_Response_Succeeds()
        {
            bool result = base.SerializeMessage( StockLocationInfoRequestEnvelopeDataContractTests.Response );

            Assert.IsTrue( result );
        }

        [TestMethod]
        public void Deserialize_Request_Succeeds()
        {
            bool result = base.DeserializeMessage( StockLocationInfoRequestEnvelopeDataContractTests.Request );

            Assert.IsTrue( result );
        }

        [TestMethod]
        public void Deserialize_Response_Succeeds()
        {
            bool result = base.DeserializeMessage( StockLocationInfoRequestEnvelopeDataContractTests.Response );

            Assert.IsTrue( result );
        }
    }
}

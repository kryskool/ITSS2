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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockDeliveryInfo;
using Reth.Itss2.Messaging;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Formats.Json.Messages.StockDeliveryInfoDialog
{
    [TestClass]
    public class StockDeliveryInfoRequestEnvelopeDataContractTests:JsonMessageTests
    {
        public static ( String Json, IMessageEnvelope Object ) Request
        {
            get
            {
                String deliveryNumber = new( "K90" );

                bool includeTaskDetails = true;

                return (    $@" {{
                                    ""StockDeliveryInfoRequest"":
                                    {{
                                        ""Id"": ""{ JsonMessageTests.MessageId }"",
                                        ""Source"": ""{ JsonMessageTests.Source }"",
                                        ""Destination"": ""{ JsonMessageTests.Destination }"",
                                        ""IncludeTaskDetails"": ""{ includeTaskDetails }"",
                                        ""Task"":
                                        {{
                                            ""Id"": ""{ deliveryNumber }""
                                        }}
                                    }},
                                    ""Version"": ""2.0"",
                                    ""TimeStamp"": ""{ JsonMessageTests.Timestamp }""
                                }}",
                            new MessageEnvelope(    new StockDeliveryInfoRequest(   JsonMessageTests.MessageId,
                                                                                    JsonMessageTests.Source,
                                                                                    JsonMessageTests.Destination,
                                                                                    new StockDeliveryInfoRequestTask( deliveryNumber ),
                                                                                    includeTaskDetails  ),
                                                    JsonMessageTests.Timestamp    )  );
            }
        }   

        [TestMethod]
        public void Serialize_Request_Succeeds()
        {
            bool result = base.SerializeMessage( StockDeliveryInfoRequestEnvelopeDataContractTests.Request );

            Assert.IsTrue( result );
        }

        [TestMethod]
        public void Deserialize_Request_Succeeds()
        {
            bool result = base.DeserializeMessage( StockDeliveryInfoRequestEnvelopeDataContractTests.Request );

            Assert.IsTrue( result );
        }
    }
}

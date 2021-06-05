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
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockDeliverySet;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Formats.Json.Messages.StockDeliverySetDialog
{
    [TestClass]
    public class StockDeliverySetResponseEnvelopeDataContractTests:JsonMessageTests
    {
        public static ( String Json, IMessageEnvelope Object ) Response
        {
            get
            {
                StockDeliverySetResult result = new( StockDeliverySetResultValue.Accepted, "All articles accepted." );

                return (    $@" {{
                                    ""StockDeliverySetResponse"":
                                    {{
                                        ""Id"": ""{ JsonMessageTests.MessageId }"",
                                        ""Source"": ""{ JsonMessageTests.Source }"",
                                        ""Destination"": ""{ JsonMessageTests.Destination }"",
                                        ""SetResult"":
                                        {{
                                            ""Value"": ""{ result.Value }"",
                                            ""Text"": ""{ result.Text }""
                                        }}
                                    }},
                                    ""Version"": ""2.0"",
                                    ""TimeStamp"": ""{ JsonMessageTests.Timestamp }""
                                }}",
                            new MessageEnvelope(    new StockDeliverySetResponse(   JsonMessageTests.MessageId,
                                                                                    JsonMessageTests.Source,
                                                                                    JsonMessageTests.Destination,
                                                                                    result  ),
                                                    JsonMessageTests.Timestamp    ) );
            }
        }

        [TestMethod]
        public void Serialize_Response_Succeeds()
        {
            bool result = base.SerializeMessage( StockDeliverySetResponseEnvelopeDataContractTests.Response );

            Assert.IsTrue( result );
        }

        [TestMethod]
        public void Deserialize_Response_Succeeds()
        {
            bool result = base.DeserializeMessage( StockDeliverySetResponseEnvelopeDataContractTests.Response );

            Assert.IsTrue( result );
        }
    }
}

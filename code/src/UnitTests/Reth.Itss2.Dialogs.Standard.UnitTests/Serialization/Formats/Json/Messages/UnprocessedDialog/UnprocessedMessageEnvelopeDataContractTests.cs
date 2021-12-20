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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Unprocessed;
using Reth.Itss2.Messaging;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Formats.Json.Messages.UnprocessedDialog
{
    [TestClass]
    public class UnprocessedMessageEnvelopeDataContractTests:JsonMessageTests
    {
        public static ( String Json, IMessageEnvelope Object ) Message
        {
            get
            {
                UnprocessedReason reason = UnprocessedReason.NotSupported;
                String message = "<data>...more data...</data>";
                String text = "Something went wrong.";

                return (    $@" {{
                                    ""UnprocessedMessage"":
                                    {{
                                        ""Id"": ""{ JsonMessageTests.MessageId }"",
                                        ""Source"": ""{ JsonMessageTests.Source }"",
                                        ""Destination"": ""{ JsonMessageTests.Destination }"",
                                        ""Reason"": ""{ reason }"",
                                        ""Text"": ""{ text }"",
                                        ""Message"": ""{ message }""
                                    }},
                                    ""Version"": ""2.0"",
                                    ""TimeStamp"": ""{ JsonMessageTests.Timestamp }""
                                }}",
                            new MessageEnvelope(    new UnprocessedMessage( JsonMessageTests.MessageId,
                                                                            JsonMessageTests.Source,
                                                                            JsonMessageTests.Destination,
                                                                            message,
                                                                            text,
                                                                            reason  ),
                                                    JsonMessageTests.Timestamp    ) );
            }
        }   

        [TestMethod]
        public void Serialize_Message_Succeeds()
        {
            bool result = base.SerializeMessage( UnprocessedMessageEnvelopeDataContractTests.Message );

            Assert.IsTrue( result );
        }

        [TestMethod]
        public void Deserialize_Message_Succeeds()
        {
            bool result = base.DeserializeMessage( UnprocessedMessageEnvelopeDataContractTests.Message );

            Assert.IsTrue( result );
        }
    }
}

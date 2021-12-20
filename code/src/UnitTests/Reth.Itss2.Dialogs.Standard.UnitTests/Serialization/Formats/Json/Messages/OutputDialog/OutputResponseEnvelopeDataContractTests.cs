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
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Output;
using Reth.Itss2.Messaging;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Formats.Json.Messages.OutputDialog
{
    [TestClass]
    public class OutputResponseEnvelopeDataContractTests:JsonMessageTests
    {
        public static ( String Json, IMessageEnvelope Object ) Response
        {
            get
            {
                (   int OutputDestination,
                    int OutputPoint,
                    OutputPriority Priority,
                    OutputResponseStatus Status ) details = ( 14, 0, OutputPriority.Normal, OutputResponseStatus.Queued );

                (   int Quantity,
                    int SubItemQuantity,
                    ArticleId ArticleId,
                    PackId PackId,
                    PackDate MinimumExpiryDate,
                    String BatchNumber,
                    String ExternalId,
                    String SerialNumber,
                    String MachineLocation,
                    StockLocationId StockLocationId,
                    bool SingleBatchNumber ) criteria = (   23,
                                                            45,
                                                            new ArticleId( "4711" ),
                                                            new PackId( "1998" ),
                                                            new PackDate( 1999, 7, 23 ),
                                                            "BATCH-1",
                                                            "EXT-3",
                                                            "SER-5",
                                                            "default",
                                                            new StockLocationId( "main" ),
                                                            true    );

                ( String TemplateId, String Content ) label = ( "TEMPLATE-DEFAULT", "<LABEL>Print me.</LABEL>" );

                String boxNumber = "1023";

                return (    $@" {{
                                    ""OutputResponse"":
                                    {{
                                        ""Id"": ""{ JsonMessageTests.MessageId }"",
                                        ""Source"": ""{ JsonMessageTests.Source }"",
                                        ""Destination"": ""{ JsonMessageTests.Destination }"",
                                        ""BoxNumber"": ""{ boxNumber }"",
                                        ""Details"":
                                        {{
                                            ""Priority"": ""{ details.Priority }"",
                                            ""OutputDestination"": ""{ details.OutputDestination }"",
                                            ""OutputPoint"": ""{ details.OutputPoint }"",
                                            ""Status"": ""{ details.Status }""
                                        }},
                                        ""Criteria"":
                                        [
                                            {{
                                                ""ArticleId"": ""{ criteria.ArticleId }"",
                                                ""PackId"": ""{ criteria.PackId }"",
                                                ""MinimumExpiryDate"": ""{ criteria.MinimumExpiryDate }"",
                                                ""BatchNumber"": ""{ criteria.BatchNumber }"",
                                                ""ExternalId"": ""{ criteria.ExternalId }"",
                                                ""SerialNumber"": ""{ criteria.SerialNumber }"",
                                                ""MachineLocation"": ""{ criteria.MachineLocation }"",
                                                ""StockLocationId"": ""{ criteria.StockLocationId }"",
                                                ""Quantity"": ""{ criteria.Quantity }"",
                                                ""SubItemQuantity"": ""{ criteria.SubItemQuantity }"",
                                                ""SingleBatchNumber"": ""{ criteria.SingleBatchNumber }"",
                                                ""Label"":
                                                [
                                                    {{
                                                        ""TemplateId"": ""{ label.TemplateId }"",
                                                        ""Content"": ""{ label.Content }""
                                                    }}
                                                ]
                                            }}
                                        ]
                                    }},
                                    ""Version"": ""2.0"",
                                    ""TimeStamp"": ""{ JsonMessageTests.Timestamp }""
                                }}",
                            new MessageEnvelope(    new OutputResponse( JsonMessageTests.MessageId,
                                                                        JsonMessageTests.Source,
                                                                        JsonMessageTests.Destination,
                                                                        new OutputResponseDetails(  details.OutputDestination,
                                                                                                    details.Status,
                                                                                                    details.Priority,
                                                                                                    details.OutputPoint ),
                                                                        new OutputCriteria[]
                                                                        {
                                                                            new OutputCriteria( criteria.Quantity,
                                                                                                criteria.SubItemQuantity,
                                                                                                criteria.ArticleId,
                                                                                                criteria.PackId,
                                                                                                criteria.MinimumExpiryDate,
                                                                                                criteria.BatchNumber,
                                                                                                criteria.ExternalId,
                                                                                                criteria.SerialNumber,
                                                                                                criteria.MachineLocation,
                                                                                                criteria.StockLocationId,
                                                                                                criteria.SingleBatchNumber,
                                                                                                new OutputLabel[]
                                                                                                {
                                                                                                    new( label.TemplateId, label.Content )
                                                                                                }   )
                                                                        },
                                                                        boxNumber   ),
                                                    JsonMessageTests.Timestamp    ) );
            }
        }   

        [TestMethod]
        public void Serialize_Response_Succeeds()
        {
            bool result = base.SerializeMessage( OutputResponseEnvelopeDataContractTests.Response );

            Assert.IsTrue( result );
        }

        [TestMethod]
        public void Deserialize_Response_Succeeds()
        {
            bool result = base.DeserializeMessage( OutputResponseEnvelopeDataContractTests.Response );

            Assert.IsTrue( result );
        }
    }
}

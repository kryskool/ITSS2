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
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Input;
using Reth.Itss2.Messaging;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Formats.Json.Messages.InputDialog
{
    [TestClass]
    public class InputResponseEnvelopeDataContractTests:JsonMessageTests
    {
        public static ( String Json, IMessageEnvelope Object ) Response
        {
            get
            {
                (   ArticleId Id,
                    String Name,
                    String DosageForm,
                    String PackagingUnit,
                    bool RequiresFridge,
                    int MaxSubItemQuantity,
                    PackDate SerialNumberSinceExpiryDate ) article = ( new ArticleId( "1985" ), "Whole Device", "Flux Capacitor", "Box", false, 100, new PackDate( 1999, 7, 23 ) );

                (   int Index,
                    String DeliveryNumber,
                    String BatchNumber,
                    String ExternalId,
                    String SerialNumber,
                    PackDate ExpiryDate,
                    int SubItemQuantity,
                    int Depth,
                    int Width,
                    int Height,
                    int Weight,
                    StockLocationId StockLocationId ) pack = (  42,
                                                                "4711",
                                                                "0815",
                                                                "EXT-1",
                                                                "SER-3",
                                                                new PackDate( 2021, 5, 5 ),
                                                                30,
                                                                100,
                                                                50,
                                                                24,
                                                                153,
                                                                new StockLocationId( "default" )    );

                InputResponsePackHandling handling = new( InputResponsePackHandlingInput.Allowed, "OK." );

                ProductCode productCode = new( new ProductCodeId( "5783" ) );

                bool isNewDelivery = false;

                return (    $@" {{
                                    ""InputResponse"":
                                    {{
                                        ""Id"": ""{ JsonMessageTests.MessageId }"",
                                        ""Source"": ""{ JsonMessageTests.Source }"",
                                        ""Destination"": ""{ JsonMessageTests.Destination }"",
                                        ""IsNewDelivery"": ""{ isNewDelivery }""
                                        ""Article"":
                                        [
                                            {{
                                                ""Id"": ""{ article.Id }"",
                                                ""Name"": ""{ article.Name }"",
                                                ""DosageForm"": ""{ article.DosageForm }"",
                                                ""PackagingUnit"": ""{ article.PackagingUnit }"",
                                                ""MaxSubItemQuantity"": ""{ article.MaxSubItemQuantity }"",
                                                ""SerialNumberSinceExpiryDate"": ""{ article.SerialNumberSinceExpiryDate }"",
                                                ""ProductCode"":
                                                [
                                                    {{
                                                        ""Code"": ""{ productCode.Code }""
                                                    }}
                                                ],
                                                ""Pack"":
                                                [
                                                    {{
                                                        ""DeliveryNumber"": ""{ pack.DeliveryNumber }"",
                                                        ""BatchNumber"": ""{ pack.BatchNumber }"",
                                                        ""ExternalId"": ""{ pack.ExternalId }"",
                                                        ""SerialNumber"": ""{ pack.SerialNumber }"",
                                                        ""StockLocationId"": ""{ pack.StockLocationId }"",
                                                        ""ExpiryDate"": ""{ pack.ExpiryDate }"",
                                                        ""Index"": ""{ pack.Index }"",
                                                        ""SubItemQuantity"": ""{ pack.SubItemQuantity }"",
                                                        ""Depth"": ""{ pack.Depth }"",
                                                        ""Width"": ""{ pack.Width }"",
                                                        ""Height"": ""{ pack.Height }"",
                                                        ""Weight"": ""{ pack.Weight }""
                                                    }}
                                                ]
                                            }}
                                        ]
                                    }},
                                    ""Version"": ""2.0"",
                                    ""TimeStamp"": ""{ JsonMessageTests.Timestamp }""
                                }}",
                            new MessageEnvelope(    new InputResponse(  JsonMessageTests.MessageId,
                                                                        JsonMessageTests.Source,
                                                                        JsonMessageTests.Destination,
                                                                        new InputResponseArticle[]
                                                                        {
                                                                            new InputResponseArticle(   article.Id,
                                                                                                        article.Name,
                                                                                                        article.DosageForm,
                                                                                                        article.PackagingUnit,
                                                                                                        article.RequiresFridge,
                                                                                                        article.MaxSubItemQuantity,
                                                                                                        article.SerialNumberSinceExpiryDate,
                                                                                                        new ProductCode[]
                                                                                                        {
                                                                                                            productCode
                                                                                                        },
                                                                                                        new InputResponsePack[]
                                                                                                        {
                                                                                                            new InputResponsePack(  handling,
                                                                                                                                    pack.DeliveryNumber,
                                                                                                                                    pack.BatchNumber,
                                                                                                                                    pack.ExternalId,
                                                                                                                                    pack.SerialNumber,
                                                                                                                                    pack.StockLocationId,
                                                                                                                                    pack.ExpiryDate,
                                                                                                                                    pack.Index,
                                                                                                                                    pack.SubItemQuantity,
                                                                                                                                    pack.Depth,
                                                                                                                                    pack.Width,
                                                                                                                                    pack.Height,
                                                                                                                                    pack.Weight )
                                                                                                        }   )
                                                                        },
                                                                        isNewDelivery   ),
                                                    JsonMessageTests.Timestamp    ) );
            }
        }

        [TestMethod]
        public void Serialize_Request_Succeeds()
        {
            bool result = base.SerializeMessage( InputRequestEnvelopeDataContractTests.Request );

            Assert.IsTrue( result );
        }

        [TestMethod]
        public void Deserialize_Request_Succeeds()
        {
            bool result = base.DeserializeMessage( InputRequestEnvelopeDataContractTests.Request );

            Assert.IsTrue( result );
        }
    }
}

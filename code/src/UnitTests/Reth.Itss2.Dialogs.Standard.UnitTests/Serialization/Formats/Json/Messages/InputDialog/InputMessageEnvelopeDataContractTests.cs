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
    public class InputMessageEnvelopeDataContractTests:JsonMessageTests
    {
        public static ( String Json, IMessageEnvelope Object ) Message
        {
            get
            {
                (   ArticleId Id,
                    String Name,
                    String DosageForm,
                    String PackagingUnit,
                    int MaxSubItemQuantity  ) article = ( new ArticleId( "1985" ), "Whole Device", "Flux Capacitor", "Box", 100 );

                (   PackId Id,
                    String DeliveryNumber,
                    String BatchNumber,
                    String ExternalId,
                    String SerialNumber,
                    String ScanCode,
                    String MachineLocation,
                    PackDate ExpiryDate,
                    PackDate StockInDate,
                    int Index,
                    int SubItemQuantity,
                    int Depth,
                    int Width,
                    int Height,
                    int Weight,
                    PackShape Shape,
                    PackState State,
                    bool IsInFridge,
                    StockLocationId StockLocationId ) pack = (  new PackId( "1984" ),
                                                                "4711",
                                                                "0815",
                                                                "EXT-1",
                                                                "SER-3",
                                                                "1001101",
                                                                "main",
                                                                new PackDate( 2021, 5, 5 ),
                                                                new PackDate( 1999, 7, 23 ),
                                                                42,
                                                                30,
                                                                100,
                                                                50,
                                                                24,
                                                                153,
                                                                PackShape.Cuboid,
                                                                PackState.Available,
                                                                true,
                                                                new StockLocationId( "default" )    );

                InputMessagePackHandling handling = new( InputMessagePackHandlingInput.Completed, "Done." );

                ProductCode productCode = new( new ProductCodeId( "5783" ) );

                bool isNewDelivery = false;

                return (    $@" {{
                                    ""InputMessage"":
                                    {{
                                        ""Id"": ""{ JsonMessageTests.MessageId }"",
                                        ""Source"": ""{ JsonMessageTests.Source }"",
                                        ""Destination"": ""{ JsonMessageTests.Destination }"",
                                        ""IsNewDelivery"": ""{ isNewDelivery }"",
                                        ""Article"":
                                        [
                                            {{
                                                ""Id"": ""{ article.Id }"",
                                                ""Name"": ""{ article.Name }"",
                                                ""DosageForm"": ""{ article.DosageForm }"",
                                                ""PackagingUnit"": ""{ article.PackagingUnit }"",
                                                ""MaxSubItemQuantity"": ""{ article.MaxSubItemQuantity }"",
                                                ""ProductCode"":
                                                [
                                                    {{
                                                        ""Code"": ""{ productCode.Code }""
                                                    }}
                                                ],
                                                ""Pack"":
                                                [
                                                    {{
                                                        ""Id"": ""{ pack.Id }"",
                                                        ""DeliveryNumber"": ""{ pack.DeliveryNumber }"",
                                                        ""BatchNumber"": ""{ pack.BatchNumber }"",
                                                        ""ExternalId"": ""{ pack.ExternalId }"",
                                                        ""SerialNumber"": ""{ pack.SerialNumber }"",
                                                        ""ScanCode"": ""{ pack.ScanCode }"",
                                                        ""MachineLocation"": ""{ pack.MachineLocation }"",
                                                        ""StockLocationId"": ""{ pack.StockLocationId }"",
                                                        ""ExpiryDate"": ""{ pack.ExpiryDate }"",
                                                        ""StockInDate"": ""{ pack.StockInDate }"",
                                                        ""Index"": ""{ pack.Index }"",
                                                        ""SubItemQuantity"": ""{ pack.SubItemQuantity }"",
                                                        ""Depth"": ""{ pack.Depth }"",
                                                        ""Width"": ""{ pack.Width }"",
                                                        ""Height"": ""{ pack.Height }"",
                                                        ""Weight"": ""{ pack.Weight }"",
                                                        ""Shape"": ""{ pack.Shape }"",
                                                        ""State"": ""{ pack.State }"",
                                                        ""IsInFridge"": ""{ pack.IsInFridge }"",
                                                        ""Handling"":
                                                        {{
                                                            ""Input"": ""{ handling.Input }"",
                                                            ""Text"": ""{ handling.Text }""
                                                        }}
                                                    }}
                                                ]
                                            }}
                                        ]
                                    }},
                                    ""Version"": ""2.0"",
                                    ""TimeStamp"": ""{ JsonMessageTests.Timestamp }""
                                }}",
                            new MessageEnvelope(    new InputMessage(   JsonMessageTests.MessageId,
                                                                        JsonMessageTests.Source,
                                                                        JsonMessageTests.Destination,
                                                                        new InputMessageArticle[]
                                                                        {
                                                                            new InputMessageArticle(    article.Id,
                                                                                                        article.Name,
                                                                                                        article.DosageForm,
                                                                                                        article.PackagingUnit,
                                                                                                        article.MaxSubItemQuantity,
                                                                                                        new ProductCode[]
                                                                                                        {
                                                                                                            productCode
                                                                                                        },
                                                                                                        new InputMessagePack[]
                                                                                                        {
                                                                                                            new InputMessagePack(   pack.Id,
                                                                                                                                    handling,
                                                                                                                                    pack.DeliveryNumber,
                                                                                                                                    pack.BatchNumber,
                                                                                                                                    pack.ExternalId,
                                                                                                                                    pack.SerialNumber,
                                                                                                                                    pack.ScanCode,
                                                                                                                                    pack.MachineLocation,
                                                                                                                                    pack.StockLocationId,
                                                                                                                                    pack.ExpiryDate,
                                                                                                                                    pack.StockInDate,
                                                                                                                                    pack.Index,
                                                                                                                                    pack.SubItemQuantity,
                                                                                                                                    pack.Depth,
                                                                                                                                    pack.Width,
                                                                                                                                    pack.Height,
                                                                                                                                    pack.Weight,
                                                                                                                                    pack.Shape,
                                                                                                                                    pack.State,
                                                                                                                                    pack.IsInFridge )
                                                                                                        }   )
                                                                        },
                                                                        isNewDelivery   ),
                                                    JsonMessageTests.Timestamp    ) );
            }
        }

        [TestMethod]
        public void Serialize_Message_Succeeds()
        {
            bool result = base.SerializeMessage( InputMessageEnvelopeDataContractTests.Message );

            Assert.IsTrue( result );
        }

        [TestMethod]
        public void Deserialize_Message_Succeeds()
        {
            bool result = base.DeserializeMessage( InputMessageEnvelopeDataContractTests.Message );

            Assert.IsTrue( result );
        }
    }
}

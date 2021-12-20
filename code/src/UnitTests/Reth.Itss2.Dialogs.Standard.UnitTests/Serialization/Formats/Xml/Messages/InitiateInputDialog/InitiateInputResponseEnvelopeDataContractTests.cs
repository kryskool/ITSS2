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
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.InitiateInput;
using Reth.Itss2.Messaging;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Formats.Xml.Messages.InitiateInputDialog
{
    [TestClass]
    public class InititateInputResponseEnvelopeDataContractTests:XmlMessageTests
    {
        public static ( String Xml, IMessageEnvelope Object ) Response
        {
            get
            {
                ( int InputSource, int InputPoint, InitiateInputResponseStatus Status ) details = ( 1, 2, InitiateInputResponseStatus.Accepted );

                (   ArticleId Id,
                    String Name,
                    String DosageForm,
                    String PackagingUnit,
                    int MaxSubItemQuantity,
                    PackDate SerialNumberSinceExpiryDate ) article = ( new ArticleId( "1985" ), "Whole Device", "Flux Capacitor", "Box", 100, new PackDate( 1999, 7, 23 ) );

                (   int Index,
                    String ScanCode,
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
                    StockLocationId StockLocationId,
                    PackShape Shape ) pack = (  42,
                                                "1100101",
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
                                                new StockLocationId( "default" ),
                                                PackShape.Cuboid    );

                ProductCode productCode = new( new ProductCodeId( "5783" ) );

                bool isNewDelivery = false;
                bool setPickingIndicator = true;

                return (    $@" <WWKS Version=""2.0"" TimeStamp=""{ XmlMessageTests.Timestamp }"">
                                    <InitiateInputResponse  Id=""{ XmlMessageTests.MessageId }""
                                                            Source=""{ XmlMessageTests.Source }""
                                                            Destination=""{ XmlMessageTests.Destination }""
                                                            IsNewDelivery=""{ isNewDelivery }""
                                                            SetPickingIndicator=""{ setPickingIndicator }"">
                                        <Details    Status=""{ details.Status }""
                                                    InputSource=""{ details.InputSource }""
                                                    InputPoint=""{ details.InputPoint }"" />
                                        <Article    Id=""{ article.Id }""
                                                    Name=""{ article.Name }""
                                                    DosageForm=""{ article.DosageForm }""
                                                    PackagingUnit=""{ article.PackagingUnit }""
                                                    MaxSubItemQuantity=""{ article.MaxSubItemQuantity }""
                                                    SerialNumberSinceExpiryDate=""{ article.SerialNumberSinceExpiryDate }"">
                                            <ProductCode Code=""{ productCode.Code }"" />
                                            <Pack   ScanCode=""{ pack.ScanCode }""
                                                    DeliveryNumber=""{ pack.DeliveryNumber }""
                                                    BatchNumber=""{ pack.BatchNumber }""
                                                    ExternalId=""{ pack.ExternalId }""
                                                    SerialNumber=""{ pack.SerialNumber }""
                                                    StockLocationId=""{ pack.StockLocationId }""
                                                    ExpiryDate=""{ pack.ExpiryDate }""
                                                    Index=""{ pack.Index }""
                                                    SubItemQuantity=""{ pack.SubItemQuantity }""
                                                    Depth=""{ pack.Depth }""
                                                    Width=""{ pack.Width }""
                                                    Height=""{ pack.Height }""
                                                    Weight=""{ pack.Weight }""
                                                    Shape=""{ pack.Shape }""  />
                                        </Article>
                                    </InitiateInputResponse>
                                </WWKS>",
                            new MessageEnvelope(    new InitiateInputResponse(  XmlMessageTests.MessageId,
                                                                                XmlMessageTests.Source,
                                                                                XmlMessageTests.Destination,
                                                                                new(    details.Status,
                                                                                        details.InputSource,
                                                                                        details.InputPoint  ),
                                                                                new InitiateInputResponseArticle[]
                                                                                {
                                                                                    new(    article.Id,
                                                                                            article.Name,
                                                                                            article.DosageForm,
                                                                                            article.PackagingUnit,
                                                                                            article.MaxSubItemQuantity,
                                                                                            article.SerialNumberSinceExpiryDate,
                                                                                            new ProductCode[]
                                                                                            {
                                                                                                productCode
                                                                                            },
                                                                                            new InitiateInputResponsePack[]
                                                                                            {
                                                                                                new(    pack.ScanCode,
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
                                                                                                        pack.Weight,
                                                                                                        pack.Shape  )
                                                                                            }   )
                                                                                },
                                                                                isNewDelivery,
                                                                                setPickingIndicator ),
                                                    XmlMessageTests.Timestamp    ) );
            }
        }

        [TestMethod]
        public void Serialize_Response_Succeeds()
        {
            bool result = base.SerializeMessage( InititateInputResponseEnvelopeDataContractTests.Response );

            Assert.IsTrue( result );
        }

        [TestMethod]
        public void Deserialize_Response_Succeeds()
        {
            bool result = base.DeserializeMessage( InititateInputResponseEnvelopeDataContractTests.Response );

            Assert.IsTrue( result );
        }
    }
}

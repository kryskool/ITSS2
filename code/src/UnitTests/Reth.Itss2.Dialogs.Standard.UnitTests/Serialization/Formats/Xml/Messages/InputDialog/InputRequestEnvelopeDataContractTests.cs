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

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Formats.Xml.Messages.InputDialog
{
    [TestClass]
    public class InputRequestEnvelopeDataContractTests:XmlMessageTests
    {
        public static ( String Xml, IMessageEnvelope Object ) Request
        {
            get
            {
                (   ArticleId Id,
                    String FMDId    ) article = ( new ArticleId( "1985" ), "FMD-23" );

                (   int Index,
                    String ScanCode,
                    String DeliveryNumber,
                    String BatchNumber,
                    String ExternalId,
                    String SerialNumber,
                    PackDate ExpiryDate,
                    int SubItemQuantity,
                    String MachineLocation,
                    StockLocationId StockLocationId ) pack = (  42,
                                                                "0101010",
                                                                "4711",
                                                                "0815",
                                                                "EXT-1",
                                                                "SER-3",
                                                                new PackDate( 2021, 5, 5 ),
                                                                30,
                                                                "main",
                                                                new StockLocationId( "default" )    );

                bool isNewDelivery = false;
                bool setPickingIndicator = true;

                return (    $@" <WWKS Version=""2.0"" TimeStamp=""{ XmlMessageTests.Timestamp }"">
                                    <InputRequest   Id=""{ XmlMessageTests.MessageId }""
                                                    Source=""{ XmlMessageTests.Source }""
                                                    Destination=""{ XmlMessageTests.Destination }""
                                                    IsNewDelivery=""{ isNewDelivery }""
                                                    SetPickingIndicator=""{ setPickingIndicator }"">
                                        <Article    Id=""{ article.Id }""
                                                    FMDId=""{ article.FMDId }"">
                                            <Pack   ScanCode=""{ pack.ScanCode }""
                                                    DeliveryNumber=""{ pack.DeliveryNumber }""
                                                    BatchNumber=""{ pack.BatchNumber }""
                                                    ExternalId=""{ pack.ExternalId }""
                                                    SerialNumber=""{ pack.SerialNumber }""
                                                    MachineLocation=""{ pack.MachineLocation }""
                                                    StockLocationId=""{ pack.StockLocationId }""
                                                    ExpiryDate=""{ pack.ExpiryDate }""
                                                    Index=""{ pack.Index }""
                                                    SubItemQuantity=""{ pack.SubItemQuantity }""    />
                                        </Article>
                                    </InputRequest>
                                </WWKS>",
                            new MessageEnvelope(    new InputRequest(   XmlMessageTests.MessageId,
                                                                        XmlMessageTests.Source,
                                                                        XmlMessageTests.Destination,
                                                                        new InputRequestArticle[]
                                                                        {
                                                                            new InputRequestArticle(    article.Id,
                                                                                                        article.FMDId,
                                                                                                        new InputRequestPack[]
                                                                                                        {
                                                                                                            new InputRequestPack(   pack.ScanCode,
                                                                                                                                    pack.DeliveryNumber,
                                                                                                                                    pack.BatchNumber,
                                                                                                                                    pack.ExternalId,
                                                                                                                                    pack.SerialNumber,
                                                                                                                                    pack.MachineLocation,
                                                                                                                                    pack.StockLocationId,
                                                                                                                                    pack.ExpiryDate,
                                                                                                                                    pack.Index,
                                                                                                                                    pack.SubItemQuantity    )
                                                                                                        }   )
                                                                        },
                                                                        isNewDelivery,
                                                                        setPickingIndicator ),
                                                    XmlMessageTests.Timestamp    ) );
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

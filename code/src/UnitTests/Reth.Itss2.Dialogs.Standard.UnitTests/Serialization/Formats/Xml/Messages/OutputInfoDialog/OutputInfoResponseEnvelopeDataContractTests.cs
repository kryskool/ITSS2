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
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.OutputInfo;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Formats.Xml.Messages.OutputInfoDialog
{
    [TestClass]
    public class OutputInfoResponseEnvelopeDataContractTests:XmlMessageTests
    {
        public static ( String Xml, IMessageEnvelope Object ) Response
        {
            get
            {
                MessageId taskId = new MessageId( "4711" );
                OutputInfoStatus taskStatus = OutputInfoStatus.Completed;

                ArticleId articleId = new ArticleId( "42" );

                (   PackId Id,
                    int OutputDestination,
                    int OutputPoint,
                    String DeliveryNumber,
                    String BatchNumber,
                    String ExternalId,
                    String SerialNumber,
                    String ScanCode,
                    String BoxNumber,
                    String MachineLocation,
                    StockLocationId StockLocationId,
                    PackDate ExpiryDate,
                    PackDate StockInDate,
                    int SubItemQuantity,
                    int Depth,
                    int Width,
                    int Height,
                    int Weight,
                    PackShape Shape,
                    bool IsInFridge,
                    LabelStatus LabelStatus    ) pack = (   new PackId( "1998" ),
                                                            14,
                                                            0,
                                                            "DEL-4",
                                                            "BAT-3",
                                                            "EXT-2",
                                                            "SER-A",
                                                            "11001100",
                                                            "1023",
                                                            "default",
                                                            new StockLocationId( "main" ),
                                                            new PackDate( 2012, 5, 6 ),
                                                            new PackDate( 1999, 7, 23 ),
                                                            50,
                                                            100,
                                                            40,
                                                            15,
                                                            765,
                                                            PackShape.Cylinder,
                                                            false,
                                                            LabelStatus.Labelled    );
                
                return (    $@" <WWKS Version=""2.0"" TimeStamp=""{ XmlMessageTests.Timestamp }"">
                                    <OutputInfoResponse Id=""{ XmlMessageTests.MessageId }"" Source=""{ XmlMessageTests.Source }"" Destination=""{ XmlMessageTests.Destination }"">
                                        <Task Id=""{ taskId }"" Status=""{ taskStatus }"">
                                            <Article Id=""{ articleId }"">
                                                <Pack   Id=""{ pack.Id }""
                                                        OutputDestination=""{ pack.OutputDestination }""
                                                        OutputPoint=""{ pack.OutputPoint }""
                                                        DeliveryNumber=""{ pack.DeliveryNumber }""
                                                        BatchNumber=""{ pack.BatchNumber }""
                                                        ExternalId=""{ pack.ExternalId }""
                                                        SerialNumber=""{ pack.SerialNumber }""
                                                        ScanCode=""{ pack.ScanCode }""
                                                        BoxNumber=""{ pack.BoxNumber }""
                                                        MachineLocation=""{ pack.MachineLocation }""
                                                        StockLocationId=""{ pack.StockLocationId }""
                                                        ExpiryDate=""{ pack.ExpiryDate }""
                                                        StockInDate=""{ pack.StockInDate }""
                                                        SubItemQuantity=""{ pack.SubItemQuantity }""
                                                        Depth=""{ pack.Depth }""
                                                        Width=""{ pack.Width }""
                                                        Height=""{ pack.Height }""
                                                        Weight=""{ pack.Weight }""
                                                        Shape=""{ pack.Shape }""
                                                        IsInFridge=""{ pack.IsInFridge }""
                                                        LabelStatus=""{ pack.LabelStatus }"" />
                                            </Article>
                                        </Task>
                                    </OutputInfoResponse>
                                </WWKS>",
                            new MessageEnvelope(    new OutputInfoResponse( XmlMessageTests.MessageId,
                                                                            XmlMessageTests.Source,
                                                                            XmlMessageTests.Destination,
                                                                            new(    taskId,
                                                                                    taskStatus,
                                                                                    new OutputInfoArticle[]
                                                                                    {
                                                                                        new(    articleId,
                                                                                                new OutputInfoPack[]
                                                                                                {
                                                                                                    new(    pack.Id,
                                                                                                            pack.OutputDestination,
                                                                                                            pack.OutputPoint,
                                                                                                            pack.DeliveryNumber,
                                                                                                            pack.BatchNumber,
                                                                                                            pack.ExternalId,
                                                                                                            pack.SerialNumber,
                                                                                                            pack.ScanCode,
                                                                                                            pack.BoxNumber,
                                                                                                            pack.MachineLocation,
                                                                                                            pack.StockLocationId,
                                                                                                            pack.ExpiryDate,
                                                                                                            pack.StockInDate,
                                                                                                            pack.SubItemQuantity,
                                                                                                            pack.Depth,
                                                                                                            pack.Width,
                                                                                                            pack.Height,
                                                                                                            pack.Weight,
                                                                                                            pack.Shape,
                                                                                                            pack.IsInFridge,
                                                                                                            pack.LabelStatus    )
                                                                                                                                                                                                }   )
                                                                                                        }   )  ),
                                                    XmlMessageTests.Timestamp    ) );
            }
        }

        [TestMethod]
        public void Serialize_Response_Succeeds()
        {
            bool result = base.SerializeMessage( OutputInfoResponseEnvelopeDataContractTests.Response );

            Assert.IsTrue( result );
        }

        [TestMethod]
        public void Deserialize_Response_Succeeds()
        {
            bool result = base.DeserializeMessage( OutputInfoResponseEnvelopeDataContractTests.Response );

            Assert.IsTrue( result );
        }
    }
}

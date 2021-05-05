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
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.ArticleMasterSet;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Formats.Xml.Messages.ArticleMasterSetDialog
{
    [TestClass]
    public class ArticleMasterSetRequestEnvelopeDataContractTests:XmlMessageTests
    {
        public static ( String Xml, IMessageEnvelope Object ) Request
        {
            get
            {
                //String name = "";
                //String dosageForm = "";
                //String packagingUnit = "";
                //String machineLocation = "";
                //StockLocationId stockLocationId = new StockLocationId( "" );
                //bool requiresFridge = true;
                //int maxSubItemQuantity = 50;
                //int depth = 20;
                //int width = 30;
                //int height = 15;
                //int weight = 67;
                //PackDate serialNumberSinceExpiryDate = new PackDate( 2024, 12, 2 );

                //ArticleMasterSetArticle article = new(  new ArticleId( "4711" ),
                //                                        name,
                //                                        dosageForm,
                //                                        packagingUnit,
                //                                        machineLocation,
                //                                        stockLocationId,
                //                                        requiresFridge,
                //                                        maxSubItemQuantity,
                //                                        depth,
                //                                        width,
                //                                        height,
                //                                        width,
                //                                        serialNumberSinceExpiryDate,
                //                                        );

                ArticleMasterSetArticle article = new(  new ArticleId( "4711" ) );

                return (    $@" <WWKS Version=""2.0"" TimeStamp=""{ XmlMessageTests.Timestamp }"">
                                    <ArticleMasterSetRequest Id=""{ XmlMessageTests.MessageId }"" Source=""{ XmlMessageTests.Source }"" Destination=""{ XmlMessageTests.Destination }"">
                                        <Article Id=""{ article.Id }"" />
                                    </ArticleMasterSetRequest>
                                </WWKS>",
                            new MessageEnvelope(    new ArticleMasterSetRequest(    XmlMessageTests.MessageId,
                                                                                    XmlMessageTests.Source,
                                                                                    XmlMessageTests.Destination,
                                                                                    new ArticleMasterSetArticle[]
                                                                                    {
                                                                                        article
                                                                                    }   ),
                                                    XmlMessageTests.Timestamp    ) );
            }
        }

        [TestMethod]
        public void Serialize_Response_Succeeds()
        {
            bool result = base.SerializeMessage( ArticleMasterSetRequestEnvelopeDataContractTests.Request );

            Assert.IsTrue( result );
        }

        [TestMethod]
        public void Deserialize_Response_Succeeds()
        {
            bool result = base.DeserializeMessage( ArticleMasterSetRequestEnvelopeDataContractTests.Request );

            Assert.IsTrue( result );
        }
    }
}

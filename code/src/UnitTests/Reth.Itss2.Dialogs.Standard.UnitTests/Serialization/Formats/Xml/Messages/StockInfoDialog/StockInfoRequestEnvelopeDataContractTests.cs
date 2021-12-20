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
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockInfo;
using Reth.Itss2.Messaging;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Formats.Xml.Messages.StockInfoDialog
{
    [TestClass]
    public class StockInfoRequestEnvelopeDataContractTests:XmlMessageTests
    {
        public static ( String Xml, IMessageEnvelope Object ) Request
        {
            get
            {
                bool includePacks = true;
                bool includeArticleDetails = true;

                ArticleId articleId = new ArticleId( "1999" );

                String batchNumber = "5483";
                String externalId = "asdf";
                String serialNumber = "X-11";
                String machineLocation = "default";

                StockLocationId stockLocationId = new StockLocationId( "main" );

                return (    $@" <WWKS Version=""2.0"" TimeStamp=""{ XmlMessageTests.Timestamp }"">
                                    <StockInfoRequest   Id=""{ XmlMessageTests.MessageId }""
                                                        Source=""{ XmlMessageTests.Source }""
                                                        Destination=""{ XmlMessageTests.Destination }""
                                                        IncludePacks=""{ includePacks }""
                                                        IncludeArticleDetails=""{ includeArticleDetails }"">
                                        <Criteria   ArticleId=""{ articleId }""
                                                    BatchNumber=""{ batchNumber }""
                                                    ExternalId=""{ externalId }""
                                                    SerialNumber=""{ serialNumber }""
                                                    MachineLocation=""{ machineLocation }""
                                                    StockLocationId=""{ stockLocationId }"" />
                                    </StockInfoRequest>
                                </WWKS>",
                            new MessageEnvelope(    new StockInfoRequest(   XmlMessageTests.MessageId,
                                                                            XmlMessageTests.Source,
                                                                            XmlMessageTests.Destination,
                                                                            includePacks,
                                                                            includeArticleDetails,
                                                                            new StockInfoRequestCriteria[]
                                                                            {
                                                                                new StockInfoRequestCriteria(   articleId,
                                                                                                                batchNumber,
                                                                                                                externalId,
                                                                                                                serialNumber,
                                                                                                                machineLocation,
                                                                                                                stockLocationId )
                                                                            }   ),
                                                    XmlMessageTests.Timestamp    ) );
            }
        }   

        [TestMethod]
        public void Serialize_Request_Succeeds()
        {
            bool result = base.SerializeMessage( StockInfoRequestEnvelopeDataContractTests.Request );

            Assert.IsTrue( result );
        }

        [TestMethod]
        public void Deserialize_Request_Succeeds()
        {
            bool result = base.DeserializeMessage( StockInfoRequestEnvelopeDataContractTests.Request );

            Assert.IsTrue( result );
        }
    }
}

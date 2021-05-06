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
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.ArticleInfo;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Formats.Xml.Messages.ArticleInfoDialog
{
    [TestClass]
    public class ArticleInfoRequestEnvelopeDataContractTests:XmlMessageTests
    {
        public static ( String Xml, IMessageEnvelope Object ) Request
        {
            get
            {
                (   ArticleId Id,
                    int Depth,
                    int Width,
                    int Height,
                    int Weight ) article = ( new ArticleId( "4711" ), 20, 30, 15, 67 );

                return (    $@" <WWKS Version=""2.0"" TimeStamp=""{ XmlMessageTests.Timestamp }"">
                                    <ArticleInfoRequest Id=""{ XmlMessageTests.MessageId }"" Source=""{ XmlMessageTests.Source }"" Destination=""{ XmlMessageTests.Destination }"">
                                        <Article    Id=""{ article.Id }""
                                                    Depth=""{ article.Depth }""
                                                    Width=""{ article.Width }""
                                                    Height=""{ article.Height }""
                                                    Weight=""{ article.Weight }"">
                                        </Article>
                                    </ArticleInfoRequest>
                                </WWKS>",
                            new MessageEnvelope(    new ArticleInfoRequest( XmlMessageTests.MessageId,
                                                                            XmlMessageTests.Source,
                                                                            XmlMessageTests.Destination,
                                                                            new ArticleInfoRequestArticle[]
                                                                            {
                                                                                new(    article.Id,
                                                                                        article.Depth,
                                                                                        article.Width,
                                                                                        article.Height,
                                                                                        article.Weight  )
                                                                            }   ),
                                                    XmlMessageTests.Timestamp    ) );
            }
        }

        [TestMethod]
        public void Serialize_Request_Succeeds()
        {
            bool result = base.SerializeMessage( ArticleInfoRequestEnvelopeDataContractTests.Request );

            Assert.IsTrue( result );
        }

        [TestMethod]
        public void Deserialize_Request_Succeeds()
        {
            bool result = base.DeserializeMessage( ArticleInfoRequestEnvelopeDataContractTests.Request );

            Assert.IsTrue( result );
        }
    }
}

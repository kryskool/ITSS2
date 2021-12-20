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
using Reth.Itss2.Messaging;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Formats.Xml.Messages.ArticleInfoDialog
{
    [TestClass]
    public class ArticleInfoResponseEnvelopeDataContractTests:XmlMessageTests
    {
        public static ( String Xml, IMessageEnvelope Object ) Response
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

                return (    $@" <WWKS Version=""2.0"" TimeStamp=""{ XmlMessageTests.Timestamp }"">
                                    <ArticleInfoResponse    Id=""{ XmlMessageTests.MessageId }""
                                                            Source=""{ XmlMessageTests.Source }""
                                                            Destination=""{ XmlMessageTests.Destination }"">
                                        <Article    Id=""{ article.Id }""
                                                    Name=""{ article.Name }""
                                                    DosageForm=""{ article.DosageForm }""
                                                    PackagingUnit=""{ article.PackagingUnit }""
                                                    RequiresFridge=""{ article.RequiresFridge }""
                                                    MaxSubItemQuantity=""{ article.MaxSubItemQuantity }""
                                                    SerialNumberSinceExpiryDate=""{ article.SerialNumberSinceExpiryDate }"" />
                                    </ArticleInfoResponse>
                                </WWKS>",
                            new MessageEnvelope(    new ArticleInfoResponse(    XmlMessageTests.MessageId,
                                                                                XmlMessageTests.Source,
                                                                                XmlMessageTests.Destination,
                                                                                new ArticleInfoResponseArticle[]
                                                                                {
                                                                                    new ArticleInfoResponseArticle( article.Id,
                                                                                                                    article.Name,
                                                                                                                    article.DosageForm,
                                                                                                                    article.PackagingUnit,
                                                                                                                    article.RequiresFridge,
                                                                                                                    article.MaxSubItemQuantity,
                                                                                                                    article.SerialNumberSinceExpiryDate )
                                                                                } ),
                                                    XmlMessageTests.Timestamp    ) );
            }
        }

        [TestMethod]
        public void Serialize_Response_Succeeds()
        {
            bool result = base.SerializeMessage( ArticleInfoResponseEnvelopeDataContractTests.Response );

            Assert.IsTrue( result );
        }

        [TestMethod]
        public void Deserialize_Response_Succeeds()
        {
            bool result = base.DeserializeMessage( ArticleInfoResponseEnvelopeDataContractTests.Response );

            Assert.IsTrue( result );
        }
    }
}

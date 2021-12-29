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

using System.Xml;
using System.Xml.Serialization;

using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ArticlePrice;
using Reth.Itss2.Messaging;
using Reth.Itss2.Serialization.Formats.Xml;
using Reth.Itss2.Serialization.Formats.Xml.Messages;

namespace Reth.Itss2.Dialogs.Experimental.Serialization.Formats.Xml.Messages.ArticlePrice
{
    [XmlDataContractMapping( typeof( ArticlePriceResponse ), typeof( ArticlePriceResponseDataContract ) )]
    public class ArticlePriceResponseEnvelopeDataContract:MessageEnvelopeDataContract
    {
        public ArticlePriceResponseEnvelopeDataContract()
        {
            this.ArticlePriceResponse = new ArticlePriceResponseDataContract();
        }

        public ArticlePriceResponseEnvelopeDataContract( IMessageEnvelope dataObject )
        :
            base( dataObject )
        {
            this.ArticlePriceResponse = new ArticlePriceResponseDataContract( ( ArticlePriceResponse )( dataObject.Message ) );
        }

        public override IMessage Message => this.ArticlePriceResponse.GetDataObject();

        [XmlElement]
        public ArticlePriceResponseDataContract ArticlePriceResponse
        {
            get; set;
        }
    }
}

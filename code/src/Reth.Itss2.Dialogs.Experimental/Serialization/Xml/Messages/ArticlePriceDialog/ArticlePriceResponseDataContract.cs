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
using System.Xml.Serialization;

using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ArticlePriceDialog;
using Reth.Itss2.Dialogs.Standard.Serialization.Xml.Messages;

namespace Reth.Itss2.Dialogs.Experimental.Serialization.Xml.Messages.ArticlePriceDialog
{
    public class ArticlePriceResponseDataContract:SubscribedResponseDataContract<ArticlePriceResponse>
    {
        public ArticlePriceResponseDataContract()
        {
            this.Articles = new ArticlePriceResponseArticleDataContract[]{};
        }

        public ArticlePriceResponseDataContract( ArticlePriceResponse dataObject )
        :
            base( dataObject )
        {
            this.Articles = TypeConverter.ConvertFromDataObjects<ArticlePriceResponseArticle, ArticlePriceResponseArticleDataContract>( dataObject.GetArticles() );
            this.Currency = TypeConverter.Iso4217Code.ConvertNullableFrom( dataObject.Currency );
        }

        [XmlElement( ElementName = "Article" )]
        public ArticlePriceResponseArticleDataContract[] Articles{ get; set; }

        [XmlAttribute]
        public String? Currency{ get; set; }

        public override ArticlePriceResponse GetDataObject()
        {
            return new ArticlePriceResponse(    TypeConverter.MessageId.ConvertTo( this.Id ),
                                                TypeConverter.SubscriberId.ConvertTo( this.Source ),
                                                TypeConverter.SubscriberId.ConvertTo( this.Destination ),
                                                TypeConverter.ConvertToDataObjects<ArticlePriceResponseArticle, ArticlePriceResponseArticleDataContract>( this.Articles ),
                                                TypeConverter.Iso4217Code.ConvertNullableTo( this.Currency )    );
        }

        public override Type GetEnvelopeType()
        {
            return typeof( ArticlePriceResponseEnvelopeDataContract );
        }
    }
}

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

using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ArticleSelectedDialog;
using Reth.Itss2.Dialogs.Experimental.Serialization.Conversion;
using Reth.Itss2.Dialogs.Standard.Serialization.Formats.Xml.Messages;

namespace Reth.Itss2.Dialogs.Experimental.Serialization.Formats.Xml.Messages.ArticleSelectedDialog
{
    public class ArticleSelectedMessageDataContract:SubscribedMessageDataContract<ArticleSelectedMessage>
    {
        public ArticleSelectedMessageDataContract()
        {
            this.Article = new ArticleSelectedArticleDataContract();
        }

        public ArticleSelectedMessageDataContract( ArticleSelectedMessage dataObject )
        :
            base( dataObject )
        {
            this.Article = TypeConverter.ConvertFromDataObject<ArticleSelectedArticle, ArticleSelectedArticleDataContract>( dataObject.Article );
        }

        [XmlAttribute]
        public ArticleSelectedArticleDataContract Article{ get; set; }

        public override ArticleSelectedMessage GetDataObject()
        {
            return new ArticleSelectedMessage(  TypeConverter.MessageId.ConvertTo( this.Id ),
                                                TypeConverter.SubscriberId.ConvertTo( this.Source ),
                                                TypeConverter.SubscriberId.ConvertTo( this.Destination ),
                                                TypeConverter.ConvertToDataObject<ArticleSelectedArticle, ArticleSelectedArticleDataContract>( this.Article )   );
        }

        public override Type GetEnvelopeType()
        {
            return typeof( ArticleSelectedMessageEnvelopeDataContract );
        }
    }
}

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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.InputDialog;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Xml.Messages.InputDialog
{
    public class InputMessageDataContract:SubscribedMessageDataContract<InputMessage>
    {
        public InputMessageDataContract()
        {
            this.Articles = new InputMessageArticleDataContract[]{};
        }

        public InputMessageDataContract( InputMessage dataObject )
        :
            base( dataObject )
        {
            this.IsNewDelivery = TypeConverter.Boolean.ConvertNullableFrom( dataObject.IsNewDelivery );
            this.Articles = TypeConverter.ConvertFromDataObjects<InputMessageArticle, InputMessageArticleDataContract>( dataObject.GetArticles() );
        }

        [XmlAttribute]
        public String? IsNewDelivery{ get; set; }

        [XmlElement( ElementName = "Article" )]
        public InputMessageArticleDataContract[] Articles{ get; set; }

        public override InputMessage GetDataObject()
        {
            return new InputMessage(    TypeConverter.MessageId.ConvertTo( this.Id ),
                                        TypeConverter.SubscriberId.ConvertTo( this.Source ),
                                        TypeConverter.SubscriberId.ConvertTo( this.Destination ),
                                        TypeConverter.ConvertToDataObjects<InputMessageArticle, InputMessageArticleDataContract>( this.Articles ),
                                        TypeConverter.Boolean.ConvertNullableTo( this.IsNewDelivery )   );
        }

        public override Type GetEnvelopeType()
        {
            return typeof( InputRequestEnvelopeDataContract );
        }
    }
}

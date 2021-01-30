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
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Xml.Messages.InputDialog
{
    public class InputRequestDataContract:SubscribedRequestDataContract<InputRequest>
    {
        public InputRequestDataContract()
        {
            this.Articles = new InputRequestArticleDataContract[]{};
        }

        public InputRequestDataContract( InputRequest dataObject )
        :
            base( dataObject )
        {
            this.Articles = TypeConverter.ConvertFromDataObjects<InputRequestArticle, InputRequestArticleDataContract>( dataObject.GetArticles() );
            this.IsNewDelivery = TypeConverter.Boolean.ConvertNullableFrom( dataObject.IsNewDelivery );
            this.SetPickingIndicator = TypeConverter.Boolean.ConvertNullableFrom( dataObject.SetPickingIndicator );
        }

        [XmlAttribute]
        public String? IsNewDelivery{ get; set; }

        [XmlAttribute]
        public String? SetPickingIndicator{ get; set; }

        [XmlElement( ElementName = "Article" )]
        public InputRequestArticleDataContract[] Articles{ get; set; }

        public override InputRequest GetDataObject()
        {
            return new InputRequest(    TypeConverter.MessageId.ConvertTo( this.Id ),
                                        TypeConverter.SubscriberId.ConvertTo( this.Source ),
                                        TypeConverter.SubscriberId.ConvertTo( this.Destination ),
                                        TypeConverter.ConvertToDataObjects<InputRequestArticle, InputRequestArticleDataContract>( this.Articles ),
                                        TypeConverter.Boolean.ConvertNullableTo( this.IsNewDelivery ),
                                        TypeConverter.Boolean.ConvertNullableTo( this.SetPickingIndicator ) );
        }

        public override Type GetEnvelopeType()
        {
            return typeof( InputRequestEnvelopeDataContract );
        }
    }
}

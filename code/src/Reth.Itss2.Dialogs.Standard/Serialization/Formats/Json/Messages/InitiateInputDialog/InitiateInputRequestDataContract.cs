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
using System.Text.Json.Serialization;

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.InitiateInput;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json.Messages.InitiateInput
{
    public class InitiateInputRequestDataContract:SubscribedRequestDataContract<InitiateInputRequest>
    {
        public InitiateInputRequestDataContract()
        {
            this.Details = new InitiateInputRequestDetailsDataContract();
            this.Articles = new InitiateInputRequestArticleDataContract[]{};
        }

        public InitiateInputRequestDataContract( InitiateInputRequest dataObject )
        :
            base( dataObject )
        {
            this.Details = TypeConverter.ConvertFromDataObject<InitiateInputRequestDetails, InitiateInputRequestDetailsDataContract>( dataObject.Details );
            this.Articles = TypeConverter.ConvertFromDataObjects<InitiateInputRequestArticle, InitiateInputRequestArticleDataContract>( dataObject.GetArticles() );
            this.IsNewDelivery = TypeConverter.Boolean.ConvertNullableFrom( dataObject.IsNewDelivery );
            this.SetPickingIndicator = TypeConverter.Boolean.ConvertNullableFrom( dataObject.SetPickingIndicator );
        }

        public InitiateInputRequestDetailsDataContract Details{ get; set; }

        public String? IsNewDelivery{ get; set; }

        public String? SetPickingIndicator{ get; set; }

        [JsonPropertyName( "Article" )]
        public InitiateInputRequestArticleDataContract[] Articles{ get; set; }

        public override InitiateInputRequest GetDataObject()
        {
            return new InitiateInputRequest(    TypeConverter.MessageId.ConvertTo( this.Id ),
                                                TypeConverter.SubscriberId.ConvertTo( this.Source ),
                                                TypeConverter.SubscriberId.ConvertTo( this.Destination ),
                                                TypeConverter.ConvertToDataObject<InitiateInputRequestDetails, InitiateInputRequestDetailsDataContract>( this.Details ),
                                                TypeConverter.ConvertToDataObjects<InitiateInputRequestArticle, InitiateInputRequestArticleDataContract>( this.Articles ),
                                                TypeConverter.Boolean.ConvertNullableTo( this.IsNewDelivery ),
                                                TypeConverter.Boolean.ConvertNullableTo( this.SetPickingIndicator ) );
        }

        public override Type GetEnvelopeType()
        {
            return typeof( InitiateInputRequestEnvelopeDataContract );
        }
    }
}

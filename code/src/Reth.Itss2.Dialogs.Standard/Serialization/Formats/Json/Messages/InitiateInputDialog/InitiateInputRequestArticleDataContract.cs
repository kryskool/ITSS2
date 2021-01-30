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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.InitiateInputDialog;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json.Messages.InitiateInputDialog
{
    public class InitiateInputRequestArticleDataContract:IDataContract<InitiateInputRequestArticle>
    {
        public InitiateInputRequestArticleDataContract()
        {
        }

        public InitiateInputRequestArticleDataContract( InitiateInputRequestArticle dataObject )
        {
            this.Id = TypeConverter.ArticleId.ConvertNullableFrom( dataObject.Id );
            this.FmdId = dataObject.FmdId;
            this.Packs = TypeConverter.ConvertFromDataObjects<InitiateInputRequestPack, InitiateInputRequestPackDataContract>( dataObject.GetPacks() );
        }

        public String? Id{ get; set; }

        public String? FmdId{ get; set; }

        [JsonPropertyName( "Pack" )]
        public InitiateInputRequestPackDataContract[]? Packs{ get; set; }
        
        public InitiateInputRequestArticle GetDataObject()
        {
            return new InitiateInputRequestArticle( TypeConverter.ArticleId.ConvertNullableTo( this.Id ),
                                            this.FmdId,
                                            TypeConverter.ConvertToDataObjects<InitiateInputRequestPack, InitiateInputRequestPackDataContract>( this.Packs )   );
        }
    }
}

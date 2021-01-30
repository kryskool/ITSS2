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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.OutputInfoDialog;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json.Messages.OutputInfoDialog
{
    public class OutputInfoArticleDataContract:IDataContract<OutputInfoArticle>
    {
        public OutputInfoArticleDataContract()
        {
        }

        public OutputInfoArticleDataContract( OutputInfoArticle dataObject )
        {
            this.Id = TypeConverter.ArticleId.ConvertFrom( dataObject.Id );
            this.Packs = TypeConverter.ConvertFromDataObjects<OutputInfoPack, OutputInfoPackDataContract>( dataObject.GetPacks() );
        }

        public String Id{ get; set; } = String.Empty;

        [JsonPropertyName( "Pack" )]
        public OutputInfoPackDataContract[]? Packs{ get; set; }
        
        public OutputInfoArticle GetDataObject()
        {
            return new OutputInfoArticle(   TypeConverter.ArticleId.ConvertTo( this.Id ),
                                            TypeConverter.ConvertToDataObjects<OutputInfoPack, OutputInfoPackDataContract>( this.Packs )   );
        }
    }
}

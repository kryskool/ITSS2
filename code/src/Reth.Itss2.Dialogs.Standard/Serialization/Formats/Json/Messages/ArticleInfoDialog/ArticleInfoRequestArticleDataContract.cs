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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.ArticleInfo;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json.Messages.ArticleInfo
{
    public class ArticleInfoRequestArticleDataContract:IDataContract<ArticleInfoRequestArticle>
    {
        public ArticleInfoRequestArticleDataContract()
        {
        }

        public ArticleInfoRequestArticleDataContract( ArticleInfoRequestArticle dataObject )
        {
            this.Id = TypeConverter.ArticleId.ConvertFrom( dataObject.Id );
            this.Depth = TypeConverter.Int32.ConvertNullableFrom( dataObject.Depth );
            this.Width = TypeConverter.Int32.ConvertNullableFrom( dataObject.Width );
            this.Height = TypeConverter.Int32.ConvertNullableFrom( dataObject.Height );
            this.Weight = TypeConverter.Int32.ConvertNullableFrom( dataObject.Weight );
        }

        public String Id{ get; set; } = String.Empty;

        public String? Depth{ get; set; }

        public String? Width{ get; set; }

        public String? Height{ get; set; }

        public String? Weight{ get; set; }

        public ArticleInfoRequestArticle GetDataObject()
        {
            return new ArticleInfoRequestArticle(   TypeConverter.ArticleId.ConvertTo( this.Id ),
                                                    TypeConverter.Int32.ConvertNullableTo( this.Depth ),
                                                    TypeConverter.Int32.ConvertNullableTo( this.Width ),
                                                    TypeConverter.Int32.ConvertNullableTo( this.Height ),
                                                    TypeConverter.Int32.ConvertNullableTo( this.Weight ) );
        }
    }
}

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
using System.Xml;
using System.Xml.Serialization;

using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ArticleInfo;
using Reth.Itss2.Dialogs.Experimental.Serialization.Conversion;
using Reth.Itss2.Serialization;

namespace Reth.Itss2.Dialogs.Experimental.Serialization.Formats.Xml.Messages.ArticleInfo
{
    public class ArticleInfoResponseArticleDataContract:IDataContract<ArticleInfoResponseArticle>
    {
        public ArticleInfoResponseArticleDataContract()
        {
        }

        public ArticleInfoResponseArticleDataContract( ArticleInfoResponseArticle dataObject )
        {
            this.Id = TypeConverter.ArticleId.ConvertFrom( dataObject.Id );
            this.Name = dataObject.Name;
            this.DosageForm = dataObject.DosageForm;
            this.PackagingUnit = dataObject.PackagingUnit;
            this.RequiresFridge = TypeConverter.Boolean.ConvertNullableFrom( dataObject.RequiresFridge );
            this.MaxSubItemQuantity = TypeConverter.Int32.ConvertNullableFrom( dataObject.MaxSubItemQuantity );
            this.SerialNumberSinceExpiryDate = TypeConverter.PackDate.ConvertNullableFrom( dataObject.SerialNumberSinceExpiryDate );
            this.ArticleTags = TypeConverter.ConvertFromDataObjects<ArticleTag, ArticleTagDataContract>( dataObject.GetArticleTags() );
            this.CrossSellingArticles = TypeConverter.ConvertFromDataObjects<CrossSellingArticle, CrossSellingArticleDataContract>( dataObject.GetCrossSellingArticles() );
            this.AlternativeArticles = TypeConverter.ConvertFromDataObjects<AlternativeArticle, AlternativeArticleDataContract>( dataObject.GetAlternativeArticles() );
            this.AlternativePackSizeArticles = TypeConverter.ConvertFromDataObjects<AlternativePackSizeArticle, AlternativePackSizeArticleDataContract>( dataObject.GetAlternativePackSizeArticles() );

        }

        [XmlAttribute]
        public String Id{ get; set; } = String.Empty;

        [XmlAttribute]
        public String? Name{ get; set; }
        
        [XmlAttribute]
        public String? DosageForm{ get; set; }
        
        [XmlAttribute]
        public String? PackagingUnit{ get; set; }
        
        [XmlAttribute]
        public String? RequiresFridge{ get; set; }
        
        [XmlAttribute]
        public String? MaxSubItemQuantity{ get; set; }
        
        [XmlAttribute]
        public String? SerialNumberSinceExpiryDate{ get; set; }
        
        [XmlElement( ElementName = "Tag" )]
        public ArticleTagDataContract[]? ArticleTags{ get; set; }
        
        [XmlElement( ElementName = "Article" )]
        public CrossSellingArticleDataContract[]? CrossSellingArticles{ get; set; }
        
        [XmlElement( ElementName = "Article" )]
        public AlternativeArticleDataContract[]? AlternativeArticles{ get; set; }
        
        [XmlElement( ElementName = "Article" )]
        public AlternativePackSizeArticleDataContract[]? AlternativePackSizeArticles{ get; set; }
        
        public ArticleInfoResponseArticle GetDataObject()
        {
            return new ArticleInfoResponseArticle(  TypeConverter.ArticleId.ConvertTo( this.Id ),
                                                    this.Name,
                                                    this.DosageForm,
                                                    this.PackagingUnit,
                                                    TypeConverter.Boolean.ConvertNullableTo( this.RequiresFridge ),
                                                    TypeConverter.Int32.ConvertNullableTo( this.MaxSubItemQuantity ),
                                                    TypeConverter.PackDate.ConvertNullableTo( this.SerialNumberSinceExpiryDate ),
                                                    TypeConverter.ConvertToDataObjects<ArticleTag, ArticleTagDataContract>( this.ArticleTags ),
                                                    TypeConverter.ConvertToDataObjects<CrossSellingArticle, CrossSellingArticleDataContract>( this.CrossSellingArticles ),
                                                    TypeConverter.ConvertToDataObjects<AlternativeArticle, AlternativeArticleDataContract>( this.AlternativeArticles ),
                                                    TypeConverter.ConvertToDataObjects<AlternativePackSizeArticle, AlternativePackSizeArticleDataContract>( this.AlternativePackSizeArticles )  );
        }
    }
}

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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Input;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Xml.Messages.Input
{
    public class InputRequestArticleDataContract:IDataContract<InputRequestArticle>
    {
        public InputRequestArticleDataContract()
        {
        }

        public InputRequestArticleDataContract( InputRequestArticle dataObject )
        {
            this.Id = TypeConverter.ArticleId.ConvertNullableFrom( dataObject.Id );
            this.FmdId = dataObject.FmdId;
            this.Packs = TypeConverter.ConvertFromDataObjects<InputRequestPack, InputRequestPackDataContract>( dataObject.GetPacks() );
        }

        [XmlAttribute]
        public String? Id{ get; set; }

        [XmlAttribute( AttributeName = "FMDId" ) ]
        public String? FmdId{ get; set; }

        [XmlElement( ElementName = "Pack" )]
        public InputRequestPackDataContract[]? Packs{ get; set; }
        
        public InputRequestArticle GetDataObject()
        {
            return new InputRequestArticle( TypeConverter.ArticleId.ConvertNullableTo( this.Id ),
                                            this.FmdId,
                                            TypeConverter.ConvertToDataObjects<InputRequestPack, InputRequestPackDataContract>( this.Packs )   );
        }
    }
}

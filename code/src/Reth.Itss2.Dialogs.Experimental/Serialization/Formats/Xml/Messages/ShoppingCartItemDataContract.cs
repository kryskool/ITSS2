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

using Reth.Itss2.Dialogs.Experimental.Protocol.Messages;
using Reth.Itss2.Dialogs.Experimental.Serialization.Conversion;
using Reth.Itss2.Dialogs.Standard.Serialization;

namespace Reth.Itss2.Dialogs.Experimental.Serialization.Formats.Xml.Messages
{
    public class ShoppingCartItemDataContract:IDataContract<ShoppingCartItem>
    {
        public ShoppingCartItemDataContract()
        {
        }

        public ShoppingCartItemDataContract( ShoppingCartItem dataObject )
        {
            this.ArticleId = TypeConverter.ArticleId.ConvertFrom( dataObject.ArticleId );
            this.OrderedQuantity = TypeConverter.Int32.ConvertFrom( dataObject.OrderedQuantity );
            this.DispensedQuantity = TypeConverter.Int32.ConvertFrom( dataObject.DispensedQuantity );
            this.PaidQuantity = TypeConverter.Int32.ConvertFrom( dataObject.PaidQuantity );
            this.Price = TypeConverter.Decimal.ConvertFrom( dataObject.Price );
            this.Currency = TypeConverter.Iso4217Code.ConvertFrom( dataObject.Currency );
        }

        [XmlAttribute]
        public String ArticleId{ get; set; } = String.Empty;

        [XmlAttribute]
        public String OrderedQuantity{ get; set; } = String.Empty;

        [XmlAttribute]
        public String DispensedQuantity{ get; set; } = String.Empty;

        [XmlAttribute]
        public String PaidQuantity{ get; set; } = String.Empty;

        [XmlAttribute]
        public String Price{ get; set; } = String.Empty;

        [XmlAttribute]
        public String Currency{ get; set; } = String.Empty;
        
        public ShoppingCartItem GetDataObject()
        {
            return new ShoppingCartItem(    TypeConverter.ArticleId.ConvertTo( this.ArticleId ),
                                            TypeConverter.Int32.ConvertTo( this.OrderedQuantity ),
                                            TypeConverter.Int32.ConvertTo( this.DispensedQuantity ),
                                            TypeConverter.Int32.ConvertTo( this.PaidQuantity ),
                                            TypeConverter.Decimal.ConvertTo( this.Price ),
                                            TypeConverter.Iso4217Code.ConvertTo( this.Currency )   );
        }
    }
}

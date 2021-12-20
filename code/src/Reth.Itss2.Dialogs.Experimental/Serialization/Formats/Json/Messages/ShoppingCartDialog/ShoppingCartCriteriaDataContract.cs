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

using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ShoppingCart;
using Reth.Itss2.Dialogs.Experimental.Serialization.Conversion;
using Reth.Itss2.Serialization;

namespace Reth.Itss2.Dialogs.Experimental.Serialization.Formats.Json.Messages.ShoppingCart
{
    public class ShoppingCartCriteriaDataContract:IDataContract<ShoppingCartCriteria>
    {
        public ShoppingCartCriteriaDataContract()
        {
        }

        public ShoppingCartCriteriaDataContract( ShoppingCartCriteria dataObject )
        {
            this.ShoppingCartId = TypeConverter.ShoppingCartId.ConvertNullableFrom( dataObject.ShoppingCartId );
            this.SalesPointId = TypeConverter.SalesPointId.ConvertNullableFrom( dataObject.SalesPointId );
            this.ViewPointId = TypeConverter.ViewPointId.ConvertNullableFrom( dataObject.ViewPointId );
            this.SalesPersonId = TypeConverter.SalesPersonId.ConvertNullableFrom( dataObject.SalesPersonId );
            this.CustomerId = TypeConverter.CustomerId.ConvertNullableFrom( dataObject.CustomerId );
        }

        public String? ShoppingCartId{ get; set; }

        public String? SalesPointId{ get; set; }

        public String? ViewPointId{ get; set; }

        public String? SalesPersonId{ get; set; }

        public String? CustomerId{ get; set; }
        
        public ShoppingCartCriteria GetDataObject()
        {
            return new ShoppingCartCriteria(    TypeConverter.ShoppingCartId.ConvertNullableTo( this.ShoppingCartId ),
                                                TypeConverter.SalesPointId.ConvertNullableTo( this.SalesPointId ),
                                                TypeConverter.ViewPointId.ConvertNullableTo( this.ViewPointId ),
                                                TypeConverter.SalesPersonId.ConvertNullableTo( this.SalesPersonId ),
                                                TypeConverter.CustomerId.ConvertNullableTo( this.CustomerId )   );
        }
    }
}

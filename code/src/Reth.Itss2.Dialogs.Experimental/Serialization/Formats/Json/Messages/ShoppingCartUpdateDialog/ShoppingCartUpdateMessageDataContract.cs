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

using Reth.Itss2.Dialogs.Experimental.Protocol.Messages;
using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ShoppingCartUpdateDialog;
using Reth.Itss2.Dialogs.Experimental.Serialization.Conversion;
using Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json.Messages;

namespace Reth.Itss2.Dialogs.Experimental.Serialization.Formats.Json.Messages.ShoppingCartUpdateDialog
{
    public class ShoppingCartUpdateMessageDataContract:SubscribedMessageDataContract<ShoppingCartUpdateMessage>
    {
        public ShoppingCartUpdateMessageDataContract()
        {
            this.ShoppingCart = new ShoppingCartContentDataContract();
        }

        public ShoppingCartUpdateMessageDataContract( ShoppingCartUpdateMessage dataObject )
        :
            base( dataObject )
        {
            this.ShoppingCart = TypeConverter.ConvertFromDataObject<ShoppingCart, ShoppingCartContentDataContract>( dataObject.ShoppingCart );
        }

        public ShoppingCartContentDataContract ShoppingCart{ get; set; }

        public override ShoppingCartUpdateMessage GetDataObject()
        {
            return new ShoppingCartUpdateMessage(   TypeConverter.MessageId.ConvertTo( this.Id ),
                                                    TypeConverter.SubscriberId.ConvertTo( this.Source ),
                                                    TypeConverter.SubscriberId.ConvertTo( this.Destination ),
                                                    TypeConverter.ConvertToDataObject<ShoppingCart, ShoppingCartContentDataContract>( this.ShoppingCart )    );
        }

        public override Type GetEnvelopeType()
        {
            return typeof( ShoppingCartUpdateMessageEnvelopeDataContract );
        }
    }
}

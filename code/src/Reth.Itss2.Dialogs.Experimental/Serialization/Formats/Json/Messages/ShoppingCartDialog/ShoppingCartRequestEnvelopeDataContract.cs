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

using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ShoppingCart;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json;
using Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json.Messages;

namespace Reth.Itss2.Dialogs.Experimental.Serialization.Formats.Json.Messages.ShoppingCart
{
    [JsonDataContractMapping( typeof( ShoppingCartRequest ), typeof( ShoppingCartRequestDataContract ) )]
    public class ShoppingCartRequestEnvelopeDataContract:MessageEnvelopeDataContract
    {
        public ShoppingCartRequestEnvelopeDataContract()
        {
            this.ShoppingCartRequest = new ShoppingCartRequestDataContract();
        }

        protected ShoppingCartRequestEnvelopeDataContract( IMessageEnvelope dataObject )
        :
            base( dataObject )
        {
            this.ShoppingCartRequest = new ShoppingCartRequestDataContract( ( ShoppingCartRequest )( dataObject.Message ) );
        }

        public override IMessage Message => this.ShoppingCartRequest.GetDataObject();
        public ShoppingCartRequestDataContract ShoppingCartRequest
        {
            get; set;
        }
    }
}

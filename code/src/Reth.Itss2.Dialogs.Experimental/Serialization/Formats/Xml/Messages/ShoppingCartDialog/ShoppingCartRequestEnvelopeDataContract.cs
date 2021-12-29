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

using System.Xml;
using System.Xml.Serialization;

using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ShoppingCart;
using Reth.Itss2.Messaging;
using Reth.Itss2.Serialization.Formats.Xml;
using Reth.Itss2.Serialization.Formats.Xml.Messages;

namespace Reth.Itss2.Dialogs.Experimental.Serialization.Formats.Xml.Messages.ShoppingCart
{
    [XmlDataContractMapping( typeof( ShoppingCartRequest ), typeof( ShoppingCartRequestDataContract ) )]
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

        [XmlElement]
        public ShoppingCartRequestDataContract ShoppingCartRequest
        {
            get; set;
        }
    }
}

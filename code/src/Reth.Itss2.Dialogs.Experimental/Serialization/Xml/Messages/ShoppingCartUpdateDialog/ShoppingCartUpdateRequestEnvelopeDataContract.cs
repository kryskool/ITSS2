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

using Reth.Itss2.Dialogs.Experimental.Protocol.Messages.ShoppingCartUpdateDialog;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Serialization;
using Reth.Itss2.Dialogs.Standard.Serialization.Xml.Messages;

namespace Reth.Itss2.Dialogs.Experimental.Serialization.Xml.Messages.ShoppingCartUpdateDialog
{
    [DataContractMapping( typeof( ShoppingCartUpdateRequest ) )]
    public class ShoppingCartRequestEnvelopeDataContract:MessageEnvelopeDataContract
    {
        public ShoppingCartRequestEnvelopeDataContract()
        {
            this.ShoppingCartUpdateRequest = new ShoppingCartUpdateRequestDataContract();
        }

        protected ShoppingCartRequestEnvelopeDataContract( IMessageEnvelope dataObject )
        :
            base( dataObject )
        {
            this.ShoppingCartUpdateRequest = new ShoppingCartUpdateRequestDataContract( ( ShoppingCartUpdateRequest )( dataObject.Message ) );
        }

        public override IMessage Message => this.ShoppingCartUpdateRequest.GetDataObject();

        [XmlElement]
        public ShoppingCartUpdateRequestDataContract ShoppingCartUpdateRequest
        {
            get; set;
        }
    }
}

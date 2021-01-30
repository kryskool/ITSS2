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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockInfoDialog;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Xml.Messages.StockInfoDialog
{
    [XmlDataContractMapping( typeof( StockInfoRequest ) )]
    public class StockInfoRequestEnvelopeDataContract:MessageEnvelopeDataContract
    {
        public StockInfoRequestEnvelopeDataContract()
        {
            this.StockInfoRequest = new StockInfoRequestDataContract();
        }

        public StockInfoRequestEnvelopeDataContract( IMessageEnvelope dataObject )
        :
            base( dataObject )
        {
            this.StockInfoRequest = new StockInfoRequestDataContract( ( StockInfoRequest )( dataObject.Message ) );
        }

        public override IMessage Message => this.StockInfoRequest.GetDataObject();

        [XmlElement]
        public StockInfoRequestDataContract StockInfoRequest
        {
            get; set;
        }
    }
}

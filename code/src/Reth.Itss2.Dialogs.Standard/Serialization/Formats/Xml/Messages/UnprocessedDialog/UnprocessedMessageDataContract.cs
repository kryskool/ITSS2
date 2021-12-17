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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Unprocessed;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Xml.Messages.Unprocessed
{
    public class UnprocessedMessageDataContract:SubscribedMessageDataContract<UnprocessedMessage>
    {
        public UnprocessedMessageDataContract()
        {
            this.Message = new XmlDocument().CreateCDataSection( String.Empty );
        }

        public UnprocessedMessageDataContract( UnprocessedMessage dataObject )
        :
            base( dataObject )
        {
            this.Message = new XmlDocument().CreateCDataSection( dataObject.Message );
            this.Text = dataObject.Text;
            this.Reason = TypeConverter.UnprocessedReason.ConvertNullableFrom( dataObject.Reason );
        }

        [XmlElement( "Message", typeof( XmlCDataSection ) )]
        public XmlCDataSection Message{ get; set; }

        [XmlAttribute]
        public String? Text{ get; set; }

        [XmlAttribute]
        public String? Reason{ get; set; }

        public override UnprocessedMessage GetDataObject()
        {
            return new UnprocessedMessage(  TypeConverter.MessageId.ConvertTo( this.Id ),
                                            TypeConverter.SubscriberId.ConvertTo( this.Source ),
                                            TypeConverter.SubscriberId.ConvertTo( this.Destination ),
                                            this.Message.Value ?? String.Empty,
                                            this.Text,
                                            TypeConverter.UnprocessedReason.ConvertNullableTo( this.Reason ) );
        }

        public override Type GetEnvelopeType()
        {
            return typeof( UnprocessedMessageEnvelopeDataContract );
        }
    }
}

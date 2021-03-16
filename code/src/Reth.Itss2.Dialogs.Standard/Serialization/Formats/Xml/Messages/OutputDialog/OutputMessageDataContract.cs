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
using System.Xml.Serialization;

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Output;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Xml.Messages.Output
{
    public class OutputMessageDataContract:SubscribedMessageDataContract<OutputMessage>
    {
        public OutputMessageDataContract()
        {
            this.Details = new OutputMessageDetailsDataContract();
            this.Articles = new OutputArticleDataContract[]{};
            this.Boxes = new OutputBoxDataContract[]{};
        }

        public OutputMessageDataContract( OutputMessage dataObject )
        :
            base( dataObject )
        {
            this.Details = TypeConverter.ConvertFromDataObject<OutputMessageDetails, OutputMessageDetailsDataContract>( dataObject.Details );
            this.Articles = TypeConverter.ConvertFromDataObjects<OutputArticle, OutputArticleDataContract>( dataObject.GetArticles() );
            this.Boxes = TypeConverter.ConvertFromDataObjects<OutputBox, OutputBoxDataContract>( dataObject.GetBoxes() );
        }

        [XmlElement]
        public OutputMessageDetailsDataContract Details{ get; set; }

        [XmlElement( ElementName = "Article" )]
        public OutputArticleDataContract[] Articles{ get; set; }

        [XmlElement( ElementName = "Box" )]
        public OutputBoxDataContract[] Boxes{ get; set; }

        public override OutputMessage GetDataObject()
        {
            return new OutputMessage(   TypeConverter.MessageId.ConvertTo( this.Id ),
                                        TypeConverter.SubscriberId.ConvertTo( this.Source ),
                                        TypeConverter.SubscriberId.ConvertTo( this.Destination ),
                                        TypeConverter.ConvertToDataObject<OutputMessageDetails, OutputMessageDetailsDataContract>( this.Details ),
                                        TypeConverter.ConvertToDataObjects<OutputArticle, OutputArticleDataContract>( this.Articles ),
                                        TypeConverter.ConvertToDataObjects<OutputBox, OutputBoxDataContract>( this.Boxes )   );
        }

        public override Type GetEnvelopeType()
        {
            return typeof( OutputMessageEnvelopeDataContract );
        }
    }
}

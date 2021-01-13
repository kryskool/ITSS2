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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.OutputDialog;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Xml.Messages.OutputDialog
{
    public class OutputMessageDetailsDataContract:IDataContract<OutputMessageDetails>
    {
        public OutputMessageDetailsDataContract()
        {
        }

        public OutputMessageDetailsDataContract( OutputMessageDetails dataObject )
        {
            this.OutputDestination = TypeConverter.Int32.ConvertFrom( dataObject.OutputDestination );
            this.Status = TypeConverter.OutputMessageStatus.ConvertFrom( dataObject.Status );
            this.Priority = TypeConverter.OutputPriority.ConvertNullableFrom( dataObject.Priority );
            this.OutputPoint = TypeConverter.Int32.ConvertNullableFrom( dataObject.OutputPoint );
        }

        [XmlAttribute]
        public String OutputDestination{ get; set; } = String.Empty;

        [XmlAttribute]
        public String Status{ get; set; } = String.Empty;

        [XmlAttribute]
        public String? Priority{ get; set; }

        [XmlAttribute]
        public String? OutputPoint{ get; set; }
        
        public OutputMessageDetails GetDataObject()
        {
            return new OutputMessageDetails(    TypeConverter.Int32.ConvertTo( this.OutputDestination ),
                                                TypeConverter.OutputMessageStatus.ConvertTo( this.Status ),
                                                TypeConverter.OutputPriority.ConvertNullableTo( this.Priority ),
                                                TypeConverter.Int32.ConvertNullableTo( this.OutputPoint )    );
        }
    }
}

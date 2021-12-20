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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Output;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion;
using Reth.Itss2.Serialization;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json.Messages.Output
{
    public class OutputResponseDetailsDataContract:IDataContract<OutputResponseDetails>
    {
        public OutputResponseDetailsDataContract()
        {
        }

        public OutputResponseDetailsDataContract( OutputResponseDetails dataObject )
        {
            this.OutputDestination = TypeConverter.Int32.ConvertFrom( dataObject.OutputDestination );
            this.Status = TypeConverter.OutputResponseStatus.ConvertFrom( dataObject.Status );
            this.Priority = TypeConverter.OutputPriority.ConvertNullableFrom( dataObject.Priority );
            this.OutputPoint = TypeConverter.Int32.ConvertNullableFrom( dataObject.OutputPoint );
        }

        public String OutputDestination{ get; set; } = String.Empty;

        public String Status{ get; set; } = String.Empty;

        public String? Priority{ get; set; }

        public String? OutputPoint{ get; set; }
        
        public OutputResponseDetails GetDataObject()
        {
            return new OutputResponseDetails(   TypeConverter.Int32.ConvertTo( this.OutputDestination ),
                                                TypeConverter.OutputResponseStatus.ConvertTo( this.Status ),
                                                TypeConverter.OutputPriority.ConvertNullableTo( this.Priority ),
                                                TypeConverter.Int32.ConvertNullableTo( this.OutputPoint )    );
        }
    }
}

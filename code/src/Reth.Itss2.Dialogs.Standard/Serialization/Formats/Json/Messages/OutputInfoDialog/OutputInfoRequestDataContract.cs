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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.OutputInfo;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json.Messages.OutputInfo
{
    public class OutputInfoRequestDataContract:SubscribedRequestDataContract<OutputInfoRequest>
    {
        public OutputInfoRequestDataContract()
        {
            this.Task = new OutputInfoRequestTaskDataContract();
        }

        public OutputInfoRequestDataContract( OutputInfoRequest dataObject )
        :
            base( dataObject )
        {
            this.IncludeTaskDetails = TypeConverter.Boolean.ConvertNullableFrom( dataObject.IncludeTaskDetails );
            this.Task = TypeConverter.ConvertFromDataObject<OutputInfoRequestTask, OutputInfoRequestTaskDataContract>( dataObject.Task );
        }

        public String? IncludeTaskDetails{ get; set; }

        public OutputInfoRequestTaskDataContract Task{ get; set; }

        public override OutputInfoRequest GetDataObject()
        {
            return new OutputInfoRequest(   TypeConverter.MessageId.ConvertTo( this.Id ),
                                            TypeConverter.SubscriberId.ConvertTo( this.Source ),
                                            TypeConverter.SubscriberId.ConvertTo( this.Destination ),
                                            TypeConverter.ConvertToDataObject<OutputInfoRequestTask, OutputInfoRequestTaskDataContract>( this.Task ),
                                            TypeConverter.Boolean.ConvertNullableTo( this.IncludeTaskDetails )  );
        }

        public override Type GetEnvelopeType()
        {
            return typeof( OutputInfoRequestEnvelopeDataContract );
        }
    }
}

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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockDeliverySetDialog;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json.Messages.StockDeliverySetDialog
{
    public class StockDeliverySetResultDataContract:IDataContract<StockDeliverySetResult>
    {
        public StockDeliverySetResultDataContract()
        {
        }

        public StockDeliverySetResultDataContract( StockDeliverySetResult dataObject )
        {
            this.Value = TypeConverter.StockDeliverySetResultValue.ConvertFrom( dataObject.Value );
            this.Text = dataObject.Text;
        }

        public String Value{ get; set; } = String.Empty;

        public String? Text{ get; set; }
        
        public StockDeliverySetResult GetDataObject()
        {
            return new StockDeliverySetResult(  TypeConverter.StockDeliverySetResultValue.ConvertTo( this.Value ),
                                                this.Text    );
        }
    }
}

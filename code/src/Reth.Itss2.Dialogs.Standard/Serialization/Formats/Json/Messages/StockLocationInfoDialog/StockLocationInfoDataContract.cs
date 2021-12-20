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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockLocationInfo;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion;
using Reth.Itss2.Serialization;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json.Messages.StockLocationInfo
{
    public class StockLocationDataContract:IDataContract<StockLocation>
    {
        public StockLocationDataContract()
        {
        }

        public StockLocationDataContract( StockLocation dataObject )
        {
            this.Id = TypeConverter.StockLocationId.ConvertFrom( dataObject.Id );
            this.Description = dataObject.Description;
        }

        public String Id{ get; set; } = String.Empty;

        public String? Description{ get; set; }
        
        public StockLocation GetDataObject()
        {
            return new StockLocation(   TypeConverter.StockLocationId.ConvertTo( this.Id ),
                                        this.Description    );
        }
    }
}

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
using System.Text.Json.Serialization;

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.Output;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json.Messages.Output
{
    public class OutputCriteriaDataContract:IDataContract<OutputCriteria>
    {
        public OutputCriteriaDataContract()
        {
        }

        public OutputCriteriaDataContract( OutputCriteria dataObject )
        {
            this.Quantity = TypeConverter.Int32.ConvertFrom( dataObject.Quantity );
            this.SubItemQuantity = TypeConverter.Int32.ConvertNullableFrom( dataObject.SubItemQuantity );
            this.ArticleId = TypeConverter.ArticleId.ConvertNullableFrom( dataObject.ArticleId );
            this.PackId = TypeConverter.PackId.ConvertNullableFrom( dataObject.PackId );
            this.MinimumExpiryDate = TypeConverter.PackDate.ConvertNullableFrom( dataObject.MinimumExpiryDate );
            this.BatchNumber = dataObject.BatchNumber;
            this.ExternalId = dataObject.ExternalId;
            this.SerialNumber = dataObject.SerialNumber;
            this.MachineLocation = dataObject.MachineLocation;
            this.StockLocationId = TypeConverter.StockLocationId.ConvertNullableFrom( dataObject.StockLocationId );
            this.SingleBatchNumber = TypeConverter.Boolean.ConvertNullableFrom( dataObject.SingleBatchNumber );
            this.Labels = TypeConverter.ConvertFromDataObjects<OutputLabel, OutputLabelDataContract>( dataObject.GetLabels() );
        }

        public String Quantity{ get; set; } = String.Empty;

        public String? SubItemQuantity{ get; set; }

        public String? ArticleId{ get; set; }

        public String? PackId{ get; set; }

        public String? MinimumExpiryDate{ get; set; }

        public String? BatchNumber{ get; set; }

        public String? ExternalId{ get; set; }

        public String? SerialNumber{ get; set; }

        public String? MachineLocation{ get; set; }

        public String? StockLocationId{ get; set; }

        public String? SingleBatchNumber{ get; set; }
        
        [JsonPropertyName( "Label" )]
        public OutputLabelDataContract[]? Labels{ get; set; }
        
        public OutputCriteria GetDataObject()
        {
            return new OutputCriteria(  TypeConverter.Int32.ConvertTo( this.Quantity ),
                                        TypeConverter.Int32.ConvertNullableTo( this.SubItemQuantity ),
                                        TypeConverter.ArticleId.ConvertNullableTo( this.ArticleId ),
                                        TypeConverter.PackId.ConvertNullableTo( this.PackId ),
                                        TypeConverter.PackDate.ConvertNullableTo( this.MinimumExpiryDate ),
                                        this.BatchNumber,
                                        this.ExternalId,
                                        this.SerialNumber,
                                        this.MachineLocation,
                                        TypeConverter.StockLocationId.ConvertNullableTo( this.StockLocationId ),
                                        TypeConverter.Boolean.ConvertNullableTo( this.SingleBatchNumber ),
                                        TypeConverter.ConvertToDataObjects<OutputLabel, OutputLabelDataContract>( this.Labels )    );
        }
    }
}

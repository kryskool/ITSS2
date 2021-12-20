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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockDeliveryInfo;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion;
using Reth.Itss2.Serialization;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json.Messages.StockDeliveryInfo
{
    public class StockDeliveryInfoResponseTaskDataContract:IDataContract<StockDeliveryInfoResponseTask>
    {
        public StockDeliveryInfoResponseTaskDataContract()
        {
        }

        public StockDeliveryInfoResponseTaskDataContract( StockDeliveryInfoResponseTask dataObject )
        {
            this.Id = dataObject.Id;
            this.Status = TypeConverter.StockDeliveryInfoStatus.ConvertFrom( dataObject.Status );
            this.Articles = TypeConverter.ConvertFromDataObjects<StockDeliveryInfoArticle, StockDeliveryInfoArticleDataContract>( dataObject.GetArticles() );
        }

        public String Id{ get; set; } = String.Empty;

        public String Status{ get; set; } = String.Empty;

        [JsonPropertyName( "Article" )]
        public StockDeliveryInfoArticleDataContract[]? Articles{ get; set; }
        
        public StockDeliveryInfoResponseTask GetDataObject()
        {
            return new StockDeliveryInfoResponseTask(   this.Id,
                                                        TypeConverter.StockDeliveryInfoStatus.ConvertTo( this.Status ),
                                                        TypeConverter.ConvertToDataObjects<StockDeliveryInfoArticle, StockDeliveryInfoArticleDataContract>( this.Articles )   );
        }
    }
}

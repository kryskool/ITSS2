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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockInfoDialog;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Xml.Messages.StockInfoDialog
{
    public class StockInfoRequestCriteriaDataContract:IDataContract<StockInfoRequestCriteria>
    {
        public StockInfoRequestCriteriaDataContract()
        {
        }

        public StockInfoRequestCriteriaDataContract( StockInfoRequestCriteria dataObject )
        {
            this.ArticleId = TypeConverter.ArticleId.ConvertNullableFrom( dataObject.ArticleId );
            this.BatchNumber = dataObject.BatchNumber;
            this.ExternalId = dataObject.ExternalId;
            this.SerialNumber = dataObject.SerialNumber;
            this.MachineLocation = dataObject.MachineLocation;
            this.StockLocationId = TypeConverter.StockLocationId.ConvertNullableFrom( dataObject.StockLocationId );
        }

        [XmlAttribute]
        public String? ArticleId{ get; set; }

        [XmlAttribute]
        public String? BatchNumber{ get; set; }

        [XmlAttribute]
        public String? ExternalId{ get; set; }

        [XmlAttribute]
        public String? SerialNumber{ get; set; }

        [XmlAttribute]
        public String? MachineLocation{ get; set; }

        [XmlAttribute]
        public String? StockLocationId{ get; set; }
        
        public StockInfoRequestCriteria GetDataObject()
        {
            return new StockInfoRequestCriteria(    TypeConverter.ArticleId.ConvertNullableTo( this.ArticleId ),
                                                    this.BatchNumber,
                                                    this.ExternalId,
                                                    this.SerialNumber,
                                                    this.MachineLocation,
                                                    TypeConverter.StockLocationId.ConvertNullableTo( this.StockLocationId )  );
        }
    }
}

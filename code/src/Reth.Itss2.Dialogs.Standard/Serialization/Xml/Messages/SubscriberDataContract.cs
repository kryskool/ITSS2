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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Xml.Messages
{
    public class SubscriberDataContract:IDataContract<Subscriber>
    {
        public SubscriberDataContract()
        {
            this.Capabilities = new CapabilityDataContract[]{};
        }

        public SubscriberDataContract( Subscriber dataObject )
        {
            this.Id = TypeConverter.SubscriberId.ConvertFrom( dataObject.Id );
            this.Type = TypeConverter.SubscriberType.ConvertFrom( dataObject.Type );
            this.TenantId = dataObject.TenantId;
            this.Manufacturer = dataObject.Manufacturer;
            this.ProductInfo = dataObject.ProductInfo;
            this.VersionInfo = dataObject.VersionInfo;
            this.Capabilities = TypeConverter.ConvertFromDataObjects<Capability, CapabilityDataContract>( dataObject.GetCapabilities() );
        }

        [XmlAttribute]
        public String Id{ get; set; } = String.Empty;

        [XmlAttribute]
        public String Type{ get; set; } = String.Empty;

        [XmlAttribute]
        public String? TenantId{ get; set; }

        [XmlAttribute]
        public String Manufacturer{ get; set; } = String.Empty;

        [XmlAttribute]
        public String ProductInfo{ get; set; } = String.Empty;

        [XmlAttribute]
        public String VersionInfo{ get; set; } = String.Empty;

        [XmlElement( ElementName = nameof( Capability ) )]
        public CapabilityDataContract[] Capabilities{ get; set; }
        
        public Subscriber GetDataObject()
        {
            return new Subscriber(  TypeConverter.ConvertToDataObjects<Capability, CapabilityDataContract>( this.Capabilities ),
                                    TypeConverter.SubscriberId.ConvertTo( this.Id ),
                                    TypeConverter.SubscriberType.ConvertTo( this.Type ),
                                    this.TenantId,
                                    this.Manufacturer,
                                    this.ProductInfo,
                                    this.VersionInfo    );
        }
    }
}

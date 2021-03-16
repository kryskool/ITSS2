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

using Reth.Itss2.Dialogs.Standard.Serialization.Conversion;
using Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json.Messages;
using Reth.Itss2.Dialogs.StandardExtensions.Protocol.Messages.ConfigurationGet;

namespace Reth.Itss2.Dialogs.StandardExtensions.Serialization.Formats.Json.Messages.ConfigurationGet
{
    public class ConfigurationGetResponseDataContract:SubscribedResponseDataContract<ConfigurationGetResponse>
    {
        public ConfigurationGetResponseDataContract()
        {
            this.Configuration = new ConfigurationGetDataContract();
        }

        public ConfigurationGetResponseDataContract( ConfigurationGetResponse dataObject )
        :
            base( dataObject )
        {
            this.Configuration = TypeConverter.ConvertFromDataObject<Configuration, ConfigurationGetDataContract>( dataObject.Configuration );
        }

        public ConfigurationGetDataContract Configuration{ get; set; }

        public override ConfigurationGetResponse GetDataObject()
        {
            return new ConfigurationGetResponse(    TypeConverter.MessageId.ConvertTo( this.Id ),
                                                    TypeConverter.SubscriberId.ConvertTo( this.Source ),
                                                    TypeConverter.SubscriberId.ConvertTo( this.Destination ),
                                                    TypeConverter.ConvertToDataObject<Configuration, ConfigurationGetDataContract>( this.Configuration )   );
        }

        public override Type GetEnvelopeType()
        {
            return typeof( ConfigurationGetResponseEnvelopeDataContract );
        }
    }
}

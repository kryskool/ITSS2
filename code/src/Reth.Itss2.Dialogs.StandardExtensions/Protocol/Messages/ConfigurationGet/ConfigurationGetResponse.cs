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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Messaging;

namespace Reth.Itss2.Dialogs.StandardExtensions.Protocol.Messages.ConfigurationGet
{
    public class ConfigurationGetResponse:SubscribedResponse, IEquatable<ConfigurationGetResponse>
    {
        public static bool operator==( ConfigurationGetResponse? left, ConfigurationGetResponse? right )
		{
            return ConfigurationGetResponse.Equals( left, right );
		}
		
		public static bool operator!=( ConfigurationGetResponse? left, ConfigurationGetResponse? right )
		{
			return !( ConfigurationGetResponse.Equals( left, right ) );
		}

        public static bool Equals( ConfigurationGetResponse? left, ConfigurationGetResponse? right )
		{
            bool result = SubscribedResponse.Equals( left, right );

            result &= ( result ? Configuration.Equals( left?.Configuration, right?.Configuration ) : false );

            return result;
		}

        public ConfigurationGetResponse(    MessageId id,
                                            SubscriberId source,
                                            SubscriberId destination,
                                            Configuration configuration )
        :
            base( id, StandardExtensionsDialogs.ConfigurationGet, source, destination )
        {
            this.Configuration = configuration;
        }

        public ConfigurationGetResponse( ConfigurationGetRequest request, Configuration configuration )
        :
            base( request )
        {
            this.Configuration = configuration;
        }

        public Configuration Configuration
        {
            get;
        }

        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as ConfigurationGetResponse );
		}
		
        public bool Equals( ConfigurationGetResponse? other )
		{
            return ConfigurationGetResponse.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Configuration.GetHashCode();
		}
    }
}

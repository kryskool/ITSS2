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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages
{
    public class Subscriber:IEquatable<Subscriber>
    {
        public static bool operator==( Subscriber? left, Subscriber? right )
		{
            return Subscriber.Equals( left, right );
		}
		
		public static bool operator!=( Subscriber? left, Subscriber? right )
		{
			return !( Subscriber.Equals( left, right ) );
		}

        public static bool Equals( Subscriber? left, Subscriber? right )
		{
            bool result = SubscriberId.Equals( left?.Id, right?.Id );

            result &= ( result ? EqualityComparer<SubscriberType?>.Default.Equals( left?.Type, right?.Type ) : false );
            result &= ( result ? String.Equals( left?.Manufacturer, right?.Manufacturer, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? String.Equals( left?.ProductInfo, right?.ProductInfo, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? String.Equals( left?.VersionInfo, right?.VersionInfo, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? String.Equals( left?.TenantId, right?.TenantId, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? ( left?.Capabilities.SequenceEqual( right?.Capabilities ) ).GetValueOrDefault() : false );

            return result;
		}

        public Subscriber(  IEnumerable<Capability>? capabilities,
                            SubscriberId id,
                            SubscriberType type,
                            String? tenantId,
                            String manufacturer,
                            String productInfo  )
        :
            this( capabilities,
                  id,
                  type,
                  tenantId,
                  manufacturer,
                  productInfo,
                  Assembly.GetCallingAssembly().GetName().Version?.ToString() ?? String.Empty   )
        {
        }

        public Subscriber(  IEnumerable<Capability>? capabilities,
                            SubscriberId id,
                            SubscriberType type,
                            String? tenantId,
                            String manufacturer,
                            String productInfo,
                            String versionInfo  )
        {
            manufacturer.ThrowIfEmpty();
            productInfo.ThrowIfEmpty();
            versionInfo.ThrowIfEmpty();

            this.Id = id;
            this.Type = type;
            this.Manufacturer = manufacturer;
            this.TenantId = tenantId;
            this.ProductInfo = productInfo;
            this.VersionInfo = versionInfo;

            if( capabilities is not null )
            {
                foreach( Capability capability in capabilities )
                {
                    if( capability is not null )
                    {
                        this.Capabilities.Add( capability.Name, capability );
                    }
                }
            }
        }

        public SubscriberId Id
        {
            get;
        }

        public SubscriberType Type
        {
            get;
        } = SubscriberType.IMS;

        public String? TenantId
        {
            get;
        }

        public String Manufacturer
        {
            get;
        }

        public String ProductInfo
        {
            get;
        }

        public String VersionInfo
        {
            get;
        }
        
        private Dictionary<String, Capability> Capabilities
        {
            get;
        } = new Dictionary<String, Capability>( StringComparer.OrdinalIgnoreCase );

        public Capability[] GetCapabilities()
        {
            return this.Capabilities.Values.ToArray();
        }

        public bool IsSupported( Capability capability )
        {
            return this.Capabilities.ContainsKey( capability.Name );
        }

        public bool IsSupported( String capability )
        {
           return this.Capabilities.ContainsKey( capability );
        }

        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as Subscriber );
		}
		
        public bool Equals( Subscriber? other )
		{
            return Subscriber.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}

        public override String? ToString()
        {
            return this.Id.ToString();
        }
    }
}

using System;
using System.Collections.Generic;

using Reth.Protocols;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.ListExtensions;
using Reth.Protocols.Extensions.ObjectExtensions;
using Reth.Protocols.Extensions.StringExtensions;

namespace Reth.Itss2.Standard.Dialogs.General.Hello
{
    public class Subscriber:IEquatable<Subscriber>
    {
        public static bool operator==( Subscriber left, Subscriber right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( Subscriber left, Subscriber right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( Subscriber left, Subscriber right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = SubscriberId.Equals( left.Id, right.Id );
                                                        result &= ( left.Type == right.Type );
                                                        result &= String.Equals( left.Manufacturer, right.Manufacturer, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.ProductInfo, right.ProductInfo, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.VersionInfo, right.VersionInfo, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.TenantId, right.TenantId, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= left.Capabilities.ElementsEqual( right.Capabilities );

                                                        return result;
                                                    }   );
		}

        private SubscriberId id;

        private String manufacturer = String.Empty;
        private String productInfo = String.Empty;
        private String versionInfo = String.Empty;

        public Subscriber(  SubscriberId id,
                            SubscriberType type,
                            String manufacturer,
                            String productInfo,
                            String versionInfo,
                            String tenantId,
                            IEnumerable<Capability> capabilities    )
        {
            this.Id = id;
            this.Type = type;
            this.Manufacturer = manufacturer;
            this.ProductInfo = productInfo;
            this.VersionInfo = versionInfo;
            this.TenantId = tenantId;

            if( !( capabilities is null ) )
            {
                foreach( Capability capability in capabilities )
                {
                    if( !( capability is null ) )
                    {
                        this.Capabilities.Add( capability );
                    }
                }
            }
        }

        public Subscriber(  SubscriberId id,
                            SubscriberType type,
                            String manufacturer,
                            String productInfo,
                            String versionInfo,
                            String tenantId,
                            IEnumerable<IDialogName> capabilities    )
        {
            this.Id = id;
            this.Type = type;
            this.Manufacturer = manufacturer;
            this.ProductInfo = productInfo;
            this.VersionInfo = versionInfo;
            this.TenantId = tenantId;

            if( !( capabilities is null ) )
            {
                foreach( IDialogName dialog in capabilities )
                {
                    if( !( dialog is null ) )
                    {
                        this.Capabilities.Add( new Capability( dialog ) );
                    }
                }
            }
        }

        public Subscriber(  SubscriberInfo subscriberInfo,
                            IEnumerable<IDialogName> capabilities    )
        {
            subscriberInfo.ThrowIfNull();

            this.Id = subscriberInfo.Id;
            this.Type = subscriberInfo.Type;
            this.Manufacturer = subscriberInfo.Manufacturer;
            this.ProductInfo = subscriberInfo.ProductInfo;
            this.VersionInfo = subscriberInfo.VersionInfo;
            this.TenantId = subscriberInfo.TenantId;

            if( !( capabilities is null ) )
            {
                foreach( IDialogName dialog in capabilities )
                {
                    if( !( dialog is null ) )
                    {
                        this.Capabilities.Add( new Capability( dialog ) );
                    }
                }
            }
        }

        public SubscriberId Id
        {
            get{ return this.id; }

            private set
            {
                value.ThrowIfNull();

                this.id = value;
            }
        }

        public SubscriberType Type
        {
            get;
        } = SubscriberType.IMS;

        public String Manufacturer
        {
            get{ return this.manufacturer; }
            
            private set
            {
                value.ThrowIfNullOrEmpty();

                this.manufacturer = value;
            }
        }

        public String ProductInfo
        {
            get{ return this.productInfo; }
            
            private set
            {
                value.ThrowIfNullOrEmpty();

                this.productInfo = value;
            }
        }

        public String VersionInfo
        {
            get{ return this.versionInfo; }
            
            private set
            {
                value.ThrowIfNullOrEmpty();

                this.versionInfo = value;
            }
        }

        public String TenantId
        {
            get;
        }

        private List<Capability> Capabilities
        {
            get;
        } = new List<Capability>();
        
        public Capability[] GetCapabilities()
        {
            return this.Capabilities.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as Subscriber );
		}
		
		public bool Equals( Subscriber other )
		{
            return Subscriber.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}

        public override String ToString()
        {
            return this.Id.ToString();
        }
    }
}
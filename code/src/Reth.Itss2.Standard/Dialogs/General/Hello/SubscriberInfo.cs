using System;
using System.Reflection;

using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.General.Hello
{
    public class SubscriberInfo
    {
        public SubscriberInfo(  SubscriberId id,
                                SubscriberType type,
                                String manufacturer,
                                String tenantId,
                                String productInfo  )
        :
            this(   id,
                    type,
                    manufacturer,
                    tenantId,
                    productInfo,
                    Assembly.GetCallingAssembly().GetName().Version.ToString()  )
        {
        }

        public SubscriberInfo(  SubscriberId id,
                                SubscriberType type,
                                String manufacturer,
                                String tenantId,
                                String productInfo,
                                String versionInfo  )
        {
            manufacturer.ThrowIfNull();
            productInfo.ThrowIfNull();
            versionInfo.ThrowIfNull();

            this.Id = id;
            this.Type = type;
            this.Manufacturer = manufacturer;
            this.TenantId = tenantId;
            this.ProductInfo = productInfo;
            this.VersionInfo = versionInfo;
        }

        public SubscriberId Id
        {
            get;
        }

        public SubscriberType Type
        {
            get;
        }

        public String Manufacturer
        {
            get;
        }
        
        public String TenantId
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
    }
}

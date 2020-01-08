using System;
using System.Configuration;

namespace Reth.Protocols.Transfer.Tcp.Configuration
{
    internal class IPv6Element:ConfigurationElement
    {
        public const String EnabledName = "enabled";

        public const bool EnabledDefaultValue = false;

        private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

        private static ConfigurationProperty enabled = new ConfigurationProperty(   IPv6Element.EnabledName,
                                                                                    typeof( bool ),
                                                                                    IPv6Element.EnabledDefaultValue,
                                                                                    null,
                                                                                    null,
                                                                                    ConfigurationPropertyOptions.None   );

        static IPv6Element()
        {
            IPv6Element.properties.Add( IPv6Element.enabled );
        }

        [ConfigurationProperty( IPv6Element.EnabledName )]
        public bool Enabled
        {
            get{ return ( bool )( base[ IPv6Element.enabled ] ); }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get{ return IPv6Element.properties; }
        }
    }
}

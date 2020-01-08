using System;
using System.Configuration;

namespace Reth.Protocols.Transfer.Tcp.Configuration
{
    internal class IPv4Element:ConfigurationElement
    {
        public const String EnabledName = "enabled";
        
        public const bool EnabledDefaultValue = true;
        
        private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

        private static ConfigurationProperty enabled = new ConfigurationProperty(   IPv4Element.EnabledName,
                                                                                    typeof( bool ),
                                                                                    IPv4Element.EnabledDefaultValue,
                                                                                    null,
                                                                                    null,
                                                                                    ConfigurationPropertyOptions.None   );

        static IPv4Element()
        {
            IPv4Element.properties.Add( IPv4Element.enabled );
        }

        [ConfigurationProperty( IPv4Element.EnabledName )]
        public bool Enabled
        {
            get{ return ( bool )( base[ IPv4Element.enabled ] ); }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get{ return IPv4Element.properties; }
        }
    }
}

using System;
using System.Configuration;

namespace Reth.Protocols.Transfer.Tcp.Configuration
{
    internal class ListenersElement:ConfigurationElement
    {
        public const String IPv4Name = "ipv4";
        public const String IPv6Name = "ipv6";

        private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

        private static ConfigurationProperty ipv4 = new ConfigurationProperty(  ListenersElement.IPv4Name,
                                                                                typeof( IPv4Element ),
                                                                                null,
                                                                                ConfigurationPropertyOptions.None   );

        private static ConfigurationProperty ipv6 = new ConfigurationProperty(  ListenersElement.IPv6Name,
                                                                                typeof( IPv6Element ),
                                                                                null,
                                                                                ConfigurationPropertyOptions.None   );

        static ListenersElement()
        {
            ListenersElement.properties.Add( ListenersElement.ipv4 );
            ListenersElement.properties.Add( ListenersElement.ipv6 );
        }

        [ConfigurationProperty( ListenersElement.IPv4Name )]
        public IPv4Element IPv4
        {
            get{ return ( IPv4Element )( base[ ListenersElement.ipv4 ] ); }
        }

        [ConfigurationProperty( ListenersElement.IPv6Name )]
        public IPv6Element IPv6
        {
            get{ return ( IPv6Element )( base[ ListenersElement.ipv6 ] ); }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get{ return ListenersElement.properties; }
        }
    }
}
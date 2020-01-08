using System;
using System.Configuration;

namespace Reth.Protocols.Transfer.Tcp.Configuration
{
    internal class ServerSection:ConfigurationSection
    {
        public const String ListenersName = "listeners";
        public const String MaxClientConnectionsName = "maxClientConnections";

        public const int MaxClientConnectionsDefaultValue = 20;

        private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

        private static ConfigurationProperty listeners = new ConfigurationProperty( ServerSection.ListenersName,
                                                                                    typeof( ListenersElement ),
                                                                                    null,
                                                                                    ConfigurationPropertyOptions.None   );

        private static ConfigurationProperty maxClientConnections = new ConfigurationProperty(  ServerSection.MaxClientConnectionsName,     
                                                                                                typeof( int ),
                                                                                                ServerSection.MaxClientConnectionsDefaultValue,
                                                                                                null,
                                                                                                new IntegerValidator( 1, int.MaxValue, false ),
                                                                                                ConfigurationPropertyOptions.None   );

        static ServerSection()
        {
            ServerSection.properties.Add( ServerSection.listeners );
            ServerSection.properties.Add( ServerSection.maxClientConnections );
        }

        [ConfigurationProperty( ServerSection.ListenersName )]
        public ListenersElement Listeners
        {
            get{ return ( ListenersElement )( base[ ServerSection.listeners ] ); }
        }

        [ConfigurationProperty( ServerSection.MaxClientConnectionsName )]
        public int MaxClientConnections
        {
            get{ return ( int )( base[ ServerSection.maxClientConnections ] ); }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get{ return ServerSection.properties; }
        }
    }
}
using System;
using System.Configuration;

namespace Reth.Protocols.Transfer.Tcp.Configuration
{
    internal class SectionGroup:ConfigurationSectionGroup
    {
        public const String ConfigName = "reth.protocols.transfer.tcp";

        public const String ServerSectionName = "server";

        public SectionGroup()
        {
        }

        [ConfigurationProperty( SectionGroup.ServerSectionName )]
        public ServerSection Server
        {
            get{ return ( ServerSection )( base.Sections[ SectionGroup.ServerSectionName ] ); }
        }
    }
}
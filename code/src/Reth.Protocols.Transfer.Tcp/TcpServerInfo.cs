using System;
using System.Globalization;

using Reth.Protocols.Extensions.Int32Extensions;

namespace Reth.Protocols.Transfer.Tcp
{
    public class TcpServerInfo
    {
        public TcpServerInfo(   int port,
                                int maxClientConnections,
                                bool enableIPv4Listening,
                                bool enableIPv6Listening    )
        {
            port.ThrowIfNegative();
            maxClientConnections.ThrowIfNotPositive();

            this.Port = port;
            this.MaxClientConnections = maxClientConnections;
            this.EnableIPv4Listening = enableIPv4Listening;
            this.EnableIPv6Listening = enableIPv6Listening;
        }

        public int Port
        {
            get;
        }

        public int MaxClientConnections
        {
            get;
        }

        public bool EnableIPv4Listening
        {
            get;
        }

        public bool EnableIPv6Listening
        {
            get;
        }

        public override String ToString()
        {
            return this.Port.ToString( CultureInfo.InvariantCulture );
        }
    }
}

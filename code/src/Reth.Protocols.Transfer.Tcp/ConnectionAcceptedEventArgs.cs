using System;
using System.Net.Sockets;

using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Protocols.Transfer.Tcp
{
    public class ConnectionAcceptedEventArgs:EventArgs
    {
        public ConnectionAcceptedEventArgs( TcpClient tcpClient )
        {
            tcpClient.ThrowIfNull();

            this.TcpClient = tcpClient;
        }

        public TcpClient TcpClient
        {
            get;
        }

        public override String ToString()
        {
            return this.TcpClient.ToString();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net.Sockets;

using Reth.Protocols.Diagnostics;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Protocols.Transfer.Tcp
{
    public abstract class TcpMessageServer:MessageServer
    {
        protected TcpMessageServer( TcpServerInfo serverInfo )
        {
            serverInfo.ThrowIfNull();
            
            this.ServerInfo = serverInfo;

            ExecutionLogProvider.LogInformation( $"Listening on port: { serverInfo.Port }" );
            ExecutionLogProvider.LogInformation( $"Maximum allowed client connections for TCP: { serverInfo.MaxClientConnections }" );
            ExecutionLogProvider.LogInformation( $"Enable IPv4: { serverInfo.EnableIPv4Listening }" );
            ExecutionLogProvider.LogInformation( $"Enable IPv6: { serverInfo.EnableIPv6Listening }" );
    
            this.Connections = new RemoteMessageClientCollection( serverInfo.MaxClientConnections );
        }

        public TcpServerInfo ServerInfo
        {
            get;
        }

        protected override IEnumerable<IMessageServerListener> CreateListeners()
        {
            List<TcpMessageServerListener> listeners = new List<TcpMessageServerListener>();

            if( this.ServerInfo.EnableIPv4Listening == true )
            {
                TcpMessageServerListener[] listenersIPv4 = TcpMessageServerListener.CreateIPv4Listeners( this.ServerInfo.Port );

                foreach( TcpMessageServerListener listener in listenersIPv4 )
                {
                    listener.ConnectionAccepted += this.Listener_ConnectionAccepted;
                }

                listeners.AddRange( listenersIPv4 );
            }

            if( this.ServerInfo.EnableIPv6Listening == true )
            {
                TcpMessageServerListener[] listenersIPv6 = TcpMessageServerListener.CreateIPv6Listeners( this.ServerInfo.Port );

                foreach( TcpMessageServerListener listener in listenersIPv6 )
                {
                    listener.ConnectionAccepted += this.Listener_ConnectionAccepted;
                }

                listeners.AddRange( listenersIPv6 );
            }

            return listeners;
        }

        private void Listener_ConnectionAccepted( Object sender, ConnectionAcceptedEventArgs e )
        {
            lock( this.Connections.SyncRoot )
            {
                if( this.Connections.HasMaximumReached == false )
                {
                    ExecutionLogProvider.LogInformation( "Creating message client." );

                    IRemoteMessageClient messageClient = this.CreateMessageClient( e.TcpClient );

                    this.Connections.Add( messageClient );
                }else
                {
                    ExecutionLogProvider.LogInformation( "Number of maximum allowed connections has been reached." );

                    e.TcpClient.Dispose();
                }
            }
        }

        protected abstract IRemoteMessageClient CreateMessageClient( TcpClient tcpClient );
    }
}
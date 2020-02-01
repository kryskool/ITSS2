using System;
using System.Collections.Generic;
using System.Net.Sockets;

using Reth.Protocols.Diagnostics;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Protocols.Transfer.Tcp
{
    public abstract class TcpMessageServer:MessageServer
    {
        private volatile bool isDisposed;

        protected TcpMessageServer( TcpServerInfo serverInfo )
        {
            serverInfo.ThrowIfNull();
            
            this.ServerInfo = serverInfo;

            ExecutionLogProvider.LogInformation( $"Listening on port: { serverInfo.Port }" );
            ExecutionLogProvider.LogInformation( $"Maximum allowed client connections for TCP: { serverInfo.MaxClientConnections }" );
            ExecutionLogProvider.LogInformation( $"Enable IPv4: { serverInfo.EnableIPv4Listening }" );
            ExecutionLogProvider.LogInformation( $"Enable IPv6: { serverInfo.EnableIPv6Listening }" );
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
            String remoteEndPoint = String.Empty;

            try
            {
                remoteEndPoint = e.TcpClient.Client.RemoteEndPoint.ToString();
            }catch( Exception ex )
            {
                ExecutionLogProvider.LogError( ex );
            }

            lock( this.SyncRoot )
            {
                try
                {
                    if( this.CanAccept() == true )
                    {
                        ExecutionLogProvider.LogInformation( $"Connection '{ remoteEndPoint }' is accepted." );

                        this.OnConnectionAccepted( e.TcpClient );
                    }else
                    {
                        ExecutionLogProvider.LogInformation( $"Connection '{ remoteEndPoint }' is not accepted." );

                        e.TcpClient.Dispose();
                    }
                }catch( Exception ex )
                {
                    ExecutionLogProvider.LogError( ex );
                    ExecutionLogProvider.LogError( $"Failed to accept connection '{ remoteEndPoint }'." );
                }
            }
        }

        protected abstract bool CanAccept();
        
        protected abstract void OnConnectionAccepted( TcpClient tcpClient );

        protected override void Dispose( bool disposing )
        {
            ExecutionLogProvider.LogInformation( "Disposing." );

            if( this.isDisposed == false )
            {
                lock( this.SyncRoot )
                {
                    foreach( IMessageServerListener listener in this.GetListeners() )
                    {
                        TcpMessageServerListener tcpMessageServerListener = listener as TcpMessageServerListener;

                        if( !( tcpMessageServerListener is null ) )
                        {
                            tcpMessageServerListener.ConnectionAccepted -= this.Listener_ConnectionAccepted;
                        }
                    }
                }
                
                this.isDisposed = true;
            }

            base.Dispose();
        }
    }
}
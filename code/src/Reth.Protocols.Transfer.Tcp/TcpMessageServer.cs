using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Sockets;

using Reth.Protocols.Diagnostics;
using Reth.Protocols.Extensions.Int32Extensions;
using Reth.Protocols.Transfer.Tcp.Configuration;

namespace Reth.Protocols.Transfer.Tcp
{
    public abstract class TcpMessageServer:MessageServer
    {
        protected TcpMessageServer( int port )
        {
            port.ThrowIfNegative();

            ExecutionLogProvider.LogInformation( $"Listening on port: { port }" );

            this.Port = port;

            System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration( ConfigurationUserLevel.None );

            SectionGroup group = configuration.GetSectionGroup( SectionGroup.ConfigName ) as SectionGroup;

            int maxClientConnections = ServerSection.MaxClientConnectionsDefaultValue;

            bool enableIPv4Listening = IPv4Element.EnabledDefaultValue;
            bool enableIPv6Listening = IPv6Element.EnabledDefaultValue;

            if( !( group is null ) )
            {
                ServerSection server = group.Server;

                ListenersElement listenersElement = server.Listeners;

                maxClientConnections = server.MaxClientConnections;

                enableIPv4Listening = listenersElement.IPv4.Enabled;
                enableIPv6Listening = listenersElement.IPv6.Enabled;
            }

            ExecutionLogProvider.LogInformation( $"Maximum allowed client connections for TCP: { maxClientConnections }" );

            this.MaxClientConnections = maxClientConnections;
            this.EnableIPv4Listening = enableIPv4Listening;
            this.EnableIPv6Listening = enableIPv6Listening;

            this.Connections = new RemoteMessageClientCollection( maxClientConnections );
        }

        protected TcpMessageServer( int port,
                                    int maxClientConnections,
                                    bool enableIPv4Listening,
                                    bool enableIPv6Listening    )
        {
            port.ThrowIfNotPositive();
            maxClientConnections.ThrowIfNotPositive();

            ExecutionLogProvider.LogInformation( $"Listening on port: { port }" );
            ExecutionLogProvider.LogInformation( $"Maximum allowed client connections for TCP: { maxClientConnections }" );

            this.Port = port;
            this.MaxClientConnections = maxClientConnections;
            this.EnableIPv4Listening = enableIPv4Listening;
            this.EnableIPv6Listening = enableIPv6Listening;

            this.Connections = new RemoteMessageClientCollection( maxClientConnections );
        }

        public override int MaxClientConnections
        {
            get;
        }

        public int Port
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

        protected override IEnumerable<IMessageServerListener> CreateListeners()
        {
            List<TcpMessageServerListener> listeners = new List<TcpMessageServerListener>();

            if( this.EnableIPv4Listening == true )
            {
                TcpMessageServerListener[] listenersIPv4 = TcpMessageServerListener.CreateIPv4Listeners( this.Port );

                foreach( TcpMessageServerListener listener in listenersIPv4 )
                {
                    listener.ConnectionAccepted += this.Listener_ConnectionAccepted;
                }

                listeners.AddRange( listenersIPv4 );
            }

            if( this.EnableIPv6Listening == true )
            {
                TcpMessageServerListener[] listenersIPv6 = TcpMessageServerListener.CreateIPv6Listeners( this.Port );

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
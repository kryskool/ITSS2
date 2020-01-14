using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using Reth.Protocols.Diagnostics;
using Reth.Protocols.Extensions.EventArgsExtensions;
using Reth.Protocols.Transfer.Tcp.Extensions.TcpClientExtensions;

namespace Reth.Protocols.Transfer.Tcp
{
    public class TcpMessageServerListener:MessageServerListener
    {
        public static TcpMessageServerListener[] CreateIPv4Listeners( int port )
        {
            List<TcpMessageServerListener> result = new List<TcpMessageServerListener>();

            if( Socket.OSSupportsIPv4 == true )
            {
                HashSet<IPAddress> addresses = TcpMessageServerListener.GetAddresses( AddressFamily.InterNetwork );

                if( addresses.Contains( IPAddress.Loopback ) == false )
                {
                    addresses.Add( IPAddress.Loopback );
                }

                result = TcpMessageServerListener.CreateListeners( port, addresses );
            }else
            {
                ExecutionLogProvider.LogWarning( "No IPv4 support by the operating system." );
            }

            return result.ToArray();
        }

        public static TcpMessageServerListener[] CreateIPv6Listeners( int port )
        {
            List<TcpMessageServerListener> result = new List<TcpMessageServerListener>();

            if( Socket.OSSupportsIPv6 == true )
            {
                HashSet<IPAddress> addresses = TcpMessageServerListener.GetAddresses( AddressFamily.InterNetworkV6 );

                if( addresses.Contains( IPAddress.IPv6Loopback ) == false )
                {
                    addresses.Add( IPAddress.IPv6Loopback );
                }

                result = TcpMessageServerListener.CreateListeners( port, addresses );
            }else
            {
                ExecutionLogProvider.LogWarning( "No IPv6 support by the operating system." );
            }

            return result.ToArray();
        }

        private static List<TcpMessageServerListener> CreateListeners( int port, HashSet<IPAddress> addresses )
        {
            List<TcpMessageServerListener> result = new List<TcpMessageServerListener>( addresses.Count );

            foreach( IPAddress address in addresses )
            {
                if( !( address is null ) )
                {
                    try
                    {
                        ExecutionLogProvider.LogInformation( $"Creating listener for endpoint: { address }:{ port }" );

                        TcpMessageServerListener listener = new TcpMessageServerListener( new IPEndPoint( address, port ) );

                        result.Add( listener );
                    }catch( ArgumentOutOfRangeException ex )
                    {
                        ExecutionLogProvider.LogError( ex );
                    }
                }
            }

            return result;
        }

        private static HashSet<IPAddress> GetAddresses( AddressFamily addressFamily )
        {
            IEnumerable<IPAddress> addresses = Dns.GetHostAddresses(    Dns.GetHostName() ).Where(  ( IPAddress address ) =>
                                                                                                    {
                                                                                                        bool predicate = false;
                                                                                                    
                                                                                                        if( !( address is null ) )
                                                                                                        {
                                                                                                            if( address.AddressFamily == addressFamily )
                                                                                                            {
                                                                                                                predicate = true;
                                                                                                            }
                                                                                                        }

                                                                                                        return predicate;
                                                                                                    }   );

            return new HashSet<IPAddress>( addresses );
        }

        public event EventHandler<ConnectionAcceptedEventArgs> ConnectionAccepted;

        public TcpMessageServerListener( IPEndPoint endPoint )
        {
            this.Listener = new TcpListener( endPoint );
        }

        private TcpListener Listener
        {
            get;
        }

        public override String LocalName
        {
            get{ return this.EndPoint.ToString(); }
        }

        public IPEndPoint EndPoint
        {
            get{ return ( IPEndPoint )( this.Listener.LocalEndpoint ); }
        }

        protected override void OnStart()
        {
            this.Listener.Start();
        }

        protected override void OnTerminate()
        {
            this.Listener.Stop();
        }

        protected override void OnAccept()
        {
            TcpClient tcpClient = null;

            try
            {
                tcpClient = this.Listener.AcceptTcpClient();
                tcpClient.SetKeepAlive();
                tcpClient.SetReceiveBufferSize();
                tcpClient.SetSendBufferSize();

                this.ConnectionAccepted?.SafeInvoke( this, new ConnectionAcceptedEventArgs( tcpClient ) );
            }catch( SocketException ex )
            {
                if( ex.SocketErrorCode == SocketError.Interrupted )
                {
                    ExecutionLogProvider.LogInformation( "Accept of tcp listener interrupted." );
                }else
                {
                    CleanupOnError();
                }
            }catch( Exception ex )
            {
                ExecutionLogProvider.LogError( ex );

                CleanupOnError();

                throw;
            }

            void CleanupOnError()
            {
                if( !( tcpClient is null ) )
                {
                    ExecutionLogProvider.LogError( "Closing tcp client." );

                    tcpClient.Close();
                }
            }
        }
    }
}

using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

using Reth.Protocols.Diagnostics;
using Reth.Protocols.Extensions.ObjectExtensions;
using Reth.Protocols.Transfer.Tcp.Extensions.TcpClientExtensions;

namespace Reth.Protocols.Transfer.Tcp
{
	public class LocalMessageClient:ILocalMessageClient, IDisposable
	{
        private static TimeSpan EstablishConnectionTimeout
        {
            get
            {
#if DEBUG
                TimeSpan result = TimeSpan.FromSeconds( 30 );

                if( Debugger.IsAttached == true )
                {
                    result = TimeSpan.FromMinutes( 20 );
                }

                return result;
#else
                return TimeSpan.FromSeconds( 30 );    
#endif
            }
        }

        private bool isConnected;
        private volatile bool isDisposed;

        public event EventHandler Disconnected
        {
            add
            {
                this.MessageTransceiver.Terminated += value;
            }

            remove
            {
                this.MessageTransceiver.Terminated -= value;
            }
        }

        public LocalMessageClient(  IMessageTransceiver messageTransceiver,
                                    IPEndPoint localEndPoint   )
        {
            messageTransceiver.ThrowIfNull();
            localEndPoint.ThrowIfNull();

            this.MessageTransceiver = messageTransceiver;
            this.LocalEndPoint = localEndPoint;
        }

        ~LocalMessageClient()
        {
            this.Dispose( false );
        }

        private Object SyncRoot
        {
            get;
        } = new Object();

        private bool IsConnected
        {
            get
            {
                lock( this.SyncRoot )
                {
                    return this.isConnected;
                }
            }

            set
            {
                lock( this.SyncRoot )
                {
                    this.isConnected = value;
                }
            }
        }

        public IMessageTransceiver MessageTransceiver
        {
            get;
        }

        public String LocalName
        {
            get{ return this.LocalEndPoint.ToString(); }
        }

        public String RemoteName
        {
            get
            {
                String result = String.Empty;

                if( !( this.RemoteEndPoint is null ) )
                {
                    result = this.RemoteEndPoint.ToString();
                }

                return result;
            }
        }

        public IPEndPoint LocalEndPoint
        {
            get;
        }

        public IPEndPoint RemoteEndPoint
        {
            get
            {
                lock( this.SyncRoot )
                {
                    IPEndPoint result = null;

                    try
                    {
                        result = ( IPEndPoint )( this.TcpClient.Client.RemoteEndPoint );
                    }catch( Exception ex )
                    {
                        ExecutionLogProvider.LogWarning( ex );
                        ExecutionLogProvider.LogWarning( "Remote endpoint is not available." );
                    }

                    return result;
                }
            }
        }

        private TcpClient TcpClient
        {
            get; set;
        }

        private MessageStream Stream
        {
            get; set;
        }

        public void Connect()
        {
            lock( this.SyncRoot )
            {
                if( this.IsConnected == false )
                {
                    ExecutionLogProvider.LogInformation( "Connecting local message client." );

                    TcpClient tcpClient = this.TcpClient = new TcpClient();

                    try
                    {
                        IPAddress address = this.LocalEndPoint.Address;
                        int port = this.LocalEndPoint.Port;

                        if( tcpClient.ConnectAsync( address, port ).Wait( LocalMessageClient.EstablishConnectionTimeout ) == true )
                        {
                            tcpClient.SetKeepAlive();
                            tcpClient.SetReceiveBufferSize();
                            tcpClient.SetSendBufferSize();

                            this.Stream = new MessageStream( tcpClient.GetStream() );

                            this.MessageTransceiver.Start( this.Stream );

                            this.IsConnected = true;
                        }else
                        {
                            ExecutionLogProvider.LogInformation( "Connecting local message client has timed out." );
                        }
                    } catch( Exception ex )
                    {
                        this.TcpClient?.Dispose();

                        throw new TransferException( "Connecting failed.", ex );
                    }
                }
            }
        }

        public void Disconnect()
        {
            lock( this.SyncRoot )
            {
                if( this.IsConnected == true )
                {
                    ExecutionLogProvider.LogInformation( "Disconnecting local message client." );

                    try
                    {
                        ExecutionLogProvider.LogInformation( "Shutting down tcp client." );

                        this.TcpClient.Client.Shutdown( SocketShutdown.Both );

                        ExecutionLogProvider.LogInformation( "Disposing tcp client." );

                        this.TcpClient.Dispose();
                        this.TcpClient = null;

                        this.MessageTransceiver.Terminate();
                    }catch( Exception ex )
                    {
                        ExecutionLogProvider.LogError( ex );
                    }

                    this.IsConnected = false;
                }
            }
        }

        public void Dispose()
        {
            this.Dispose( true );

            GC.SuppressFinalize( this );
        }

        protected virtual void Dispose( bool disposing )
        {
            ExecutionLogProvider.LogInformation( "Disposing local message client." );

            if( this.isDisposed == false )
            {
                if( disposing == true )
                {
                    lock( this.SyncRoot )
                    {
                        this.Disconnect();

                        this.MessageTransceiver.Dispose();
                        this.Stream.Dispose();
                    }
                }

                this.isDisposed = true;
            }
        }
	}
}
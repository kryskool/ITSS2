using System;
using System.Net;
using System.Net.Sockets;

using Reth.Protocols.Diagnostics;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Protocols.Transfer.Tcp
{
    public class RemoteMessageClient:IRemoteMessageClient
    {
        private bool isStarted;
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

        public RemoteMessageClient( IMessageTransceiver messageTransceiver,
                                    TcpClient tcpClient  )
        {
            messageTransceiver.ThrowIfNull();
            tcpClient.ThrowIfNull();

            this.MessageTransceiver = messageTransceiver;
            this.TcpClient = tcpClient;

            this.Stream = new MessageStream( tcpClient.GetStream() );
        }

        ~RemoteMessageClient()
        {
            this.Dispose( false );
        }

        private Object SyncRoot
        {
            get;
        } = new Object();

        private bool IsStarted
        {
            get
            {
                lock( this.SyncRoot )
                {
                    return this.isStarted;
                }
            }

            set
            {
                lock( this.SyncRoot )
                {
                    this.isStarted = value;
                }
            }
        }

        public IMessageTransceiver MessageTransceiver
        {
            get;
        }

        public String LocalName
        {
            get
            {
                String result = String.Empty;

                if( !( this.LocalEndPoint is null ) )
                {
                    result = this.LocalEndPoint.ToString();
                }

                return result;
            }
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
            get
            {
                IPEndPoint result = null;

                try
                {
                    result = ( IPEndPoint )( this.TcpClient.Client.LocalEndPoint );
                }catch( Exception ex )
                {
                    ExecutionLogProvider.LogWarning( ex );
                    ExecutionLogProvider.LogWarning( "Local endpoint is not available." );
                }

                return result;
            }
        }

        public IPEndPoint RemoteEndPoint
        {
            get
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

        private TcpClient TcpClient
        {
            get; set;
        }

        private MessageStream Stream
        {
            get; set;
        }

        public void Start()
        {
            lock( this.SyncRoot )
            {
                if( this.IsStarted == false )
                {
                    ExecutionLogProvider.LogInformation( "Starting remote message client." );

                    try
                    {
                        this.MessageTransceiver.Start( this.Stream );
                    }catch( Exception ex )
                    {
                        throw new TransferException( "Starting of remote message client failed.", ex );
                    }

                    this.IsStarted = true;
                }
            }
        }

        public void Terminate()
        {
            lock( this.SyncRoot )
            {
                if( this.IsStarted == true )
                {
                    ExecutionLogProvider.LogInformation( "Terminating remote message client." );

                    try
                    {
                        ExecutionLogProvider.LogInformation( "Shutting down tcp client." );

                        this.TcpClient.Client.Shutdown( SocketShutdown.Both );

                        this.MessageTransceiver.Terminate();
                    }catch( Exception ex )
                    {
                        ExecutionLogProvider.LogError( ex );
                    }

                    this.IsStarted = false;
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
            ExecutionLogProvider.LogInformation( "Disposing remote message client." );

            if( this.isDisposed == false )
            {
                if( disposing == true )
                {
                    lock( this.SyncRoot )
                    {
                        this.Terminate();

                        this.MessageTransceiver.Dispose();

                        ExecutionLogProvider.LogInformation( "Disposing tcp client." );

                        this.TcpClient.Dispose();
                        this.Stream.Dispose();
                    }
                }

                this.isDisposed = true;
            }
        }
    }
}

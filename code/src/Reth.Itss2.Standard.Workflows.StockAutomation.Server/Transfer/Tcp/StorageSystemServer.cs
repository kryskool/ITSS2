using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

using Reth.Itss2.Standard.Dialogs.General.Hello;
using Reth.Itss2.Standard.Serialization;
using Reth.Protocols.Diagnostics;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.ObjectExtensions;
using Reth.Protocols.Transfer.Tcp;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Server.Transfer.Tcp
{
    public class StorageSystemServer:TcpMessageServer, IStorageSystemServer
    {
        private volatile bool isDisposed;

        public StorageSystemServer( SubscriberInfo subscriberInfo,
                                    IProtocolProvider protocolProvider,
                                    Func<String, String, IInteractionLog> createInteractionLogCallback,
                                    IEnumerable<IDialogName> supportedDialogs,
                                    TcpServerInfo serverInfo    )
        :
            base( serverInfo )
        {
            subscriberInfo.ThrowIfNull();
            protocolProvider.ThrowIfNull();
            supportedDialogs.ThrowIfNull();

            this.SubscriberInfo = subscriberInfo;
            this.ProtocolProvider = protocolProvider;
            this.CreateInteractionLogCallback = createInteractionLogCallback;
            this.SupportedDialogs = supportedDialogs;

            this.StorageSystems = new StorageSystemCollection( serverInfo.MaxClientConnections, this.SyncRoot );
        }        

        public SubscriberInfo SubscriberInfo
        {
            get;
        }

        public StorageSystemCollection StorageSystems
        {
            get;
        }

        public IProtocolProvider ProtocolProvider
        {
            get;
        }

        public Func<String, String, IInteractionLog> CreateInteractionLogCallback
        {
            get;
        }

        public IEnumerable<IDialogName> SupportedDialogs
        {
            get;
        }

        private void StorageSystem_Disconnected( Object sender, EventArgs e )
        {
            IStorageSystem storageSystem = ( IStorageSystem )sender;

            lock( this.SyncRoot )
            {
                this.StorageSystems.Remove( storageSystem );
                
                storageSystem.Dispose();
            }
        }

        protected override bool CanAccept()
        {
            lock( this.SyncRoot )
            {
                return !( this.StorageSystems.HasMaximumReached );
            }
        }
        
        protected override void OnConnectionAccepted( TcpClient tcpClient )
        {
            lock( this.SyncRoot )
            {
                IInteractionLog interactionLog = this.CreateInteractionLog( tcpClient );
                IStorageSystem storageSystem = this.CreateStorageSystem(    this.ProtocolProvider,
                                                                            interactionLog,
                                                                            this.SupportedDialogs,
                                                                            tcpClient   );

                storageSystem.Disconnected += this.StorageSystem_Disconnected;
                
                this.StorageSystems.Add( storageSystem );

                storageSystem.Start();
            }
        }

        private IInteractionLog CreateInteractionLog( TcpClient tcpClient )
        {
            IInteractionLog result = null;

            try
            {
                Socket client = tcpClient.Client;

                String localName = ( ( IPEndPoint )( client.LocalEndPoint ) ).ToString();
                String remoteName = ( ( IPEndPoint )( client.RemoteEndPoint ) ).ToString();

                result = this.CreateInteractionLogCallback?.Invoke( localName, remoteName );
            }catch( Exception ex )
            {
                ExecutionLogProvider.LogError( ex );
                ExecutionLogProvider.LogError( "Failed to create interaction log." );
            }

            return result;
        }

        protected virtual IStorageSystem CreateStorageSystem(   IProtocolProvider protocolProvider,
                                                                IInteractionLog interactionLog,
                                                                IEnumerable<IDialogName> supportedDialogs,
                                                                TcpClient tcpClient  )
        {
            StorageSystemFactory storageSystemFactory = new StorageSystemFactory( this.SubscriberInfo );

            return storageSystemFactory.CreateTcp(  protocolProvider,
                                                    interactionLog,
                                                    supportedDialogs,
                                                    tcpClient   );
        }

        public override void Terminate()
        {
            lock( this.SyncRoot )
            {
                base.Terminate();

                foreach( IStorageSystem storageSystem in this.StorageSystems )
                {
                    storageSystem.Dispose();
                }

                this.StorageSystems.Clear();
            }
        }

        protected override void Dispose( bool disposing )
        {
            ExecutionLogProvider.LogInformation( "Disposing." );

            if( this.isDisposed == false )
            {
                if( disposing == true )
                {
                    lock( this.SyncRoot )
                    {
                        this.Terminate();

                        foreach( IStorageSystem storageSystem in this.StorageSystems )
                        {
                            storageSystem.Dispose();
                        }

                        this.StorageSystems.Clear();
                    }
                }

                this.isDisposed = true;
            }

            base.Dispose( disposing );
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

using Reth.Itss2.Standard.Dialogs.General.Hello;
using Reth.Itss2.Standard.Serialization;
using Reth.Protocols.Diagnostics;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.ObjectExtensions;
using Reth.Protocols.Transfer;
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

            base.Connections.Removed += this.Connections_Removed;
            this.StorageSystems.Removed += this.StorageSystems_Removed;
        }        

        public SubscriberInfo SubscriberInfo
        {
            get;
        }

        public StorageSystemCollection StorageSystems
        {
            get;
        } = new StorageSystemCollection();

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

        private void Connections_Removed( Object sender, RemoteMessageClientEventArgs e )
        {
            IRemoteMessageClient messageClient = e.MessageClient;

            lock( this.StorageSystems.SyncRoot )
            {
                IStorageSystem remove = null;

                foreach( IStorageSystem storageSystem in this.StorageSystems )
                {
                    if( storageSystem.MessageClient.LocalName.Equals( messageClient.LocalName, StringComparison.OrdinalIgnoreCase ) == true )
                    {
                        remove = storageSystem;

                        break;
                    }
                }

                this.StorageSystems.Remove( remove );
            }
        }

        private void StorageSystems_Removed( Object sender, StorageSystemEventArgs e )
        {
            IStorageSystem storageSystem = e.StorageSystem;

            base.Connections.Remove( storageSystem.MessageClient );
        }

        private void StorageSystem_Disconnected( Object sender, EventArgs e )
        {
            IStorageSystem storageSystem = ( IStorageSystem )sender;

            this.StorageSystems.Remove( storageSystem );
        }

        protected override IRemoteMessageClient CreateMessageClient( TcpClient tcpClient )
        {
            lock( this.StorageSystems.SyncRoot )
            {
                IInteractionLog interactionLog = this.CreateInteractionLog( tcpClient );
                IStorageSystem storageSystem = this.CreateStorageSystem(    this.ProtocolProvider,
                                                                            interactionLog,
                                                                            this.SupportedDialogs,
                                                                            tcpClient   );

                storageSystem.Disconnected += this.StorageSystem_Disconnected;
                storageSystem.Run();
                
                this.StorageSystems.Add( storageSystem );
                        
                return storageSystem.MessageClient;
            }
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

        public override void Start()
        {
            lock( this.StorageSystems.SyncRoot )
            {
                base.Start();
            }
        }

        public override void Terminate()
        {
            IStorageSystem[] storageSystems = null;

            lock( this.StorageSystems.SyncRoot )
            {
                storageSystems = new IStorageSystem[ this.StorageSystems.Count ];

                this.StorageSystems.CopyTo( storageSystems );
            }

            base.Terminate();

            foreach( IStorageSystem storageSystem in storageSystems )
            {
                storageSystem.Terminate();
            }
        }

        protected override void Dispose( bool disposing )
        {
            ExecutionLogProvider.LogInformation( "Disposing." );

            if( this.isDisposed == false )
            {
                if( disposing == true )
                {
                    this.Terminate();
                }

                this.isDisposed = true;
            }

            base.Dispose( disposing );
        }
    }
}

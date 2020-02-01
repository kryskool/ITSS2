using System;
using System.Collections.Generic;

using Reth.Itss2.Standard.Dialogs.General.Hello;
using Reth.Itss2.StandardExtensions.Dialogs;
using Reth.Itss2.StandardExtensions.Dialogs.Storage.ConfigurationGet;
using Reth.Protocols;
using Reth.Protocols.Diagnostics;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Transfer;

namespace Reth.Itss2.StandardExtensions.Workflows.StockAutomation.Server
{
    public class StorageSystem:Reth.Itss2.Standard.Workflows.StockAutomation.Server.StorageSystem, IStorageSystem
    {
        private volatile bool isDisposed;

        public StorageSystem(   SubscriberInfo subscriberInfo,
                                IRemoteMessageClient messageClient,
                                IRemoteClientDialogProvider dialogProvider   )
        :
            base(   subscriberInfo,
                    messageClient,
                    dialogProvider  )
        {
            dialogProvider.ConfigurationGet.RequestReceived += this.ConfigurationGet_RequestReceived;
        }

        public StorageSystem(   SubscriberInfo subscriberInfo,
                                IRemoteMessageClient messageClient,
                                IRemoteClientDialogProvider dialogProvider,
                                IEnumerable<IDialogName> supportedDialogs   )
        :
            base(   subscriberInfo,
                    messageClient,
                    dialogProvider,
                    supportedDialogs    )
        {
            dialogProvider.ConfigurationGet.RequestReceived += this.ConfigurationGet_RequestReceived;
        }

        public Func<IStorageSystem, ConfigurationGetRequest, ConfigurationGetResponse> ConfigurationGetRequestReceivedCallback
        {
            get; set;
        }

        private void ConfigurationGet_RequestReceived( Object sender, MessageReceivedEventArgs e )
        {
            e.IsHandled = true;
              
            try
            {
                ConfigurationGetResponse response = this.ConfigurationGetRequestReceivedCallback.Invoke( this, ( ConfigurationGetRequest )( e.Message ) );

                IRemoteClientDialogProvider dialogProvider = ( IRemoteClientDialogProvider )( this.DialogProvider );

                dialogProvider.ConfigurationGet.SendResponse( response );
            }catch( Exception ex )
            {
                ExecutionLogProvider.LogError( ex );
            }
        }

        protected override void Dispose( bool disposing )
        {
            ExecutionLogProvider.LogInformation( "Disposing storage system." );

            if( this.isDisposed == false )
            {
                IRemoteClientDialogProvider dialogProvider = ( IRemoteClientDialogProvider )this.DialogProvider;
                
                dialogProvider.ConfigurationGet.RequestReceived -= this.ConfigurationGet_RequestReceived;

                this.isDisposed = true;
            }

            base.Dispose();
        }
    }
}

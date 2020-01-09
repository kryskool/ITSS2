using System.Collections.Generic;

using Reth.Itss2.StandardExtensions.Dialogs.Storage.ConfigurationGet;
using Reth.Itss2.StandardExtensions.Serialization;
using Reth.Protocols.Diagnostics;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.StandardExtensions.Dialogs
{
    public class RemoteClientDialogProvider:Reth.Itss2.Standard.Dialogs.RemoteClientDialogProvider, Reth.Itss2.StandardExtensions.Dialogs.IRemoteClientDialogProvider
    {      
        private volatile bool isDisposed;

        public RemoteClientDialogProvider(  IProtocolProvider protocolProvider,
                                            IInteractionLog interactionLog  )
        :
            this( protocolProvider, interactionLog, DialogName.GetAvailableNames() )
        {
        }

        public RemoteClientDialogProvider(  IProtocolProvider protocolProvider,
                                            IInteractionLog interactionLog,
                                            IEnumerable<IDialogName> supportedDialogs   )
        :
            base( protocolProvider, interactionLog, supportedDialogs )
        {
            this.ConfigurationGet = new ConfigurationGetServerDialog( this );
        }

        public IConfigurationGetServerDialog ConfigurationGet
        {
            get;
        }

        protected override void Dispose( bool disposing )
        {
            ExecutionLogProvider.LogInformation( "Disposing remote client dialog provider." );

            if( this.isDisposed == false )
            {
                this.isDisposed = true;
            }

            base.Dispose( disposing );
        }
    }
}
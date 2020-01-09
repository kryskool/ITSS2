using System.Collections.Generic;

using Reth.Itss2.StandardExtensions.Dialogs.Storage.ConfigurationGet;
using Reth.Itss2.StandardExtensions.Serialization;
using Reth.Protocols.Diagnostics;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.StandardExtensions.Dialogs
{
    public class LocalClientDialogProvider:Reth.Itss2.Standard.Dialogs.LocalClientDialogProvider, Reth.Itss2.StandardExtensions.Dialogs.ILocalClientDialogProvider
    {        
        private volatile bool isDisposed;

        public LocalClientDialogProvider(   IProtocolProvider protocolProvider,
                                            IInteractionLog interactionLog  )
        :
            this(   protocolProvider,
                    interactionLog,
                    DialogName.GetAvailableNames()  )
        {
        }

        public LocalClientDialogProvider(   IProtocolProvider protocolProvider,
                                            IInteractionLog interactionLog,
                                            IEnumerable<IDialogName> supportedDialogs   )
        :
            base( protocolProvider, interactionLog, supportedDialogs )
        {
            this.ConfigurationGet = new ConfigurationGetClientDialog( this );
        }

        public IConfigurationGetClientDialog ConfigurationGet
        {
            get;
        }

        protected override void Dispose( bool disposing )
        {
            ExecutionLogProvider.LogInformation( "Disposing local client dialog provider." );

            if( this.isDisposed == false )
            {
                this.isDisposed = true;
            }

            base.Dispose( disposing );
        }
    }
}
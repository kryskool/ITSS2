using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.StandardExtensions.Dialogs;
using Reth.Itss2.StandardExtensions.Dialogs.Storage.ConfigurationGet;
using Reth.Itss2.Standard.Dialogs.General.Hello;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Transfer;

namespace Reth.Itss2.StandardExtensions.Workflows.StockAutomation.Client
{
    public class StorageSystem:Reth.Itss2.Standard.Workflows.StockAutomation.Client.StorageSystem, IStorageSystem
    {
        public StorageSystem(   SubscriberInfo subscriberInfo,
                                ILocalMessageClient messageClient,
                                ILocalClientDialogProvider dialogProvider   )
        :
            base(   subscriberInfo,
                    messageClient,
                    dialogProvider  )
        {
        }

        public StorageSystem(   SubscriberInfo subscriberInfo,
                                ILocalMessageClient messageClient,
                                ILocalClientDialogProvider dialogProvider,
                                IEnumerable<IDialogName> supportedDialogs   )
        :
            base(   subscriberInfo,
                    messageClient,
                    dialogProvider,
                    supportedDialogs    )
        {
        }
        
        private ConfigurationGetRequest CreateConfigurationGetRequest()
        {
            return new ConfigurationGetRequest( Reth.Itss2.Standard.Dialogs.MessageId.NewId(),
                                                this.LocalSubscriber.Id,
                                                this.RemoteSubscriber.Id    );
        }

        public ConfigurationGetResponse GetConfiguration()
        {
            this.VerifyCapability( DialogName.ConfigurationGet );

            ILocalClientDialogProvider dialogProvider = ( ILocalClientDialogProvider )( this.DialogProvider );

            return dialogProvider.ConfigurationGet.SendRequest( this.CreateConfigurationGetRequest() );
        }

        public Task<ConfigurationGetResponse> GetConfigurationAsync()
        {
            return this.GetConfigurationAsync( CancellationToken.None );
        }

        public Task<ConfigurationGetResponse> GetConfigurationAsync( CancellationToken cancellationToken )
        {
            this.VerifyCapability( DialogName.ConfigurationGet );
         
            ILocalClientDialogProvider dialogProvider = ( ILocalClientDialogProvider )( this.DialogProvider );

            return dialogProvider.ConfigurationGet.SendRequestAsync( this.CreateConfigurationGetRequest(), cancellationToken );
        }
    }
}

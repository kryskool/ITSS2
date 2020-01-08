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
            ConfigurationGetResponse result = null;

            ConfigurationGetRequest request = this.CreateConfigurationGetRequest();

            if( base.VerifyCapability( DialogName.ConfigurationGet, request ) == true )
            {
                ILocalClientDialogProvider dialogProvider = ( ILocalClientDialogProvider )( this.DialogProvider );

                result = dialogProvider.ConfigurationGet.SendRequest( request );
            }

            return result;
        }

        public Task<ConfigurationGetResponse> GetConfigurationAsync()
        {
            return this.GetConfigurationAsync( CancellationToken.None );
        }

        public Task<ConfigurationGetResponse> GetConfigurationAsync( CancellationToken cancellationToken )
        {
            Task<ConfigurationGetResponse> result = Task.FromResult<ConfigurationGetResponse>( null );

            ConfigurationGetRequest request = this.CreateConfigurationGetRequest();

            if( base.VerifyCapability( DialogName.ConfigurationGet, request ) == true )
            {
                ILocalClientDialogProvider dialogProvider = ( ILocalClientDialogProvider )( this.DialogProvider );

                result = dialogProvider.ConfigurationGet.SendRequestAsync( request, cancellationToken );
            }

            return result;
        }
    }
}

using System;
using System.Diagnostics;

using Reth.Itss2.Standard.Dialogs.Storage.Input;
using Reth.Protocols;
using Reth.Protocols.Diagnostics;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Server.Processing.Input
{
    internal class InputStatusCreated:InputStatus
    {
        public InputStatusCreated( StorageSystem storageSystem, IMessageId id )
        :
            base( storageSystem, id )
        {
        }

        private InputRequest CreateInputRequest(    InputRequestArticle article,
                                                    Nullable<bool> isNewDelivery,
                                                    Nullable<bool> setPickingIndicator  )
        {
            return new InputRequest(    this.Id,
                                        this.StorageSystem.LocalSubscriber.Id,
                                        this.StorageSystem.RemoteSubscriber.Id,
                                        article,
                                        isNewDelivery,
                                        setPickingIndicator );
        }

        public override InputResponse Start(    InputProcess process,
                                                InputRequestArticle article,
                                                Nullable<bool> isNewDelivery,
                                                Nullable<bool> setPickingIndicator )
        {
            process.ThrowIfNull();

            InputRequest request = this.CreateInputRequest( article,
                                                            isNewDelivery,
                                                            setPickingIndicator );

            ExecutionLogProvider.LogInformation( "Sending input request." );

            IInputServerDialog dialog = this.StorageSystem.DialogProvider.Input;

            InputResponse response = dialog.SendRequest( request );

            process.Status = new InputStatusStarted( this.StorageSystem, this.Id );
            
            return response;
        }

        public override void Finish(    InputProcess process,
                                        InputMessageArticle article,
                                        Nullable<bool> isNewDelivery    )
        {
            Debug.Assert( false, "false" );

            throw new InvalidOperationException();
        }
    }
}

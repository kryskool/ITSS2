using System;
using System.Diagnostics;

using Reth.Itss2.Standard.Dialogs.Storage.Input;
using Reth.Protocols;
using Reth.Protocols.Diagnostics;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Server.Processing.Input
{
    internal class InputStatusStarted:InputStatus
    {
        public InputStatusStarted( StorageSystem storageSystem, IMessageId id )
        :
            base( storageSystem, id )
        {
        }

        private InputMessage CreateInputMessage(    InputMessageArticle article,
                                                    Nullable<bool> isNewDelivery  )
        {
            return new InputMessage(    this.Id,
                                        this.StorageSystem.LocalSubscriber.Id,
                                        this.StorageSystem.RemoteSubscriber.Id,
                                        article,
                                        isNewDelivery   );
        }

        public override InputResponse Start(    InputProcess process,
                                                InputRequestArticle article,
                                                Nullable<bool> isNewDelivery,
                                                Nullable<bool> setPickingIndicator )
        {
            Debug.Assert( false, "false" );

            throw new InvalidOperationException();
        }

        public override void Finish(    InputProcess process,
                                        InputMessageArticle article,
                                        Nullable<bool> isNewDelivery    )
        {
            process.ThrowIfNull();

            InputMessage message = this.CreateInputMessage( article, isNewDelivery );

            ExecutionLogProvider.LogInformation( "Sending input message." );

            IInputServerDialog dialog = this.StorageSystem.DialogProvider.Input;

            dialog.SendMessage( message );

            process.Status = new InputStatusFinished( this.StorageSystem, this.Id );
        }
    }
}

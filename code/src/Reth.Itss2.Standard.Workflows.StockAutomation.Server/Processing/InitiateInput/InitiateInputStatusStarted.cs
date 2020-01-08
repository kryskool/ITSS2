using System;
using System.Collections.Generic;
using System.Diagnostics;

using Reth.Itss2.Standard.Dialogs.Storage;
using Reth.Itss2.Standard.Dialogs.Storage.InitiateInput;
using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Server.Processing.InitiateInput
{
    internal class InitiateInputStatusStarted:InitiateInputStatus
    {
        public InitiateInputStatusStarted( StorageSystem storageSystem, IMessageId id )
        :
            base( storageSystem, id )
        {
        }

        private InitiateInputMessage CreateInitiateInputMessage(    InitiateInputMessageDetails details,
                                                                    InitiateInputMessageArticle article )
        {
            return new InitiateInputMessage(    this.Id,
                                                this.StorageSystem.LocalSubscriber.Id,
                                                this.StorageSystem.RemoteSubscriber.Id,
                                                details,
                                                article );
        }

        public override void Start( InitiateInputProcess process,
                                    InitiateInputResponseDetails details,
                                    InitiateInputResponseArticle article    )
        {
            Debug.Assert( false, "false" );

            throw new InvalidOperationException();
        }

        public override void Finish(    InitiateInputProcess process,
                                        InitiateInputMessageDetails details,
                                        InitiateInputMessageArticle article    )
        {
            process.ThrowIfNull();

            InitiateInputMessage message = this.CreateInitiateInputMessage( details, article );

            IInitiateInputServerDialog dialog = this.StorageSystem.DialogProvider.InitiateInput;

            dialog.SendMessage( message );

            process.Status = new InitiateInputStatusFinished( this.StorageSystem, this.Id );
        }
    }
}

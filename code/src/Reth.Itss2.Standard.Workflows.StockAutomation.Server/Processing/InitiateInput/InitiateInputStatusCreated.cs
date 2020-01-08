using System;
using System.Collections.Generic;
using System.Diagnostics;

using Reth.Itss2.Standard.Dialogs.Storage;
using Reth.Itss2.Standard.Dialogs.Storage.InitiateInput;
using Reth.Protocols.Diagnostics;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Server.Processing.InitiateInput
{
    internal class InitiateInputStatusCreated:InitiateInputStatus
    {
        public InitiateInputStatusCreated( StorageSystem storageSystem, InitiateInputRequest request )
        :
            base( storageSystem )
        {
            request.ThrowIfNull();

            this.Request = request;
            this.Id = request.Id;
        }

        private InitiateInputRequest Request
        {
            get;
        }

        private InitiateInputResponse CreateInitiateInputResponse(  InitiateInputResponseDetails details,
                                                                    InitiateInputResponseArticle article    )
        {
            return new InitiateInputResponse( this.Request, details, article );
        }

        public override void Start( InitiateInputProcess process,
                                    InitiateInputResponseDetails details,
                                    InitiateInputResponseArticle article    )
        {
            process.ThrowIfNull();

            InitiateInputResponse response = this.CreateInitiateInputResponse( details, article );

            IInitiateInputServerDialog dialog = this.StorageSystem.DialogProvider.InitiateInput;

            ExecutionLogProvider.LogInformation( "Sending initiate input response." );

            dialog.SendResponse( response );

            process.Status = new InitiateInputStatusStarted( this.StorageSystem, this.Id );
        }

        public override void Finish(    InitiateInputProcess process,
                                        InitiateInputMessageDetails details,
                                        InitiateInputMessageArticle article    )
        {
            Debug.Assert( false, "false" );

            throw new InvalidOperationException();
        }
    }
}

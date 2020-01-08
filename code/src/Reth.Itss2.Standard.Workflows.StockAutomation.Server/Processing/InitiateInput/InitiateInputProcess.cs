using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Standard.Dialogs.Storage;
using Reth.Itss2.Standard.Dialogs.Storage.InitiateInput;
using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Server.Processing.InitiateInput
{
    internal class InitiateInputProcess:IInitiateInputProcess
    {
        public InitiateInputProcess( StorageSystem storageSystem, InitiateInputRequest request )
        {
            storageSystem.ThrowIfNull();
            request.ThrowIfNull();

            this.StorageSystem = storageSystem;
            this.Id = request.Id;

            this.Status = new InitiateInputStatusCreated( storageSystem, request );
        }

        public StorageSystem StorageSystem{ get; }
        public IMessageId Id{ get; }

        internal InitiateInputStatus Status
        {
            get; set;
        }

        public void Start(  InitiateInputResponseDetails details,
                            InitiateInputResponseArticle article    )
        {
            this.Status.Start( this, details, article );
        }

        public Task StartAsync( InitiateInputResponseDetails details,
                                InitiateInputResponseArticle article )
        {
            return this.StartAsync( details, article, CancellationToken.None );
        }

        public Task StartAsync( InitiateInputResponseDetails details,
                                InitiateInputResponseArticle article,
                                CancellationToken cancellationToken )
        {
            return Task.Run(    () =>
                                {
                                    this.Start( details, article );
                                },
                                cancellationToken );
        }

        public void Finish( InitiateInputMessageDetails details,
                            InitiateInputMessageArticle article )
        {
            this.Status.Finish( this, details, article );
        }

        public void FinishAsync(    InitiateInputMessageDetails details,
                                    InitiateInputMessageArticle article )
        {
            this.FinishAsync(   details,
                                article,
                                CancellationToken.None  );
        }

        public void FinishAsync(    InitiateInputMessageDetails details,
                                    InitiateInputMessageArticle article,
                                    CancellationToken cancellationToken )
        {
            Task.Run(   () =>
                        {
                            this.Finish( details, article );
                        },
                        cancellationToken );
        }
    }
}

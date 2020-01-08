using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Standard.Dialogs.Storage;
using Reth.Itss2.Standard.Dialogs.Storage.Output;
using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Server.Processing.Output
{
    internal class OutputProcess:IOutputProcess
    {
        public OutputProcess( StorageSystem storageSystem, OutputRequest request )
        {
            storageSystem.ThrowIfNull();
            request.ThrowIfNull();

            this.StorageSystem = storageSystem;
            this.Id = request.Id;

            this.Status = new OutputStatusCreated( storageSystem, request );
        }

        public StorageSystem StorageSystem{ get; }
        public IMessageId Id{ get; }

        internal OutputStatus Status
        {
            get; set;
        }

        public void Start( OutputResponseDetails details )
        {
            this.Status.Start( this, details );
        }

        public Task StartAsync( OutputResponseDetails details )
        {
            return this.StartAsync( details, CancellationToken.None );
        }

        public Task StartAsync( OutputResponseDetails details, CancellationToken cancellationToken )
        {
            return Task.Run(    () =>
                                {
                                    this.Start( details );
                                },
                                cancellationToken );
        }

        public void ReportProgress( OutputMessageDetails details,
                                    IEnumerable<OutputArticle> articles,
                                    IEnumerable<OutputBox> boxes    )
        {
            this.Status.ReportProgress( this, details, articles, boxes );
        }

        public void ReportProgressAsync(    OutputMessageDetails details,
                                            IEnumerable<OutputArticle> articles,
                                            IEnumerable<OutputBox> boxes    )
        {
            this.ReportProgressAsync(   details,
                                        articles,
                                        boxes,
                                        CancellationToken.None  );
        }

        public void ReportProgressAsync(    OutputMessageDetails details,
                                            IEnumerable<OutputArticle> articles,
                                            IEnumerable<OutputBox> boxes,
                                            CancellationToken cancellationToken )
        {
            Task.Run(   () =>
                        {
                            this.ReportProgress( details, articles, boxes );
                        },
                        cancellationToken );
        }

        public void Finish( OutputMessageDetails details,
                            IEnumerable<OutputArticle> articles,
                            IEnumerable<OutputBox> boxes    )
        {
            this.Status.Finish( this, details, articles, boxes );
        }

        public void FinishAsync(    OutputMessageDetails details,
                                    IEnumerable<OutputArticle> articles,
                                    IEnumerable<OutputBox> boxes    )
        {
            this.FinishAsync(   details,
                                articles,
                                boxes,
                                CancellationToken.None  );
        }

        public void FinishAsync(    OutputMessageDetails details,
                                    IEnumerable<OutputArticle> articles,
                                    IEnumerable<OutputBox> boxes,
                                    CancellationToken cancellationToken )
        {
            Task.Run(   () =>
                        {
                            this.Finish( details, articles, boxes );
                        },
                        cancellationToken );
        }
    }
}

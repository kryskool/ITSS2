using System.Collections.Generic;

using Reth.Itss2.Standard.Dialogs.Storage;
using Reth.Itss2.Standard.Dialogs.Storage.Output;
using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Server.Processing.Output
{
    internal abstract class OutputStatus
    {
        private IMessageId id;

        protected OutputStatus( StorageSystem storageSystem )
        {
            storageSystem.ThrowIfNull();

            this.StorageSystem = storageSystem;
        }

        protected OutputStatus( StorageSystem storageSystem, IMessageId id )
        {
            storageSystem.ThrowIfNull();
            id.ThrowIfNull();

            this.StorageSystem = storageSystem;
            this.Id = id;
        }

        public StorageSystem StorageSystem{ get; }
        
        public IMessageId Id
        {
            get{ return this.id; }

            protected set
            {
                value.ThrowIfNull();

                this.id = value;
            }
        }

        public abstract void Start( OutputProcess process, OutputResponseDetails details );
        
        public abstract void ReportProgress(    OutputProcess process,
                                                OutputMessageDetails details,
                                                IEnumerable<OutputArticle> articles,
                                                IEnumerable<OutputBox> boxes    );

        public abstract void Finish(    OutputProcess process,
                                        OutputMessageDetails details,
                                        IEnumerable<OutputArticle> articles,
                                        IEnumerable<OutputBox> boxes    );
    }
}

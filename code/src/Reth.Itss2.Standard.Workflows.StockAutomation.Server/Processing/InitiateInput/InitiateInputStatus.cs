using Reth.Itss2.Standard.Dialogs.Storage.InitiateInput;
using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Server.Processing.InitiateInput
{
    internal abstract class InitiateInputStatus
    {
        private IMessageId id;

        protected InitiateInputStatus( StorageSystem storageSystem )
        {
            storageSystem.ThrowIfNull();

            this.StorageSystem = storageSystem;
        }

        protected InitiateInputStatus( StorageSystem storageSystem, IMessageId id )
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

        public abstract void Start( InitiateInputProcess process,
                                    InitiateInputResponseDetails details,
                                    InitiateInputResponseArticle article    );
        
        public abstract void Finish(    InitiateInputProcess process,
                                        InitiateInputMessageDetails details,
                                        InitiateInputMessageArticle article    );
    }
}

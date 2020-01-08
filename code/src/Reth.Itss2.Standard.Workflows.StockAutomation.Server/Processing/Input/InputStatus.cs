using System;

using Reth.Itss2.Standard.Dialogs.Storage.Input;
using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Server.Processing.Input
{
    internal abstract class InputStatus
    {
        protected InputStatus( StorageSystem storageSystem, IMessageId id )
        {
            storageSystem.ThrowIfNull();
            id.ThrowIfNull();

            this.StorageSystem = storageSystem;
            this.Id = id;
        }

        public StorageSystem StorageSystem{ get; }
        public IMessageId Id{ get; }

        public abstract InputResponse Start(    InputProcess process,
                                                InputRequestArticle article,
                                                Nullable<bool> isNewDelivery,
                                                Nullable<bool> setPickingIndicator );

        public abstract void Finish(    InputProcess process,
                                        InputMessageArticle article,
                                        Nullable<bool> isNewDelivery    );
    }
}

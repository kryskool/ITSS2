using System;
using System.Threading;
using System.Threading.Tasks;

using Reth.Itss2.Standard.Dialogs;
using Reth.Itss2.Standard.Dialogs.Storage.Input;
using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Server.Processing.Input
{
    internal class InputProcess:IInputProcess
    {
        public InputProcess( StorageSystem storageSystem )
        :
            this( storageSystem, MessageId.NewId() )
        {
        }

        public InputProcess( StorageSystem storageSystem, IMessageId id )
        {
            storageSystem.ThrowIfNull();
            id.ThrowIfNull();

            this.StorageSystem = storageSystem;
            this.Id = id;

            this.Status = new InputStatusCreated( storageSystem, this.Id );
        }

        public StorageSystem StorageSystem{ get; }
        public IMessageId Id{ get; }

        internal InputStatus Status
        {
            get; set;
        }

        public InputResponse Start( InputRequestArticle article,
                                    Nullable<bool> isNewDelivery,
                                    Nullable<bool> setPickingIndicator  )
        {
            return this.Status.Start( this, article, isNewDelivery, setPickingIndicator );
        }

        public Task<InputResponse> StartAsync(  InputRequestArticle article,
                                                Nullable<bool> isNewDelivery,
                                                Nullable<bool> setPickingIndicator  )
        {
            return this.StartAsync( article, isNewDelivery, setPickingIndicator, CancellationToken.None );
        }

        public Task<InputResponse> StartAsync(  InputRequestArticle article,
                                                Nullable<bool> isNewDelivery,
                                                Nullable<bool> setPickingIndicator,
                                                CancellationToken cancellationToken )
        {
            return Task.Run(    () =>
                                {
                                    return this.Start( article, isNewDelivery, setPickingIndicator );
                                },
                                cancellationToken );
        }

        public void Finish( InputMessageArticle article, Nullable<bool> isNewDelivery )
        {
            this.Status.Finish( this, article, isNewDelivery );
        }

        public Task FinishAsync( InputMessageArticle article, Nullable<bool> isNewDelivery )
        {
            return this.FinishAsync( article, isNewDelivery, CancellationToken.None );
        }

        public Task FinishAsync( InputMessageArticle article, Nullable<bool> isNewDelivery, CancellationToken cancellationToken )
        {
            return Task.Run(    () =>
                                {
                                    this.Finish( article, isNewDelivery );
                                },
                                cancellationToken );
        }
    }
}

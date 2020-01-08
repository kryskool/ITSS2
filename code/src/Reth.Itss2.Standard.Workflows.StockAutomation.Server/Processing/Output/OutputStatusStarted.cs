using System;
using System.Collections.Generic;
using System.Diagnostics;

using Reth.Itss2.Standard.Dialogs.Storage;
using Reth.Itss2.Standard.Dialogs.Storage.Output;
using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Server.Processing.Output
{
    internal class OutputStatusStarted:OutputStatus
    {
        public OutputStatusStarted( StorageSystem storageSystem, IMessageId id )
        :
            base( storageSystem, id )
        {
        }

        private OutputMessage CreateOutputMessage(  OutputMessageDetails details,
                                                    IEnumerable<OutputArticle> articles,
                                                    IEnumerable<OutputBox> boxes    )
        {
            return new OutputMessage(   this.Id,
                                        this.StorageSystem.LocalSubscriber.Id,
                                        this.StorageSystem.RemoteSubscriber.Id,
                                        details,
                                        articles,
                                        boxes   );
        }

        public override void Start( OutputProcess process, OutputResponseDetails details )
        {
            Debug.Assert( false, "false" );

            throw new InvalidOperationException();
        }

        public override void ReportProgress(    OutputProcess process,
                                                OutputMessageDetails details,
                                                IEnumerable<OutputArticle> articles,
                                                IEnumerable<OutputBox> boxes    )
        {
            process.ThrowIfNull();

            OutputMessage message = this.CreateOutputMessage( details, articles, boxes );

            IOutputServerDialog dialog = this.StorageSystem.DialogProvider.Output;

            dialog.SendMessage( message );
        }

        public override void Finish(    OutputProcess process,
                                        OutputMessageDetails details,
                                        IEnumerable<OutputArticle> articles,
                                        IEnumerable<OutputBox> boxes    )
        {
            process.ThrowIfNull();

            OutputMessage message = this.CreateOutputMessage( details, articles, boxes );

            IOutputServerDialog dialog = this.StorageSystem.DialogProvider.Output;

            dialog.SendMessage( message );

            process.Status = new OutputStatusFinished( this.StorageSystem, this.Id );
        }
    }
}

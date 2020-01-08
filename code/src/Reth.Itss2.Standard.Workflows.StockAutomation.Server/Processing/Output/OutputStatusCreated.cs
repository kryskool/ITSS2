using System;
using System.Collections.Generic;
using System.Diagnostics;

using Reth.Itss2.Standard.Dialogs.Storage;
using Reth.Itss2.Standard.Dialogs.Storage.Output;
using Reth.Protocols.Diagnostics;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Server.Processing.Output
{
    internal class OutputStatusCreated:OutputStatus
    {
        public OutputStatusCreated( StorageSystem storageSystem, OutputRequest request )
        :
            base( storageSystem )
        {
            request.ThrowIfNull();

            this.Request = request;
            this.Id = request.Id;
        }

        private OutputRequest Request
        {
            get;
        }

        private OutputResponse CreateOutputResponse( OutputResponseDetails details )
        {
            return new OutputResponse( this.Request, details );
        }

        public override void Start( OutputProcess process, OutputResponseDetails details )
        {
            process.ThrowIfNull();

            OutputResponse response = this.CreateOutputResponse( details );

            IOutputServerDialog dialog = this.StorageSystem.DialogProvider.Output;

            ExecutionLogProvider.LogInformation( "Sending output response." );

            dialog.SendResponse( response );

            process.Status = new OutputStatusStarted( this.StorageSystem, this.Id );
        }

        public override void ReportProgress(    OutputProcess process,
                                                OutputMessageDetails details,
                                                IEnumerable<OutputArticle> articles,
                                                IEnumerable<OutputBox> boxes    )
        {
            Debug.Assert( false, "false" );

            throw new InvalidOperationException();
        }

        public override void Finish(    OutputProcess process,
                                        OutputMessageDetails details,
                                        IEnumerable<OutputArticle> articles,
                                        IEnumerable<OutputBox> boxes    )
        {
            Debug.Assert( false, "false" );

            throw new InvalidOperationException();
        }
    }
}

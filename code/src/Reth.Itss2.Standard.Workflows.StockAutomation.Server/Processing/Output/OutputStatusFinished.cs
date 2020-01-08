using System;
using System.Collections.Generic;
using System.Diagnostics;

using Reth.Itss2.Standard.Dialogs.Storage;
using Reth.Itss2.Standard.Dialogs.Storage.Output;
using Reth.Protocols;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Server.Processing.Output
{
    internal class OutputStatusFinished:OutputStatus
    {
        public OutputStatusFinished( StorageSystem storageSystem, IMessageId id )
        :
            base( storageSystem, id )
        {
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

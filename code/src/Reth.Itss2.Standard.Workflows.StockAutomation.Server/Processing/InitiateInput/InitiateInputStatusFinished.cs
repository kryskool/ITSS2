using System;
using System.Diagnostics;

using Reth.Itss2.Standard.Dialogs.Storage.InitiateInput;
using Reth.Protocols;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Server.Processing.InitiateInput
{
    internal class InitiateInputStatusFinished:InitiateInputStatus
    {
        public InitiateInputStatusFinished( StorageSystem storageSystem, IMessageId id )
        :
            base( storageSystem, id )
        {
        }

        public override void Start( InitiateInputProcess process,
                                    InitiateInputResponseDetails details,
                                    InitiateInputResponseArticle article )
        {
            Debug.Assert( false, "false" );

            throw new InvalidOperationException();
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

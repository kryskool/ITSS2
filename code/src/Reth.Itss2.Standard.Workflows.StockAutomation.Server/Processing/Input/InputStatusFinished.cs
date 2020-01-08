using System;
using System.Diagnostics;

using Reth.Itss2.Standard.Dialogs.Storage.Input;
using Reth.Protocols;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Server.Processing.Input
{
    internal class InputStatusFinished:InputStatus
    {
        public InputStatusFinished( StorageSystem storageSystem, IMessageId id )
        :
            base( storageSystem, id )
        {
        }

        public override InputResponse Start(    InputProcess process,
                                                InputRequestArticle article,
                                                Nullable<bool> isNewDelivery,
                                                Nullable<bool> setPickingIndicator )
        {
            Debug.Assert( false, "false" );

            throw new InvalidOperationException();
        }

        public override void Finish(    InputProcess process,
                                        InputMessageArticle article,
                                        Nullable<bool> isNewDelivery    )
        {
            Debug.Assert( false, "false" );

            throw new InvalidOperationException();
        }
    }
}

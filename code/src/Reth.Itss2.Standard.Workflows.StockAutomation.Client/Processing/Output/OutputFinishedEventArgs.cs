using Reth.Itss2.Standard.Dialogs.Storage.Output;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Client.Processing.Output
{
    public class OutputFinishedEventArgs:OutputEventArgs
    {
        public OutputFinishedEventArgs( OutputMessage message )
        :
            base( message )
        {
        }
    }
}

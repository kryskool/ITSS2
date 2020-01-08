using Reth.Itss2.Standard.Dialogs.Storage.Output;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Client.Processing.Output
{
    public class OutputProcessingEventArgs:OutputEventArgs
    {
        public OutputProcessingEventArgs( OutputMessage message )
        :
            base( message )
        {
        }
    }
}

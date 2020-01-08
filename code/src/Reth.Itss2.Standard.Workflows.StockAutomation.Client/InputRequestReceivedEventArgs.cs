using Reth.Itss2.Standard.Dialogs.Storage.Input;
using Reth.Itss2.Standard.Workflows.StockAutomation.Client.Processing.Input;
using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Client
{
    public class InputRequestReceivedEventArgs:MessageReceivedEventArgs
    {
        public InputRequestReceivedEventArgs( InputRequest request, IInputProcess process )
        :
            base( request )
        {
            process.ThrowIfNull();

            this.Process = process;
        }

        public IInputProcess Process
        {
            get;
        }
    }
}

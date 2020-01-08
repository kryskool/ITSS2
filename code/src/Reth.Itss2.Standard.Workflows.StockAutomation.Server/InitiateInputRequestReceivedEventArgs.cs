using Reth.Itss2.Standard.Dialogs.Storage.InitiateInput;
using Reth.Itss2.Standard.Workflows.StockAutomation.Server.Processing.InitiateInput;
using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Server
{
    public class InitiateInputRequestReceivedEventArgs:MessageReceivedEventArgs
    {
        public InitiateInputRequestReceivedEventArgs( InitiateInputRequest request, IInitiateInputProcess process )
        :
            base( request )
        {
            process.ThrowIfNull();

            this.Process = process;
        }

        public IInitiateInputProcess Process
        {
            get;
        }
    }
}

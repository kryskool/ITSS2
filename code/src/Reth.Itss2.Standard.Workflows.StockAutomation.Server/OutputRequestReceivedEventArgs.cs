using Reth.Itss2.Standard.Dialogs.Storage.Output;
using Reth.Itss2.Standard.Workflows.StockAutomation.Server.Processing.Output;
using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Server
{
    public class OutputRequestReceivedEventArgs:MessageReceivedEventArgs
    {
        public OutputRequestReceivedEventArgs( OutputRequest request, IOutputProcess process )
        :
            base( request )
        {
            process.ThrowIfNull();

            this.Process = process;
        }

        public IOutputProcess Process
        {
            get;
        }
    }
}

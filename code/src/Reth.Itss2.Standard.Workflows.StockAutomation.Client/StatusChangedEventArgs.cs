using System;

using Reth.Itss2.Standard.Dialogs.Storage.Status;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Client
{
    public class StatusChangedEventArgs:EventArgs
    {
        public StatusChangedEventArgs( StatusResponse statusResponse )
        {
            this.StatusResponse = statusResponse;
        }

        public StatusResponse StatusResponse
        {
            get;
        }

        public override String ToString()
        {
            return this.StatusResponse.ToString();
        }
    }
}

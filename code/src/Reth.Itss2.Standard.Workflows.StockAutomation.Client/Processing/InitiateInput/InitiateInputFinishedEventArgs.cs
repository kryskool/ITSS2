using System;

using Reth.Itss2.Standard.Dialogs.Storage.InitiateInput;
using Reth.Protocols;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Client.Processing.InitiateInput
{
    public class InitiateInputFinishedEventArgs:MessageReceivedEventArgs
    {
        public InitiateInputFinishedEventArgs( InitiateInputMessage message )
        :
            base( message )
        {
        }

        public InitiateInputMessage InitiateInputMessage
        {
            get{ return ( InitiateInputMessage )( this.Message ); }
        }

        public override String ToString()
        {
            return this.Message.ToString();
        }
    }
}

using System;

using Reth.Itss2.Standard.Dialogs.Storage.Input;
using Reth.Protocols;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Client.Processing.Input
{
    public class InputFinishedEventArgs:MessageReceivedEventArgs
    {
        public InputFinishedEventArgs( InputMessage message )
        :
            base( message )
        {
        }

        public InputMessage InputMessage
        {
            get{ return ( InputMessage )( base.Message ); }
        }

        public override String ToString()
        {
            return this.Message.ToString();
        }
    }
}

using System;

using Reth.Itss2.Standard.Dialogs.Storage.Output;
using Reth.Protocols;

namespace Reth.Itss2.Standard.Workflows.StockAutomation.Client.Processing.Output
{
    public abstract class OutputEventArgs:MessageReceivedEventArgs
    {
        protected OutputEventArgs( OutputMessage message )
        :
            base( message )
        {
        }

        public OutputMessage OutputMessage
        {
            get{ return ( OutputMessage )( this.Message ); }
        }

        public override String ToString()
        {
            return this.Message.ToString();
        }
    }
}

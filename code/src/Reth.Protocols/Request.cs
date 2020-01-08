using Reth.Protocols.Dialogs;

namespace Reth.Protocols
{
    public abstract class Request:Message, IRequest
    {
        protected Request( IDialogName relatedDialog, IMessageId id )
        :
            base( relatedDialog, id )
        {
        }

        protected Request( Request other )
        :
            base( other )
        {
        }
    }
}
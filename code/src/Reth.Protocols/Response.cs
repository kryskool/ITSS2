using Reth.Protocols.Dialogs;

namespace Reth.Protocols
{
    public abstract class Response:Message, IResponse
    {
        protected Response( IDialogName relatedDialog, IMessageId id )
        :
            base( relatedDialog, id )
        {
        }

        protected Response( Request request )
        :
            base( request )
        {
        }
    }
}
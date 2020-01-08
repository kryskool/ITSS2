using Reth.Protocols.Dialogs;

namespace Reth.Protocols
{
    public interface IMessage
    {
        IMessageId Id{ get; }
        
        IDialogName RelatedDialog{ get; }

        void Dispatch( IMessageDispatcher dispatcher );
    }
}
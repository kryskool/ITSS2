using Reth.Protocols;
using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs
{
    public interface IDialogProvider:IMessageTransceiver
    {
        IDialogName[] GetSupportedDialogs();
    }
}
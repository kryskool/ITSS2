using System.Collections.Generic;

using Reth.Protocols.Dialogs;

namespace Reth.Protocols
{
    internal class MessageFilter
    {
        public MessageFilter( IEnumerable<IDialogName> supportedDialogs )
        {
            if( !( supportedDialogs is null ) )
            {
                foreach( IDialogName supportedDialog in supportedDialogs )
                {
                    this.SupportedDialogs.Add( supportedDialog );
                }
            }
        }

        private HashSet<IDialogName> SupportedDialogs
        {
            get;
        } = new HashSet<IDialogName>();

        public IEnumerable<IDialogName> GetSupportedDialogs()
        {
            return this.SupportedDialogs;
        }

        public virtual bool Granted( IMessage message )
        {
            bool result = false;
            
            if( !( message is null ) )
            {
                result = this.SupportedDialogs.Contains( message.RelatedDialog );
            }

            return result;
        }
    }
}

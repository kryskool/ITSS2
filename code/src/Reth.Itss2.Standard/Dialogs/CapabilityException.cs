using System;
using System.Runtime.Serialization;

using Reth.Protocols.Dialogs;

namespace Reth.Itss2.Standard.Dialogs
{
    [Serializable]
    public class CapabilityException:Exception
    {
        public CapabilityException()
        {
        }

        public CapabilityException( String message )
        :
            base( message )
        {
        }

        public CapabilityException( String message, Exception innerException )
        :
            base( message, innerException )
        {
        }

        public CapabilityException( String message, IDialogName capability )
        :
            base( message )
        {
            this.Capability = capability;
        }

        public CapabilityException( String message, IDialogName capability, Exception innerException )
        :
            base( message, innerException )
        {
            this.Capability = capability;
        }

        protected CapabilityException( SerializationInfo info, StreamingContext context )
        :
            base( info, context )
        {
        }

        public IDialogName Capability
        {
            get;
        }
    }
}

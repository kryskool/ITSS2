using System;

using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Protocols
{
    public abstract class Message:IMessage, IEquatable<Message>
    {
        public static bool operator==( Message left, Message right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( Message left, Message right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( Message left, Message right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        return ( left.Id == right.Id );
                                                    }   );
		}

        private IDialogName relatedDialog;
        private IMessageId id;

        protected Message( IDialogName relatedDialog, IMessageId id )
        {
            this.RelatedDialog = relatedDialog;
            this.Id = id;
        }

        protected Message( Message other )
        {
            other.ThrowIfNull();

            this.RelatedDialog = other.RelatedDialog;
            this.Id = other.Id;
        }

        public IDialogName RelatedDialog
        {
            get{ return this.relatedDialog; }

            protected internal set
            {
                value.ThrowIfNull();

                this.relatedDialog = value;
            }
        }

        public IMessageId Id
        {
            get{ return this.id; }

            protected internal set
            {
                value.ThrowIfNull();

                this.id = value;
            }
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as Message );
		}
		
        public bool Equals( Message other )
		{
            return Message.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Id.GetHashCode();
		}

        public override String ToString()
        {
            return this.Id.ToString();
        }

        public abstract void Dispatch( IMessageDispatcher dispatcher );
    }
}
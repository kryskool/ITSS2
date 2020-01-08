using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.General.Hello
{
    public class HelloRequest:Request, IEquatable<HelloRequest>
    {
        public static bool operator==( HelloRequest left, HelloRequest right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( HelloRequest left, HelloRequest right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( HelloRequest left, HelloRequest right )
		{
            bool result = Request.Equals( left, right );

            if( result == true )
            {
                result &= Subscriber.Equals( left.Subscriber, right.Subscriber );
            }

            return result;
		}

        private Subscriber subscriber;

        public HelloRequest( IMessageId id, Subscriber subscriber )
        :
            base( DialogName.Hello, id )
        {
            this.Subscriber = subscriber;
        }

        public Subscriber Subscriber
        {
            get{ return this.subscriber; }

            private set
            {
                value.ThrowIfNull();

                this.subscriber = value;
            }
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as HelloRequest );
		}
		
		public bool Equals( HelloRequest other )
		{
            return HelloRequest.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void Dispatch( IMessageDispatcher dispatcher )
        {
            dispatcher.ThrowIfNull();
            dispatcher.Dispatch( this );
        }
    }
}
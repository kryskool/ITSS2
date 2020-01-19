using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.General.Hello
{
    public class HelloResponse:Response, IEquatable<HelloResponse>
    {
        public static bool operator==( HelloResponse left, HelloResponse right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( HelloResponse left, HelloResponse right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( HelloResponse left, HelloResponse right )
		{
            bool result = Response.Equals( left, right );

            if( result == true )
            {
                result &= Subscriber.Equals( left.Subscriber, right.Subscriber );
            }

            return result;
		}

        public HelloResponse(   IMessageId id,
                                Subscriber subscriber   )
        :
            base( DialogName.Hello, id )
        {
            subscriber.ThrowIfNull();

            this.Subscriber = subscriber;
        }

        public HelloResponse(   HelloRequest request,
                                Subscriber subscriber   )
        :
            base( request )
        {
            subscriber.ThrowIfNull();

            this.Subscriber = subscriber;
        }

        public Subscriber Subscriber
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as HelloResponse );
		}
		
		public bool Equals( HelloResponse other )
		{
            return HelloResponse.Equals( this, other );
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
using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.General.Unprocessed
{
    public class UnprocessedContent:IEquatable<UnprocessedContent>
    {
        public static bool operator==( UnprocessedContent left, UnprocessedContent right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( UnprocessedContent left, UnprocessedContent right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( UnprocessedContent left, UnprocessedContent right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = Message.Equals( left.Data, right.Data );
                                                        result &= MessageId.Equals( left.Id, right.Id );

                                                        return result;
                                                    }   );
		}

        public UnprocessedContent( IMessage data )
        {
            data.ThrowIfNull();

            this.Data = data;
        }

        public UnprocessedContent( IMessage data, MessageId id )
        {
            data.ThrowIfNull();

            this.Data = data;
            this.Id = id;
        }

        public IMessage Data
        {
            get;
        }

        public MessageId Id
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as UnprocessedContent );
		}
		
		public bool Equals( UnprocessedContent other )
		{
            return UnprocessedContent.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Data.GetHashCode();
		}

        public override String ToString()
        {
            return this.Data.ToString();
        }
    }
}
using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.OutputInfo
{
    public class OutputInfoRequestTask:IEquatable<OutputInfoRequestTask>
    {
        public static bool operator==( OutputInfoRequestTask left, OutputInfoRequestTask right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( OutputInfoRequestTask left, OutputInfoRequestTask right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( OutputInfoRequestTask left, OutputInfoRequestTask right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        return MessageId.Equals( left.Id, right.Id );
                                                    }   );
		}

        private MessageId id;

        public OutputInfoRequestTask( MessageId id )
        {
            this.Id = id;
        }

        public MessageId Id
        {
            get{ return this.id; }

            private set
            {
                value.ThrowIfNull();

                this.id = value;
            }
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as OutputInfoRequestTask );
		}
		
        public bool Equals( OutputInfoRequestTask other )
		{
			return OutputInfoRequestTask.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override String ToString()
        {
            return this.Id.ToString();
        }
    }
}
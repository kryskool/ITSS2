using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.TaskCancelOutput
{
    public class TaskCancelOutputRequestTask:IEquatable<TaskCancelOutputRequestTask>
    {
        public static bool operator==( TaskCancelOutputRequestTask left, TaskCancelOutputRequestTask right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( TaskCancelOutputRequestTask left, TaskCancelOutputRequestTask right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( TaskCancelOutputRequestTask left, TaskCancelOutputRequestTask right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        return MessageId.Equals( left.Id, right.Id );
                                                    }   );
		}

        public TaskCancelOutputRequestTask( IMessageId id )
        {
            id.ThrowIfNull();

            this.Id = id;
        }

        public IMessageId Id
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as TaskCancelOutputRequestTask );
		}
		
        public bool Equals( TaskCancelOutputRequestTask other )
		{
            return TaskCancelOutputRequestTask.Equals( this, other );
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
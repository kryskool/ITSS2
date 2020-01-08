using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.TaskCancelOutput
{
    public class TaskCancelOutputResponseTask:IEquatable<TaskCancelOutputResponseTask>
    {
        public static bool operator==( TaskCancelOutputResponseTask left, TaskCancelOutputResponseTask right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( TaskCancelOutputResponseTask left, TaskCancelOutputResponseTask right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( TaskCancelOutputResponseTask left, TaskCancelOutputResponseTask right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = MessageId.Equals( left.Id, right.Id );
                                                        result &= ( left.Status == right.Status );

                                                        return result;
                                                    }   );
		}

        private MessageId id;

        public TaskCancelOutputResponseTask(    MessageId id,
                                                TaskCancelOutputStatus status    )
        {
            this.Id = id;
            this.Status = status;
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

        public TaskCancelOutputStatus Status
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as TaskCancelOutputResponseTask );
		}
		
        public bool Equals( TaskCancelOutputResponseTask other )
		{
            return TaskCancelOutputResponseTask.Equals( this, other );
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
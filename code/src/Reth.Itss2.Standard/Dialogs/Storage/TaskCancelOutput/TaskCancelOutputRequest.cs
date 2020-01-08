using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.TaskCancelOutput
{
    public class TaskCancelOutputRequest:TraceableRequest, IEquatable<TaskCancelOutputRequest>
    {
        public static bool operator==( TaskCancelOutputRequest left, TaskCancelOutputRequest right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( TaskCancelOutputRequest left, TaskCancelOutputRequest right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( TaskCancelOutputRequest left, TaskCancelOutputRequest right )
		{
			bool result = TraceableRequest.Equals( left, right );

            if( result == true )
            {
                result &= TaskCancelOutputRequestTask.Equals( left.Task, right.Task );
            }

            return result;
		}

        private TaskCancelOutputRequestTask task;

        public TaskCancelOutputRequest( IMessageId id,
                                        SubscriberId source,
                                        SubscriberId destination,
                                        TaskCancelOutputRequestTask task  )
        :
            base( DialogName.TaskCancelOutput, id, source, destination )
        {
            this.Task = task;
        }

        public TaskCancelOutputRequestTask Task
        {
            get{ return this.task; }

            private set
            {
                value.ThrowIfNull();

                this.task = value;
            }
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as TaskCancelOutputRequest );
		}
		
		public bool Equals( TaskCancelOutputRequest other )
		{
           return TaskCancelOutputRequest.Equals( this, other );
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
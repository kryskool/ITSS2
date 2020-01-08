using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.TaskCancelOutput
{
    public class TaskCancelOutputResponse:TraceableResponse, IEquatable<TaskCancelOutputResponse>
    {
        public static bool operator==( TaskCancelOutputResponse left, TaskCancelOutputResponse right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( TaskCancelOutputResponse left, TaskCancelOutputResponse right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( TaskCancelOutputResponse left, TaskCancelOutputResponse right )
		{
            bool result = TraceableResponse.Equals( left, right );

            if( result == true )
            {
                result &= TaskCancelOutputResponseTask.Equals( left.Task, right.Task );
            }

            return result;
		}

        private TaskCancelOutputResponseTask task;

        public TaskCancelOutputResponse(    IMessageId id,
                                            SubscriberId source,
                                            SubscriberId destination,
                                            TaskCancelOutputResponseTask task  )
        :
            base( DialogName.TaskCancelOutput, id, source, destination )
        {
            this.Task = task;
        }

        public TaskCancelOutputResponse(    TaskCancelOutputRequest request,
                                            TaskCancelOutputResponseTask task   )
        :
            base( request )
        {
            this.Task = task;
        }

        public TaskCancelOutputResponseTask Task
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
			return this.Equals( obj as TaskCancelOutputResponse );
		}
		
		public bool Equals( TaskCancelOutputResponse other )
		{
            return TaskCancelOutputResponse.Equals( this, other );
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
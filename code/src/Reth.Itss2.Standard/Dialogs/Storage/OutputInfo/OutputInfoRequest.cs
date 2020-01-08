using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.OutputInfo
{
    public class OutputInfoRequest:TraceableRequest, IEquatable<OutputInfoRequest>
    {
        public static bool operator==( OutputInfoRequest left, OutputInfoRequest right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( OutputInfoRequest left, OutputInfoRequest right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( OutputInfoRequest left, OutputInfoRequest right )
		{
            bool result = TraceableRequest.Equals( left, right );

            if( result == true )
            {
                result &= Nullable.Equals( left.IncludeTaskDetails, right.IncludeTaskDetails );
                result &= OutputInfoRequestTask.Equals( left.Task, right.Task );
            }

            return result;
		}

        private OutputInfoRequestTask task;

        public OutputInfoRequest(    IMessageId id,
                                     SubscriberId source,
                                     SubscriberId destination,
                                     OutputInfoRequestTask task    )
        :
            base( DialogName.OutputInfo, id, source, destination )
        {
            this.Task = task;
        }

        public OutputInfoRequest(   IMessageId id,
                                    SubscriberId source,
                                    SubscriberId destination,
                                    OutputInfoRequestTask task,
                                    Nullable<bool> includeTaskDetails   )
        :
            base( DialogName.OutputInfo, id, source, destination )
        {
            this.Task = task;
            this.IncludeTaskDetails = includeTaskDetails;
        }

        public OutputInfoRequestTask Task
        {
            get{ return this.task; }

            private set
            {
                value.ThrowIfNull();

                this.task = value;
            }
        }

        public Nullable<bool> IncludeTaskDetails
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as OutputInfoRequest );
		}
		
		public bool Equals( OutputInfoRequest other )
		{
            return OutputInfoRequest.Equals( this, other );
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
using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.OutputInfo
{
    public class OutputInfoResponse:TraceableResponse, IEquatable<OutputInfoResponse>
    {
        public static bool operator==( OutputInfoResponse left, OutputInfoResponse right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( OutputInfoResponse left, OutputInfoResponse right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( OutputInfoResponse left, OutputInfoResponse right )
		{
            bool result = TraceableResponse.Equals( left, right );

            if( result == true )
            {
                result &= OutputInfoResponseTask.Equals( left.Task, right.Task );
            }

            return result;
		}

        private OutputInfoResponseTask task;

        public OutputInfoResponse(  IMessageId id,
                                    SubscriberId source,
                                    SubscriberId destination,
                                    OutputInfoResponseTask task    )
        :
            base( DialogName.OutputInfo, id, source, destination )
        {
            this.Task = task;
        }

        public OutputInfoResponse(  OutputInfoRequest request,
                                    OutputInfoResponseTask task    )
        :
            base( request )
        {
            this.Task = task;
        }

        public OutputInfoResponseTask Task
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
			return this.Equals( obj as OutputInfoResponse );
		}
		
		public bool Equals( OutputInfoResponse other )
		{
            return OutputInfoResponse.Equals( this, other );
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
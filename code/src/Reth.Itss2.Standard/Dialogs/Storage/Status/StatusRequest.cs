using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.Status
{
    public class StatusRequest:TraceableRequest, IEquatable<StatusRequest>
    {
        public static bool operator==( StatusRequest left, StatusRequest right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( StatusRequest left, StatusRequest right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( StatusRequest left, StatusRequest right )
		{
            bool result = TraceableRequest.Equals( left, right );

            if( result == true )
            {
                result &= Nullable.Equals( left.IncludeDetails, right.IncludeDetails );
            }

            return result;
		}

        public StatusRequest(    IMessageId id,
                                 SubscriberId source,
                                 SubscriberId destination    )
        :
            base( DialogName.OutputInfo, id, source, destination )
        {
        }

        public StatusRequest(    IMessageId id,
                                 SubscriberId source,
                                 SubscriberId destination,
                                 Nullable<bool> includeDetails    )
        :
            base( DialogName.OutputInfo, id, source, destination )
        {
            this.IncludeDetails = includeDetails;
        }

        public Nullable<bool> IncludeDetails
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as StatusRequest );
		}
		
		public bool Equals( StatusRequest other )
		{
            return StatusRequest.Equals( this, other );
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
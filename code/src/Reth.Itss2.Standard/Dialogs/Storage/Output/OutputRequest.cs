using System;
using System.Collections.Generic;

using Reth.Protocols;
using Reth.Protocols.Extensions.ListExtensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.Output
{
    public class OutputRequest:TraceableRequest, IEquatable<OutputRequest>
    {
        public static bool operator==( OutputRequest left, OutputRequest right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( OutputRequest left, OutputRequest right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( OutputRequest left, OutputRequest right )
		{
            bool result = TraceableRequest.Equals( left, right );

            if( result == true )
            {
                result &= OutputRequestDetails.Equals( left.Details, right.Details );
                result &= String.Equals( left.BoxNumber, right.BoxNumber, StringComparison.InvariantCultureIgnoreCase );
                result &= left.Criterias.ElementsEqual( right.Criterias );
            }

            return result;
		}

        private OutputRequestDetails details;

        public OutputRequest(   IMessageId id,
                                SubscriberId source,
                                SubscriberId destination,
                                OutputRequestDetails details   )
        :
            base( DialogName.Output, id, source, destination )
        {
            this.Details = details;
        }

        public OutputRequest(   IMessageId id,
                                SubscriberId source,
                                SubscriberId destination,
                                OutputRequestDetails details,
                                String boxNumber,
                                IEnumerable<OutputCriteria> criterias   )
        :
            base( DialogName.Output, id, source, destination )
        {
            this.Details = details;
            this.BoxNumber = boxNumber;

            if( !( criterias is null ) )
            {
                this.Criterias.AddRange( criterias );
            }
        }

        public OutputRequestDetails Details
        {
            get{ return this.details; }

            private set
            {
                value.ThrowIfNull();

                this.details = value;
            }
        }

        public String BoxNumber
        {
            get;
        }

        private List<OutputCriteria> Criterias
        {
            get;
        } = new List<OutputCriteria>();

        public OutputCriteria[] GetCriterias()
        {
            return this.Criterias.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as OutputRequest );
		}
		
		public bool Equals( OutputRequest other )
		{
            return OutputRequest.Equals( this, other );
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
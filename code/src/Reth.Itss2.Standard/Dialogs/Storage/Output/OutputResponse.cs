using System;
using System.Collections.Generic;

using Reth.Protocols;
using Reth.Protocols.Extensions.ListExtensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.Output
{
    public class OutputResponse:TraceableResponse, IEquatable<OutputResponse>
    {
        public static bool operator==( OutputResponse left, OutputResponse right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( OutputResponse left, OutputResponse right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( OutputResponse left, OutputResponse right )
		{
            bool result = TraceableResponse.Equals( left, right );

            if( result == true )
            {
                result &= OutputResponseDetails.Equals( left.Details, right.Details );
                result &= String.Equals( left.BoxNumber, right.BoxNumber, StringComparison.InvariantCultureIgnoreCase );
                result &= left.Criterias.ElementsEqual( right.Criterias );
            }

            return result;
		}

        private OutputResponseDetails details;

        public OutputResponse(  IMessageId id,
                                SubscriberId source,
                                SubscriberId destination,
                                OutputResponseDetails details   )
        :
            base( DialogName.Output, id, source, destination )
        {
            this.Details = details;
        }

        public OutputResponse(  IMessageId id,
                                SubscriberId source,
                                SubscriberId destination,
                                OutputResponseDetails details,
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

        public OutputResponse(  OutputRequest request,
                                OutputResponseDetails details   )
        :
            base( request )
        {
            this.Details = details;
            this.BoxNumber = request.BoxNumber;

            this.Criterias.AddRange( request.GetCriterias() );
        }

        public OutputResponseDetails Details
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
			return this.Equals( obj as OutputResponse );
		}
		
		public bool Equals( OutputResponse other )
		{
            return OutputResponse.Equals( this, other );
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
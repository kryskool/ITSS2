// Implementation of the WWKS2 protocol.
// Copyright (C) 2020  Thomas Reth

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.

// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;

using Reth.Itss2.Messaging;

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.Output
{
    public class OutputRequest:SubscribedRequest, IEquatable<OutputRequest>
    {
        public static bool operator==( OutputRequest? left, OutputRequest? right )
		{
            return OutputRequest.Equals( left, right );
		}
		
		public static bool operator!=( OutputRequest? left, OutputRequest? right )
		{
			return !( OutputRequest.Equals( left, right ) );
		}

        public static bool Equals( OutputRequest? left, OutputRequest? right )
		{
            bool result = SubscribedRequest.Equals( left, right );

            result &= ( result ? OutputRequestDetails.Equals( left?.Details, right?.Details ) : false );
            result &= ( result ? String.Equals( left?.BoxNumber, right?.BoxNumber, StringComparison.OrdinalIgnoreCase ) : false );
            result &= ( result ? ( left?.Criterias.SequenceEqual( right?.Criterias ) ).GetValueOrDefault() : false );

            return result;
		}

        public OutputRequest(   MessageId id,
                                SubscriberId source,
                                SubscriberId destination,
                                OutputRequestDetails details,
                                IEnumerable<OutputCriteria> criterias  )
        :
            this( id, source, destination, details, criterias, boxNumber:null )
        {
        }

        public OutputRequest(   MessageId id,
								SubscriberId source,
                                SubscriberId destination,
                                OutputRequestDetails details,
                                IEnumerable<OutputCriteria> criterias,
                                String? boxNumber   )
        :
            base( id, StandardDialogs.Output, source, destination )
        {
            this.Details = details;
            
            this.Criterias.AddRange( criterias );

            this.BoxNumber = boxNumber;
        }

        public OutputRequestDetails Details
        {
            get;
        }

        public String? BoxNumber
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

        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as OutputRequest );
		}
		
        public bool Equals( OutputRequest? other )
		{
            return OutputRequest.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}

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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.StatusDialog
{
    public class StatusRequest:SubscribedRequest, IEquatable<StatusRequest>
    {
        public static bool operator==( StatusRequest? left, StatusRequest? right )
		{
            return StatusRequest.Equals( left, right );
		}
		
		public static bool operator!=( StatusRequest? left, StatusRequest? right )
		{
			return !( StatusRequest.Equals( left, right ) );
		}

        public static bool Equals( StatusRequest? left, StatusRequest? right )
		{
            bool result = SubscribedRequest.Equals( left, right );

            result &= ( result ? EqualityComparer<bool?>.Default.Equals( left?.IncludeDetails, right?.IncludeDetails ) : false );

            return result;
		}

        public StatusRequest(   MessageId id,
								SubscriberId source,
                                SubscriberId destination    )
        :
            base( id, Dialogs.Status, source, destination )
        {
        }

        public StatusRequest(   MessageId id,
							    SubscriberId source,
                                SubscriberId destination,
                                bool? includeDetails )
        :
            base( id, Dialogs.Status, source, destination )
        {
            this.IncludeDetails = includeDetails;
        }

        public bool? IncludeDetails
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as StatusRequest );
		}
		
        public bool Equals( StatusRequest? other )
		{
            return StatusRequest.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}

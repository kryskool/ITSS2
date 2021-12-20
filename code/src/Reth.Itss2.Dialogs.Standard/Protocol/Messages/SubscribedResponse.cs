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

using Reth.Itss2.Messaging;

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages
{
    public class SubscribedResponse:SubscribedMessage, IResponse, IEquatable<SubscribedResponse>
    {
        public static bool operator==( SubscribedResponse? left, SubscribedResponse? right )
		{
            return SubscribedResponse.Equals( left, right );
		}
		
		public static bool operator!=( SubscribedResponse? left, SubscribedResponse? right )
		{
			return !( SubscribedResponse.Equals( left, right ) );
		}

        public static bool Equals( SubscribedResponse? left, SubscribedResponse? right )
		{
            return SubscribedMessage.Equals( left, right );
		}

        protected SubscribedResponse(   MessageId id,
                                        String dialogName,
                                        SubscriberId source,
                                        SubscriberId destination )
        :
            base( id, dialogName, source, destination )
        {
        }

        protected SubscribedResponse( SubscribedRequest request )
        :
            base( request.Id, request.DialogName, request.Destination, request.Source )
        {
        }

        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as SubscribedResponse );
		}
		
        public bool Equals( SubscribedResponse? other )
		{
            return SubscribedResponse.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

        public override String? ToString()
        {
            return this.Id.ToString();
        }
    }
}

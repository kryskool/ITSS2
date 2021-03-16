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
using System.Linq;

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.InitiateInput
{
    public class InitiateInputMessage:SubscribedMessage, IEquatable<InitiateInputMessage>
    {
        public static bool operator==( InitiateInputMessage? left, InitiateInputMessage? right )
		{
            return InitiateInputMessage.Equals( left, right );
		}
		
		public static bool operator!=( InitiateInputMessage? left, InitiateInputMessage? right )
		{
			return !( InitiateInputMessage.Equals( left, right ) );
		}

        public static bool Equals( InitiateInputMessage? left, InitiateInputMessage? right )
		{
            bool result = SubscribedMessage.Equals( left, right );

            result &= ( result ? InitiateInputMessageDetails.Equals( left?.Details, right?.Details ) : false );
            result &= ( result ? ( left?.Articles.SequenceEqual( right?.Articles ) ).GetValueOrDefault() : false );

            return result;
		}

        public InitiateInputMessage(    MessageId id,
								        SubscriberId source,
                                        SubscriberId destination,
                                        InitiateInputMessageDetails details,
                                        IEnumerable<InitiateInputMessageArticle> articles  )
        :
            base( id, Dialogs.InitiateInput, source, destination )
        {
            this.Details = details;

            this.Articles.AddRange( articles );
        }

        public InitiateInputMessage(    InitiateInputRequest request,
                                        InitiateInputMessageDetails details,
                                        IEnumerable<InitiateInputMessageArticle> articles  )
        :
            this( request.Id, request.Destination, request.Source, details, articles )
        {
        }

        public InitiateInputMessageDetails Details
        {
            get;
        }

        private List<InitiateInputMessageArticle> Articles
        {
            get;
        } = new List<InitiateInputMessageArticle>();

        public InitiateInputMessageArticle[] GetArticles()
        {
            return this.Articles.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as InitiateInputMessage );
		}
		
        public bool Equals( InitiateInputMessage? other )
		{
            return InitiateInputMessage.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}

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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.Input
{
    public class InputRequest:SubscribedRequest, IEquatable<InputRequest>
    {
        public static bool operator==( InputRequest? left, InputRequest? right )
		{
            return InputRequest.Equals( left, right );
		}
		
		public static bool operator!=( InputRequest? left, InputRequest? right )
		{
			return !( InputRequest.Equals( left, right ) );
		}

        public static bool Equals( InputRequest? left, InputRequest? right )
		{
            bool result = SubscribedRequest.Equals( left, right );

            result &= ( result ? ( left?.Articles.SequenceEqual( right?.Articles ) ).GetValueOrDefault() : false );
            result &= ( result ? EqualityComparer<bool?>.Default.Equals( left?.IsNewDelivery, right?.IsNewDelivery ) : false );
            result &= ( result ? EqualityComparer<bool?>.Default.Equals( left?.SetPickingIndicator, right?.SetPickingIndicator ) : false );

            return result;
		}

        public InputRequest(    MessageId id,
								SubscriberId source,
                                SubscriberId destination,
                                IEnumerable<InputRequestArticle> articles  )
        :
            this( id, source, destination, articles, isNewDelivery:null, setPickingIndicator:null )
        {
        }

        public InputRequest(    MessageId id,
								SubscriberId source,
                                SubscriberId destination,
                                IEnumerable<InputRequestArticle> articles,
                                bool? isNewDelivery,
                                bool? setPickingIndicator   )
        :
            base( id, StandardDialogs.Input, source, destination )
        {
            this.Articles.AddRange( articles );

            this.IsNewDelivery = isNewDelivery;
            this.SetPickingIndicator = setPickingIndicator;
        }

        public bool? IsNewDelivery
        {
            get;
        }

        public bool? SetPickingIndicator
        {
            get;
        }

        private List<InputRequestArticle> Articles
        {
            get;
        } = new List<InputRequestArticle>();

        public InputRequestArticle[] GetArticles()
        {
            return this.Articles.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as InputRequest );
		}
		
        public bool Equals( InputRequest? other )
		{
            return InputRequest.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}

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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.InputDialog
{
    public class InputMessage:SubscribedMessage, IEquatable<InputMessage>
    {
        public static bool operator==( InputMessage? left, InputMessage? right )
		{
            return InputMessage.Equals( left, right );
		}
		
		public static bool operator!=( InputMessage? left, InputMessage? right )
		{
			return !( InputMessage.Equals( left, right ) );
		}

        public static bool Equals( InputMessage? left, InputMessage? right )
		{
            bool result = SubscribedMessage.Equals( left, right );

            result &= ( result ? EqualityComparer<bool?>.Default.Equals( left?.IsNewDelivery, right?.IsNewDelivery ) : false );
            result &= ( result ? ( left?.Articles.SequenceEqual( right?.Articles ) ).GetValueOrDefault() : false );

            return result;
		}

        public InputMessage(    MessageId id,
								SubscriberId source,
                                SubscriberId destination,
                                IEnumerable<InputMessageArticle>? articles  )
        :
            this( id, source, destination, articles, null )
        {
        }

        public InputMessage(    MessageId id,
								SubscriberId source,
                                SubscriberId destination,
                                IEnumerable<InputMessageArticle>? articles,
                                bool? isNewDelivery   )
        :
            base( id, source, destination )
        {
            if( articles is not null )
            {
                this.Articles.AddRange( articles );
            }

            this.IsNewDelivery = isNewDelivery;
        }

        public bool? IsNewDelivery
        {
            get;
        }

        private List<InputMessageArticle> Articles
        {
            get;
        } = new List<InputMessageArticle>();

        public InputMessageArticle[] GetArticles()
        {
            return this.Articles.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as InputMessage );
		}
		
        public bool Equals( InputMessage? other )
		{
            return InputMessage.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}

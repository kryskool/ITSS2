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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.StockInfo
{
    public class StockInfoMessage:SubscribedMessage, IEquatable<StockInfoMessage>
    {
        public static bool operator==( StockInfoMessage? left, StockInfoMessage? right )
		{
            return StockInfoMessage.Equals( left, right );
		}
		
		public static bool operator!=( StockInfoMessage? left, StockInfoMessage? right )
		{
			return !( StockInfoMessage.Equals( left, right ) );
		}

        public static bool Equals( StockInfoMessage? left, StockInfoMessage? right )
		{
            bool result = SubscribedMessage.Equals( left, right );

            result &= ( result ? ( left?.Articles.SequenceEqual( right?.Articles ) ).GetValueOrDefault() : false );

            return result;
		}

        public StockInfoMessage(    MessageId id,
                                    SubscriberId source,
                                    SubscriberId destination,
                                    IEnumerable<StockInfoArticle> articles  )
        :
            base( id, Dialogs.StockInfo, source, destination )
        {
            this.Articles.AddRange( articles );
        }

        private List<StockInfoArticle> Articles
        {
            get;
        } = new List<StockInfoArticle>();
        
        public StockInfoArticle[] GetArticles()
        {
            return this.Articles.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as StockInfoMessage );
		}
		
        public bool Equals( StockInfoMessage? other )
		{
            return StockInfoMessage.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}

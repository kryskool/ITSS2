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
    public class InputResponse:SubscribedResponse, IEquatable<InputResponse>
    {
        public static bool operator==( InputResponse? left, InputResponse? right )
		{
            return InputResponse.Equals( left, right );
		}
		
		public static bool operator!=( InputResponse? left, InputResponse? right )
		{
			return !( InputResponse.Equals( left, right ) );
		}

        public static bool Equals( InputResponse? left, InputResponse? right )
		{
            bool result = SubscribedResponse.Equals( left, right );

            result &= ( result ? EqualityComparer<bool?>.Default.Equals( left?.IsNewDelivery, right?.IsNewDelivery ) : false );
            result &= ( result ? ( left?.Articles.SequenceEqual( right?.Articles ) ).GetValueOrDefault() : false );

            return result;
		}

        public InputResponse(   MessageId id,
								SubscriberId source,
                                SubscriberId destination,
                                IEnumerable<InputResponseArticle>? articles  )
        :
            this( id, source, destination, articles, null )
        {
        }

        public InputResponse(   MessageId id,
								SubscriberId source,
                                SubscriberId destination,
                                IEnumerable<InputResponseArticle>? articles,
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

        public InputResponse(   InputRequest request,
                                IEnumerable<InputResponseArticle>? articles  )
        :
            this( request, articles, null )
        {
        }

        public InputResponse(   InputRequest request,
                                IEnumerable<InputResponseArticle>? articles,
                                bool? isNewDelivery   )
        :
            this(   request.Id,
                    request.Source,
                    request.Destination,
                    articles,
                    null    )
        {
        }

        public bool? IsNewDelivery
        {
            get;
        }

        private List<InputResponseArticle> Articles
        {
            get;
        } = new List<InputResponseArticle>();

        public InputResponseArticle[] GetArticles()
        {
            return this.Articles.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as InputResponse );
		}
		
        public bool Equals( InputResponse? other )
		{
            return InputResponse.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}

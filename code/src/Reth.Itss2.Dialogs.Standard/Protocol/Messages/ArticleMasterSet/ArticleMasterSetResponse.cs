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

namespace Reth.Itss2.Dialogs.Standard.Protocol.Messages.ArticleMasterSet
{
    public class ArticleMasterSetResponse:SubscribedResponse, IEquatable<ArticleMasterSetResponse>
    {
        public static bool operator==( ArticleMasterSetResponse? left, ArticleMasterSetResponse? right )
		{
            return ArticleMasterSetResponse.Equals( left, right );
		}
		
		public static bool operator!=( ArticleMasterSetResponse? left, ArticleMasterSetResponse? right )
		{
			return !( ArticleMasterSetResponse.Equals( left, right ) );
		}

        public static bool Equals( ArticleMasterSetResponse? left, ArticleMasterSetResponse? right )
		{
            bool result = SubscribedResponse.Equals( left, right );

            result &= ( result ? ArticleMasterSetResult.Equals( left?.Result, right?.Result ) : false );

            return result;
		}

        public ArticleMasterSetResponse(    MessageId id,
                                            SubscriberId source,
                                            SubscriberId destination,
                                            ArticleMasterSetResult result   )
        :
            base( id, StandardDialogs.ArticleMasterSet, source, destination )
        {
            this.Result = result;
        }

        public ArticleMasterSetResponse(    ArticleMasterSetRequest request,
                                            ArticleMasterSetResult result   )
        :
            base( request )
        {
            this.Result = result;
        }

        public ArticleMasterSetResult Result{ get; }
        
        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as ArticleMasterSetResponse );
		}
		
        public bool Equals( ArticleMasterSetResponse? other )
		{
            return ArticleMasterSetResponse.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}

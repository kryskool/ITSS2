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

using Reth.Itss2.Dialogs.Standard.Protocol.Messages;
using Reth.Itss2.Messaging;

namespace Reth.Itss2.Dialogs.StandardExtensions.Protocol.Messages.ArticleMasterSet
{
    public class ArticleMasterSetRequest:Standard.Protocol.Messages.ArticleMasterSet.ArticleMasterSetRequest, IEquatable<ArticleMasterSetRequest>
    {
        public static bool operator==( ArticleMasterSetRequest? left, ArticleMasterSetRequest? right )
		{
            return ArticleMasterSetRequest.Equals( left, right );
		}
		
		public static bool operator!=( ArticleMasterSetRequest? left, ArticleMasterSetRequest? right )
		{
			return !( ArticleMasterSetRequest.Equals( left, right ) );
		}

        public static bool Equals( ArticleMasterSetRequest? left, ArticleMasterSetRequest? right )
		{
            return Standard.Protocol.Messages.ArticleMasterSet.ArticleMasterSetRequest.Equals( left, right );
		}

        public ArticleMasterSetRequest( MessageId id,
									    SubscriberId source,
                                        SubscriberId destination,
                                        IEnumerable<ArticleMasterSetArticle> articles  )
        :
            base( id, source, destination, articles )
        {
        }

        public override bool Equals( Object? obj )
		{
			return this.Equals( obj as ArticleMasterSetRequest );
		}
		
        public bool Equals( ArticleMasterSetRequest? other )
		{
            return ArticleMasterSetRequest.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
    }
}

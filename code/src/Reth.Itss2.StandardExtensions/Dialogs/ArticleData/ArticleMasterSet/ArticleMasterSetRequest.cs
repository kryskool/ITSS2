using System;
using System.Collections.Generic;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;

namespace Reth.Itss2.StandardExtensions.Dialogs.ArticleData.ArticleMasterSet
{
    public class ArticleMasterSetRequest:Reth.Itss2.Standard.Dialogs.ArticleData.ArticleMasterSet.ArticleMasterSetRequest, IEquatable<ArticleMasterSetRequest>
    {
        public static bool operator==( ArticleMasterSetRequest left, ArticleMasterSetRequest right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ArticleMasterSetRequest left, ArticleMasterSetRequest right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( ArticleMasterSetRequest left, ArticleMasterSetRequest right )
		{
            return Reth.Itss2.Standard.Dialogs.ArticleData.ArticleMasterSet.ArticleMasterSetRequest.Equals( left, right );
		}        

        public ArticleMasterSetRequest( IMessageId id,
                                        SubscriberId source,
                                        SubscriberId destination,
                                        IEnumerable<ArticleMasterSetArticle> articles    )
        :
            base( id, source, destination, articles )
        {
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ArticleMasterSetRequest );
		}
		
		public bool Equals( ArticleMasterSetRequest other )
		{
            return ArticleMasterSetRequest.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
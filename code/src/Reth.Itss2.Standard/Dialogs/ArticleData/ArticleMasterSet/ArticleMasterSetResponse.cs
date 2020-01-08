using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.ArticleMasterSet
{
    public class ArticleMasterSetResponse:TraceableResponse, IEquatable<ArticleMasterSetResponse>
    {
        public static bool operator==( ArticleMasterSetResponse left, ArticleMasterSetResponse right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ArticleMasterSetResponse left, ArticleMasterSetResponse right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( ArticleMasterSetResponse left, ArticleMasterSetResponse right )
		{
            bool result = TraceableResponse.Equals( left, right );

            if( result == true )
            {
                result &= ArticleMasterSetResult.Equals( left.Result, right.Result );
            }

            return result;
		}

        private ArticleMasterSetResult result;

        public ArticleMasterSetResponse(    IMessageId id,
                                            SubscriberId source,
                                            SubscriberId destination,
                                            ArticleMasterSetResult result    )
        :
            base( DialogName.ArticleMasterSet, id, source, destination )
        {
            this.Result = result;
        }

        public ArticleMasterSetResponse(    ArticleMasterSetRequest request,
                                            ArticleMasterSetResult result    )
        :
            base( request )
        {
            this.Result = result;
        }

        public ArticleMasterSetResult Result
        {
            get{ return this.result; }

            private set
            {
                value.ThrowIfNull();

                this.result = value;
            }
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ArticleMasterSetResponse );
		}
		
		public bool Equals( ArticleMasterSetResponse other )
		{
            return ArticleMasterSetResponse.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void Dispatch( IMessageDispatcher dispatcher )
        {
            dispatcher.ThrowIfNull();
            dispatcher.Dispatch( this );
        }
    }
}
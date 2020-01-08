using System;

using Reth.Protocols;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.ArticleMasterSet
{
    public class ArticleMasterSetResult:IEquatable<ArticleMasterSetResult>
    {
        public static bool operator==( ArticleMasterSetResult left, ArticleMasterSetResult right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ArticleMasterSetResult left, ArticleMasterSetResult right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( ArticleMasterSetResult left, ArticleMasterSetResult right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = ( left.Value == right.Value );
			                                            result &= String.Equals( left.Text, right.Text, StringComparison.InvariantCultureIgnoreCase );

                                                        return result;
                                                    }   );
		}

        public ArticleMasterSetResult( ArticleMasterSetResultValue value, String text )
        {
            this.Value = value;
            this.Text = text;
        }

        public ArticleMasterSetResultValue Value
        {
            get;
        }

        public String Text
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ArticleMasterSetResult );
		}
		
		public bool Equals( ArticleMasterSetResult other )
		{
            return ArticleMasterSetResult.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

        public override String ToString()
        {
            return this.Value.ToString();
        }
    }
}
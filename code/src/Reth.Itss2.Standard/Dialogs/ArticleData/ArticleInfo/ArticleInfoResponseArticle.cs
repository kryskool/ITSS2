using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.Int32Extensions;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.ArticleInfo
{
    public class ArticleInfoResponseArticle:Article, IEquatable<ArticleInfoResponseArticle>
    {
        public static bool operator==( ArticleInfoResponseArticle left, ArticleInfoResponseArticle right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ArticleInfoResponseArticle left, ArticleInfoResponseArticle right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( ArticleInfoResponseArticle left, ArticleInfoResponseArticle right )
		{
            bool result = Article.Equals( left, right );

            if( result == true )
            {
                result &= String.Equals( left.Name, right.Name, StringComparison.InvariantCultureIgnoreCase );
                result &= String.Equals( left.DosageForm, right.DosageForm, StringComparison.InvariantCultureIgnoreCase );
                result &= String.Equals( left.PackingUnit, right.PackingUnit, StringComparison.InvariantCultureIgnoreCase );
                result &= Nullable.Equals( left.RequiresFridge, right.RequiresFridge );
                result &= Nullable.Equals( left.MaxSubItemQuantity, right.MaxSubItemQuantity );
                result &= PackDate.Equals( left.SerialNumberSinceExpiryDate, right.SerialNumberSinceExpiryDate );
            }

            return result;
		}

        private Nullable<int> maxSubItemQuantity;

        public ArticleInfoResponseArticle( ArticleId id )
        :
            base( id )
        {
        }

        public ArticleInfoResponseArticle(  ArticleId id,
                                            String name,
                                            String dosageForm,
                                            String packingUnit,
                                            Nullable<bool> requiresFridge,
                                            Nullable<int> maxSubItemQuantity,
                                            PackDate serialNumberSinceExpiryDate    )
        :
            base( id )
        {
            this.Name = name;
            this.DosageForm = dosageForm;
            this.PackingUnit = packingUnit;
            this.RequiresFridge = requiresFridge;
            this.MaxSubItemQuantity = maxSubItemQuantity;
            this.SerialNumberSinceExpiryDate = serialNumberSinceExpiryDate;
        }

        public String Name
        {
            get;
        }

        public String DosageForm
        {
            get;
        }

        public String PackingUnit
        {
            get;
        }

        public Nullable<bool> RequiresFridge
        {
            get;
        }

        public Nullable<int> MaxSubItemQuantity
        {
            get{ return this.maxSubItemQuantity; }
            
            private set
            {
                value?.ThrowIfNegative();

                this.maxSubItemQuantity = value;
            }
        }

        public PackDate SerialNumberSinceExpiryDate
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ArticleInfoResponseArticle );
		}
		
        public bool Equals( ArticleInfoResponseArticle other )
		{
            return ArticleInfoResponseArticle.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
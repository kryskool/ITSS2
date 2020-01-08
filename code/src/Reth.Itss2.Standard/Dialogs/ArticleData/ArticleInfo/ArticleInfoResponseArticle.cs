using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.Int32Extensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.ArticleInfo
{
    public class ArticleInfoResponseArticle:IEquatable<ArticleInfoResponseArticle>
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
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = left.Id.Equals( right.Id );
                                                        result &= String.Equals( left.Name, right.Name, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.DosageForm, right.DosageForm, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.PackingUnit, right.PackingUnit, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= Nullable.Equals( left.RequiresFridge, right.RequiresFridge );
                                                        result &= Nullable.Equals( left.MaxSubItemQuantity, right.MaxSubItemQuantity );
                                                        result &= PackDate.Equals( left.SerialNumberSinceExpiryDate, right.SerialNumberSinceExpiryDate );

                                                        return result;
                                                    }   );
		}

        private ArticleId id;

        private Nullable<int> maxSubItemQuantity;

        public ArticleInfoResponseArticle( ArticleId id )
        {
            this.Id = id;
        }

        public ArticleInfoResponseArticle(  ArticleId id,
                                            String name,
                                            String dosageForm,
                                            String packingUnit,
                                            Nullable<bool> requiresFridge,
                                            Nullable<int> maxSubItemQuantity,
                                            PackDate serialNumberSinceExpiryDate    )
        {
            this.Id = id;
            this.Name = name;
            this.DosageForm = dosageForm;
            this.PackingUnit = packingUnit;
            this.RequiresFridge = requiresFridge;
            this.MaxSubItemQuantity = maxSubItemQuantity;
            this.SerialNumberSinceExpiryDate = serialNumberSinceExpiryDate;
        }

        public ArticleId Id
        {
            get{ return this.id; }

            private set
            {
                value.ThrowIfNull();

                this.id = value;
            }
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
            return this.Id.GetHashCode();
        }

        public override String ToString()
        {
            return this.Id.ToString();
        }
    }
}
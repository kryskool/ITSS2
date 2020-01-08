using System;
using System.Collections.Generic;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;

namespace Reth.Itss2.StandardExtensions.Dialogs.ArticleData.ArticleMasterSet
{
    public class ArticleMasterSetArticle:Reth.Itss2.Standard.Dialogs.ArticleData.ArticleMasterSet.ArticleMasterSetArticle, IEquatable<ArticleMasterSetArticle>
    {
        public static bool operator==( ArticleMasterSetArticle left, ArticleMasterSetArticle right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ArticleMasterSetArticle left, ArticleMasterSetArticle right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( ArticleMasterSetArticle left, ArticleMasterSetArticle right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = Reth.Itss2.Standard.Dialogs.ArticleData.ArticleMasterSet.ArticleMasterSetArticle.Equals( left, right );

                                                        if( result == true )
                                                        {
                                                            result &= Nullable<bool>.Equals( left.BatchTracking, right.BatchTracking );
                                                            result &= Nullable<bool>.Equals( left.ExpiryTracking, right.ExpiryTracking );
                                                            result &= Nullable<bool>.Equals( left.SerialTracking, right.SerialTracking );
                                                        }

                                                        return result;
                                                    }   );
		}

        public ArticleMasterSetArticle( ArticleId id )
        :
            base( id )
        {
        }

        public ArticleMasterSetArticle( ArticleId id,
                                        String name,
                                        String dosageForm,
                                        String packingUnit,
                                        String machineLocation,
                                        StockLocationId stockLocationId,
                                        Nullable<bool> requiresFridge,
                                        Nullable<bool> batchTracking,
                                        Nullable<bool> expiryTracking,
                                        Nullable<bool> serialTracking,
                                        Nullable<int> maxSubItemQuantity,
                                        Nullable<int> depth,
                                        Nullable<int> width,
                                        Nullable<int> height,
                                        Nullable<int> weight,
                                        PackDate serialNumberSinceExpiryDate,
                                        IEnumerable<ProductCode> productCodes   )
        :
            base(   id,
                    name,
                    dosageForm,
                    packingUnit,
                    machineLocation,
                    stockLocationId,
                    requiresFridge,
                    maxSubItemQuantity,
                    depth,
                    width,
                    height,
                    weight,
                    serialNumberSinceExpiryDate,
                    productCodes    )
        {
            this.BatchTracking = batchTracking;
            this.ExpiryTracking = expiryTracking;
            this.SerialTracking = serialTracking;
        }
        
        public Nullable<bool> BatchTracking
        {
            get;
        }

        public Nullable<bool> ExpiryTracking
        {
            get;
        }

        public Nullable<bool> SerialTracking
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ArticleMasterSetArticle );
		}
		
        public bool Equals( ArticleMasterSetArticle other )
		{
            return ArticleMasterSetArticle.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
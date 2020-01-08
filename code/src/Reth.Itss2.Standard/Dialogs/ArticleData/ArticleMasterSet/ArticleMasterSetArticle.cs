using System;
using System.Collections.Generic;

using Reth.Protocols;
using Reth.Protocols.Extensions.Int32Extensions;
using Reth.Protocols.Extensions.ListExtensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.ArticleMasterSet
{
    public class ArticleMasterSetArticle:IEquatable<ArticleMasterSetArticle>
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
                                                        bool result = false;

                                                        result = left.Id.Equals( right.Id );
                                                        result &= String.Equals( left.Name, right.Name, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.DosageForm, right.DosageForm, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.PackingUnit, right.PackingUnit, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.MachineLocation, right.MachineLocation, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= StockLocationId.Equals( left.StockLocationId, right.StockLocationId );
                                                        result &= Nullable.Equals( left.RequiresFridge, right.RequiresFridge );
                                                        result &= Nullable.Equals( left.MaxSubItemQuantity, right.MaxSubItemQuantity );
                                                        result &= Nullable.Equals( left.Depth, right.Depth );
                                                        result &= Nullable.Equals( left.Width, right.Width );
                                                        result &= Nullable.Equals( left.Height, right.Height );
                                                        result &= Nullable.Equals( left.Weight, right.Weight );
                                                        result &= PackDate.Equals( left.SerialNumberSinceExpiryDate, right.SerialNumberSinceExpiryDate );
                                                        result &= left.ProductCodes.ElementsEqual<ProductCode>( right.ProductCodes );

                                                        return result;
                                                    }   );
		}

        private ArticleId id;

        private Nullable<int> maxSubItemQuantity;
        private Nullable<int> depth;
        private Nullable<int> width;
        private Nullable<int> height;
        private Nullable<int> weight;

        public ArticleMasterSetArticle( ArticleId id )
        {
            this.Id = id;
        }

        public ArticleMasterSetArticle( ArticleId id,
                                        String name,
                                        String dosageForm,
                                        String packingUnit,
                                        String machineLocation,
                                        StockLocationId stockLocationId,
                                        Nullable<bool> requiresFridge,
                                        Nullable<int> maxSubItemQuantity,
                                        Nullable<int> depth,
                                        Nullable<int> width,
                                        Nullable<int> height,
                                        Nullable<int> weight,
                                        PackDate serialNumberSinceExpiryDate,
                                        IEnumerable<ProductCode> productCodes   )
        {
            this.Id = id;
            this.Name = name;
            this.DosageForm = dosageForm;
            this.PackingUnit = packingUnit;
            this.MachineLocation = machineLocation;
            this.StockLocationId = stockLocationId;
            this.RequiresFridge = requiresFridge;
            this.MaxSubItemQuantity = maxSubItemQuantity;
            this.Depth = depth;
            this.Width = width;
            this.Height = height;
            this.Weight = weight;
            this.SerialNumberSinceExpiryDate = serialNumberSinceExpiryDate;

            if( !( productCodes is null ) )
            {
                this.ProductCodes.AddRange( productCodes );
            }
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

        public String MachineLocation
        {
            get;
        }

        public StockLocationId StockLocationId
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

        public Nullable<int> Depth
        {
            get{ return this.depth; }
            
            private set
            {
                value?.ThrowIfNegative();

                this.depth = value;
            }
        }

        public Nullable<int> Width
        {
            get{ return this.width; }
            
            private set
            {
                value?.ThrowIfNegative();

                this.width = value;
            }
        }

        public Nullable<int> Height
        {
            get{ return this.height; }
            
            private set
            {
                value?.ThrowIfNegative();

                this.height = value;
            }
        }

        public Nullable<int> Weight
        {
            get{ return this.weight; }
            
            private set
            {
                value?.ThrowIfNegative();

                this.weight = value;
            }
        }

        public PackDate SerialNumberSinceExpiryDate
        {
            get;
        }

        private List<ProductCode> ProductCodes
        {
            get;
        } = new List<ProductCode>();
        
        public ProductCode[] GetProductCodes()
        {
            return this.ProductCodes.ToArray();
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
            return this.Id.GetHashCode();
        }

        public override String ToString()
        {
            return this.Id.ToString();
        }
    }
}
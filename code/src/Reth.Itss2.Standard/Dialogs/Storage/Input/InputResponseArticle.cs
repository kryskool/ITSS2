using System;
using System.Collections.Generic;

using Reth.Protocols;
using Reth.Protocols.Extensions.Int32Extensions;
using Reth.Protocols.Extensions.ListExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.Input
{
    public class InputResponseArticle:IEquatable<InputResponseArticle>
    {
        public static bool operator==( InputResponseArticle left, InputResponseArticle right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( InputResponseArticle left, InputResponseArticle right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( InputResponseArticle left, InputResponseArticle right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = ArticleId.Equals( left.Id, right.Id );
                                                        result &= String.Equals( left.Name, right.Name, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.DosageForm, right.DosageForm, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.PackingUnit, right.PackingUnit, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= Nullable.Equals( left.RequiresFridge, right.RequiresFridge );
                                                        result &= Nullable.Equals( left.MaxSubItemQuantity, right.MaxSubItemQuantity );
                                                        result &= PackDate.Equals( left.SerialNumberSinceExpiryDate, right.SerialNumberSinceExpiryDate );
                                                        result &= left.ProductCodes.ElementsEqual( right.ProductCodes );
                                                        result &= left.Packs.ElementsEqual( right.Packs );

                                                        return result;
                                                    }   );
		}

        private Nullable<int> maxSubItemQuantity;

        public InputResponseArticle(    ArticleId id,
                                        String name,
                                        String dosageForm,
                                        String packingUnit,
                                        Nullable<bool> requiresFridge,
                                        Nullable<int> maxSubItemQuantity,
                                        PackDate serialNumberSinceExpiryDate,
                                        IEnumerable<ProductCode> productCodes,
                                        IEnumerable<InputResponsePack> packs )
        {
            this.Id = id;
            this.Name = name;
            this.DosageForm = dosageForm;
            this.PackingUnit = packingUnit;
            this.RequiresFridge = requiresFridge;
            this.MaxSubItemQuantity = maxSubItemQuantity;
            this.SerialNumberSinceExpiryDate = serialNumberSinceExpiryDate;

            if( !( productCodes is null ) )
            {
                this.ProductCodes.AddRange( productCodes );
            }

            if( !( packs is null ) )
            {
                this.Packs.AddRange( packs );
            }
        }

        public ArticleId Id
        {
            get;
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

        private List<ProductCode> ProductCodes
        {
            get;
        } = new List<ProductCode>();

        private List<InputResponsePack> Packs
        {
            get;
        } = new List<InputResponsePack>();
        
        public ProductCode[] GetProductCodes()
        {
            return this.ProductCodes.ToArray();
        }

        public InputResponsePack[] GetPacks()
        {
            return this.Packs.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as InputResponseArticle );
		}
		
        public bool Equals( InputResponseArticle other )
		{
            return InputResponseArticle.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
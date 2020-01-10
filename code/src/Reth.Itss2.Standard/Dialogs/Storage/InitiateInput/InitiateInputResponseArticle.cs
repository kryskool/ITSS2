using System;
using System.Collections.Generic;

using Reth.Protocols;
using Reth.Protocols.Extensions.Int32Extensions;
using Reth.Protocols.Extensions.ListExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.InitiateInput
{
    public class InitiateInputResponseArticle:IEquatable<InitiateInputResponseArticle>
    {
        public static bool operator==( InitiateInputResponseArticle left, InitiateInputResponseArticle right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( InitiateInputResponseArticle left, InitiateInputResponseArticle right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( InitiateInputResponseArticle left, InitiateInputResponseArticle right )
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
                                                        result &= Nullable.Equals( left.MaxSubItemQuantity, right.MaxSubItemQuantity );
                                                        result &= PackDate.Equals( left.SerialNumberSinceExpiryDate, right.SerialNumberSinceExpiryDate );
                                                        result &= left.ProductCodes.ElementsEqual( right.ProductCodes );
                                                        result &= left.Packs.ElementsEqual( right.Packs );

                                                        return result;
                                                    }   );
		}
        
        private Nullable<int> maxSubItemQuantity;

        public InitiateInputResponseArticle(    ArticleId id,
                                                String name,
                                                String dosageForm,
                                                String packingUnit,
                                                Nullable<int> maxSubItemQuantity,
                                                PackDate serialNumberSinceExpiryDate,
                                                IEnumerable<ProductCode> productCodes,
                                                IEnumerable<InitiateInputResponsePack> packs )
        {
            this.Id = id;
            this.Name = name;
            this.DosageForm = dosageForm;
            this.PackingUnit = packingUnit;
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

        private List<InitiateInputResponsePack> Packs
        {
            get;
        } = new List<InitiateInputResponsePack>();
        
        public ProductCode[] GetProductCodes()
        {
            return this.ProductCodes.ToArray();
        }

        public InitiateInputResponsePack[] GetPacks()
        {
            return this.Packs.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as InitiateInputResponseArticle );
		}
		
        public bool Equals( InitiateInputResponseArticle other )
		{
            return InitiateInputResponseArticle.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
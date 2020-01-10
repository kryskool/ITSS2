using System;
using System.Collections.Generic;

using Reth.Protocols;
using Reth.Protocols.Extensions.Int32Extensions;
using Reth.Protocols.Extensions.ListExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.Input
{
    public class InputMessageArticle:Article, IEquatable<InputMessageArticle>
    {
        public static bool operator==( InputMessageArticle left, InputMessageArticle right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( InputMessageArticle left, InputMessageArticle right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( InputMessageArticle left, InputMessageArticle right )
		{
            bool result = Article.Equals( left, right );

            if( result == true )
            {
                result &= String.Equals( left.Name, right.Name, StringComparison.InvariantCultureIgnoreCase );
                result &= String.Equals( left.DosageForm, right.DosageForm, StringComparison.InvariantCultureIgnoreCase );
                result &= String.Equals( left.PackingUnit, right.PackingUnit, StringComparison.InvariantCultureIgnoreCase );
                result &= Nullable.Equals( left.MaxSubItemQuantity, right.MaxSubItemQuantity );
                result &= left.ProductCodes.ElementsEqual( right.ProductCodes );
                result &= left.Packs.ElementsEqual( right.Packs );
            }

            return result;
		}

        private Nullable<int> maxSubItemQuantity;

        public InputMessageArticle( ArticleId id )
        :
            base( id )
        {
        }

        public InputMessageArticle( ArticleId id,
                                    String name,
                                    String dosageForm,
                                    String packingUnit,
                                    Nullable<int> maxSubItemQuantity,
                                    IEnumerable<ProductCode> productCodes,
                                    IEnumerable<InputMessagePack> packs )
        :
            base( id )
        {
            this.Name = name;
            this.DosageForm = dosageForm;
            this.PackingUnit = packingUnit;
            this.MaxSubItemQuantity = maxSubItemQuantity;

            if( !( productCodes is null ) )
            {
                this.ProductCodes.AddRange( productCodes );
            }

            if( !( packs is null ) )
            {
                this.Packs.AddRange( packs );
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

        public Nullable<int> MaxSubItemQuantity
        {
            get{ return this.maxSubItemQuantity; }
            
            private set
            {
                value?.ThrowIfNegative();

                this.maxSubItemQuantity = value;
            }
        }

        private List<ProductCode> ProductCodes
        {
            get;
        } = new List<ProductCode>();

        private List<InputMessagePack> Packs
        {
            get;
        } = new List<InputMessagePack>();
        
        public ProductCode[] GetProductCodes()
        {
            return this.ProductCodes.ToArray();
        }

        public InputMessagePack[] GetPacks()
        {
            return this.Packs.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as InputMessageArticle );
		}
		
        public bool Equals( InputMessageArticle other )
		{
            return InputMessageArticle.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
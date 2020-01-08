using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs
{
    public class ProductCode:IEquatable<ProductCode>
    {
        public static bool operator==( ProductCode left, ProductCode right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ProductCode left, ProductCode right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( ProductCode left, ProductCode right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        return ( left.Code == right.Code );
                                                    }   );
		}

        private ProductCodeId code;

        public ProductCode( ProductCodeId code )
        {
            this.Code = code;
        }

        public ProductCodeId Code
        {
            get{ return this.code; }

            private set
            {
                value.ThrowIfNull();

                this.code = value;
            }
        }
        
        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ProductCode );
		}
		
		public bool Equals( ProductCode other )
		{
            return ProductCode.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Code.GetHashCode();
		}

        public override String ToString()
        {
            return this.Code.ToString();
        }
    }
}
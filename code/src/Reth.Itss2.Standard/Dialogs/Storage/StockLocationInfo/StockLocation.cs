using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.StockLocationInfo
{
    public class StockLocation:IEquatable<StockLocation>
    {
        public static bool operator==( StockLocation left, StockLocation right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( StockLocation left, StockLocation right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( StockLocation left, StockLocation right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = left.Id.Equals( right.Id );
                                                        result &= String.Equals( left.Description, right.Description, StringComparison.InvariantCultureIgnoreCase );

                                                        return result;
                                                    }   );
		}

        public StockLocation( StockLocationId id )
        {
            id.ThrowIfNull();

            this.Id = id;
        }

        public StockLocation( StockLocationId id, String description )
        {
            id.ThrowIfNull();

            this.Id = id;
            this.Description = description;
        }

        public StockLocationId Id
        {
            get;
        }

        public String Description
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as StockLocation );
		}
		
        public bool Equals( StockLocation other )
		{
            return StockLocation.Equals( this, other );
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
using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.Int32Extensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.ArticleData.StockDeliverySet
{
    public class StockDeliveryLine:IEquatable<StockDeliveryLine>
    {
        public static bool operator==( StockDeliveryLine left, StockDeliveryLine right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( StockDeliveryLine left, StockDeliveryLine right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( StockDeliveryLine left, StockDeliveryLine right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = left.Id.Equals( right.Id );
                                                        result &= String.Equals( left.BatchNumber, right.BatchNumber, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.ExternalId, right.ExternalId, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.SerialNumber, right.SerialNumber, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.MachineLocation, right.MachineLocation, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= StockLocationId.Equals( left.StockLocationId, right.StockLocationId );
                                                        result &= Nullable.Equals( left.Quantity, right.Quantity );
                                                        result &= PackDate.Equals( left.ExpiryDate, right.ExpiryDate );

                                                        return result;
                                                    }   );
		}

        private ArticleId id;
        
        private Nullable<int> quantity;

        public StockDeliveryLine( ArticleId id )
        {
            this.Id = id;
        }

        public StockDeliveryLine(   ArticleId id,
                                    String batchNumber,
                                    String externalId,
                                    String serialNumber,
                                    String machineLocation,
                                    StockLocationId stockLocationId,
                                    Nullable<int> quantity,
                                    PackDate expiryDate   )
        {
            this.Id = id;
            this.BatchNumber = batchNumber;
            this.ExternalId = externalId;
            this.SerialNumber = serialNumber;            
            this.MachineLocation = machineLocation;
            this.StockLocationId = stockLocationId;
            this.Quantity = quantity;
            this.ExpiryDate = expiryDate;
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

        public String BatchNumber
        {
            get;
        }

        public String ExternalId
        {
            get;
        }

        public String SerialNumber
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

        public Nullable<int> Quantity
        {
            get{ return this.quantity; }
            
            private set
            {
                value?.ThrowIfNegative();

                this.quantity = value;
            }
        }

        public PackDate ExpiryDate
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as StockDeliveryLine );
		}
		
        public bool Equals( StockDeliveryLine other )
		{
            return StockDeliveryLine.Equals( this, other );
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
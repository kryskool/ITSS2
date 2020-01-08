using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.Int32Extensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.StockInfo
{
    public class StockInfoPack:IEquatable<StockInfoPack>
    {
        public static bool operator==( StockInfoPack left, StockInfoPack right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( StockInfoPack left, StockInfoPack right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( StockInfoPack left, StockInfoPack right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = left.Id.Equals( right.Id );
                                                        result &= String.Equals( left.DeliveryNumber, right.DeliveryNumber, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.BatchNumber, right.BatchNumber, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.ExternalId, right.ExternalId, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.SerialNumber, right.SerialNumber, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.ScanCode, right.ScanCode, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.MachineLocation, right.MachineLocation, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= StockLocationId.Equals( left.StockLocationId, right.StockLocationId );
                                                        result &= PackDate.Equals( left.ExpiryDate, right.ExpiryDate );
                                                        result &= PackDate.Equals( left.StockInDate, right.StockInDate );
                                                        result &= Nullable.Equals( left.SubItemQuantity, right.SubItemQuantity );
                                                        result &= Nullable.Equals( left.Depth, right.Depth );
                                                        result &= Nullable.Equals( left.Width, right.Width );
                                                        result &= Nullable.Equals( left.Height, right.Height );
                                                        result &= Nullable.Equals( left.Weight, right.Weight );
                                                        result &= Nullable.Equals( left.Shape, right.Shape );
                                                        result &= Nullable.Equals( left.State, right.State );
                                                        result &= Nullable.Equals( left.IsInFridge, right.IsInFridge );

                                                        return result;
                                                    }   );
		}

        private PackId id;

        private Nullable<int> subItemQuantity;
        private Nullable<int> depth;
        private Nullable<int> width;
        private Nullable<int> height;
        private Nullable<int> weight;

        public StockInfoPack( PackId id )
        {
            this.Id = id;
        }

        public StockInfoPack(   PackId id,
                                String deliveryNumber,
                                String batchNumber,
                                String externalId,
                                String serialNumber,
                                String scanCode,
                                String machineLocation,
                                StockLocationId stockLocationId,
                                PackDate expiryDate,
                                PackDate stockInDate,
                                Nullable<int> subItemQuantity,
                                Nullable<int> depth,
                                Nullable<int> width,
                                Nullable<int> height,
                                Nullable<int> weight,
                                Nullable<PackShape> shape,
                                Nullable<PackState> state,
                                Nullable<bool> isInFridge  )
        {
            this.Id = id;
            this.DeliveryNumber = deliveryNumber;
            this.BatchNumber = batchNumber;
            this.ExternalId = externalId;
            this.SerialNumber = serialNumber;
            this.ScanCode = scanCode;
            this.MachineLocation = machineLocation;
            this.StockLocationId = stockLocationId;
            this.ExpiryDate = expiryDate;
            this.StockInDate = stockInDate;
            this.SubItemQuantity = subItemQuantity;
            this.Depth = depth;
            this.Width = width;
            this.Height = height;
            this.Weight = weight;
            this.Shape = shape;
            this.State = state;
            this.IsInFridge = isInFridge;
        }

        public PackId Id
        {
            get{ return this.id; }

            private set
            {
                value.ThrowIfNull();

                this.id = value;
            }
        }

        public String DeliveryNumber
        {
            get;
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

        public String ScanCode
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

        public PackDate ExpiryDate
        {
            get;
        }

        public PackDate StockInDate
        {
            get;
        }

        public Nullable<int> SubItemQuantity
        {
            get{ return this.subItemQuantity; }
            
            private set
            {
                value?.ThrowIfNegative();

                this.subItemQuantity = value;
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

        public Nullable<PackShape> Shape
        {
            get;
        }

        public Nullable<PackState> State
        {
            get;
        }

        public Nullable<bool> IsInFridge
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as StockInfoPack );
		}
		
        public bool Equals( StockInfoPack other )
		{
            return StockInfoPack.Equals( this, other );
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
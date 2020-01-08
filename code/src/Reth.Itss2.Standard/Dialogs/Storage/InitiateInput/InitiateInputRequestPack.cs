using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.Int32Extensions;
using Reth.Protocols.Extensions.StringExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.InitiateInput
{
    public class InitiateInputRequestPack:IEquatable<InitiateInputRequestPack>
    {
        public static bool operator==( InitiateInputRequestPack left, InitiateInputRequestPack right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( InitiateInputRequestPack left, InitiateInputRequestPack right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( InitiateInputRequestPack left, InitiateInputRequestPack right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = String.Equals( left.ScanCode, right.ScanCode, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.DeliveryNumber, right.DeliveryNumber, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.BatchNumber, right.BatchNumber, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.ExternalId, right.ExternalId, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.SerialNumber, right.SerialNumber, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.MachineLocation, right.MachineLocation, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= StockLocationId.Equals( left.StockLocationId, right.StockLocationId );
                                                        result &= PackDate.Equals( left.ExpiryDate, right.ExpiryDate );
                                                        result &= Nullable.Equals( left.Index, right.Index );
                                                        result &= Nullable.Equals( left.SubItemQuantity, right.SubItemQuantity );
                                                        result &= Nullable.Equals( left.Depth, right.Depth );
                                                        result &= Nullable.Equals( left.Width, right.Width );
                                                        result &= Nullable.Equals( left.Height, right.Height );
                                                        result &= Nullable.Equals( left.Weight, right.Weight );
                                                        result &= Nullable.Equals( left.Shape, right.Shape );

                                                        return result;
                                                    }   );
		}

        private String scanCode = String.Empty;
        
        private Nullable<int> index;
        private Nullable<int> subItemQuantity;
        private Nullable<int> depth;
        private Nullable<int> width;
        private Nullable<int> height;
        private Nullable<int> weight;

        public InitiateInputRequestPack( String scanCode )
        {
            this.ScanCode = scanCode;
        }

        public InitiateInputRequestPack(    String scanCode,
                                            String deliveryNumber,
                                            String batchNumber,
                                            String externalId,
                                            String serialNumber,
                                            String machineLocation,
                                            StockLocationId stockLocationId,
                                            PackDate expiryDate,
                                            Nullable<int> index,
                                            Nullable<int> subItemQuantity,
                                            Nullable<int> depth,
                                            Nullable<int> width,
                                            Nullable<int> height,
                                            Nullable<int> weight,
                                            Nullable<PackShape> shape   )
        {
            this.ScanCode = scanCode;
            this.DeliveryNumber = deliveryNumber;
            this.BatchNumber = batchNumber;
            this.ExternalId = externalId;
            this.SerialNumber = serialNumber;
            this.MachineLocation = machineLocation;
            this.StockLocationId = stockLocationId;
            this.ExpiryDate = expiryDate;
            this.Index = index;
            this.SubItemQuantity = subItemQuantity;
            this.Depth = depth;
            this.Width = width;
            this.Height = height;
            this.Weight = weight;
            this.Shape = shape;
        }

        public String ScanCode
        {
            get{ return this.scanCode; }

            private set
            {
                value.ThrowIfNullOrEmpty();

                this.scanCode = value;
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

        public Nullable<int> Index
        {
            get{ return this.index; }
            
            private set
            {
                value?.ThrowIfNegative();

                this.index = value;
            }
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

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as InitiateInputRequestPack );
		}
		
        public bool Equals( InitiateInputRequestPack other )
		{
            return InitiateInputRequestPack.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.ScanCode.GetHashCode();
        }
    }
}
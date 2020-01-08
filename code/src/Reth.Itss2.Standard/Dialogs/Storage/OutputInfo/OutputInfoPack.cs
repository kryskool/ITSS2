using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.Int32Extensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.OutputInfo
{
    public class OutputInfoPack:IEquatable<OutputInfoPack>
    {
        public static bool operator==( OutputInfoPack left, OutputInfoPack right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( OutputInfoPack left, OutputInfoPack right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( OutputInfoPack left, OutputInfoPack right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = left.Id.Equals( right.Id );
                                                        result &= ( left.OutputDestination == right.OutputDestination );
                                                        result &= Nullable.Equals( left.OutputPoint, right.OutputPoint );
                                                        result &= String.Equals( left.DeliveryNumber, right.DeliveryNumber, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.BatchNumber, right.BatchNumber, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.ExternalId, right.ExternalId, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.SerialNumber, right.SerialNumber, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.ScanCode, right.ScanCode, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.BoxNumber, right.BoxNumber, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.MachineLocation, right.MachineLocation, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.StockLocationId, right.StockLocationId );
                                                        result &= PackDate.Equals( left.ExpiryDate, right.ExpiryDate );
                                                        result &= PackDate.Equals( left.StockInDate, right.StockInDate );
                                                        result &= Nullable.Equals( left.SubItemQuantity, right.SubItemQuantity );
                                                        result &= Nullable.Equals( left.Depth, right.Depth );
                                                        result &= Nullable.Equals( left.Width, right.Width );
                                                        result &= Nullable.Equals( left.Height, right.Height );
                                                        result &= Nullable.Equals( left.Weight, right.Weight );
                                                        result &= Nullable.Equals( left.Shape, right.Shape );
                                                        result &= Nullable.Equals( left.IsInFridge, right.IsInFridge );
                                                        result &= Nullable.Equals( left.LabelStatus, right.LabelStatus );
                                                        
                                                        return result;
                                                    }   );
		}

        private PackId id;

        private Nullable<int> subItemQuantity;
        private Nullable<int> depth;
        private Nullable<int> width;
        private Nullable<int> height;
        private Nullable<int> weight;
        
        public OutputInfoPack(  PackId id,
                                int outputDestination   )
        {
            this.Id = id;
            this.OutputDestination = outputDestination;
        }

        public OutputInfoPack(  PackId id,
                                int outputDestination,
                                Nullable<int> outputPoint,
                                String deliveryNumber,
                                String batchNumber,
                                String externalId,
                                String serialNumber,
                                String scanCode,
                                String boxNumber,
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
                                Nullable<bool> isInFridge,
                                Nullable<LabelStatus> labelStatus   )
        {
            this.Id = id;
            this.OutputDestination = outputDestination;
            this.OutputPoint = outputPoint;
            this.DeliveryNumber = deliveryNumber;
            this.BatchNumber = batchNumber;
            this.ExternalId = externalId;
            this.SerialNumber = serialNumber;
            this.ScanCode = scanCode;
            this.BoxNumber = boxNumber;
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
            this.IsInFridge = isInFridge;
            this.LabelStatus = labelStatus;
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

        public int OutputDestination
        {
            get;
        }

        public Nullable<int> OutputPoint
        {
            get;
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

        public String BoxNumber
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

        public Nullable<bool> IsInFridge
        {
            get;
        }

        public Nullable<LabelStatus> LabelStatus
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as OutputInfoPack );
		}
		
        public bool Equals( OutputInfoPack other )
		{
            return OutputInfoPack.Equals( this, other );
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
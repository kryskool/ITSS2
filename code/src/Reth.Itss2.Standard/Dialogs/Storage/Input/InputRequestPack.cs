using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.Int32Extensions;
using Reth.Protocols.Extensions.StringExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.Input
{
    public class InputRequestPack:IEquatable<InputRequestPack>
    {
        public static bool operator==( InputRequestPack left, InputRequestPack right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( InputRequestPack left, InputRequestPack right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( InputRequestPack left, InputRequestPack right )
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
                                                        result &= PackId.Equals( left.Id, right.Id );
                                                        result &= PackDate.Equals( left.ExpiryDate, right.ExpiryDate );
                                                        result &= Nullable.Equals( left.Index, right.Index );
                                                        result &= Nullable.Equals( left.SubItemQuantity, right.SubItemQuantity );

                                                        return result;
                                                    }   );
		}

        private String scanCode = String.Empty;
        
        private Nullable<int> index;
        private Nullable<int> subItemQuantity;

        public InputRequestPack( String scanCode )
        {
            this.ScanCode = scanCode;
        }

        public InputRequestPack(    String scanCode,
                                    String deliveryNumber,
                                    String batchNumber,
                                    String externalId,
                                    String serialNumber,
                                    String machineLocation,
                                    StockLocationId stockLocationId,
                                    PackId id,
                                    PackDate expiryDate,
                                    Nullable<int> index,
                                    Nullable<int> subItemQuantity   )
        {
            this.ScanCode = scanCode;
            this.DeliveryNumber = deliveryNumber;
            this.BatchNumber = batchNumber;
            this.ExternalId = externalId;
            this.SerialNumber = serialNumber;
            this.MachineLocation = machineLocation;
            this.StockLocationId = stockLocationId;
            this.Id = id;
            this.ExpiryDate = expiryDate;
            this.Index = index;
            this.SubItemQuantity = subItemQuantity;
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

        public PackId Id
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

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as InputRequestPack );
		}
		
        public bool Equals( InputRequestPack other )
		{
            return InputRequestPack.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.ScanCode.GetHashCode();
        }
    }
}
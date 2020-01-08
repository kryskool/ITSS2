using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.Int32Extensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.Input
{
    public class InputResponsePack:IEquatable<InputResponsePack>
    {
        public static bool operator==( InputResponsePack left, InputResponsePack right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( InputResponsePack left, InputResponsePack right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( InputResponsePack left, InputResponsePack right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = InputResponsePackHandling.Equals( left.Handling, right.Handling );
                                                        result &= String.Equals( left.DeliveryNumber, right.DeliveryNumber, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.BatchNumber, right.BatchNumber, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.ExternalId, right.ExternalId, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.SerialNumber, right.SerialNumber, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= StockLocationId.Equals( left.StockLocationId, right.StockLocationId );
                                                        result &= PackDate.Equals( left.ExpiryDate, right.ExpiryDate );
                                                        result &= Nullable.Equals( left.Index, right.Index );
                                                        result &= Nullable.Equals( left.SubItemQuantity, right.SubItemQuantity );
                                                        result &= Nullable.Equals( left.Depth, right.Depth );
                                                        result &= Nullable.Equals( left.Width, right.Width );
                                                        result &= Nullable.Equals( left.Height, right.Height );
                                                        result &= Nullable.Equals( left.Weight, right.Weight );

                                                        return result;
                                                    }   );
		}

        private InputResponsePackHandling handling;

        private Nullable<int> index;
        private Nullable<int> subItemQuantity;
        private Nullable<int> depth;
        private Nullable<int> width;
        private Nullable<int> height;
        private Nullable<int> weight;

        public InputResponsePack(   InputResponsePackHandling handling,
                                    String deliveryNumber,
                                    String batchNumber,
                                    String externalId,
                                    String serialNumber,
                                    StockLocationId stockLocationId,
                                    PackDate expiryDate,
                                    Nullable<int> index,
                                    Nullable<int> subItemQuantity,
                                    Nullable<int> depth,
                                    Nullable<int> width,
                                    Nullable<int> height,
                                    Nullable<int> weight   )
        {
            this.Handling = handling;
            this.DeliveryNumber = deliveryNumber;
            this.BatchNumber = batchNumber;
            this.ExternalId = externalId;
            this.SerialNumber = serialNumber;
            this.StockLocationId = stockLocationId;
            this.ExpiryDate = expiryDate;
            this.Index = index;       
            this.SubItemQuantity = subItemQuantity;
            this.Depth = depth;
            this.Width = width;
            this.Height = height;
            this.Weight = weight;
        }

        public InputResponsePackHandling Handling
        {
            get{ return this.handling; }

            private set
            {
                value.ThrowIfNull();

                this.handling = value;
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

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as InputResponsePack );
		}
		
        public bool Equals( InputResponsePack other )
		{
            return InputResponsePack.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
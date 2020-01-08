using System;
using System.Collections.Generic;

using Reth.Protocols;
using Reth.Protocols.Extensions.Int32Extensions;
using Reth.Protocols.Extensions.ListExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.Output
{
    public class OutputCriteria:IEquatable<OutputCriteria>
    {
        public static bool operator==( OutputCriteria left, OutputCriteria right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( OutputCriteria left, OutputCriteria right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( OutputCriteria left, OutputCriteria right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = ( left.Quantity == right.Quantity );
                                                        result &= Nullable.Equals( left.SubItemQuantity, right.SubItemQuantity );
                                                        result &= ArticleId.Equals( left.ArticleId, right.ArticleId );
                                                        result &= PackId.Equals( left.PackId, right.PackId );
                                                        result &= PackDate.Equals( left.MinimumExpiryDate, right.MinimumExpiryDate );
                                                        result &= String.Equals( left.BatchNumber, right.BatchNumber, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.ExternalId, right.ExternalId, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.SerialNumber, right.SerialNumber, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.MachineLocation, right.MachineLocation, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= StockLocationId.Equals( left.StockLocationId, right.StockLocationId );
                                                        result &= Nullable.Equals( left.SingleBatchNumber, right.SingleBatchNumber );
                                                        result &= left.Labels.ElementsEqual( right.Labels );

                                                        return result;
                                                    }   );
		}

        private int quantity;
        private Nullable<int> subItemQuantity;

        public OutputCriteria( int quantity )
        {
            this.Quantity = quantity;
        }

        public OutputCriteria(  int quantity,
                                Nullable<int> subItemQuantity,
                                ArticleId articleId,
                                PackId packId,
                                PackDate minimumExpiryDate,
                                String batchNumber,
                                String externalId,
                                String serialNumber,
                                String machineLocation,
                                StockLocationId stockLocationId,
                                Nullable<bool> singleBatchNumber,
                                IEnumerable<OutputLabel> labels   )
        {
            this.Quantity = quantity;
            this.SubItemQuantity = subItemQuantity;
            this.ArticleId = articleId;
            this.PackId = packId;
            this.MinimumExpiryDate = minimumExpiryDate;
            this.BatchNumber = batchNumber;
            this.ExternalId = externalId;
            this.SerialNumber = serialNumber;
            this.MachineLocation = machineLocation;
            this.StockLocationId = stockLocationId;
            this.SingleBatchNumber = singleBatchNumber;

            if( !( labels is null ) )
            {
                this.Labels.AddRange( labels );
            }
        }

        public int Quantity
        {
            get{ return this.quantity; }

            private set
            {
                value.ThrowIfNegative();

                this.quantity = value;
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

        public ArticleId ArticleId
        {
            get;
        }

        public PackId PackId
        {
            get;
        }

        public PackDate MinimumExpiryDate
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

        public Nullable<bool> SingleBatchNumber
        {
            get;
        }

        private List<OutputLabel> Labels
        {
            get;
        } = new List<OutputLabel>();

        public OutputLabel[] GetLabels()
        {
            return this.Labels.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as OutputCriteria );
		}
		
        public bool Equals( OutputCriteria other )
		{
            return OutputCriteria.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
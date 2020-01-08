using System;

using Reth.Protocols;

namespace Reth.Itss2.Standard.Dialogs.Storage.StockInfo
{
    public class StockInfoRequestCriteria:IEquatable<StockInfoRequestCriteria>
    {
        public static bool operator==( StockInfoRequestCriteria left, StockInfoRequestCriteria right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( StockInfoRequestCriteria left, StockInfoRequestCriteria right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( StockInfoRequestCriteria left, StockInfoRequestCriteria right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = ArticleId.Equals( left.ArticleId, right.ArticleId );
                                                        result &= String.Equals( left.BatchNumber, right.BatchNumber, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.ExternalId, right.ExternalId, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.SerialNumber, right.SerialNumber, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= String.Equals( left.MachineLocation, right.MachineLocation, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= StockLocationId.Equals( left.StockLocationId, right.StockLocationId );

                                                        return result;
                                                    }   );
		}

        public StockInfoRequestCriteria()
        {
        }

        public StockInfoRequestCriteria(    ArticleId articleId,
                                            String batchNumber,
                                            String externalId,
                                            String serialNumber,
                                            String machineLocation,
                                            StockLocationId stockLocationId )
        {
            this.ArticleId = articleId;
            this.BatchNumber = batchNumber;
            this.ExternalId = externalId;
            this.SerialNumber = serialNumber;
            this.MachineLocation = machineLocation;
            this.StockLocationId = stockLocationId;
        }

        public ArticleId ArticleId
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

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as StockInfoRequestCriteria );
		}
		
        public bool Equals( StockInfoRequestCriteria other )
		{
            return StockInfoRequestCriteria.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
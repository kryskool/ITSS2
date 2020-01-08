using System;

using Reth.Protocols;

namespace Reth.Itss2.Standard.Dialogs.Storage.Output
{
    public class OutputRequestDetails:IEquatable<OutputRequestDetails>
    {
        public static bool operator==( OutputRequestDetails left, OutputRequestDetails right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( OutputRequestDetails left, OutputRequestDetails right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( OutputRequestDetails left, OutputRequestDetails right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = ( left.OutputDestination == right.OutputDestination );
                                                        result &= Nullable.Equals( left.Priority, right.Priority );
                                                        result &= Nullable.Equals( left.OutputPoint, right.OutputPoint );

                                                        return result;
                                                    }   );
		}
        
        public OutputRequestDetails(    int outputDestination,
                                        Nullable<OutputPriority> priority,
                                        Nullable<int> outputPoint   )
        {
            this.OutputDestination = outputDestination;
            this.Priority = priority;
            this.OutputPoint = outputPoint;
        }

        public int OutputDestination
        {
            get;
        }

        public Nullable<OutputPriority> Priority
        {
            get;
        }

        public Nullable<int> OutputPoint
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as OutputRequestDetails );
		}
		
        public bool Equals( OutputRequestDetails other )
		{
            return OutputRequestDetails.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.OutputDestination.GetHashCode();
        }
    }
}
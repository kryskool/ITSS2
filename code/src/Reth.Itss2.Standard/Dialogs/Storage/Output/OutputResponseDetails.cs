using System;

using Reth.Protocols;

namespace Reth.Itss2.Standard.Dialogs.Storage.Output
{
    public class OutputResponseDetails:IEquatable<OutputResponseDetails>
    {
        public static bool operator==( OutputResponseDetails left, OutputResponseDetails right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( OutputResponseDetails left, OutputResponseDetails right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( OutputResponseDetails left, OutputResponseDetails right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = ( left.OutputDestination == right.OutputDestination );
                                                        result &= ( left.Status == right.Status );
                                                        result &= Nullable.Equals( left.Priority, right.Priority );
                                                        result &= Nullable.Equals( left.OutputPoint, right.OutputPoint );

                                                        return result;
                                                    }   );
		}
        
        public OutputResponseDetails(   int outputDestination,
                                        OutputResponseStatus status   )
        {
            this.OutputDestination = outputDestination;
            this.Status = status;
        }

        public OutputResponseDetails(   int outputDestination,
                                        OutputResponseStatus status,
                                        Nullable<OutputPriority> priority,
                                        Nullable<int> outputPoint   )
        {
            this.OutputDestination = outputDestination;
            this.Status = status;
            this.Priority = priority;
            this.OutputPoint = outputPoint;
        }

        public int OutputDestination
        {
            get;
        }

        public OutputResponseStatus Status
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
			return this.Equals( obj as OutputResponseDetails );
		}
		
        public bool Equals( OutputResponseDetails other )
		{
            return OutputResponseDetails.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.OutputDestination.GetHashCode();
        }
    }
}
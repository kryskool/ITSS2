using System;

using Reth.Protocols;

namespace Reth.Itss2.Standard.Dialogs.Storage.Output
{
    public class OutputMessageDetails:IEquatable<OutputMessageDetails>
    {
        public static bool operator==( OutputMessageDetails left, OutputMessageDetails right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( OutputMessageDetails left, OutputMessageDetails right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( OutputMessageDetails left, OutputMessageDetails right )
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
        
        public OutputMessageDetails(    int outputDestination,
                                        OutputMessageStatus status,
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

        public OutputMessageStatus Status
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
			return this.Equals( obj as OutputMessageDetails );
		}
		
        public bool Equals( OutputMessageDetails other )
		{
            return OutputMessageDetails.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.OutputDestination.GetHashCode();
        }
    }
}
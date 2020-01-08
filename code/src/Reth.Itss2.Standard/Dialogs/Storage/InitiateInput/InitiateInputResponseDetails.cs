using System;

using Reth.Protocols;

namespace Reth.Itss2.Standard.Dialogs.Storage.InitiateInput
{
    public class InitiateInputResponseDetails:IEquatable<InitiateInputResponseDetails>
    {
        public static bool operator==( InitiateInputResponseDetails left, InitiateInputResponseDetails right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( InitiateInputResponseDetails left, InitiateInputResponseDetails right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( InitiateInputResponseDetails left, InitiateInputResponseDetails right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = ( left.InputSource == right.InputSource );
                                                        result &= ( left.Status == right.Status );
                                                        result &= Nullable.Equals( left.InputPoint, right.InputPoint );

                                                        return result;
                                                    }   );
		}
        
        public InitiateInputResponseDetails(    int inputSource,
                                                InitiateInputResponseStatus status,
                                                Nullable<int> inputPoint    )
        {
            this.InputSource = inputSource;
            this.Status = status;
            this.InputPoint = inputPoint;
        }

        public int InputSource
        {
            get;
        }

        public InitiateInputResponseStatus Status
        {
            get;
        }

        public Nullable<int> InputPoint
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as InitiateInputResponseDetails );
		}
		
        public bool Equals( InitiateInputResponseDetails other )
		{
            return InitiateInputResponseDetails.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.InputSource.GetHashCode();
        }
    }
}
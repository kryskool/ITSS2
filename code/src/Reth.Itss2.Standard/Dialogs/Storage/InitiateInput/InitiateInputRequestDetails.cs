using System;

using Reth.Protocols;

namespace Reth.Itss2.Standard.Dialogs.Storage.InitiateInput
{
    public class InitiateInputRequestDetails:IEquatable<InitiateInputRequestDetails>
    {
        public static bool operator==( InitiateInputRequestDetails left, InitiateInputRequestDetails right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( InitiateInputRequestDetails left, InitiateInputRequestDetails right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( InitiateInputRequestDetails left, InitiateInputRequestDetails right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = ( left.InputSource == right.InputSource );
                                                        result &= Nullable.Equals( left.InputPoint, right.InputPoint );

                                                        return result;
                                                    }   );
		}
        
        public InitiateInputRequestDetails( int inputSource, Nullable<int> inputPoint )
        {
            this.InputSource = inputSource;
            this.InputPoint = inputPoint;
        }

        public int InputSource
        {
            get;
        }

        public Nullable<int> InputPoint
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as InitiateInputRequestDetails );
		}
		
        public bool Equals( InitiateInputRequestDetails other )
		{
            return InitiateInputRequestDetails.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.InputSource.GetHashCode();
        }
    }
}
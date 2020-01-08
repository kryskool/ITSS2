using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.Int32Extensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.InitiateInput
{
    public class InitiateInputMessageDetails:IEquatable<InitiateInputMessageDetails>
    {
        public static bool operator==( InitiateInputMessageDetails left, InitiateInputMessageDetails right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( InitiateInputMessageDetails left, InitiateInputMessageDetails right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( InitiateInputMessageDetails left, InitiateInputMessageDetails right )
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
        
        private int inputSource;

        public InitiateInputMessageDetails( int inputSource,
                                            InitiateInputMessageStatus status,
                                            Nullable<int> inputPoint    )
        {
            this.InputSource = inputSource;
            this.Status = status;
            this.InputPoint = inputPoint;
        }

        public int InputSource
        {
            get{ return this.inputSource; }

            private set
            {
                value.ThrowIfNegative();

                this.inputSource = value;
            }
        }

        public InitiateInputMessageStatus Status
        {
            get;
        }

        public Nullable<int> InputPoint
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as InitiateInputMessageDetails );
		}
		
        public bool Equals( InitiateInputMessageDetails other )
		{
            return InitiateInputMessageDetails.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.InputSource.GetHashCode();
        }
    }
}
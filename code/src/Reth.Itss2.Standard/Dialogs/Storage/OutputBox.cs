using System;
using System.Globalization;

using Reth.Protocols;
using Reth.Protocols.Extensions.StringExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage
{
    public class OutputBox:IEquatable<OutputBox>
    {
        public static bool operator==( OutputBox left, OutputBox right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( OutputBox left, OutputBox right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( OutputBox left, OutputBox right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        return String.Equals( left.Number, right.Number, StringComparison.InvariantCultureIgnoreCase );
                                                    }   );
		}

        private String number = String.Empty;

        public OutputBox( String number )
        {
            this.Number = number;
        }

        public String Number
        {
            get{ return this.number; }

            private set
            {
                value.ThrowIfNullOrEmpty();

                this.number = value;
            }
        }
        
        public override bool Equals( Object obj )
		{
			return this.Equals( obj as OutputBox );
		}
		
		public bool Equals( OutputBox other )
		{
            return OutputBox.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Number.GetHashCode();
		}

        public override String ToString()
        {
            return this.Number.ToString( CultureInfo.InvariantCulture );
        }
    }
}
using System;

using Reth.Protocols;

namespace Reth.Itss2.Standard.Dialogs.Storage.Input
{
    public class InputResponsePackHandling:IEquatable<InputResponsePackHandling>
    {
        public static bool operator==( InputResponsePackHandling left, InputResponsePackHandling right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( InputResponsePackHandling left, InputResponsePackHandling right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( InputResponsePackHandling left, InputResponsePackHandling right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = ( left.Input == right.Input );
                                                        result &= String.Equals( left.Text, right.Text, StringComparison.InvariantCultureIgnoreCase );

                                                        return result;
                                                    }   );
		}        

        public InputResponsePackHandling( InputResponsePackHandlingInput input )
        {
            this.Input = input;
        }

        public InputResponsePackHandling(   InputResponsePackHandlingInput input,
                                            String text )
        {
            this.Input = input;
            this.Text = text;
        }

        public InputResponsePackHandlingInput Input
        {
            get;
        }

        public String Text
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as InputResponsePackHandling );
		}
		
		public bool Equals( InputResponsePackHandling other )
		{
            return InputResponsePackHandling.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return this.Input.GetHashCode();
		}

        public override String ToString()
        {
            return this.Input.ToString();
        }
    }
}
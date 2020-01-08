using System;

using Reth.Protocols;

namespace Reth.Itss2.Standard.Dialogs.Storage.Input
{
    public class InputMessagePackHandling:IEquatable<InputMessagePackHandling>
    {
        public static bool operator==( InputMessagePackHandling left, InputMessagePackHandling right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( InputMessagePackHandling left, InputMessagePackHandling right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( InputMessagePackHandling left, InputMessagePackHandling right )
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

        public InputMessagePackHandling( InputMessagePackHandlingInput input )
        {
            this.Input = input;
        }

        public InputMessagePackHandling(    InputMessagePackHandlingInput input,
                                            String text )
        {
            this.Input = input;
            this.Text = text;
        }

        public InputMessagePackHandlingInput Input
        {
            get;
        }

        public String Text
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as InputMessagePackHandling );
		}
		
		public bool Equals( InputMessagePackHandling other )
		{
            return InputMessagePackHandling.Equals( this, other );
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
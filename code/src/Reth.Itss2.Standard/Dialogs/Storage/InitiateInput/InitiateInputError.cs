using System;
using System.Globalization;
using System.Text;

using Reth.Protocols;

namespace Reth.Itss2.Standard.Dialogs.Storage.InitiateInput
{
    public class InitiateInputError:IEquatable<InitiateInputError>
    {
        public static bool operator==( InitiateInputError left, InitiateInputError right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( InitiateInputError left, InitiateInputError right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( InitiateInputError left, InitiateInputError right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = ( left.Type == right.Type );
                                                        result &= String.Equals( left.Text, right.Text, StringComparison.InvariantCultureIgnoreCase );

                                                        return result;
                                                    }   );

			
		}

        public InitiateInputError( InitiateInputErrorType type )
        {
            this.Type = type;
        }

        public InitiateInputError( InitiateInputErrorType type, String text )
        {
            this.Type = type;
            this.Text = text;
        }

        public InitiateInputErrorType Type
        {
            get;
        } = InitiateInputErrorType.Rejected;

        public String Text
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as InitiateInputError );
		}
		
		public bool Equals( InitiateInputError other )
		{
            return InitiateInputError.Equals( this, other );
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

        public override String ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append( this.Type.ToString() );

            if( String.IsNullOrEmpty( this.Text ) == false )
            {
                result.Append( " (" );
                result.Append( this.Text.ToString( CultureInfo.InvariantCulture ) );
                result.Append( ")" );
            }

            return result.ToString();
        }
    }
}
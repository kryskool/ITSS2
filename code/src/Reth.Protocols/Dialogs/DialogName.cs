using System;
using System.Collections.Generic;

using Reth.Protocols.Extensions.StringExtensions;

namespace Reth.Protocols.Dialogs
{
    internal class DialogName:IComparable<DialogName>, IDialogName, IEquatable<DialogName>
    {
        public static readonly DialogName Raw = new DialogName( "Raw" );
        public static readonly DialogName Unhandled = new DialogName( "Unhandled" );
        
        static DialogName()
        {            
            DialogName.AvailableNames.Add( DialogName.Raw.Value, DialogName.Raw );
            DialogName.AvailableNames.Add( DialogName.Unhandled.Value, DialogName.Unhandled );
        }

        protected static Dictionary<String, DialogName> AvailableNames
        {
            get;
        } = new Dictionary<String, DialogName>();

        public static bool operator==( DialogName left, DialogName right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( DialogName left, DialogName right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool operator<( DialogName left, DialogName right )
		{
			return ObjectComparer.LessThan( left, right );
		}
		
		public static bool operator<=( DialogName left, DialogName right )
		{
			return ObjectComparer.LessThanOrEqual( left, right );
		}
		
		public static bool operator>( DialogName left, DialogName right )
		{
            return ObjectComparer.GreaterThan( left, right );
		}
		
		public static bool operator>=( DialogName left, DialogName right )
		{
			return ObjectComparer.GreaterThanOrEqual( left, right );
		}

        public static implicit operator String( DialogName instance )
        {
            String result = String.Empty;

            if( !( instance is null ) )
            {
                result = instance.ToString();
            }

            return result;
        }

        public static bool Equals( DialogName left, DialogName right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        return String.Equals( left.Value, right.Value, StringComparison.OrdinalIgnoreCase );
                                                    }   );
		}

        public static int Compare( DialogName left, DialogName right )
		{
            return ObjectComparer.Compare(  left,
                                            right,
                                            () =>
                                            {
                                                return String.Compare( left.Value, right.Value, StringComparison.OrdinalIgnoreCase );
                                            }   );
		}

        public static IReadOnlyDictionary<String, DialogName> GetAvailableNames()
        {
            return DialogName.AvailableNames;
        }

        public DialogName( String value )
        {
            value.ThrowIfNullOrEmpty();

            this.Value = value;
        }

        public String Value
        {
            get; private set;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as DialogName );
		}
		
        public bool Equals( DialogName other )
		{
            return DialogName.Equals( this, other );
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public int CompareTo( DialogName other )
		{
            return DialogName.Compare( this, other );
		}

        public override String ToString()
        {
            return this.Value;
        }
    }
}
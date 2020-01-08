using System;
using System.Collections.Generic;

using Reth.Protocols;
using Reth.Protocols.Dialogs;
using Reth.Protocols.Extensions.StringExtensions;

namespace Reth.Itss2.Standard.Dialogs
{
    public class DialogName:IComparable<DialogName>, IDialogName, IEquatable<DialogName>
    {
        public static readonly DialogName ArticleInfo = new DialogName( "ArticleInfo" );
        public static readonly DialogName ArticleMasterSet = new DialogName( "ArticleMaster" );
        public static readonly DialogName Hello = new DialogName( "Hello" );
        public static readonly DialogName InitiateInput = new DialogName( "InitiateInput" );
        public static readonly DialogName Input = new DialogName( "Input" );
        public static readonly DialogName KeepAlive = new DialogName( "KeepAlive" );
        public static readonly DialogName OutputInfo = new DialogName( "OutputInfo" );
        public static readonly DialogName Output = new DialogName( "Output" );
        public static readonly DialogName Status = new DialogName( "Status" );
        public static readonly DialogName StockDeliveryInfo = new DialogName( "StockDeliveryInfo" );
        public static readonly DialogName StockDeliverySet = new DialogName( "StockDelivery" );
        public static readonly DialogName StockInfo = new DialogName( "StockInfo" );
        public static readonly DialogName StockLocationInfo = new DialogName( "StockLocationInfo" );
        public static readonly DialogName TaskCancelOutput = new DialogName( "TaskCancelOutput" );
        public static readonly DialogName Unprocessed = new DialogName( "Unprocessed" );

        static DialogName()
        {            
            DialogName.AvailableNames.Add( DialogName.ArticleInfo.Value, DialogName.ArticleInfo );
            DialogName.AvailableNames.Add( DialogName.ArticleMasterSet.Value, DialogName.ArticleMasterSet );
            DialogName.AvailableNames.Add( DialogName.Hello.Value, DialogName.Hello );
            DialogName.AvailableNames.Add( DialogName.InitiateInput.Value, DialogName.InitiateInput );
            DialogName.AvailableNames.Add( DialogName.Input.Value, DialogName.Input );
            DialogName.AvailableNames.Add( DialogName.KeepAlive.Value, DialogName.KeepAlive );
            DialogName.AvailableNames.Add( DialogName.OutputInfo.Value, DialogName.OutputInfo );
            DialogName.AvailableNames.Add( DialogName.Output.Value, DialogName.Output );
            DialogName.AvailableNames.Add( DialogName.Status.Value, DialogName.Status );
            DialogName.AvailableNames.Add( DialogName.StockDeliveryInfo.Value, DialogName.StockDeliveryInfo );
            DialogName.AvailableNames.Add( DialogName.StockDeliverySet.Value, DialogName.StockDeliverySet );
            DialogName.AvailableNames.Add( DialogName.StockInfo.Value, DialogName.StockInfo );
            DialogName.AvailableNames.Add( DialogName.StockLocationInfo.Value, DialogName.StockLocationInfo );
            DialogName.AvailableNames.Add( DialogName.TaskCancelOutput.Value, DialogName.TaskCancelOutput );
            DialogName.AvailableNames.Add( DialogName.Unprocessed.Value, DialogName.Unprocessed );
        }

        private static Dictionary<String, DialogName> AvailableNames
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

        protected static void AddAvailableName( String name, DialogName dialog )
        {
            DialogName.AvailableNames.Add( name, dialog );
        }

        public static IEnumerable<IDialogName> GetAvailableNames()
        {
            return DialogName.AvailableNames.Values;
        }

        public DialogName( String value )
        {
            value.ThrowIfNullOrEmpty();

            this.Value = value;
        }

        public String Value
        {
            get;
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
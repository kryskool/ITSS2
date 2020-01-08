using System;
using System.Collections.Generic;
using System.Text;

using Reth.Protocols;

namespace Reth.Itss2.Standard.Dialogs.Storage.Output
{
    public class OutputLabelContent:IEquatable<OutputLabelContent>
    {
        public static bool operator==( OutputLabelContent left, OutputLabelContent right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( OutputLabelContent left, OutputLabelContent right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( OutputLabelContent left, OutputLabelContent right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = true;

                                                        int leftCount = left.Data.Count;
                                                        int rightCount = right.Data.Count;

                                                        if( leftCount == rightCount )
                                                        {
                                                            for( int i = 0; i < leftCount; i++ )
                                                            {
                                                                result &= ( left.Data[ i ] == right.Data[ i ] );
                                                            }
                                                        }else
                                                        {
                                                            result = false;
                                                        }

                                                        return result;
                                                    }   );
		}
        
        public OutputLabelContent( IEnumerable<byte> data )
        {
            if( !( data is null ) )
            {
                this.Data.AddRange( data );   
            }
        }

        private List<byte> Data
        {
            get;
        } = new List<byte>();

        public byte[] GetData()
        {
            return this.Data.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as OutputLabelContent );
		}
		
        public bool Equals( OutputLabelContent other )
		{
            return OutputLabelContent.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.Data.GetHashCode();
        }

        public override String ToString()
        {
            return Encoding.UTF8.GetString( this.GetData() );
        }
    }
}
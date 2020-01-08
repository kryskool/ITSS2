using System;
using System.Collections.Generic;

using Reth.Protocols;
using Reth.Protocols.Extensions.ListExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.OutputInfo
{
    public class OutputInfoArticle:IEquatable<OutputInfoArticle>
    {
        public static bool operator==( OutputInfoArticle left, OutputInfoArticle right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( OutputInfoArticle left, OutputInfoArticle right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( OutputInfoArticle left, OutputInfoArticle right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = ArticleId.Equals( left.Id, right.Id );
                                                        result &= left.Packs.ElementsEqual( right.Packs );

                                                        return result;
                                                    }   );
		}
        
        public OutputInfoArticle( ArticleId id )
        {
            this.Id = id;
        }

        public OutputInfoArticle( ArticleId id, IEnumerable<OutputInfoPack> packs )
        {
            this.Id = id;
            
            if( !( packs is null ) )
            {
                this.Packs.AddRange( packs );
            }
        }

        public ArticleId Id
        {
            get;
        }

        private List<OutputInfoPack> Packs
        {
            get;
        } = new List<OutputInfoPack>();
        
        public OutputInfoPack[] GetPacks()
        {
            return this.Packs.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as OutputInfoArticle );
		}
		
        public bool Equals( OutputInfoArticle other )
		{
            return OutputInfoArticle.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override String ToString()
        {
            return this.Id.ToString();
        }
    }
}
using System;
using System.Collections.Generic;

using Reth.Protocols;
using Reth.Protocols.Extensions.ListExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.Output
{
    public class OutputArticle:IEquatable<OutputArticle>
    {
        public static bool operator==( OutputArticle left, OutputArticle right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( OutputArticle left, OutputArticle right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( OutputArticle left, OutputArticle right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = left.Id.Equals( right.Id );
                                                        result &= left.Packs.ElementsEqual( right.Packs );

                                                        return result;
                                                    }   );
		}

        public OutputArticle( ArticleId id )
        {
            this.Id = id;
        }

        public OutputArticle( ArticleId id, IEnumerable<OutputPack> packs )
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

        private List<OutputPack> Packs
        {
            get;
        } = new List<OutputPack>();
        
        public OutputPack[] GetPacks()
        {
            return this.Packs.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as OutputArticle );
		}
		
        public bool Equals( OutputArticle other )
		{
            return OutputArticle.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
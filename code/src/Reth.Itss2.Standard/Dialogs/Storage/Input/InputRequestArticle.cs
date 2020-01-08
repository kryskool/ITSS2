using System;
using System.Collections.Generic;

using Reth.Protocols;
using Reth.Protocols.Extensions.ListExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.Input
{
    public class InputRequestArticle:IEquatable<InputRequestArticle>
    {
        public static bool operator==( InputRequestArticle left, InputRequestArticle right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( InputRequestArticle left, InputRequestArticle right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( InputRequestArticle left, InputRequestArticle right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = ArticleId.Equals( left.Id, right.Id );
                                                        result &= String.Equals( left.FmdId, right.FmdId, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= left.Packs.ElementsEqual( right.Packs );

                                                        return result;
                                                    }   );
		}

        public InputRequestArticle( ArticleId id,
                                    String fmdId,
                                    IEnumerable<InputRequestPack> packs )
        {
            this.Id = id;
            this.FmdId = fmdId;

            if( !( packs is null ) )
            {
                this.Packs.AddRange( packs );
            }
        }
        
        public ArticleId Id
        {
            get;
        }

        public String FmdId
        {
            get;
        }

        private List<InputRequestPack> Packs
        {
            get;
        } = new List<InputRequestPack>();

        public InputRequestPack[] GetPacks()
        {
            return this.Packs.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as InputRequestArticle );
		}
		
        public bool Equals( InputRequestArticle other )
		{
            return InputRequestArticle.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
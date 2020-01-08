using System;
using System.Collections.Generic;

using Reth.Protocols;
using Reth.Protocols.Extensions.ListExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.InitiateInput
{
    public class InitiateInputRequestArticle:IEquatable<InitiateInputRequestArticle>
    {
        public static bool operator==( InitiateInputRequestArticle left, InitiateInputRequestArticle right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( InitiateInputRequestArticle left, InitiateInputRequestArticle right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( InitiateInputRequestArticle left, InitiateInputRequestArticle right )
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
        
        public InitiateInputRequestArticle( ArticleId id,
                                            String fmdId,
                                            IEnumerable<InitiateInputRequestPack> packs )
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

        private List<InitiateInputRequestPack> Packs
        {
            get;
        } = new List<InitiateInputRequestPack>();
        
        public InitiateInputRequestPack[] GetPacks()
        {
            return this.Packs.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as InitiateInputRequestArticle );
		}
		
        public bool Equals( InitiateInputRequestArticle other )
		{
            return InitiateInputRequestArticle.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
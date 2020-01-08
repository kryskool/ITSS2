using System;
using System.Collections.Generic;

using Reth.Protocols;
using Reth.Protocols.Extensions.ListExtensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.OutputInfo
{
    public class OutputInfoResponseTask:IEquatable<OutputInfoResponseTask>
    {
        public static bool operator==( OutputInfoResponseTask left, OutputInfoResponseTask right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( OutputInfoResponseTask left, OutputInfoResponseTask right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( OutputInfoResponseTask left, OutputInfoResponseTask right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = MessageId.Equals( left.Id, right.Id );
                                                        result &= ( left.Status == right.Status );
                                                        result &= left.Articles.ElementsEqual( right.Articles );
                                                        result &= left.Boxes.ElementsEqual( right.Boxes );

                                                        return result;
                                                    }   );
		}

        private MessageId id;

        public OutputInfoResponseTask(  MessageId id,
                                        OutputInfoStatus status    )
        {
            this.Id = id;
            this.Status = status;
        }

        public OutputInfoResponseTask(  MessageId id,
                                        OutputInfoStatus status,
                                        IEnumerable<OutputInfoArticle> articles,
                                        IEnumerable<OutputBox> boxes    )
        {
            this.Id = id;
            this.Status = status;
            
            if( !( articles is null ) )
            {
                this.Articles.AddRange( articles );
            }

            if( !( boxes is null ) )
            {
                this.Boxes.AddRange( boxes );
            }
        }

        public MessageId Id
        {
            get{ return this.id; }

            private set
            {
                value.ThrowIfNull();

                this.id = value;
            }
        }

        public OutputInfoStatus Status
        {
            get;
        }

        private List<OutputInfoArticle> Articles
        {
            get;
        } = new List<OutputInfoArticle>();

        private List<OutputBox> Boxes
        {
            get;
        } = new List<OutputBox>();

        public OutputInfoArticle[] GetArticles()
        {
            return this.Articles.ToArray();
        }

        public OutputBox[] GetBoxes()
        {
            return this.Boxes.ToArray();
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as OutputInfoResponseTask );
		}
		
        public bool Equals( OutputInfoResponseTask other )
		{
            return OutputInfoResponseTask.Equals( this, other );
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
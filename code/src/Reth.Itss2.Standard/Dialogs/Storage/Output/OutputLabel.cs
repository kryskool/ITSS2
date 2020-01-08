using System;

using Reth.Protocols;
using Reth.Protocols.Extensions.StringExtensions;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.Standard.Dialogs.Storage.Output
{
    public class OutputLabel:IEquatable<OutputLabel>
    {
        public static bool operator==( OutputLabel left, OutputLabel right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( OutputLabel left, OutputLabel right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( OutputLabel left, OutputLabel right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        bool result = false;

                                                        result = String.Equals( left.TemplateId, right.TemplateId, StringComparison.InvariantCultureIgnoreCase );
                                                        result &= OutputLabelContent.Equals( left.Content, right.Content );

                                                        return result;
                                                    }   );
		}
        
        private String templateId = String.Empty;
        private OutputLabelContent content;

        public OutputLabel( String templateId, OutputLabelContent content )
        {
            this.TemplateId = templateId;
            this.Content = content;
        }

        public String TemplateId
        {
            get{ return this.templateId; }

            private set
            {
                value.ThrowIfNullOrEmpty();

                this.templateId = value;
            }
        }

        public OutputLabelContent Content
        {
            get{ return this.content; }

            private set
            {
                value.ThrowIfNull();

                this.content = value;
            }
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as OutputLabel );
		}
		
        public bool Equals( OutputLabel other )
		{
            return OutputLabel.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return this.TemplateId.GetHashCode();
        }
    }
}
using System;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.StandardExtensions.Dialogs.Storage.ConfigurationGet
{
    public class ConfigurationGetRequest:TraceableRequest, IEquatable<ConfigurationGetRequest>
    {
        public static bool operator==( ConfigurationGetRequest left, ConfigurationGetRequest right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ConfigurationGetRequest left, ConfigurationGetRequest right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( ConfigurationGetRequest left, ConfigurationGetRequest right )
		{
			return TraceableRequest.Equals( left, right );
		}        

        public ConfigurationGetRequest( IMessageId id,
                                        SubscriberId source,
                                        SubscriberId destination    )
        :
            base( DialogName.ConfigurationGet, id, source, destination )
        {
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ConfigurationGetRequest );
		}
		
		public bool Equals( ConfigurationGetRequest other )
		{
            return ConfigurationGetRequest.Equals( this, other );
		}

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void Dispatch( IMessageDispatcher dispatcher )
        {
            dispatcher.ThrowIfNull();
            dispatcher.Dispatch( this );
        }
    }
}
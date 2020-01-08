using System;

using Reth.Itss2.Standard.Dialogs;
using Reth.Protocols;
using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Itss2.StandardExtensions.Dialogs.Storage.ConfigurationGet
{
    public class ConfigurationGetResponse:TraceableResponse, IEquatable<ConfigurationGetResponse>
    {
        public static bool operator==( ConfigurationGetResponse left, ConfigurationGetResponse right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( ConfigurationGetResponse left, ConfigurationGetResponse right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public static bool Equals( ConfigurationGetResponse left, ConfigurationGetResponse right )
		{
            return ObjectEqualityComparer.Equals(   left,
                                                    right,
                                                    () =>
                                                    {
                                                        return left.Configuration.Equals( right.Configuration );
                                                    }   );
		}

        public ConfigurationGetResponse(    IMessageId id,
                                            SubscriberId source,
                                            SubscriberId destination,
                                            Configuration configuration )
        :
            base( DialogName.ConfigurationGet, id, source, destination )
        {
            configuration.ThrowIfNull();

            this.Configuration = configuration;
        }

        public ConfigurationGetResponse( ConfigurationGetRequest request, Configuration configuration )
        :
            base( request )
        {
            configuration.ThrowIfNull();

            this.Configuration = configuration;
        }

        public Configuration Configuration
        {
            get;
        }

        public override bool Equals( Object obj )
		{
			return this.Equals( obj as ConfigurationGetResponse );
		}
		
		public bool Equals( ConfigurationGetResponse other )
		{
            return ConfigurationGetResponse.Equals( this, other );
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
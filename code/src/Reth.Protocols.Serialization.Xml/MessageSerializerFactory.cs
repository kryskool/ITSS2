namespace Reth.Protocols.Serialization.Xml
{
	public static class MessageSerializerFactory
	{
        public static IMessageSerializer Create( ITypeMappings typeMappings )
        {
            return new MessageSerializer( typeMappings );
        }
	}
}
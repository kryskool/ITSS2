using System;
using System.Xml.Serialization;

using Reth.Protocols;
using Reth.Protocols.Serialization;

namespace Reth.Itss2.Standard.Serialization.Xml
{
    public class TypeMapping<TInterface, TInstance, TDataContract>:ITypeMapping, IEquatable<TypeMapping<TInterface, TInstance, TDataContract>>
        where TInterface:class
        where TInstance:class
        where TDataContract:IXmlSerializable
    {
        public static bool operator==( TypeMapping<TInterface, TInstance, TDataContract> left, TypeMapping<TInterface, TInstance, TDataContract> right )
		{
			return ObjectEqualityComparer.EqualityOperator( left, right );
		}
		
		public static bool operator!=( TypeMapping<TInterface, TInstance, TDataContract> left, TypeMapping<TInterface, TInstance, TDataContract> right )
		{
			return ObjectEqualityComparer.InequalityOperator( left, right );
		}

        public TypeMapping()
        {
        }

        public Type InterfaceType
        {
            get;
        } = typeof( TInterface );

        public Type InstanceType
        {
            get;
        } = typeof( TInstance );

        public Type DataContractType
        {
            get;
        } = typeof( TDataContract );

        public override int GetHashCode()
        {
            return this.InterfaceType.Name.GetHashCode();
        }

        public override bool Equals( object obj )
        {
            return this.Equals( obj as TypeMapping<TInterface, TInstance, TDataContract> );
        }

        public bool Equals( TypeMapping<TInterface, TInstance, TDataContract> other )
		{
            return ObjectEqualityComparer.Equals(   this,
                                                    other,
                                                    () =>
                                                    {
                                                        return String.Equals( this.InterfaceType.Name, other.InterfaceType.Name, StringComparison.OrdinalIgnoreCase );
                                                    }   );
		}

        public override String ToString()
        {
            return this.InterfaceType.Name;
        }
    }
}
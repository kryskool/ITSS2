// Implementation of the WWKS2 protocol.
// Copyright (C) 2020  Thomas Reth

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.

// You should have received a copy of the GNU Affero General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Reflection;

using Reth.Itss2.Dialogs.Standard.Diagnostics;

namespace Reth.Itss2.Dialogs.Standard.Serialization
{
    internal abstract class DataContractResolver:IDataContractResolver
    {
        protected DataContractResolver( Type serializationProvider, Type dataContractMapping )
        {
            this.DataContracts = this.ResolveContracts( serializationProvider, dataContractMapping );
        }

        protected DataContractResolver( ISerializationProvider serializationProvider, Type dataContractMapping )
        {
            this.DataContracts = this.ResolveContracts( serializationProvider, dataContractMapping );
        }

        private IReadOnlyDictionary<String, DataContractMapping> DataContracts
        {
            get;
        }

        private List<Assembly> GetAssemblies( Type type )
        {
            List<Assembly> result = new List<Assembly>();

            if( type.IsInterface == false &&
                type.Equals( typeof( Object ) ) == false  )
            {
                result.AddRange( this.GetAssemblies( type.BaseType ) );
                result.Add( type.Assembly );
            }

            return result;
        }

        private IReadOnlyDictionary<String, DataContractMapping> ResolveContracts( ISerializationProvider serializationProvider, Type dataContractMapping )
        {
            return this.ResolveContracts( serializationProvider.GetType(), dataContractMapping );
        }

        private IReadOnlyDictionary<String, DataContractMapping> ResolveContracts( Type serializationProvider, Type dataContractMapping )
        {
            Dictionary<String, DataContractMapping> result = new Dictionary<String, DataContractMapping>();

            List<Assembly> assemblies = this.GetAssemblies( serializationProvider );

            foreach( Assembly assembly in assemblies )
            {
                Type[] types = assembly.GetTypes();

                foreach( Type type in types )
                {
                    IEnumerable<DataContractMappingAttribute> attributes = ( IEnumerable<DataContractMappingAttribute> )( type.GetCustomAttributes( dataContractMapping ) );
                    
                    foreach( DataContractMappingAttribute attribute in attributes )
                    {
                        String name = attribute.MessageType.Name;

                        DataContractMapping contractMapping = new DataContractMapping(  attribute.MessageType,
                                                                                        attribute.DataContractType,
                                                                                        type    );

                        if( result.ContainsKey( name ) == true )
                        {
                            result[ name ] = contractMapping;
                        }else
                        {
                            result.Add( name, contractMapping );
                        }
                    }
                }
            }

            return result;
        }

        public DataContractMapping ResolveContract( String messageName )
        {
            if( this.DataContracts.TryGetValue( messageName, out DataContractMapping result ) )
            {
                return result;
            }else
            {
                throw Assert.Exception( new MessageNotSupportedException( $"No data contract found for message '{ messageName }'." ) );
            }
        }
    }
}

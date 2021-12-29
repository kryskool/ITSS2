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
using System.Linq;

using Reth.Itss2.Diagnostics;
using Reth.Itss2.Serialization.Conversion.Messages;

namespace Reth.Itss2.Serialization.Conversion
{
    public class TypeConverter
    {
        public static BooleanConverter Boolean{ get; } = new BooleanConverter();        
        public static DecimalConverter Decimal{ get; } = new DecimalConverter();
        public static Int32Converter Int32{ get; } = new Int32Converter();
        public static MessageIdConverter MessageId{ get; } = new MessageIdConverter();
        public static MessageEnvelopeTimestampConverter MessageEnvelopeTimestamp{ get; } = new MessageEnvelopeTimestampConverter();        
        
        public static TDataContract ConvertFromDataObject<TDataObject, TDataContract>( TDataObject dataObject )
            where TDataContract:IDataContract<TDataObject>
            where TDataObject:notnull
        {
            return ( TDataContract )( Activator.CreateInstance( typeof( TDataContract ), new Object[]{ dataObject } ) )!;
        }

        public static TDataContract? ConvertNullableFromDataObject<TDataObject, TDataContract>( TDataObject? dataObject )
            where TDataContract:class, IDataContract<TDataObject>
            where TDataObject:class
        {
            TDataContract? result = null;

            if( dataObject is not null )
            {
                result = ( TDataContract? )( Activator.CreateInstance( typeof( TDataContract ), new Object[]{ dataObject } ) );
            }

            return result;
        }

        public static TDataObject ConvertToDataObject<TDataObject, TDataContract>( TDataContract dataContract )
            where TDataContract:IDataContract<TDataObject>
        {
            return dataContract.GetDataObject();
        }

        public static TDataObject? ConvertNullableToDataObject<TDataObject, TDataContract>( TDataContract? dataContract )
            where TDataContract:class, IDataContract<TDataObject>
            where TDataObject:class
        {
            TDataObject? result = null;

            if( dataContract is not null )
            {
                result = dataContract.GetDataObject();
            }

            return result;
        }

        public static TDataContract[] ConvertFromDataObjects<TDataObject, TDataContract>( TDataObject[]? dataObjects )
            where TDataContract:IDataContract<TDataObject>
        {
            if( dataObjects is not null )
            {
                return (    from dataObject in dataObjects
                            where dataObject is not null
                            select ( TDataContract? )( Activator.CreateInstance( typeof( TDataContract ), new Object[]{ dataObject } ) ) ).ToArray();

            }

            return new TDataContract[]{};
        }

        public static IEnumerable<TDataObject> ConvertToDataObjects<TDataObject, TDataContract>( TDataContract[]? dataContracts )
            where TDataContract:IDataContract<TDataObject>
        {
            List<TDataObject> result = new List<TDataObject>();

            if( dataContracts is not null )
            {
                foreach( TDataContract capabilityDataContract in dataContracts )
                {
                    if( capabilityDataContract is not null )
                    {
                        try
                        {
                            result.Add( capabilityDataContract.GetDataObject() );   
                        }catch( Exception ex )
                        {
                            Assert.Exception( ex );

                            throw;
                        }
                    }
                }
            }

            return result;
        }

        protected TypeConverter()
        {
        }
    }
}

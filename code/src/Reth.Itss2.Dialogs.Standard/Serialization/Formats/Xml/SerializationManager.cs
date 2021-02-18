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
using System.Xml.Serialization;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Xml
{
    internal static class SerializationManager
    {
        private static Object SyncRoot
        {
            get;
        } = new Object();

        private static Dictionary<Type, XmlSerializer> Serializers
        {
            get;
        } = new Dictionary<Type, XmlSerializer>();

        public static XmlSerializer GetSerializer( Type dataContractType )
        {
            return SerializationManager.GetSerializer( dataContractType, XmlSerializationSettings.EnvelopeRoot );
        }

        public static XmlSerializer GetSerializer( Type dataContractType, XmlRootAttribute xmlRoot )
        {
            lock( SerializationManager.SyncRoot )
            {
                if( SerializationManager.Serializers.TryGetValue( dataContractType, out XmlSerializer result ) )
                {
                    return result;
                }else
                {
                    result = new XmlSerializer( dataContractType, xmlRoot );

                    SerializationManager.Serializers.Add( dataContractType, result );

                    return result;
                }
            }
        }
    }
}

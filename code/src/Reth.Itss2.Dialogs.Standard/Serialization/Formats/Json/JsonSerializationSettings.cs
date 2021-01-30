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
using System.Text;
using System.Text.Json;
using System.Threading;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Json
{
    internal class JsonSerializationSettings:SerializationSettings
    {
        private JsonSerializationSettings()
        {
        }

        private const StringComparison DefaultStringComparison = StringComparison.OrdinalIgnoreCase;

        private static readonly Lazy<JsonSerializerOptions> deserializerOptions = new Lazy<JsonSerializerOptions>(  () =>
                                                                                                                    {
                                                                                                                        JsonSerializerOptions result = new JsonSerializerOptions( JsonSerializerDefaults.Web );

                                                                                                                        return result;
                                                                                                                    },
                                                                                                                    LazyThreadSafetyMode.PublicationOnly );

        private static readonly Lazy<JsonSerializerOptions> serializerOptions = new Lazy<JsonSerializerOptions>(    () =>
                                                                                                                    {
                                                                                                                        JsonSerializerOptions result = new JsonSerializerOptions( JsonSerializerDefaults.Web );

                                                                                                                        return result;
                                                                                                                    },
                                                                                                                    LazyThreadSafetyMode.PublicationOnly );

        public static readonly Encoding Encoding = Encoding.UTF8;
        
        public static StringComparison StringComparison => JsonSerializationSettings.DefaultStringComparison;
        public static char Escape => '\\';

        internal static JsonSerializerOptions DeserializerOptions => JsonSerializationSettings.deserializerOptions.Value;
        internal static JsonSerializerOptions SerializerOptions => JsonSerializationSettings.serializerOptions.Value;
    }
}

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

namespace Reth.Itss2.UnitTests.Tokenization.Json
{
    internal static class TestData
    {
        public static readonly String HelloRequest =    @"  {
                                                                ""HelloRequest"":
                                                                {
                                                                    ""Subscriber"":
                                                                    {
                                                                        ""Capability"":
                                                                        [
                                                                            {
                                                                                ""Name"": ""KeepAlive""
                                                                            },
                                                                            {
                                                                                ""Name"": ""Status""
                                                                            }
                                                                        ],
                                                                        ""Id"": ""100"",
                                                                        ""Type"": ""IMS"",
                                                                        ""Manufacturer"": ""Polly's Pharmacy"",
                                                                        ""ProductInfo"": ""Digital Pharmacy"",
                                                                        ""VersionInfo"": ""1.3"",
                                                                        ""TenantId"": ""4711""
                                                                    },
                                                                    ""Id"": ""10""
                                                                },
                                                                ""Version"": ""2.0"",
                                                                ""TimeStamp"": ""2020-09-16T19:28:00Z""
                                                            }";

        public static readonly String ArticleInfoRequest = @"  {
                                                                    ""ArticleInfoRequest"":
                                                                    {
                                                                        ""Article"":
                                                                        [
                                                                            {
                                                                                ""Id"": ""1234""
                                                                            }
                                                                        ],
                                                                        ""Id"": ""1100"",
                                                                        ""Source"": ""100"",
                                                                        ""Destination"": ""999""
                                                                    },
                                                                    ""Version"": ""2.0"",
                                                                    ""TimeStamp"": ""2020-09-16T19:28:00Z""
                                                                }";
    }
}

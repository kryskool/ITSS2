﻿// Implementation of the WWKS2 protocol.
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

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Reth.Itss2.Dialogs.Standard.Serialization.Formats.Xml;
using Reth.Itss2.Serialization.Formats.Xml;

namespace Reth.Itss2.Dialogs.Standard.UnitTests.Serialization.Formats.Xml
{
    [TestClass]
    public class XmlMessageParserTests
    {
        private const string ExpectedMessageName = "KeepAliveRequest";

        [DataRow( XmlMessageParserTests.ExpectedMessageName, $@"<WWKS Version=""2.0"" TimeStamp=""2021-05-02T20:58:03Z"">
                                                                    <{ XmlMessageParserTests.ExpectedMessageName } Id=""10"" Source=""100"" Destination=""999"" />
                                                                </WWKS>" )]
        [DataRow( XmlMessageParserTests.ExpectedMessageName, $@"<WWKS Version=""2.0"" TimeStamp=""2021-05-02T20:58:03Z""><{ XmlMessageParserTests.ExpectedMessageName } Id=""10"" Source=""100"" Destination=""999"" /></WWKS>" )]
        [DataRow( XmlMessageParserTests.ExpectedMessageName, $@"<{ XmlMessageParserTests.ExpectedMessageName } Id=""10"" Source=""100"" Destination=""999"" />" )]
        [DataTestMethod]
        public void GetMessageName_FromMessageWithOrWithoutEnvelope_Succeeds( String expectedMessageName, String message )
        {
            XmlMessageParser messageParser = new XmlMessageParser( typeof( XmlSerializationProvider ) );

            String actualMessageName = messageParser.GetMessageName( message );
             
            Assert.AreEqual( expectedMessageName, actualMessageName );
        }
    }
}

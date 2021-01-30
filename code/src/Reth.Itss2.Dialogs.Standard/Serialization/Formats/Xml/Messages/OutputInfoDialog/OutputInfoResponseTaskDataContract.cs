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
using System.Xml;
using System.Xml.Serialization;

using Reth.Itss2.Dialogs.Standard.Protocol.Messages.OutputInfoDialog;
using Reth.Itss2.Dialogs.Standard.Serialization.Conversion;

namespace Reth.Itss2.Dialogs.Standard.Serialization.Formats.Xml.Messages.OutputInfoDialog
{
    public class OutputInfoResponseTaskDataContract:IDataContract<OutputInfoResponseTask>
    {
        public OutputInfoResponseTaskDataContract()
        {
        }

        public OutputInfoResponseTaskDataContract( OutputInfoResponseTask dataObject )
        {
            this.Id = TypeConverter.MessageId.ConvertFrom( dataObject.Id );
            this.Status = TypeConverter.OutputInfoStatus.ConvertFrom( dataObject.Status );
            this.Articles = TypeConverter.ConvertFromDataObjects<OutputInfoArticle, OutputInfoArticleDataContract>( dataObject.GetArticles() );
        }

        [XmlAttribute]
        public String Id{ get; set; } = String.Empty;

        [XmlAttribute]
        public String Status{ get; set; } = String.Empty;

        [XmlElement( ElementName = "Article" )]
        public OutputInfoArticleDataContract[]? Articles{ get; set; }
        
        public OutputInfoResponseTask GetDataObject()
        {
            return new OutputInfoResponseTask(  TypeConverter.MessageId.ConvertTo( this.Id ),
                                                TypeConverter.OutputInfoStatus.ConvertTo( this.Status ),
                                                TypeConverter.ConvertToDataObjects<OutputInfoArticle, OutputInfoArticleDataContract>( this.Articles )   );
        }
    }
}

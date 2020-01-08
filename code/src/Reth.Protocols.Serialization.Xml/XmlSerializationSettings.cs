using System;
using System.Text;
using System.Threading;
using System.Xml;

namespace Reth.Protocols.Serialization.Xml
{
    internal static class XmlSerializationSettings
    {
        private static readonly String DefaultXmlPrefix = String.Empty;
        private static readonly String DefaultXmlNamespace = String.Empty;

        private const StringComparison DefaultStringComparison = StringComparison.OrdinalIgnoreCase;

        private static readonly Lazy<XmlReaderSettings> readerSettings = new Lazy<XmlReaderSettings>(   () =>
                                                                                                        {
                                                                                                            XmlReaderSettings result = new XmlReaderSettings();
    
                                                                                                            result.Async = true;
                                                                                                            result.IgnoreWhitespace = false;
                                                                                                            result.IgnoreComments = true;
                                                                                                            
                                                                                                            return result;
                                                                                                        },
                                                                                                        LazyThreadSafetyMode.PublicationOnly    );

        private static readonly Lazy<XmlWriterSettings> writerSettings = new Lazy<XmlWriterSettings>(   () =>
                                                                                                        {
                                                                                                            XmlWriterSettings result = new XmlWriterSettings();
    
                                                                                                            result.Encoding = new UTF8Encoding( false );
                                                                                                            result.OmitXmlDeclaration = true;
                                                                                                            result.CheckCharacters = false;
                                                                                                            result.Async = true;

                                                                                                            return result;
                                                                                                        },
                                                                                                        LazyThreadSafetyMode.PublicationOnly    );

        public static String XmlPrefix
        {
            get{ return XmlSerializationSettings.DefaultXmlPrefix; }
        }

        public static String XmlNamespace
        {
            get{ return XmlSerializationSettings.DefaultXmlNamespace; }
        }

        public static StringComparison StringComparison
        {
            get{ return XmlSerializationSettings.DefaultStringComparison; }
        }

        public static XmlReaderSettings ReaderSettings
        {
            get
            {
                return readerSettings.Value;
            }
        }

        public static XmlWriterSettings WriterSettings
        {
            get
            {
                return writerSettings.Value;
            }
        }
    }
}
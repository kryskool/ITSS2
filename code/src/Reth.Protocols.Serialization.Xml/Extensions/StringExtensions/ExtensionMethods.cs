using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Reth.Protocols.Serialization.Xml.Extensions.StringExtensions
{
    public static class ExtensionMethods
    {
        private const char Space = ( char )0x20;
        private const char Tab = ( char )0x09;
        private const char LineFeed = ( char )0x0A;
        private const char CarriageReturn = ( char )0x0D;
        
        public static String Escape( this String instance )
        {
            String result = instance;

            if( String.IsNullOrWhiteSpace( instance ) == false )
            {
                StringBuilder buffer = new StringBuilder( ( int )( instance.Length * 1.3 ) );
                
                foreach( char character in instance )
                {
                    if( character < ExtensionMethods.Space )
                    {
                        switch( character )
                        {
                            case ExtensionMethods.Tab:
                            case ExtensionMethods.LineFeed:
                            case ExtensionMethods.CarriageReturn:
                                buffer.Append( character );
                                break;

                            default:
                                buffer.AppendFormat( CultureInfo.InvariantCulture, @"\x{0:X2}", Convert.ToByte( character ) );
                                break;
                        }
                    }else
                    {
                        buffer.Append( character );
                    }
                }

                result = buffer.ToString();
            }

            return result;
        }

        public static String Unescape( this String instance )
        {
            String result = instance;

            if( String.IsNullOrWhiteSpace( instance ) == false )
            {                              
                result = Regex.Replace( instance,
                                        @"\\x([0-9A-F]{2})",
                                        new MatchEvaluator( ( Match match ) =>
                                                            {
                                                                byte value = byte.Parse( match.Groups[ 1 ].Value, NumberStyles.HexNumber, CultureInfo.InvariantCulture );

                                                                String replacement = Encoding.UTF8.GetString( new byte[]{ value } );

                                                                return replacement;
                                                            } ),
                                        RegexOptions.IgnoreCase | RegexOptions.Compiled );
            }

            return result;
        }
    }
}
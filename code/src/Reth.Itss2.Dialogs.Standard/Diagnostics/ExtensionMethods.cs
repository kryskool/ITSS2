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
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Reth.Itss2.Dialogs.Standard.Diagnostics
{
    public static class ExtensionMethods
    {
        public static bool WaitDebuggerAware( this ManualResetEventSlim instance, int millisecondsTimeout )
        {
            if( Debugger.IsAttached == false )
            {
                return instance.Wait( millisecondsTimeout );
            }else
            {
                return instance.Wait( -1 );
            }
        }

        public static bool WaitDebuggerAware( this ManualResetEventSlim instance, int millisecondsTimeout, CancellationToken cancellationToken )
        {
            if( Debugger.IsAttached == false )
            {
                return instance.Wait( millisecondsTimeout, cancellationToken );
            }else
            {
                return instance.Wait( -1, cancellationToken );
            }
        }

        public static String Truncate( this String instance )
        {
            return instance.Substring( 0, Math.Min( instance.Length, 256 ) );
        }

        public static String Format( this byte[] instance, Encoding encoding )
        {
            return encoding.GetString( instance, 0, Math.Min( instance.Length, 256 ) );
        }

        public static String Format( this Exception instance )
        {
            String result = String.Empty;

            if( instance is IOException )
            {
                result = ( instance as IOException ).Format();
            }else if( instance is SocketException )
            {
                result =( instance as SocketException ).Format();
            }else if( instance is AggregateException )
            {
                result = ( instance as AggregateException ).Format();
            }else
            {
                result = instance.Format(   ( StringBuilder buffer ) =>
                                            {
                                                buffer.AppendLine( instance.StackTrace );
                                            }   );
            }

            return result;
        }

        public static String Format( this IOException? instance )
        {
            return instance.Format( ( StringBuilder buffer ) =>
                                    {
                                        if( instance is not null )
                                        {
                                            buffer.AppendLine( $"HResult: { String.Format( "0x{0:X8}", instance.HResult ) }" );
                                            buffer.AppendLine( instance.StackTrace );
                                        }
                                    }   );
        }

        public static String Format( this SocketException? instance )
        {
            return instance.Format( ( StringBuilder buffer ) =>
                                    {
                                        if( instance is not null )
                                        {
                                            buffer.AppendLine( $"HResult: { String.Format( "0x{0:X8}", instance.HResult ) }" );
                                            buffer.AppendLine( $"Error code: { instance.ErrorCode }" );
                                            buffer.AppendLine( $"Socket error code: { instance.SocketErrorCode }" );
                                            buffer.AppendLine( instance.StackTrace );
                                        }
                                    }   );
        }

        public static String Format( this AggregateException? instance )
        {
            return instance.Format( ( StringBuilder buffer ) =>
                                    {
                                        if( instance is not null )
                                        {
                                            foreach( Exception innerException in instance.InnerExceptions )
                                            {
                                                buffer.AppendLine( innerException.Format() );
                                            }
                                        }
                                    }   );
        }

        private static String Format( this Exception? instance, Action<StringBuilder>? formatCallback )
        {
            String result = String.Empty;

            if( instance is not null )
            {
                StringBuilder buffer = new StringBuilder( instance.Message ); 

                buffer.AppendLine();

                formatCallback?.Invoke( buffer );

                Exception? innerException = instance.InnerException;

                if( innerException is not null )
                {
                    buffer.AppendLine( innerException.Format() );
                }

                result = buffer.ToString();
            }

            return result;
        }
    }
}

using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Reth.Protocols.Diagnostics
{
    public abstract class ExecutionLogMessage
    {
        private static String FormatException( Exception exception, Action<StringBuilder> formatCallback )
        {
            String result = String.Empty;

            if( !( exception is null ) )
            {
                StringBuilder message = new StringBuilder( exception.ToString() ); 

                message.AppendLine();

                formatCallback?.Invoke( message );

                Exception innerException = exception.InnerException;

                if( !( innerException is null ) )
                {
                    message.AppendLine( ExecutionLogMessage.FormatException( innerException ) );
                }

                result = message.ToString();
            }

            return result;
        }

        protected internal static String FormatException( Exception exception )
        {
            String result = String.Empty;

            if( exception is IOException )
            {
                result = ExecutionLogMessage.FormatException( exception as IOException );
            }else if( exception is SocketException )
            {
                result = ExecutionLogMessage.FormatException( exception as SocketException );
            }else if( exception is AggregateException )
            {
                result = ExecutionLogMessage.FormatException( exception as AggregateException );
            }else
            {
                result = ExecutionLogMessage.FormatException(   exception,
                                                                ( StringBuilder message ) =>
                                                                {
                                                                    message.AppendLine( exception.StackTrace );
                                                                }   );
            }
            
            return result;
        }

        protected internal static String FormatException( SocketException exception )
        {
            return ExecutionLogMessage.FormatException( exception,
                                                        ( StringBuilder message ) =>
                                                        {
                                                            message.AppendLine( $"Error code: { exception.ErrorCode }" );
                                                            message.AppendLine( $"Socket error code: { exception.SocketErrorCode }" );
                                                            message.AppendLine();
                                                            message.AppendLine( exception.StackTrace );
                                                        }   );
        }

        protected internal static String FormatException( IOException exception )
        {
            return ExecutionLogMessage.FormatException( exception,
                                                        ( StringBuilder message ) =>
                                                        {
                                                            message.AppendLine( $"HResult: { exception.HResult }" );
                                                            message.AppendLine();
                                                            message.AppendLine( exception.StackTrace );
                                                        }   );
        }

        protected internal static String FormatException( AggregateException exception )
        {
            return ExecutionLogMessage.FormatException( exception,
                                                        ( StringBuilder message ) =>
                                                        {
                                                            foreach( Exception innerException in exception.InnerExceptions )
                                                            {
                                                                message.AppendLine( ExecutionLogMessage.FormatException( innerException ) );
                                                            }
                                                        }   );
        }

        protected ExecutionLogMessage( ExecutionLogMessageType messageType, String message, String category )
        {
            this.Timestamp = DateTimeOffset.UtcNow;
            this.ThreadId = Thread.CurrentThread.ManagedThreadId;

            this.MessageType = messageType;

            this.Message = message;
            this.Category = category;
        }

        public DateTimeOffset Timestamp
        {
            get;
        }

        public int ThreadId
        {
            get;
        }

        public ExecutionLogMessageType MessageType
        {
            get;
        }

        public String Message
        {
            get;
        }

        public String Category
        {
            get;
        }

        public override String ToString()
        {
            return this.Message;
        }
    }
}

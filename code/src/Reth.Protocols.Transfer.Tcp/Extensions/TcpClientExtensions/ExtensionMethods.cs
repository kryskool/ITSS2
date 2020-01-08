using System;
using System.Net.Sockets;

using Reth.Protocols.Extensions.ObjectExtensions;

namespace Reth.Protocols.Transfer.Tcp.Extensions.TcpClientExtensions
{
	internal static class ExtensionMethods
	{
        public static void SetKeepAlive( this TcpClient instance )
        {
            instance.ThrowIfNull();

            KeepAliveOptions options = new KeepAliveOptions(    true,
                                                                KeepAliveOptions.TimeDefaultValue,
                                                                KeepAliveOptions.IntervalDefaultValue   );

            instance.SetKeepAlive( options );
        }

        public static void SetKeepAlive( this TcpClient instance, KeepAliveOptions options )
        {
            // http://social.msdn.microsoft.com/Forums/en-US/netfxnetcom/thread/d5b6ae25-eac8-4e3d-9782-53059de04628

            instance.ThrowIfNull();
            options.ThrowIfNull();

            byte[] optionInValue = options.GetBytes();
            byte[] optionOutValue = BitConverter.GetBytes( 0 );

            instance.Client.IOControl( IOControlCode.KeepAliveValues, optionInValue, optionOutValue );  
        }

        public static void SetReceiveBufferSize( this TcpClient instance )
        {
            instance.ThrowIfNull();
            instance.ReceiveBufferSize = 8192;
        }

        public static void SetSendBufferSize( this TcpClient instance )
        {
            instance.ThrowIfNull();
            instance.SendBufferSize = 8192;
        }
	}
}
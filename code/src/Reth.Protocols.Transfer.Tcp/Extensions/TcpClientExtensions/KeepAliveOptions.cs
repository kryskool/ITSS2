using System;
using System.Collections.Generic;

namespace Reth.Protocols.Transfer.Tcp.Extensions.TcpClientExtensions
{
	internal class KeepAliveOptions
	{
        public static TimeSpan TimeDefaultValue
        {
            get{ return TimeSpan.FromMilliseconds( 2100 ); }
        }

        public static TimeSpan IntervalDefaultValue
        {
            get{ return TimeSpan.FromMilliseconds( 600 ); }
        }

        public KeepAliveOptions( bool enabled, TimeSpan time, TimeSpan interval )
        {
            this.Enabled = enabled;
            this.Time = time;
            this.Interval = interval;
        }

        public bool Enabled
        {
            get;
        }

        public TimeSpan Time
        {
            get;
        }

        public TimeSpan Interval
        {
            get;
        }

        public byte[] GetBytes()
        {
            const int numberOfValues = 3;

            List<byte> result = new List<byte>( numberOfValues * sizeof( uint ) );

            uint enabled = this.Enabled ? 1u : 0u;

            result.AddRange( BitConverter.GetBytes( enabled ) );
            result.AddRange( BitConverter.GetBytes( ( uint )( this.Time.TotalMilliseconds ) ) );
            result.AddRange( BitConverter.GetBytes( ( uint )( this.Interval.TotalMilliseconds ) ) );

            return result.ToArray();
        }
	}
}
using System;
using System.Diagnostics;

namespace Reth.Protocols.Diagnostics
{
    public static class ExecutionLogProvider
    {
        public static void UseLog( IExecutionLog log )
        {
            ExecutionLogProvider.Log = log;
        }

        public static IExecutionLog Log
        {
            get; private set;
        }

        private static String GetCategory()
        {
            StackFrame stackFrame = new StackFrame( 2 );

            Type declaringType = stackFrame.GetMethod().DeclaringType;

            return declaringType.FullName;
        }

        public static void LogInformation( String text )
        {
            try
            {
                ExecutionLogProvider.Log?.LogMessage( new ExecutionLogInformation( text, ExecutionLogProvider.GetCategory() ) );
            }catch
            {
            }
        }

        public static void LogWarning( String text )
        {
            try
            {
                ExecutionLogProvider.Log?.LogMessage( new ExecutionLogWarning( text, ExecutionLogProvider.GetCategory() ) );
            }catch
            {
            }
        }

        public static void LogWarning( Exception exception )
        {
            try
            {
                ExecutionLogProvider.Log?.LogMessage( ExecutionLogWarning.FromException( exception, ExecutionLogProvider.GetCategory() ) );
            }catch
            {
            }
        }

		public static void LogError( String text )
        {
            try
            {
                ExecutionLogProvider.Log?.LogMessage( new ExecutionLogError( text, ExecutionLogProvider.GetCategory() ) );
            }catch
            {
            }
        }

		public static void LogError( Exception exception )
        {
            try
            {
                ExecutionLogProvider.Log?.LogMessage( ExecutionLogError.FromException( exception, ExecutionLogProvider.GetCategory() ) );
            }catch
            {
            }
        }
    }
}
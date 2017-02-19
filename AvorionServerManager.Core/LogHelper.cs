using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace AvorionServerManager.Core
{
    public static class LogHelper
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);

            return stackFrame.GetMethod().Name;
        }
        public static string MethodEnterMessage(DateTime timeStamp, string methodName)
        {
            return (timeStamp.ToString("hh:mm:ss:fff") + ": Enter " + methodName);
        }
        public static string StandardLogMessage(DateTime timeStamp, string message)
        {
            return (timeStamp.ToString("hh:mm:ss:fff") +" " + message);
        }
    }
}

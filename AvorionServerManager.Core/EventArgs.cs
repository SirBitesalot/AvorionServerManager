namespace AvorionServerManager.Core
{
    public class LogEventEventArgs
    {
        public LogEventEventArgs(LogLevel logLevel, string message) { Message = message; }
        public string Message { get; private set; } // readonly
    }
    public delegate void LogEvent(object sender, LogEventEventArgs e);
}

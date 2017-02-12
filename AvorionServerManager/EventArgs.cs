using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvorionServerManager
{
    public class LogMessageReceivedEventArgs
    {
        public LogMessageReceivedEventArgs(string message) { Message = message; }
        public string Message { get; private set; } // readonly
    }
    public class ServerStoppedEventArgs
    {
        public ServerStoppedEventArgs() { }
    }
}

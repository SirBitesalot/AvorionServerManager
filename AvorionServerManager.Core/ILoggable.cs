using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvorionServerManager.Core
{
    public interface ILoggable
    {
         event LogEvent LogEvent;
    }
}

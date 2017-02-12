using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvorionServerManager.Utils
{
    public static class StringUtils
    {
        public static string EscapeCommandString(string command)
        {
            return "\"" + command + "\"";
        }
    }
}

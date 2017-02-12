using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvorionServerManager
{
    [Serializable]
    public class ManagerSettings
    {
        public string AvorionFolder{ get; set;}
        public int HttpServerPort { get; set;}
        public string SteamCmdFolder { get; set;}
        public string SteamCmdScritpsFolder { get; set; }
    }
}

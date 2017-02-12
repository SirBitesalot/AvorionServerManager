using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvorionServerManager
{
    [Serializable]
    public class BackupSettings
    {
        public string BackupPath { get; set;}
        public int BackupInterval { get; set;}
        public bool SaveOnStartup { get; set;}
        public bool SaveOnStop { get; set; }
        public bool CompressBackups { get; set;}
    }
}

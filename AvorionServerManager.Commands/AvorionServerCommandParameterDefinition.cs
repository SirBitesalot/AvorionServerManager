using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvorionServerManager.Commands
{
    public class AvorionServerCommandParameterDefinition
    {
        public string Prefix { get; set; }
        public string DisplayName { get; set; }
        public AvorionServerCommandParameterDefinition()
        {
            Prefix = string.Empty;
        }
    }
}

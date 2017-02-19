using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvorionServerManager.Commands
{
    [Serializable]
    public class AvorionServerCommandDefinition
    {
        public string DisplayName { get; set;}
        public string LuaName { get; set; }
        public int Id { get; set;}
        public List<string> ParameterNames { get; set; }

        public AvorionServerCommandDefinition(string name,string luaName, int id, List<string> parameterNames)
        {
            DisplayName = name;
            LuaName = luaName;
            Id = id;
            ParameterNames = parameterNames;
        }
        public override string ToString()
        {
            return DisplayName;
        }
    }
}

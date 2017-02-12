using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvorionServerManager
{
    
   public class AvorionServerCommand
    {
        public string Name { get; set;}
        public int CommandId { get; set;}
        public bool HasParameters { get;  set; }
        public List<string> Parameters { get; set; }
      
        public AvorionServerCommand()
        {

        }
        public AvorionServerCommand(string name, int id)
        {
            Name = name;
            CommandId = id;
            HasParameters = false;
        }
        public AvorionServerCommand(string name, int id,List<string> parameters)
        {
            Name = name;
            CommandId = id;
            Parameters = parameters;
            HasParameters = true;
        }
        public override string ToString()
        {
            return Name;
        }
    }
}

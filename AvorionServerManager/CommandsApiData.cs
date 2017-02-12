using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvorionServerManager
{
    static class CommandsApiData
    {
        static Queue<AvorionServerCommand> _commandsToExecute =new Queue<AvorionServerCommand>();
        public static void AddCommand(AvorionServerCommand command)
        {
            _commandsToExecute.Enqueue(command);
        }
        public static AvorionServerCommand DequeueCommand()
        {
            AvorionServerCommand result = null;
            if (_commandsToExecute.Count > 0)
            {
                result= _commandsToExecute.Dequeue();
            }
            return result;
        }
        public static List<AvorionServerCommand> AllCommands()
        {
            return  new List<AvorionServerCommand>(_commandsToExecute);
        }
    }
}

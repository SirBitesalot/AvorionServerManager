using System.Collections.Generic;
using AvorionServerManager.Commands;
namespace AvorionServerManager
{
    static class CommandsApiData
    {
        static Queue<AvorionServerCommand> _commandsToExecute =new Queue<AvorionServerCommand>();
        public static ManagerController ManagerController { get; set; }
        public static void AddCommand(AvorionServerCommand command)
        {
            switch (command.ExecutionType)
            {
                case CommandExecutionTypes.Lua:
                    _commandsToExecute.Enqueue(command);
                    break;
                case CommandExecutionTypes.Console:
                    ManagerController.Server.SendCommand(command);
                    break;
                default:
                    break;
            }
            
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

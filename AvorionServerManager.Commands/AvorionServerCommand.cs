using System.Collections.Generic;

namespace AvorionServerManager.Commands
{

    public class AvorionServerCommand
    {
        public string InternalName { get; set; }
        public int InternalId { get; set; }
        public List<AvorionServerCommandParameter> Parameters { get; set; }
        public bool HasParameters { get; set; }
        public CommandExecutionTypes ExecutionType { get; set; }

        public AvorionServerCommand(AvorionServerCommandDefinition definition)
        {
            InternalName = definition.InternalName;
            InternalId = definition.InternalId;
            HasParameters = definition.HasParameters;
            ExecutionType = definition.ExecutionType;
        }
        public AvorionServerCommand(AvorionServerCommandDefinition definition, List<AvorionServerCommandParameter> parameters)
        {
            InternalName = definition.InternalName;
            InternalId = definition.InternalId;
            HasParameters = definition.HasParameters;
            ExecutionType = definition.ExecutionType;
            Parameters = new List<AvorionServerCommandParameter>(parameters);
        }
     
    }
}

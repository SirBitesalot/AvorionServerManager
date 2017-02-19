using System;
using System.Collections.Generic;

namespace AvorionServerManager.Commands
{
    [Serializable]
    public class AvorionServerCommandDefinition
    {
        /// <summary>
        /// The name that will be displayed in GUI
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// The inernally used name to call the command either in lua code or in the console.
        /// Example for Lua: Server():save
        /// Example for Console: /save
        /// </summary>
        public string InternalName { get; set; }
        /// <summary>
        /// Id used for mapping in the lua interface. Must be unique
        /// </summary>
        public int InternalId { get; set; }
        /// <summary>
        /// List of parameter Definitons
        /// </summary>
        public List<AvorionServerCommandParameterDefinition> ParameterDefinitions { get; set; }
        /// <summary>
        /// Determines if Command should be executed in Console or via lua interface
        /// </summary>
        public CommandExecutionTypes ExecutionType { get; set; }
        public bool HasParameters { get; set; }
        public AvorionServerCommandDefinition()
        {
            ParameterDefinitions = new List<AvorionServerCommandParameterDefinition>();
        }
        public void AddParameterDefinition(AvorionServerCommandParameterDefinition definition)
        {
            ParameterDefinitions.Add(definition);
        }
        public override string ToString()
        {
            switch (ExecutionType)
            {
                case CommandExecutionTypes.Lua:
                    return "[Lua]"+DisplayName;
                case CommandExecutionTypes.Console:
                    return "[CLI]" + DisplayName;
                default:
                    return DisplayName;
            }
            
        }
    }
}

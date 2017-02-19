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

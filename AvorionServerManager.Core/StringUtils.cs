namespace AvorionServerManager.Core
{
    public static class StringUtils
    {
        public static string EscapeCommandString(string command)
        {
            return "\"" + command + "\"";
        }
    }
}

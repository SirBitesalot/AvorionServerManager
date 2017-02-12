using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvorionServerManager
{
    static class CommandLoader
    {
        public static List<AvorionServerCommandDefinition> GetCommands()
        {
            List<AvorionServerCommandDefinition> result = new List<AvorionServerCommandDefinition>();
            if (File.Exists(Path.Combine(Constants.SettingsFolderName, Constants.CommandDefinitonsFileName)))
            {
                using (StreamReader file = File.OpenText(Path.Combine(Constants.SettingsFolderName, Constants.CommandDefinitonsFileName)))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    result = (List<AvorionServerCommandDefinition>)serializer.Deserialize(file, typeof(List<AvorionServerCommandDefinition>));
                }
            }
            return result;
        }
    }
}

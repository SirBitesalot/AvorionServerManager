using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvorionServerManager.Core;
using System.IO;
using System.Globalization;

namespace AvorionServerManager.Server
{
    [Serializable]
    public class AvorionServerSettings
    {
        public string DataPath { get; set; }
        public string ServerName { get; set; }
        public string GalaxyName { get; set; }
        public string Description { get; set; }
        public string Seed { get; set; }
        public int MaxPlayers { get; set;}
        public int Threads { get; set;}
        public DifficultySettings Difficulty { get; set; }
        public double CollisionDamage { get; set; }
        public bool CreativeMode { get; set; }
        public bool ShareHomesector { get; set; }
        public bool UserAuthentication { get; set; }
        public bool ListPublic { get; set; }
        public bool ExitOnLastAdminLogout { get; set; }
        public bool UseSteamNetworking { get; set;}
        public int SaveInterval { get; set; }
        public List<string> AdminIds { get; set;}
        public string AdditionalArguments { get; set;}
        public int Port { get; set; }

        private const string PortPrefix = "--port";
        private const string DataPathPrefix = "--datapath";
        private const string ServerNamePrefix = "--server-name";
        private const string GalaxyNamePrefix = "--galaxy-name";
        private const string SeedPrefix = "--seed";
        private const string MaxPlayersPrefix = "--max-players";
        private const string ThreadsPrefix = "--threads";
        private const string DifficultyPrefix = "--difficulty";
        private const string CollisionDamagePrefix = "--collision-damage";
        private const string CreativeModePrefix = "--infinite-resources";
        private const string ShareHomesectorPrefix = "--same-start-sector";
        private const string UserAuthenticationPrefix = "--authentication";
        private const string ListPublicPrefix = "--listed";
        private const string ExitOnLastAdminLogoutPrefix ="--exit-on-last-admin-logout";
        private const string SaveIntervalPrefix = "--save-interval";
        private const string UseSteamNetworkingPrefix = "--use-steam-networking";

        public AvorionServerSettings()
        {
            SaveInterval = 600;//default;
            ExitOnLastAdminLogout = false;
            AdditionalArguments = string.Empty;
        }
        private string PortArgument()
        {
            return PortPrefix + " " + Port;
        }
        private string DataPathArgument()
        {
            return DataPathPrefix +" " +  StringUtils.EscapeCommandString(DataPath);
        }
        private string ServerNameArgument()
        {
            return ServerNamePrefix+ " " + StringUtils.EscapeCommandString(ServerName.Replace(" ","_"));
        }
        private string GalaxyNameArgument()
        {
            return GalaxyNamePrefix+ " " + StringUtils.EscapeCommandString(GalaxyName).Replace(" ", "_");
        }
        private string SeedArgument()
        {
            return SeedPrefix+ " " + Seed;
        }
        private string MaxPlayersArgument()
        {
            return MaxPlayersPrefix + " " + MaxPlayers;
        }
        private string ThreadsArgument()
        {
            return ThreadsPrefix + " " + Threads;
        }
        private string DifficultyArgument()
        {
            return DifficultyPrefix + " " + (int)Difficulty;
        }
        private string CollisionDamageArgument()
        {
            return CollisionDamagePrefix + " " + CollisionDamage.ToString(CultureInfo.CreateSpecificCulture("en-US"));
        }
        private string CreativeModeArgument()
        {
            if (CreativeMode)
            {
                return CreativeModePrefix + " 1";
            }else
            {
                return CreativeModePrefix + " 0";
            }
        }
        private string ShareHomesectorArgument()
        {
            if (ShareHomesector)
            {
                return ShareHomesectorPrefix + " 1";
            }else
            {
                return ShareHomesectorPrefix + " 0";
            }
        }
        private string UserAuthenticationArgument()
        {
            if (UserAuthentication)
            {
                return UserAuthenticationPrefix + " 1";
            }else
            {
                return UserAuthenticationPrefix + " 0";
            }
        }
        private string UseSteamNetworkingArgument()
        {
            if (UseSteamNetworking)
            {
                return UseSteamNetworkingPrefix + " 1";
            }
            else
            {
                return UseSteamNetworkingPrefix + " 0";
            }
        }
        private string ListPublicArgument()
        {
            if (ListPublic)
            {
                return ListPublicPrefix + " 1";
            }else
            {
                return ListPublicPrefix + " 0";
            }
        }
        private string ExitOnLastAdminLogoutArgument()
        {
            if (ExitOnLastAdminLogout)
            {
                return ExitOnLastAdminLogoutPrefix + " 1";
            }else
            {
                return ExitOnLastAdminLogoutPrefix + " 0";
            }
        }
        private string SaveIntervalArgument()
        {
            return SaveIntervalPrefix + " " + SaveInterval;
        }
        public void LoadSettingsFromIni(string path)
        {
            List<string> iniLines = File.ReadAllLines(Path.Combine(path)).ToList();
            Dictionary<string, string> settingsDictionary = new Dictionary<string, string>();
            foreach (string currentLine in iniLines)
            {
                if (currentLine.Contains("="))
                {
                    string[] lineParts = currentLine.Split('=');
                    {
                        settingsDictionary.Add(lineParts[0], lineParts[1]);
                    }
                }
            }
            string tmpValue = string.Empty;
            if(settingsDictionary.TryGetValue(Constants.ServerNameIdentifier,out tmpValue)){
                ServerName = tmpValue;
            }
            if (settingsDictionary.TryGetValue(Constants.SeedIdentifier, out tmpValue))
            {
                Seed = tmpValue;
            }
            if (settingsDictionary.TryGetValue(Constants.MaxPlayersIdentifier, out tmpValue))
            {
                int tmpMaxPlayers = 0;
                if (int.TryParse(tmpValue,out tmpMaxPlayers)){
                    MaxPlayers = tmpMaxPlayers;
                }else
                {
                    MaxPlayers = 10;
                }
                
            }
            if (settingsDictionary.TryGetValue(Constants.ThreadsIdentifier, out tmpValue))
            {
                int tmpThreads = 0;
                if (int.TryParse(tmpValue, out tmpThreads))
                {
                    Threads = tmpThreads;
                }
                else
                {
                    Threads = Environment.ProcessorCount;
                }

            }
            if (settingsDictionary.TryGetValue(Constants.DifficultyIdentifier, out tmpValue))
            {
                int tmpDifficulty = 0;
                if (int.TryParse(tmpValue, out tmpDifficulty))
                {
                    Difficulty =  (DifficultySettings)tmpDifficulty;
                }
                else
                {
                    Difficulty = DifficultySettings.Normal;
                }

            }
            if (settingsDictionary.TryGetValue(Constants.CollisionDamageIdentifier, out tmpValue))
            {
                double tmpCollisionDamage = 0;
                if (double.TryParse(tmpValue, NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out tmpCollisionDamage))
                {
                    CollisionDamage = tmpCollisionDamage;
                }
                else
                {
                    CollisionDamage = 1;
                }

            }
            if (settingsDictionary.TryGetValue(Constants.CreativeModeIdentifier, out tmpValue))
            {
                bool tmpCreativeMode = false;
                if(bool.TryParse(tmpValue,out tmpCreativeMode)){
                    CreativeMode = tmpCreativeMode;
                }
                else
                {
                    CreativeMode = false;
                }
            }
            if (settingsDictionary.TryGetValue(Constants.SameStartsectorIdentifier, out tmpValue))
            {
                bool tmpShareHomeSector = false;
                if (bool.TryParse(tmpValue, out tmpShareHomeSector)){
                    ShareHomesector = tmpShareHomeSector;
                }
                else
                {
                    ShareHomesector = true;
                }
            }
            if (settingsDictionary.TryGetValue(Constants.UserAuthenticationIdentifier, out tmpValue))
            {
                bool tmpUserAuthentication = false;
                if (bool.TryParse(tmpValue, out tmpUserAuthentication))
                {
                    UserAuthentication = tmpUserAuthentication;
                }
                else
                {
                    UserAuthentication = true;
                }
            }
            if (settingsDictionary.TryGetValue(Constants.ListPublicIdentifier, out tmpValue))
            {
                bool tmpListPublic = false;
                if (bool.TryParse(tmpValue, out tmpListPublic))
                {
                    ListPublic = tmpListPublic;
                }
                else
                {
                    ListPublic = false;
                }
            }
            if (settingsDictionary.TryGetValue(Constants.UseSteamIdentifier, out tmpValue))
            {
                bool tmpUseSteam = false;
                if (bool.TryParse(tmpValue, out tmpUseSteam))
                {
                    UseSteamNetworking = tmpUseSteam;
                }
                else
                {
                    UseSteamNetworking = false;
                }
            }
            if (settingsDictionary.TryGetValue(Constants.SaveIntervalIdentifier, out tmpValue))
            {
                int tmpSaveInterval = 0;
                if (int.TryParse(tmpValue, out tmpSaveInterval))
                {
                    SaveInterval = tmpSaveInterval;
                }
                else
                {
                    SaveInterval = Environment.ProcessorCount;
                }

            }
            if (settingsDictionary.TryGetValue(Constants.PortIdentifier, out tmpValue))
            {
                int tmpPort = 0;
                if (int.TryParse(tmpValue, out tmpPort))
                {
                    Port = tmpPort;
                }
                else
                {
                    Port = Environment.ProcessorCount;
                }

            }
        }
        public string GetCommandLine()
        {
            List<string> guiDefinedArguments = new List<string>();
            guiDefinedArguments.Add(DataPathArgument());
            guiDefinedArguments.Add(ServerNameArgument());
            guiDefinedArguments.Add(GalaxyNameArgument());
            guiDefinedArguments.Add(SeedArgument());
            guiDefinedArguments.Add(MaxPlayersArgument());
            guiDefinedArguments.Add(ThreadsArgument());
            guiDefinedArguments.Add(DifficultyArgument());
            guiDefinedArguments.Add(CollisionDamageArgument());
            guiDefinedArguments.Add(CreativeModeArgument());
            guiDefinedArguments.Add(ShareHomesectorArgument());
            guiDefinedArguments.Add(UserAuthenticationArgument());
            guiDefinedArguments.Add(ListPublicArgument());
            guiDefinedArguments.Add(SaveIntervalArgument());
            guiDefinedArguments.Add(UseSteamNetworkingArgument());
            guiDefinedArguments.Add(PortArgument());
            StringBuilder commandLineStringBuilder = new StringBuilder();
            foreach(string currentArgument in guiDefinedArguments)
            {
                if (AdditionalArguments!=null &&!AdditionalArguments.Contains(currentArgument)){
                    commandLineStringBuilder.Append(currentArgument + " ");
                }
            }
            commandLineStringBuilder.Append(" " + AdditionalArguments);
            return commandLineStringBuilder.ToString();
        }
     
    }
}

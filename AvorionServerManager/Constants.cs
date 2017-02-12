using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvorionServerManager
{
    class Constants
    {
        public const string SettingsFolderName = "settings";
        public const string ManagerSettingsFileName = "ManagerSettings.json";
        public const string AvorionServerSettingsFileName = "ServerSettings.json";
        public const string BackupSettingsFileName = "BackupSettings.json";
        public const string CommandRequest = "asm_commandrequest";
        public const string CommandDefinitonsFileName = "CommandDefinitions.json";
        public const string UpdateTickIdentifier = "asm_UpdateTick";
        public const string ServerLuaFileName = "server.lua";
        public const string SteamCmdFolderName = "SteamCMD";
        public const string SteamCmdInstallScriptFileName = "InstallScript.txt";
        public const string SteamCmdBetaUpdateScriptFileName = "BetaUpdateScript.txt";
        public const string SteamCmdScriptsFolderName = "SteamCMDScripts";
        public const string SteamCmdDownloadLink = "https://steamcdn-a.akamaihd.net/client/installer/steamcmd.zip";
        public const string ApiKeyFileName = "ApiKeys.txt";
        public const int DefaultApiPort = 51234;

        #region Ini Identifiers
        public const string SeedIdentifier = "Seed";
        public const string DifficultyIdentifier = "Difficulity";
        public const string CreativeModeIdentifier = "InfiniteResources";
        public const string CollisionDamageIdentifier = "CollisionDamage";
        public const string SameStartsectorIdentifier = "sameStartSector";
        public const string SaveIntervalIdentifier = "saveInterval";
        public const string ThreadsIdentifier = "workerThreads";
        public const string PortIdentifier = "port";
        public const string ListPublicIdentifier = "isListed";
        public const string UserAuthenticationIdentifier = "isAuthenticated";
        public const string UseSteamIdentifier = "useSteam";
        public const string MaxPlayersIdentifier = "maxPlayers";
        public const string ServerNameIdentifier = "name";
        public const string DescriptionIdentifier = "description";
        #endregion
    }
}

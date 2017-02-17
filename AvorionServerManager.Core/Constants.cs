using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvorionServerManager
{
    public class Constants
    {
        public static readonly string SettingsFolderName = "settings";
        public static readonly string ManagerSettingsFileName = "ManagerSettings.json";
        public static readonly string AvorionServerSettingsFileName = "ServerSettings.json";
        public static readonly string BackupSettingsFileName = "BackupSettings.json";
        public static readonly string CommandRequest = "asm_commandrequest";
        public static readonly string CommandDefinitonsFileName = "CommandDefinitions.json";
        public static readonly string UpdateTickIdentifier = "asm_UpdateTick";
        public static readonly string ServerLuaFileName = "server.lua";
        public static readonly string SteamCmdFolderName = "SteamCMD";
        public static readonly string SteamCmdInstallScriptFileName = "InstallScript.txt";
        public static readonly string SteamCmdBetaUpdateScriptFileName = "BetaUpdateScript.txt";
        public static readonly string SteamCmdScriptsFolderName = "SteamCMDScripts";
        public static readonly string SteamCmdDownloadLink = "https://steamcdn-a.akamaihd.net/client/installer/steamcmd.zip";
        public static readonly string ApiKeyFileName = "ApiKeys.txt";
        public static readonly int DefaultApiPort = 51234;

        #region Ini Identifiers
        public static readonly string SeedIdentifier = "Seed";
        public static readonly string DifficultyIdentifier = "Difficulity";
        public static readonly string CreativeModeIdentifier = "InfiniteResources";
        public static readonly string CollisionDamageIdentifier = "CollisionDamage";
        public static readonly string SameStartsectorIdentifier = "sameStartSector";
        public static readonly string SaveIntervalIdentifier = "saveInterval";
        public static readonly string ThreadsIdentifier = "workerThreads";
        public static readonly string PortIdentifier = "port";
        public static readonly string ListPublicIdentifier = "isListed";
        public static readonly string UserAuthenticationIdentifier = "isAuthenticated";
        public static readonly string UseSteamIdentifier = "useSteam";
        public static readonly string MaxPlayersIdentifier = "maxPlayers";
        public static readonly string ServerNameIdentifier = "name";
        public static readonly string DescriptionIdentifier = "description";
        #endregion
    }
}

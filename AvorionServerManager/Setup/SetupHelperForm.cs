using AvorionServerManager.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvorionServerManager.Setup
{
    public partial class SetupHelperForm : Form
    {
        enum SetupStep {Welcome,InstallServer,ManagerSettings,ServerSettings,BackupSettings,SetupFinished,LuaCommands,WebApiSsl,WebApiSslSetup,WebApiKeys,WebApiCommands}
        SetupStep _step;
        private ManagerController _managerController;
        ManagerMainForm _managerMainForm;
        public SetupHelperForm(ManagerController managerController, ManagerMainForm managerMainForm)
        {
            InitializeComponent();
            _managerController = managerController;
            _managerMainForm = managerMainForm;
        }

        private void SetupHelperForm_Load(object sender, EventArgs e)
        {
            helperRichTextBox.LinkClicked += HelperRichTextBox_LinkClicked;
            _step = SetupStep.Welcome;
            helperRichTextBox.Rtf = GetResourceByName("AvorionServerManager.Setup.SetupHelperWelcome.rtf");
            doItButton.Visible = false;
        }

        private void HelperRichTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        private string GetResourceByName(string resource)
        {
            StreamReader reader = new StreamReader(GetType().Assembly.GetManifestResourceStream(resource));
            return reader.ReadToEnd();
        }
        private void ScrollToTop()
        {
            helperRichTextBox.SelectionStart = 0;
            helperRichTextBox.SelectionLength = 0;
            helperRichTextBox.ScrollToCaret();
        }
        private void nextStepButton_Click(object sender, EventArgs e)
        {
            GoToNexStep();
        }
        private void GoToPreviousStep()
        {
            switch (_step)
            {
                case SetupStep.Welcome:
                    break;
                case SetupStep.InstallServer:
                    _step = SetupStep.ManagerSettings;
                    helperRichTextBox.Rtf = GetResourceByName("AvorionServerManager.Setup.ManagerSettings.rtf");
                    _managerMainForm.ChangeTabBarItem(1);
                    Activate();
                    ScrollToTop();
                    break;
                case SetupStep.ManagerSettings:
                    doItButton.Visible = false;
                    _step = SetupStep.Welcome;
                    helperRichTextBox.Rtf = GetResourceByName("AvorionServerManager.Setup.SetupHelperWelcome.rtf");
                    Activate();
                    ScrollToTop();
                    break;
                case SetupStep.ServerSettings:
                    _step = SetupStep.InstallServer;
                    helperRichTextBox.Rtf = GetResourceByName("AvorionServerManager.Setup.ServerInstallation.rtf");
                    _managerMainForm.ChangeTabBarItem(4);
                    Activate();
                    ScrollToTop();
                    break;
                case SetupStep.BackupSettings:
                    _step = SetupStep.ServerSettings;
                    helperRichTextBox.Rtf = GetResourceByName("AvorionServerManager.Setup.AvorionServerSettings.rtf");
                    _managerMainForm.ChangeTabBarItem(2);
                    Activate();
                    ScrollToTop();
                    break;
                case SetupStep.SetupFinished:
                    doItButton.Visible = true;
                    _step = SetupStep.BackupSettings;
                    helperRichTextBox.Rtf = GetResourceByName("AvorionServerManager.Setup.BackupSettings.rtf");
                    ScrollToTop();
                    break;
                case SetupStep.LuaCommands:
                    _step = SetupStep.SetupFinished;
                    helperRichTextBox.Rtf = GetResourceByName("AvorionServerManager.Setup.AdditionalSteps.rtf");
                    ScrollToTop();
                    break;
                case SetupStep.WebApiSsl:
                    _step = SetupStep.LuaCommands;
                    helperRichTextBox.Rtf = GetResourceByName("AvorionServerManager.Setup.LuaCommands.rtf");
                    ScrollToTop();
                    break;
                case SetupStep.WebApiSslSetup:
                    _step = SetupStep.WebApiSsl;
                    helperRichTextBox.Rtf = GetResourceByName("AvorionServerManager.Setup.WebApiSsl.rtf");
                    ScrollToTop();
                    break;
                case SetupStep.WebApiKeys:
                    _step = SetupStep.WebApiSslSetup;
                    helperRichTextBox.Rtf = GetResourceByName("AvorionServerManager.Setup.WebApiSetupSsl.rtf");
                    ScrollToTop();
                    break;
                case SetupStep.WebApiCommands:
                    _step = SetupStep.WebApiKeys;
                    helperRichTextBox.Rtf = GetResourceByName("AvorionServerManager.Setup.ApiKeys.rtf");
                    ScrollToTop();
                    break;
                default:
                    break;
            }
        }
        private void GoToNexStep()
        {
            switch (_step)
            {
                case SetupStep.Welcome:
                    doItButton.Visible = true;
                    _step = SetupStep.ManagerSettings;
                    helperRichTextBox.Rtf = GetResourceByName("AvorionServerManager.Setup.ManagerSettings.rtf");
                    _managerMainForm.ChangeTabBarItem(1);
                    Activate();
                    ScrollToTop();
                    break;
                case SetupStep.InstallServer:
                    _step = SetupStep.ServerSettings;
                    helperRichTextBox.Rtf = GetResourceByName("AvorionServerManager.Setup.AvorionServerSettings.rtf");
                    _managerMainForm.ChangeTabBarItem(2);
                    Activate();
                    ScrollToTop();
                    break;
                case SetupStep.ManagerSettings:
                    _step = SetupStep.InstallServer;
                    helperRichTextBox.Rtf = GetResourceByName("AvorionServerManager.Setup.ServerInstallation.rtf");
                    _managerMainForm.ChangeTabBarItem(4);
                    Activate();
                    ScrollToTop();
                    break;
                case SetupStep.ServerSettings:
                    _step = SetupStep.BackupSettings;
                    helperRichTextBox.Rtf = GetResourceByName("AvorionServerManager.Setup.BackupSettings.rtf");
                    _managerMainForm.ChangeTabBarItem(3);
                    Activate();
                    ScrollToTop();
                    break;
                case SetupStep.BackupSettings:
                    _step = SetupStep.SetupFinished;
                    doItButton.Visible = false;
                    helperRichTextBox.Rtf = GetResourceByName("AvorionServerManager.Setup.AdditionalSteps.rtf");
                    _managerMainForm.ChangeTabBarItem(0);
                    Activate();
                    ScrollToTop();
                    break;
                case SetupStep.SetupFinished:
                    _step = SetupStep.LuaCommands;
                    helperRichTextBox.Rtf = GetResourceByName("AvorionServerManager.Setup.LuaCommands.rtf");
                    ScrollToTop();
                    break;
                case SetupStep.LuaCommands:
                    _step = SetupStep.WebApiSsl;
                    helperRichTextBox.Rtf = GetResourceByName("AvorionServerManager.Setup.WebApiSsl.rtf");
                    ScrollToTop();
                    break;
                case SetupStep.WebApiSsl:
                    _step = SetupStep.WebApiSslSetup;
                    helperRichTextBox.Rtf = GetResourceByName("AvorionServerManager.Setup.WebApiSetupSsl.rtf");
                    ScrollToTop();
                    break;
                case SetupStep.WebApiSslSetup:
                    _step = SetupStep.WebApiKeys;
                    helperRichTextBox.Rtf = GetResourceByName("AvorionServerManager.Setup.ApiKeys.rtf");
                    ScrollToTop();
                    break;
                case SetupStep.WebApiKeys:
                    _step = SetupStep.WebApiCommands;
                    helperRichTextBox.Rtf = GetResourceByName("AvorionServerManager.Setup.WebApiCommands.rtf");
                    ScrollToTop();
                    break;
                default:
                    break;
            }
        }
        private void doItButton_Click(object sender, EventArgs e)
        {
            switch (_step)
            {
                case SetupStep.Welcome:
                    break;
                case SetupStep.InstallServer:
                    try
                    {
                        AutoInstallServer();
                    }catch(Exception ex)
                    {
                        MessageBox.Show("Error while Autoinstalling Server: " + ex.Message);
                    }
                    break;
                case SetupStep.ManagerSettings:
                    try
                    {
                        AutoConfigurateManager();
                    }catch(Exception ex)
                    {
                        MessageBox.Show("Error while Autoconfiguring Manager");
                    }
                    break;
                case SetupStep.ServerSettings:
                    try
                    {
                        AutoConfigureServer();
                    }catch(Exception ex)
                    {
                        MessageBox.Show("Error while Autoconfiguring Server");
                    }
                    break;
                case SetupStep.BackupSettings:
                    try
                    {
                        AutoConfigureBackup();
                    }
                    catch
                    {
                        MessageBox.Show("Error while Autoconfiguring Backup");
                    }
                    break;
                case SetupStep.SetupFinished:
                    break;
                default:
                    break;
            }
        }
      
        private void SetAllButtonEnabledState(bool enabledState)
        {
            doItButton.Enabled = enabledState;
            nextStepButton.Enabled = enabledState;
            previousStepButton.Enabled = enabledState;
            skipSetipButton.Enabled = enabledState;
        }
        private async void AutoConfigureBackup()
        {
            SetAllButtonEnabledState(false);
            _managerController.BackupSettings.BackupPath = Path.Combine(_managerController.ManagerSettings.AvorionFolder, "Backups");
            _managerController.BackupSettings.CompressBackups = true;
            _managerController.BackupSettings.BackupInterval = 0;
            _managerController.BackupSettings.SaveOnStartup = true;
            _managerController.BackupSettings.SaveOnStop = false;
            _managerController.BackupSettingsLoadedFromFile = true;
            _managerController.SaveBackupSettings();
            _managerMainForm.ApplyBackupSettings();
            MessageBox.Show("Backup Settings Configured");
            SetAllButtonEnabledState(true);
            GoToNexStep();
        }
        private async void AutoConfigurateManager()
        {
            SetAllButtonEnabledState(false);
            if (string.IsNullOrWhiteSpace(_managerController.ManagerSettings.AvorionFolder))
            {
                _managerController.ManagerSettings.AvorionFolder = Application.StartupPath;
            }
            _managerController.ManagerSettings.HttpServerPort = Constants.DefaultApiPort;
            _managerController.SaveManagerSettings();
            _managerController.ManagerSettingsLoadedFromFile = true;
            _managerMainForm.ApplyManagerSettings();
            MessageBox.Show("Manager Settings Configured");
            SetAllButtonEnabledState(true);
            GoToNexStep();
        }
        private async void AutoConfigureServer()
        {
            SetAllButtonEnabledState(false);
            string seed = _managerController.GenerateSeed(10);
            _managerController.ServerSettings.CollisionDamage = 1.0;
            _managerController.ServerSettings.CreativeMode = false;
            _managerController.ServerSettings.DataPath = Path.Combine(_managerController.ManagerSettings.AvorionFolder, "dedicated_Server" + seed);
            _managerController.ServerSettings.Description = "An Avorion Server";
            _managerController.ServerSettings.Difficulty = DifficultySettings.Normal;
            _managerController.ServerSettings.ExitOnLastAdminLogout = false;
            _managerController.ServerSettings.GalaxyName = "Avorion Galaxy";
            _managerController.ServerSettings.ListPublic = false;
            _managerController.ServerSettings.MaxPlayers = 10;
            _managerController.ServerSettings.Port = 27000;
            _managerController.ServerSettings.Seed = seed;
            _managerController.ServerSettings.ServerName = "Avorion Server";
            _managerController.ServerSettings.ShareHomesector = true;
            _managerController.ServerSettings.Threads = Environment.ProcessorCount;
            _managerController.ServerSettings.UserAuthentication = true;
            _managerController.ServerSettings.UseSteamNetworking = true;
           // _managerController.ServerSettings.ExitOnLastAdminLogout = false;
            _managerController.ServerSettingLoadedFromFile = true;
            _managerController.SaveServerSettings();
            _managerMainForm.ApplyServerSettings();
            _managerMainForm.button2_Click(null, null);
            SetAllButtonEnabledState(true);
            MessageBox.Show("Server Settings Configured");
            GoToNexStep();
            
        }
        private async void AutoInstallServer()
        {
            SetAllButtonEnabledState(false);
            if (string.IsNullOrWhiteSpace(_managerController.ManagerSettings.AvorionFolder))
            {
                _managerController.ManagerSettings.SteamCmdFolder = Path.Combine(Application.StartupPath, Constants.SteamCmdFolderName);
                _managerController.ManagerSettings.AvorionFolder = Application.StartupPath;
                _managerController.ManagerSettings.SteamCmdScritpsFolder = Path.Combine(Application.StartupPath, Constants.SteamCmdScriptsFolderName);
            }else
            {
                _managerController.ManagerSettings.SteamCmdFolder = Path.Combine(_managerController.ManagerSettings.AvorionFolder, Constants.SteamCmdFolderName);
                _managerController.ManagerSettings.SteamCmdScritpsFolder = Path.Combine(_managerController.ManagerSettings.AvorionFolder, Constants.SteamCmdScriptsFolderName);
            }
           
            List<string> steamCmdScriptLines = new List<string>();
            steamCmdScriptLines.Add("login anonymous");
            steamCmdScriptLines.Add("force_install_dir \"" + _managerController.ManagerSettings.AvorionFolder + "\"");
            steamCmdScriptLines.Add("app_update 565060");
            steamCmdScriptLines.Add("quit");

            List<string> steamCmdUpdateBetaScript = new List<string>();
            steamCmdUpdateBetaScript.Add("login anonymous");
            steamCmdUpdateBetaScript.Add("force_install_dir \"" + _managerController.ManagerSettings.AvorionFolder + "\"");
            steamCmdUpdateBetaScript.Add("app_update 565060 -beta beta validate");
            steamCmdUpdateBetaScript.Add("quit");
            if (!Directory.Exists(_managerController.ManagerSettings.SteamCmdFolder))
            {
                Directory.CreateDirectory(_managerController.ManagerSettings.SteamCmdFolder);
            }
            if (!Directory.Exists(_managerController.ManagerSettings.SteamCmdScritpsFolder))
            {
                Directory.CreateDirectory(_managerController.ManagerSettings.SteamCmdScritpsFolder);
            }
            File.WriteAllLines(Path.Combine(_managerController.ManagerSettings.SteamCmdScritpsFolder, Constants.SteamCmdInstallScriptFileName), steamCmdScriptLines);
            File.WriteAllLines(Path.Combine(_managerController.ManagerSettings.SteamCmdScritpsFolder, Constants.SteamCmdBetaUpdateScriptFileName), steamCmdUpdateBetaScript);
            if (!File.Exists(Path.Combine(_managerController.ManagerSettings.SteamCmdFolder, "steamcmd.exe")))
            {
                if (!File.Exists(Path.Combine(_managerController.ManagerSettings.SteamCmdFolder, "steamcmd.zip")))
                {
                    using (var client = new WebClient())
                    {
                        client.DownloadFile(Constants.SteamCmdDownloadLink, Path.Combine(_managerController.ManagerSettings.SteamCmdFolder, "steamcmd.zip"));
                    }
                }
                ZipFile.ExtractToDirectory(Path.Combine(_managerController.ManagerSettings.SteamCmdFolder, "steamcmd.zip"), _managerController.ManagerSettings.SteamCmdFolder);
            }
            Process tmpProcess = new Process();
            tmpProcess.StartInfo.FileName = Path.Combine(_managerController.ManagerSettings.SteamCmdFolder, "steamcmd.exe");
            tmpProcess.StartInfo.Arguments = "+runscript " + "\""+(Path.Combine(_managerController.ManagerSettings.SteamCmdScritpsFolder, Constants.SteamCmdInstallScriptFileName)+"\"");
            tmpProcess.Start();
            tmpProcess.WaitForExit();
            MessageBox.Show("Avorion Server Installed");
            SetAllButtonEnabledState(true);
            GoToNexStep();
        }

        private void previousStepButton_Click(object sender, EventArgs e)
        {
            GoToPreviousStep();
        }

        private void skipSetipButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

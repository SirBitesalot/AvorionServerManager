using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using AvorionServerManager.Core;
using AvorionServerManager.Server;
using AvorionServerManager.Commands;
using Newtonsoft.Json;

namespace AvorionServerManager
{
    public partial class ManagerMainForm : Form
    {
        ManagerController _managerController;
        private int _seedLength = 10;
        bool showHelper;
        List<string> _commandLineHistory;
        int _commandLineHistoryIndex = -1;
        public ManagerMainForm()
        {
            InitializeComponent();
            _commandLineHistory = new List<string>();
            if (!Directory.Exists(Constants.SettingsFolderName))
            {
                Directory.CreateDirectory(Constants.SettingsFolderName);
            }
            ApiKeyController.Init(Path.Combine(Constants.SettingsFolderName, Constants.ApiKeyFileName));
            //TODO logic
            //List<AvorionServerCommandDefinition> tmpDefinitions = new List<AvorionServerCommandDefinition>();
            //AvorionServerCommandDefinition tmpSaveDefinition = new AvorionServerCommandDefinition();
            //tmpSaveDefinition.ExecutionType = CommandExecutionTypes.Lua;
            //tmpSaveDefinition.DisplayName = "Save";
            //tmpSaveDefinition.HasParameters = false;
            //tmpSaveDefinition.InternalId = 1;
            //tmpSaveDefinition.InternalName = "Server():save";
            //AvorionServerCommandDefinition tmpStopDefintion = new AvorionServerCommandDefinition();
            //tmpStopDefintion.ExecutionType = CommandExecutionTypes.Lua;
            //tmpStopDefintion.DisplayName = "Stop";
            //tmpStopDefintion.HasParameters = false;
            //tmpStopDefintion.InternalId = 2;
            //tmpStopDefintion.InternalName = "Server():stop";
            //AvorionServerCommandDefinition tmpBroadcastDefinition = new AvorionServerCommandDefinition();
            //tmpBroadcastDefinition.ExecutionType = CommandExecutionTypes.Lua;
            //tmpBroadcastDefinition.DisplayName = "Broadcast Chat Message";
            //tmpBroadcastDefinition.HasParameters = true;
            //tmpBroadcastDefinition.InternalId = 3;
            //tmpBroadcastDefinition.InternalName = "Server():broadcastChatMessage";
            //AvorionServerCommandParameterDefinition tmpsenderParameterDefinition = new AvorionServerCommandParameterDefinition();
            //tmpsenderParameterDefinition.DisplayName = "Sender";
            //AvorionServerCommandParameterDefinition tmpMessageTypeParameterDefinition = new AvorionServerCommandParameterDefinition();
            //tmpMessageTypeParameterDefinition.DisplayName = "Message Type";
            //AvorionServerCommandParameterDefinition tmpMessageParameterDefinition = new AvorionServerCommandParameterDefinition();
            //tmpMessageParameterDefinition.DisplayName = "Message";
            //tmpBroadcastDefinition.AddParameterDefinition(tmpsenderParameterDefinition);
            //tmpBroadcastDefinition.AddParameterDefinition(tmpMessageTypeParameterDefinition);
            //tmpBroadcastDefinition.AddParameterDefinition(tmpMessageParameterDefinition);
            //tmpDefinitions.Add(tmpSaveDefinition);
            //tmpDefinitions.Add(tmpStopDefintion);
            //tmpDefinitions.Add(tmpBroadcastDefinition);
            //#region AddAdminCommand
            //AvorionServerCommandDefinition tmpAddAdmin = new AvorionServerCommandDefinition();
            //tmpAddAdmin.ExecutionType = CommandExecutionTypes.Console;
            //tmpAddAdmin.DisplayName = "Add Admin";
            //tmpAddAdmin.HasParameters = true;
            //tmpAddAdmin.InternalName = "/admin --add";
            //List<AvorionServerCommandParameterDefinition> tmpAddAdminParameters = new List<AvorionServerCommandParameterDefinition>();
            //AvorionServerCommandParameterDefinition tmpNameParameter = new AvorionServerCommandParameterDefinition();
            //tmpNameParameter.DisplayName = "Player Name";
            //tmpNameParameter.Prefix = "--name";
            //tmpAddAdminParameters.Add(tmpNameParameter);
            //AvorionServerCommandParameterDefinition tmpIdParameter = new AvorionServerCommandParameterDefinition();
            //tmpIdParameter.DisplayName = "Player Steam64 Id";
            //tmpIdParameter.Prefix = "--id";
            //tmpAddAdminParameters.Add(tmpIdParameter);
            //tmpAddAdmin.ParameterDefinitions = tmpAddAdminParameters;
            //tmpDefinitions.Add(tmpAddAdmin);
            //#endregion
            //#region RemoveAdminCommand
            //AvorionServerCommandDefinition tmpRemoveAdmin = new AvorionServerCommandDefinition();
            //tmpRemoveAdmin.ExecutionType = CommandExecutionTypes.Console;
            //tmpRemoveAdmin.DisplayName = "Remove Admin";
            //tmpRemoveAdmin.HasParameters = true;
            //tmpRemoveAdmin.InternalName = "/admin --remove";
            //tmpRemoveAdmin.ParameterDefinitions = tmpAddAdminParameters;
            //tmpDefinitions.Add(tmpRemoveAdmin);
            //#endregion
            //#region Blacklist
            ////Add
            //AvorionServerCommandDefinition tmpAddBlacklist = new AvorionServerCommandDefinition();
            //tmpAddBlacklist.ExecutionType = CommandExecutionTypes.Console;
            //tmpAddBlacklist.DisplayName = "Add to Blacklist";
            //tmpAddBlacklist.HasParameters = true;
            //tmpAddBlacklist.InternalName = "/blacklist --add";
            //tmpAddBlacklist.ParameterDefinitions = tmpAddAdminParameters;
            //tmpDefinitions.Add(tmpAddBlacklist);
            ////Remove
            //AvorionServerCommandDefinition tmpRemoveBlacklist = new AvorionServerCommandDefinition();
            //tmpRemoveBlacklist.ExecutionType = CommandExecutionTypes.Console;
            //tmpRemoveBlacklist.DisplayName = "Remove From Blacklist";
            //tmpRemoveBlacklist.HasParameters = true;
            //tmpRemoveBlacklist.InternalName = "/blacklist --remove";
            //tmpRemoveBlacklist.ParameterDefinitions = tmpAddAdminParameters;
            //tmpDefinitions.Add(tmpRemoveBlacklist);
            //#endregion
            //#region accessMode
            //AvorionServerCommandDefinition tmpActivateWhitelist = new AvorionServerCommandDefinition();
            //tmpActivateWhitelist.ExecutionType = CommandExecutionTypes.Console;
            //tmpActivateWhitelist.DisplayName = "Activate Whitelist";
            //tmpActivateWhitelist.HasParameters = false;
            //tmpActivateWhitelist.InternalName = "/whitelist --activate";
            //tmpDefinitions.Add(tmpActivateWhitelist);
            //AvorionServerCommandDefinition tmpActivateBlacklist = new AvorionServerCommandDefinition();
            //tmpActivateBlacklist.ExecutionType = CommandExecutionTypes.Console;
            //tmpActivateBlacklist.DisplayName = "Activate Blacklist";
            //tmpActivateBlacklist.HasParameters = false;
            //tmpActivateBlacklist.InternalName = "/blacklist --activate";
            //tmpDefinitions.Add(tmpActivateBlacklist);
            //#endregion
            //#region Kick
            //AvorionServerCommandDefinition tmpKickCommand = new AvorionServerCommandDefinition();
            //tmpKickCommand.ExecutionType = CommandExecutionTypes.Console;
            //tmpKickCommand.DisplayName = "Kick Player";
            //tmpKickCommand.HasParameters = true;
            //tmpKickCommand.InternalName = "/kick";
            //AvorionServerCommandParameterDefinition tmpKickNameParameter = new AvorionServerCommandParameterDefinition();
            //List<AvorionServerCommandParameterDefinition> tmpKickParameters = new List<AvorionServerCommandParameterDefinition>();
            //tmpKickNameParameter.DisplayName = "Player Name";
            //AvorionServerCommandParameterDefinition tmpKickReasonParameter = new AvorionServerCommandParameterDefinition();
            //tmpKickReasonParameter.DisplayName = "Reason";
            //tmpKickParameters.Add(tmpKickReasonParameter);
            //tmpKickParameters.Add(tmpKickNameParameter);
            //tmpKickCommand.ParameterDefinitions = tmpKickParameters;
            //tmpDefinitions.Add(tmpKickCommand);
            //#endregion
            //File.WriteAllText(Path.Combine(Constants.SettingsFolderName, Constants.CommandDefinitonsFileName), JsonConvert.SerializeObject(tmpDefinitions, Formatting.Indented));
            //AvorionServerCommand tmpCommand = new AvorionServerCommand(tmpBroadcastDefinition,new List<AvorionServerCommandParameter> { new AvorionServerCommandParameter { Content="WebRequestTestSender"}, new AvorionServerCommandParameter { Content = "0" }, new AvorionServerCommandParameter { Content = "Hello From Webrequest" } });
            //File.WriteAllText(Path.Combine(Constants.SettingsFolderName, "Broadcast.json"), JsonConvert.SerializeObject(tmpCommand, Formatting.Indented));

            //AvorionServerCommand tmpStopCommand = new AvorionServerCommand(tmpStopDefintion);
            //File.WriteAllText(Path.Combine(Constants.SettingsFolderName, "Save.json"), JsonConvert.SerializeObject(tmpStopCommand, Formatting.Indented));

            //AvorionServerCommand tmpStartCommand = new AvorionServerCommand(tmpStartCommand);
            //File.WriteAllText(Path.Combine(Constants.SettingsFolderName, "Start.json"), JsonConvert.SerializeObject(tmpStartCommand, Formatting.Indented));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            showHelper = true;
            _managerController = new ManagerController();
            _managerController.LogEvent += _managerController_LogEvent; ;
            _managerController.ServerStoppedEvent += _managerController_ServerStoppedEvent;
            ApplyManagerSettings();
            commandCombobox.Items.AddRange(_managerController.AvailableCommandDefinitions.ToArray());
            AddDifficultyDropDownItems();
            ApplyServerSettings();
           
            if (serverSeedBox.Text == string.Empty)
            {
                serverSeedBox.Text = _managerController.GenerateSeed(_seedLength);
            }
            ApplyBackupSettings();
           
        }

        private void _managerController_LogEvent(object sender, Core.LogEventEventArgs e)
        {
            if (InvokeRequired)
            {
                logBox.BeginInvoke((MethodInvoker)delegate () { logBox.AppendText(e.Message + Environment.NewLine); ; });
            }
            else
            {
                logBox.AppendText(e.Message + Environment.NewLine);
            }
        }

        private void AddDifficultyDropDownItems()
        {
            difficultyDropDown.Items.Add(DifficultySettings.Beginner);
            difficultyDropDown.Items.Add(DifficultySettings.Easy);
            difficultyDropDown.Items.Add(DifficultySettings.Normal);
            difficultyDropDown.Items.Add(DifficultySettings.Veteran);
            difficultyDropDown.Items.Add(DifficultySettings.Difficult);
            difficultyDropDown.Items.Add(DifficultySettings.Hard);
            difficultyDropDown.Items.Add(DifficultySettings.Insane);
        }
        public void ApplyServerSettings()
        {
            if (Directory.Exists(_managerController.ServerSettings.DataPath))
            {
                serverSeedBox.Enabled = false;
                randomSeedButton.Enabled = false;
            }
            if (_managerController.ServerSettingLoadedFromFile)
            {
                serverDataPathBox.Text = _managerController.ServerSettings.DataPath;
                serverNameBox.Text = _managerController.ServerSettings.ServerName;
                galaxyNameBox.Text = _managerController.ServerSettings.GalaxyName;
                serverDescriptionBox.Text = _managerController.ServerSettings.Description;
                additionalArgumentsBox.Text = _managerController.ServerSettings.AdditionalArguments;
                serverSeedBox.Text = _managerController.ServerSettings.Seed;
                maxPlayersNumericUpDown.Value = _managerController.ServerSettings.MaxPlayers;
                serverThreadsNumericUpDown.Value = _managerController.ServerSettings.Threads;
                serverPortNumericUpDown.Value = _managerController.ServerSettings.Port;
                difficultyDropDown.SelectedItem = _managerController.ServerSettings.Difficulty;
                collisionDamageNumericUpDown.Value = (decimal)_managerController.ServerSettings.CollisionDamage;
                creativeModeCheckBox.Checked = _managerController.ServerSettings.CreativeMode;
                sharedHomeSectorCheckBox.Checked = _managerController.ServerSettings.ShareHomesector;
                authenticateUsersCheckBox.Checked = _managerController.ServerSettings.UserAuthentication;
                listPublicCheckBox.Checked = _managerController.ServerSettings.ListPublic;
                useSteamNetworkingCheckbox.Checked = _managerController.ServerSettings.UseSteamNetworking;
            }
        }
        public void ApplyBackupSettings()
        {
            if (_managerController.BackupSettingsLoadedFromFile)
            {
                backupPathBox.Text = _managerController.BackupSettings.BackupPath;
                backupIntervalUpDown.Value = _managerController.BackupSettings.BackupInterval;
                compressBackupsCheckbox.Checked = _managerController.BackupSettings.CompressBackups;
                startupBackupCheckBox.Checked = _managerController.BackupSettings.SaveOnStartup;
                stopBackupCeckbox.Checked = _managerController.BackupSettings.SaveOnStop;
            }
        }

        private void _managerController_ServerStoppedEvent(object sender, ServerStoppedEventArgs e)
        {
            if (InvokeRequired)
            {
                startAvorionServerButton.BeginInvoke((MethodInvoker)delegate () { startAvorionServerButton.Enabled = true; ; });
            }
            else
            {
                startAvorionServerButton.Enabled = true;
            }
        }
    

        public void ApplyManagerSettings()
        {
            if (_managerController.ManagerSettingsLoadedFromFile)
            {
                avorionFolderTextBox.Text = _managerController.GetAvorionFolder();
                httpPortNumericUpDown.Value = _managerController.GetHttpServerPort();
                _managerController.ManagerSettings.SteamCmdScritpsFolder = Path.Combine(_managerController.ManagerSettings.AvorionFolder, Constants.SteamCmdScriptsFolderName);
            }
        }
        private void avorionFolderChangeButton_Click(object sender, EventArgs e)
        {
            DialogResult result = avorionFolderBrowser.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(avorionFolderBrowser.SelectedPath))
            {
                avorionFolderTextBox.Text = avorionFolderBrowser.SelectedPath;
            }
        }

        private void saveSettingsButton_Click(object sender, EventArgs e)
        {
            _managerController.SetAvorionFolder(avorionFolderTextBox.Text);
            _managerController.SetHttpServerPort((int)httpPortNumericUpDown.Value);
            _managerController.SaveManagerSettings();
            MessageBox.Show("Settings Saved");
        }

        private void startHttpServerButton_Click(object sender, EventArgs e)
        {
            startHttpServerButton.Enabled = false;
            ServerController._managerController = _managerController;
            _managerController.StartWebApi();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (commandCombobox.SelectedIndex > -1)
            {
                AvorionServerCommandDefinition tmpCommandDefinition =(AvorionServerCommandDefinition) commandCombobox.Items[commandCombobox.SelectedIndex];
                if (tmpCommandDefinition.HasParameters)
                {
                    List<AvorionServerCommandParameter> tmpParameters = new List<AvorionServerCommandParameter>();
                    if (CommandInputPrompt.ShowInputDialog(tmpCommandDefinition.DisplayName, tmpCommandDefinition.ParameterDefinitions, ref tmpParameters) == DialogResult.OK)
                    {
                        _managerController.Server.SendCommand(new AvorionServerCommand(tmpCommandDefinition, tmpParameters));
                    }

                }else
                {
                    _managerController.AddCommand(new AvorionServerCommand(tmpCommandDefinition));
                }
            }
        }
        private void startAvorionServerButton_Click(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                startAvorionServerButton.BeginInvoke((MethodInvoker)delegate () { startAvorionServerButton.Enabled = false; ; });
            }
            else
            {
                startAvorionServerButton.Enabled = false;
            }
            _managerController.StartAvorionServer();
        }
        private void patchServerLuaButton_Click(object sender, EventArgs e)
        {
            _managerController.PatchServerLua();
            MessageBox.Show("server.lua patched");
        }

        private void cleanServerLuaButton_Click(object sender, EventArgs e)
        {
            _managerController.CleanServerLua();
            MessageBox.Show("server.lua cleaned");
        }

        public void button2_Click(object sender, EventArgs e)
        {
            _managerController.ServerSettings.DataPath = serverDataPathBox.Text;
            _managerController.ServerSettings.ServerName = serverNameBox.Text;
            _managerController.ServerSettings.GalaxyName = galaxyNameBox.Text;
            _managerController.ServerSettings.Description = serverDescriptionBox.Text;
            _managerController.ServerSettings.AdditionalArguments = additionalArgumentsBox.Text;
            _managerController.ServerSettings.Seed = serverSeedBox.Text;
            _managerController.ServerSettings.MaxPlayers =(int) maxPlayersNumericUpDown.Value;
            _managerController.ServerSettings.Threads = (int)serverThreadsNumericUpDown.Value;
            _managerController.ServerSettings.Port = (int)serverPortNumericUpDown.Value;
            _managerController.ServerSettings.Difficulty =(DifficultySettings) difficultyDropDown.SelectedItem;
            _managerController.ServerSettings.CollisionDamage =(double) collisionDamageNumericUpDown.Value;
            _managerController.ServerSettings.CreativeMode = creativeModeCheckBox.Checked;
            _managerController.ServerSettings.ShareHomesector = sharedHomeSectorCheckBox.Checked;
            _managerController.ServerSettings.UserAuthentication = authenticateUsersCheckBox.Checked;
            _managerController.ServerSettings.ListPublic = listPublicCheckBox.Checked;
            _managerController.ServerSettings.UseSteamNetworking = useSteamNetworkingCheckbox.Checked;
            _managerController.SaveServerSettings();
            MessageBox.Show("Settings Saved");
        }

        private void randomSeedButton_Click(object sender, EventArgs e)
        {
            serverSeedBox.Text = _managerController.GenerateSeed(_seedLength);
        }

        private void selectDataPathButton_Click(object sender, EventArgs e)
        {
            DialogResult result = dataPathFolderBrowser.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(dataPathFolderBrowser.SelectedPath))
            {
                serverDataPathBox.Text = dataPathFolderBrowser.SelectedPath;
                if (File.Exists(Path.Combine(serverDataPathBox.Text, "server.ini"))){
                    _managerController.ServerSettings.LoadSettingsFromIni(Path.Combine(serverDataPathBox.Text, "server.ini"));
                    _managerController.ServerSettings.DataPath = serverDataPathBox.Text;
                    _managerController.ServerSettingLoadedFromFile = true;
                    ApplyServerSettings();
                    
                }
            }
        }

        private void saveBackupButton_Click(object sender, EventArgs e)
        {
            _managerController.BackupSettings.BackupPath = backupPathBox.Text;
            _managerController.BackupSettings.BackupInterval = (int)backupIntervalUpDown.Value;
            _managerController.BackupSettings.CompressBackups = compressBackupsCheckbox.Checked;
            _managerController.BackupSettings.SaveOnStartup = startupBackupCheckBox.Checked;
            _managerController.BackupSettings.SaveOnStop = stopBackupCeckbox.Checked;
            _managerController.SaveBackupSettings();
            MessageBox.Show("Settings Saved");
        }

        private void selectBackupPathButton_Click(object sender, EventArgs e)
        {
            DialogResult result = backupPathFolderBrowser.ShowDialog();
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(backupPathFolderBrowser.SelectedPath))
            {
                backupPathBox.Text = backupPathFolderBrowser.SelectedPath;
            }
        }

        private void ManagerMainForm_Shown(object sender, EventArgs e)
        {
            if (showHelper&&!File.Exists(Path.Combine(Constants.SettingsFolderName,Constants.ManagerSettingsFileName)))
            {
                showHelper = false;
                Setup.SetupHelperForm helpForm = new Setup.SetupHelperForm(_managerController,this);
                helpForm.Show();
                helpForm.Activate();
                helpForm.FormClosed += HelpForm_FormClosed;
            }
        }



        private void updateServerButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_managerController.ManagerSettings.SteamCmdFolder))
            {
                DialogResult result = selectSteamCmdDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    _managerController.ManagerSettings.SteamCmdFolder = Path.GetDirectoryName(selectSteamCmdDialog.FileName);
                }
            }
            try
            {
                Process tmpProcess = new Process();
                tmpProcess.StartInfo.FileName = Path.Combine(_managerController.ManagerSettings.SteamCmdFolder, "steamcmd.exe");
                tmpProcess.StartInfo.Arguments = "+runscript " + "\"" + (Path.Combine(_managerController.ManagerSettings.SteamCmdScritpsFolder, Constants.SteamCmdInstallScriptFileName) + "\"");
                tmpProcess.Start();
                tmpProcess.WaitForExit();
                MessageBox.Show("Settings Updated");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error updating Server:" + Environment.NewLine + ex.Message);
            }
        }
        public void ChangeTabBarItem(int index)
        {
            tabControl1.SelectedIndex = index;
        }
        private void updateServerbetaButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_managerController.ManagerSettings.SteamCmdFolder))
            {
                DialogResult result = selectSteamCmdDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    _managerController.ManagerSettings.SteamCmdFolder = Path.GetDirectoryName(selectSteamCmdDialog.FileName);
                }
            }
            try
            {
                Process tmpProcess = new Process();
                tmpProcess.StartInfo.FileName = Path.Combine(_managerController.ManagerSettings.SteamCmdFolder, "steamcmd.exe");
                tmpProcess.StartInfo.Arguments = "+runscript " + "\"" + (Path.Combine(_managerController.ManagerSettings.SteamCmdScritpsFolder, Constants.SteamCmdBetaUpdateScriptFileName) + "\"");
                tmpProcess.Start();
                tmpProcess.WaitForExit();
                MessageBox.Show("Server Updated to Beta Branch");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Server:" + Environment.NewLine + ex.Message);
            }
        }

        private void installServerButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_managerController.ManagerSettings.SteamCmdFolder))
            {
                DialogResult result = selectSteamCmdDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    _managerController.ManagerSettings.SteamCmdFolder = Path.GetDirectoryName( selectSteamCmdDialog.FileName);
                }
            }
                try
            {
                Process tmpProcess = new Process();
                tmpProcess.StartInfo.FileName = Path.Combine(_managerController.ManagerSettings.SteamCmdFolder, "steamcmd.exe");
                tmpProcess.StartInfo.Arguments = "+runscript " + "\"" + (Path.Combine(_managerController.ManagerSettings.SteamCmdScritpsFolder, Constants.SteamCmdInstallScriptFileName) + "\"");
                tmpProcess.Start();
                tmpProcess.WaitForExit();
                MessageBox.Show("Server installed");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Server:" + Environment.NewLine + ex.Message);
            }
        }

        private void reloadApiKeysButton_Click(object sender, EventArgs e)
        {
            ApiKeyController.LoadApiKeys();
            MessageBox.Show("API Keys Reloaded");
        }

        private void createBackupButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_managerController.ServerSettings.DataPath))
            {
                _managerController.CreateBackup();
            }else
            {
                MessageBox.Show("No Server Datapath set.");
            }
        }

        private void showHelpButton_Click(object sender, EventArgs e)
        {
            if (showHelper)
            {
                showHelper = false;
                Setup.SetupHelperForm helpForm = new Setup.SetupHelperForm(_managerController, this);
                helpForm.Show();
                helpForm.Activate();
                helpForm.FormClosed += HelpForm_FormClosed;
            }
        }

        private void HelpForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            showHelper = true;
        }

        private void commandLineBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (_managerController.Server!=null&&_managerController.Server.IsRunning)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    //enter key is down
                    _managerController.Server.SendConsoleCommand(commandLineBox.Text);
                    _commandLineHistory.Add(commandLineBox.Text);
                    _commandLineHistoryIndex = _commandLineHistory.Count;
                    Activate();
                    commandLineBox.Focus();
                    commandLineBox.Text = string.Empty;
                    e.Handled = true;
                    e.SuppressKeyPress = true;

                }
                if (e.KeyCode == Keys.Up)
                {
                    if (_commandLineHistory.Count > 0)
                    {
                        if (_commandLineHistoryIndex == -1)
                        {
                            _commandLineHistoryIndex = _commandLineHistory.Count - 1;
                        }
                        else
                        {
                            _commandLineHistoryIndex--;
                        }
                        if (_commandLineHistoryIndex < 0 || _commandLineHistoryIndex >= _commandLineHistory.Count)
                        {
                            _commandLineHistoryIndex = _commandLineHistory.Count - 1;
                        }
                        commandLineBox.Text = _commandLineHistory[_commandLineHistoryIndex];
                    }
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    commandLineBox.SelectionStart = Math.Max(0, commandLineBox.Text.Length); 
                    commandLineBox.SelectionLength = 0;
                }
                if (e.KeyCode == Keys.Down)
                {
                    if (_commandLineHistory.Count > 0)
                    {
                        if (_commandLineHistoryIndex == -1)
                        {
                            _commandLineHistoryIndex = 0;
                        }
                        else
                        {
                            _commandLineHistoryIndex++;
                        }
                        if (_commandLineHistoryIndex < 0 || _commandLineHistoryIndex >= _commandLineHistory.Count)
                        {
                            _commandLineHistoryIndex = 0;
                        }
                        commandLineBox.Text = _commandLineHistory[_commandLineHistoryIndex];
                    }
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    commandLineBox.SelectionStart = Math.Max(0, commandLineBox.Text.Length); // add some logic if length is 0
                    commandLineBox.SelectionLength = 0;
                }
                

            }
        }
    }
}

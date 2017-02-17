using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvorionServerManager.Core;
namespace AvorionServerManager.Server
{
    public class AvorionServer : ILoggable
    {
        #region Properties
        public bool IsRunning { get; private set; }
        public AvorionServerSettings Settings { get; private set; }
        public Process Process { get; private set; }//TODO make private and do process logic here
        #endregion
        #region Public Events
        public delegate void ServerStoppedEvent(object sender, ServerStoppedEventArgs e);
        //TODO use this
        public event ServerStoppedEvent Stopped;
        public event LogEvent LogEvent;
        #endregion
        #region Private Variables
        private string _workingDirectory;
        private string _serverExecutablePath;
        
        #endregion
        #region Constructor
        /// <summary>
        /// Creates a new AvorionServer Object
        /// </summary>
        /// <param name="settings">Setting this server will use</param>
        /// <param name="workingDirectory">Working directory for Server Process</param>
        /// <param name="serverExecutablePath">Path to Server executable</param>
        public AvorionServer(AvorionServerSettings settings, string workingDirectory, string serverExecutablePath)
        {

            Settings = settings;
            _workingDirectory = workingDirectory;
            _serverExecutablePath = serverExecutablePath;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Starts the server if it is not already running
        /// </summary>
        public void Start()
        {
            if (!IsRunning && CheckConfigs())
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.UseShellExecute = false; //required to redirect standart input/output
                startInfo.RedirectStandardInput = true;
                startInfo.RedirectStandardOutput = true;
                startInfo.RedirectStandardError = true;
                startInfo.FileName = Path.Combine(_serverExecutablePath);
                startInfo.WorkingDirectory = _workingDirectory;
                startInfo.Arguments = Settings.GetCommandLine();
                startInfo.StandardOutputEncoding = Encoding.UTF8;
                Process = new Process();
                Process.StartInfo = startInfo;
                IsRunning = true;
                Process.Start();
            }
        }

        /// <summary>
        /// stops the server
        /// </summary>
        public void Stop()
        {
            throw new NotImplementedException();
        }
        public void SendCommand()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region Private variables
        /// <summary>
        /// Checks if the Server Config is valid
        /// </summary>
        /// <returns></returns>
        private bool CheckConfigs()
        {
            //Empty for now until logic is moved to Server Class
            return true;
        }
        #endregion
    }
}

using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using AvorionServerManager.Core;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using AvorionServerManager.Commands;
namespace AvorionServerManager.Server
{
    public class AvorionServer : ILoggable
    {
        #region DLL Imports
        [DllImport("user32.dll")]
        private static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        private static extern short VkKeyScan(char ch);
        #endregion
        #region Properties
        public bool IsRunning { get; set; }
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
        public void SendCommand(AvorionServerCommand command)
        {
            Process.StandardInput.WriteLine(command.CommandId);
            if (command.HasParameters)
            {
                foreach (string currentParameter in command.Parameters)
                {
                    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(currentParameter);
                    Process.StandardInput.BaseStream.Write(buffer, 0, buffer.Length);
                    Process.StandardInput.WriteLine();
                }
            }
        }
        public void SendConsoleCommand(string command)
        {
            if (IsRunning)
            {
                uint WM_KEYDOWN = 0x100;
                uint WM_KEYUP = 0x0101;
                IntPtr edit = Process.MainWindowHandle;


                foreach (char currentChar in command)
                {
                    switch (currentChar)
                    {
                        case '/':
                            PostMessage(edit, WM_KEYDOWN, (IntPtr)(0x6F), IntPtr.Zero);
                            break;
                        default:
                            PostMessage(edit, WM_KEYDOWN, (IntPtr)(VkKeyScan(currentChar)), IntPtr.Zero);
                            break;
                    }
                    Thread.Sleep(10);

                }

                PostMessage(edit, WM_KEYDOWN, (IntPtr)(Keys.Enter), IntPtr.Zero);
                PostMessage(edit, WM_KEYUP, (IntPtr)(Keys.Enter), IntPtr.Zero);
            }
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

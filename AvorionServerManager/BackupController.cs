using AvorionServerManager.Core;
using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Windows.Forms;

namespace AvorionServerManager
{
    class BackupController
    {
        private static object _lockObject = new object();
        public void CreateBackup(string dataPath, BackupSettings backupSettings)
        {
            Thread tmpCopyThread = new Thread(() =>
            {
                try
                {
                    string dateTimeString = DateTime.Now.ToString("dd-MM-yyyy_hh-mm-ss-f");
                    string tmpBackupPath = Path.Combine(backupSettings.BackupPath, Path.GetFileName(dataPath.TrimEnd(Path.DirectorySeparatorCh‌​ar)) + dateTimeString);

                    if (Directory.Exists(dataPath))
                    {
                        lock (_lockObject)
                        {
                            CopyHelper.CopyDirectory(dataPath, tmpBackupPath);
                        }
                        if (backupSettings.CompressBackups)
                        {
                            Thread tmpThread = new Thread(() =>
                            {
                                lock (_lockObject)
                                {
                                    ZipFile.CreateFromDirectory(tmpBackupPath, tmpBackupPath + ".zip", CompressionLevel.Fastest, false);
                                    Directory.Delete(tmpBackupPath, true);
                                }
                            });
                            tmpThread.IsBackground = true;
                            tmpThread.Start();
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while running Backup task: " + ex.Message);
                }
            });
            tmpCopyThread.IsBackground = true;
            tmpCopyThread.Start();
        }
    }
}

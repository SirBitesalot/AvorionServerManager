using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AvorionServerManager
{
    public static class ApiKeyController
    {
        private static ConcurrentBag<string> _apiKeys;
        private static string _apiKeyFilePath;
        private static object _lockObject = new object();
        public static void Init(string apiKeysFilePath)
        {
            lock (_lockObject)
            {
                if (!File.Exists(apiKeysFilePath))
                {
                   var tmpFile = File.Create(apiKeysFilePath);
                    tmpFile.Close();
                }
            }
            _apiKeyFilePath = apiKeysFilePath;
            LoadApiKeys();
        }
        public static bool IsValidKey(string key)
        {
            if (_apiKeys.Contains(key))
            {
                return true;
            }
            return false;
        }
        public static void LoadApiKeys()
        {
            _apiKeys = new ConcurrentBag<string>();
            List<string> tmpKeys;
            lock (_lockObject)
            {
                tmpKeys = File.ReadAllLines(_apiKeyFilePath).ToList();
            }
            foreach (string currentKey in tmpKeys)
            {
                if (!string.IsNullOrWhiteSpace(currentKey))
                {
                    _apiKeys.Add(currentKey);
                }
            }
        }
    }
}

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Win32;

namespace SUW
{
    public class TortoiseHandler
    {
        private readonly string _tortoise;
        private RegistryKey _registryKey;

        public TortoiseHandler()
        {
            _tortoise = "TortoiseSVN";
            OpenRegistryKey();
        }

        private void OpenRegistryKey()
        {
            _registryKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
        }

        private bool HasTortoise(string subKey)
        {
            var key = _registryKey.OpenSubKey(subKey);

            if (key == null)
                return false;

            var subKeyDisplayName = key.GetValue("DisplayName");
            return subKeyDisplayName != null && subKeyDisplayName.ToString().Contains(_tortoise);
        }

        private string CreateCommandLine(string command, string path)
        {
            return string.Format("/command:{0} /path:{1}", command, path);
        }

        public bool SearchInRegistry()
        {
            return _registryKey.GetSubKeyNames().Where(HasTortoise).Count() > 0;
        }

        public void Update(IEnumerable<DirectoryInfo> svnDirectories)
        {
            var directoriesToUpdate = string.Join("*", svnDirectories.Select(d => d.FullName).ToArray());

            ExecuteCmd(CreateCommandLine("update", directoriesToUpdate));
        }

        private void ExecuteCmd(string arguments)
        {
            var startInfo = new ProcessStartInfo
                                {
                                    FileName = "TortoiseProc.exe",
                                    UseShellExecute = false,
                                    Arguments = arguments
                                };

            Process.Start(startInfo);
        }
    }
}
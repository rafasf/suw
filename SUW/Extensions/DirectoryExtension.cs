using System.Collections.Generic;
using System.IO;
using System.Linq;
using IWshRuntimeLibrary;

namespace SUW.Extensions
{
    public static class DirectoryExtension
    {
        public static IEnumerable<DirectoryInfo> FindFirstFor(this IEnumerable<DirectoryInfo> directories, string folder)
        {
            foreach (var directory in directories)
            {
                if (DirExists(folder, directory))
                    yield return directory;
                else
                    foreach (var innerDir in directory.GetDirectories().FindFirstFor(folder))
                        yield return innerDir;
            }
        }

        public static void CreateShortcut(this IEnumerable<DirectoryInfo> directories, string rootFolder)
        {
            var wshShellClass = new WshShellClass();
            foreach (var directoryInfo in directories)
            {
                var shortcut = wshShellClass.CreateShortcut(Path.Combine(rootFolder, string.Format("{0}.lnk", directoryInfo.Name))) as IWshShortcut;
                shortcut.TargetPath = directoryInfo.FullName;
                shortcut.Save();
            }
        }

        private static bool DirExists(string folder, DirectoryInfo directory)
        {
            return directory.GetDirectories(folder, SearchOption.TopDirectoryOnly).Count() == 1;
        } 
    }
}
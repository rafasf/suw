using System;
using System.Collections.Generic;
using System.IO;
using SUW.Extensions;

namespace SUW.App
{
    class Program
    {
        private const string FolderToSearch = ".svn";

        static void Main(string[] args)
        {
            if (args.Length <= 1)
            {
                ShowHelp();
                return;
            }

            Execute(args);
        }

        private static void Execute(string[] args)
        {
            try
            {
                var parser = new InputParser(args);

                if (parser.Parameters[ParameterName.Operation].Equals("-c"))
                    CreateShortcuts(parser.Parameters);
                else if (parser.Parameters[ParameterName.Operation].Equals("-u"))
                    UpdateSvnDirectories(parser.Parameters);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void CreateShortcuts(Dictionary<ParameterName, string> parameters)
        {
            if (DoesntHaveRootOrShorcutsDirectories(parameters))
                throw new ArgumentException("I need a root directory and a place to put the shortcuts.");

            CheckDirectoryExistence(parameters[ParameterName.RootDir], "root");
            CheckDirectoryExistence(parameters[ParameterName.ShortcutDir], "shortcut");

            new DirectoryInfo(parameters[ParameterName.RootDir])
                .GetDirectories()
                .FindFirstLevelContaining(FolderToSearch)
                .CreateShortcut(parameters[ParameterName.ShortcutDir]);
        }

        private static void UpdateSvnDirectories(Dictionary<ParameterName, string> parameters)
        {
            CheckDirectoryExistence(parameters[ParameterName.RootDir], "root");

            var tortoiseHandler = new TortoiseHandler();
            if (!tortoiseHandler.IsInRegistry())
                throw new InvalidOperationException("TortoiseSVN wasn't found.");

            var svnDirectories = new DirectoryInfo(parameters[ParameterName.RootDir])
                .GetDirectories()
                .FindFirstLevelContaining(FolderToSearch);

            tortoiseHandler.Update(svnDirectories);
        }

        private static void CheckDirectoryExistence(string directory, string name)
        {
            if (!Directory.Exists(directory))
                throw new ArgumentException(string.Format("The {0} directory doesn't exist.", name));
        }

        private static bool DoesntHaveRootOrShorcutsDirectories(Dictionary<ParameterName, string> parameters)
        {
            return string.IsNullOrEmpty(parameters[ParameterName.RootDir]) || string.IsNullOrEmpty(parameters[ParameterName.ShortcutDir]);
        }

        private static void ShowHelp()
        {
            Console.WriteLine("Hey! Probably you did something unexpected so here go some guidelines.");
            Console.WriteLine("If you want to create shortcuts for the folder that have .svn (starting from a root dir)");
            Console.WriteLine("\tsuw -c <your-root-dir> <dir-to-create-the-shortcuts>");
            Console.WriteLine("");
            Console.WriteLine("If you want to update");
            Console.WriteLine("\tsuw -u <your-root-dir>");
            Console.WriteLine("");
            Console.WriteLine("That's everything I can offer.");
        }
    }
}

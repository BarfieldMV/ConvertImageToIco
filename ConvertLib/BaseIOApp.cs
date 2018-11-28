using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ConvertLib
{
    public class BaseIOApp
    {
        public static string inputFolder;
        public static string outputFolder;

        public static string TargetFile(string sourceFile, string extension)
        {
            var f = Path.GetFileName(sourceFile);
            var bonusFolder = sourceFile.Replace(inputFolder, "").Replace(f, "").Replace(@"\\", "");

            return bonusFolder;
        }

        public static void Init(string[] args)
        {
            var current = Assembly.GetExecutingAssembly().GetName();
            Console.WriteLine($"Starting {current}");
            ParseArgs(args);

            Console.WriteLine($"->Input directory is {inputFolder}");
            Console.WriteLine($"->Output directory is {outputFolder}");

            EnsureFoldersExist(inputFolder);
            EnsureFoldersExist(outputFolder);
        }

        static void ParseArgs(string[] args)
        {
            inputFolder = Environment.CurrentDirectory + @"\input\";
            outputFolder = Environment.CurrentDirectory + @"\output\";
            if (!args.Any())
            {
                Console.WriteLine("No input args found, using defaults");
                return;
            }
            if (args.Count() != 2)
            {
                Console.WriteLine("Incorrect number of input args found, using defaults");
                return;
            }

            if (FolderExists(args[0]) && FolderExists(args[1]))
            {
                inputFolder = args[0];
                outputFolder = args[1];
                Console.WriteLine("Input arguments accepted");
            }
        }

        static bool FolderExists(string folder)
        {
            if (!Directory.Exists(folder))
            {
                Console.WriteLine($"The folder {folder} does not exist");
                return false;
            }
            return true;
        }

        public static void EnsureFoldersExist(string folder)
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
                Console.WriteLine($"Created folder {folder}");
            }
        }

        public static void PressEnterToExit()
        {
            Console.WriteLine();
            Console.WriteLine("Press enter to exit...");
            Console.ReadLine();
            Console.WriteLine("Exit!");
        }
    }
}

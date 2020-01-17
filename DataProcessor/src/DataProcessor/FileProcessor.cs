using static System.Console;
using System.IO;

namespace DataProcessor
{
    internal class FileProcessor
    {
        private static readonly string BackupDirectoryName = "backup";
        private static readonly string InProgressDirectoryName = "processing";
        private static readonly string CompletedDirectoryName = "complete";
        private string InputFilePath { get; }

        public FileProcessor(string filePath)
        {
            InputFilePath = filePath;
        }

        public void Process()
        {
            WriteLine($"Begin process of {InputFilePath}");
            //check if file exist
            if(!File.Exists(InputFilePath)){
                WriteLine($"ERROR: file {InputFilePath} does not exist.");
                return;
            }
            
            string rootDirectoryPath = new DirectoryInfo(InputFilePath).Parent.Parent.FullName;
            WriteLine($"Root data path is {rootDirectoryPath}");

            // check if backup dir exists
            string inputFileDirectoryPath = Path.GetDirectoryName(InputFilePath);
            string backupDirectoryPath = Path.Combine(rootDirectoryPath, BackupDirectoryName);
         
            //create if it doenst exist
            Directory.CreateDirectory(backupDirectoryPath);

            //Copy file to backup directory
            string inputFileName = Path.GetFileName(InputFilePath);
            string backupFilePath = Path.Combine(backupDirectoryPath, inputFileName);
            WriteLine($"Copying {InputFilePath} to {backupFilePath}");
            File.Copy(InputFilePath,backupFilePath, true);

            //move to in progress dir
            Directory.CreateDirectory(Path.Combine(rootDirectoryPath, InProgressDirectoryName));
            string inprogressFilePath = Path.Combine(rootDirectoryPath, InProgressDirectoryName, inputFileName);
            if(File.Exists(inprogressFilePath)) 
            {
                WriteLine($"ERROR: A file with name {inprogressFilePath} is already being processed");
                return;
            }

            WriteLine($"Moving {InputFilePath} to {inprogressFilePath}");
            File.Move(InputFilePath, inprogressFilePath);
        }
    }
}
using System;
using static System.Console;

namespace DataProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Parsing command line options");
            WriteLine(args[0]);
            var command = args[0];

            if( command == "--file")
            {
                var filePath = args[1];
                WriteLine($"Single file {filePath} selected");
                ProcessingSingleFile(filePath);
            }
            else if (command == "--dir")
            {
                var diretoryPath = args[1];
                var fileType = args[2];
                WriteLine($"Directory [ directoryPath] selected for {fileType} files");
                ProcessDirectory(diretoryPath, fileType);
            }
            else 
            {
                WriteLine("invalid command line options");
        
            }
            WriteLine("Press enter to quite");
            ReadLine();
        }
        private static void ProcessingSingleFile(string filePath)
        {
            var fileProcessor = new FileProcessor(filePath);
            fileProcessor.Process();
        }

        private static void ProcessDirectory(string directoryPath, string fileType)
        {

        }
    }
}

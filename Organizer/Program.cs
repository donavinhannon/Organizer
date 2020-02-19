using System;
using System.Collections.Generic;
using System.IO;

namespace Organizer
{
    class Program
    {
        static string BASEDIRECTORY;
        static Queue<string> directoryQueue = new Queue<string>();
        static int recurseCount = 0;
        static int fileCount = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("Please input an entry direcory");
            BASEDIRECTORY = Console.ReadLine().Trim().ToString();
            AddToQueue(Directory.EnumerateDirectories(BASEDIRECTORY));

            DQ();
        }

        static void ProcessFolder(string folder)
        {
            if (Directory.Exists(folder))
            {
                try
                {
                    IEnumerable<string> files = Directory.EnumerateFiles(folder);
                    WriteToResults(files);
                    IEnumerable<string> directories = Directory.EnumerateDirectories(folder);
                    AddToQueue(directories);
                }
                catch (UnauthorizedAccessException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Error: Invalid Direcrory.");
            }
        }

        static void WriteToResults(IEnumerable<string> files)
        {
            foreach (string f in files)
            {
                Console.WriteLine($"File {fileCount++} Processed");
                File.AppendAllText($"{BASEDIRECTORY}\\results.txt", $"{f}\n");
            }
        }

        static void AddToQueue(IEnumerable<string> directories)
        {
            foreach (string d in directories)
                directoryQueue.Enqueue(d);
        }

        static void DQ()
        {
            while (directoryQueue.Count != 0) {
                Console.WriteLine($"Directory {recurseCount++} Processed");
                ProcessFolder(directoryQueue.Dequeue());
            }

            Console.WriteLine("Congratulations! Finito");
        }
    }
}

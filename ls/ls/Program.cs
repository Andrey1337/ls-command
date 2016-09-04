using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;



namespace ls
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> elements = new List<string>();


            Directory.GetFiles(Environment.CurrentDirectory).Select(Path.GetFileName)
                .Where(d => !new DirectoryInfo(d).Attributes.HasFlag(FileAttributes.System | FileAttributes.Hidden))
                .ToList()
                .ForEach(f => elements.Add(f));

            Directory.GetDirectories(Environment.CurrentDirectory)
                 .Select(Path.GetFileName)
                 .Where(d => !new DirectoryInfo(d).Attributes.HasFlag(FileAttributes.System | FileAttributes.Hidden))
                 .ToList()
                 .ForEach(f => elements.Add(f));


            elements.Sort();

            foreach (string element in elements)
            {
                Console.Write(DateTime.Now.ToShortDateString() + "   ");
                string time = Directory.GetLastWriteTime(element).ToShortTimeString() + "   ";
                if (time.Length != 8)
                {
                    Console.Write(time.Insert(0, "0"));
                }
                else {
                    Console.Write(time);
                }
                if ((File.GetAttributes(element) & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    Console.Write(" <DIR>   ");
                }
                else {
                    Console.Write("         ");
                }
                if ((File.GetAttributes(element) & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                Console.WriteLine(element);
                Console.ResetColor();
            }
        }
    }
}

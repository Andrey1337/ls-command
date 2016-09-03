using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;
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
                Console.Write("{0} {1} ", Directory.GetLastWriteTime(element).ToLocalTime().ToString("MM/dd/yyyy hh:mm tt"), IsDirectory(element) );
                ChangeColor(element);

            }


        }
        public static string IsDirectory(string element)
        {
            FileAttributes attr = File.GetAttributes(element);
            string msg = "";
            int fileCounter = 0;
            int directoryCounter = 0;

            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                directoryCounter++;
                msg = "<DIR> ";
            }
            else {
                fileCounter++;
                msg = "      ";
            }
            return msg;
        }

        public static void ChangeColor(string element)
        {
            FileAttributes attr = File.GetAttributes(element);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
            }

            Console.WriteLine(element);
            Console.ResetColor();
        }
    }
}

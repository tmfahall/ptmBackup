using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ptmBackup
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryInfo source = new DirectoryInfo("Q:\\vision");

            //OVERWRITES WEEKLY
            string day = DateTime.Now.DayOfWeek.ToString();

            DirectoryInfo target = new DirectoryInfo("E:\\" + day + "\\vision\\");

            CopyFilesRecursively(source, target);

            Environment.Exit(0);
        }

        public static void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            Console.Clear();
            string logFile = "E:\\" + DateTime.Now.DayOfWeek.ToString() + ".txt";

            StreamWriter output = new StreamWriter(logFile, true);
            string line = "\r\n" + DateTime.Now.ToString() + "\r\n";

            output.WriteLine(line);
            output.Flush();

            foreach (DirectoryInfo dir in source.GetDirectories())
            {
                string workingOn = "Attempting to copy " + dir;
                Console.WriteLine(workingOn);
                output.WriteLine(workingOn);
                output.Flush();

                try
                {
                    CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
                    string success = "SUCCESS";
                    Console.WriteLine(success);
                    output.WriteLine(success);
                    output.Flush();

                }
                catch (IOException ex)
                {
                    string fail = "FAIL";
                    Console.WriteLine(fail);
                    Console.WriteLine(ex.Message);
                    output.WriteLine(fail);
                    output.WriteLine(ex.Message);
                    output.Flush();
                    
                }
            }

            foreach (FileInfo file in source.GetFiles())
            {
                Console.WriteLine("Attempting to copy " + file);
                try
                {
                    file.CopyTo(Path.Combine(target.FullName, file.Name), true);
                    string success = "SUCCESS";
                    Console.WriteLine(success);
                    output.WriteLine(success);
                    output.Flush();

                }
                catch (IOException ex)
                {
                    string fail = "FAIL";
                    Console.WriteLine(fail);
                    Console.WriteLine(ex.Message);
                    output.WriteLine(fail);
                    output.WriteLine(ex.Message);
                    output.Flush();

                }
            }
            output.Close();
        }
    }
}

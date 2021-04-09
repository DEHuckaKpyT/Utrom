using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utrom
{
    class Program
    {
        static void Main(string[] args)
        {



            Console.ReadKey();
        }

        static void Get(string path)
        {
            DirectoryInfo mainDirectory = new DirectoryInfo(path);
            DirectoryInfo[] directories = mainDirectory.GetDirectories();
            FileInfo[] files = mainDirectory.GetFiles();



        }

        static int GetValue(string path)
        {
            using (StreamReader stream = new StreamReader(path))
                return int.Parse(stream.ReadLine().Replace("Kb", ""));
        }



    }
}

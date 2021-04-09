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
            ProcessFiles(mainDirectory.GetFiles());

        }

        static void ProcessFiles(FileInfo[] files)
        {
            string prevFileName = "";
            string curFileName = "";
            string curFileUUID = "";
            List<LocaFileInfo> locaFiles = new List<LocaFileInfo>();
            foreach (FileInfo file in files)
            {
                curFileName = file.Name.Remove(file.Name.LastIndexOf(' '));
                curFileUUID = file.Name.Remove(0, file.Name.LastIndexOf(' ')).Remove(file.Name.LastIndexOf('.'));
                LocaFileInfo curFile = new LocaFileInfo(curFileName, curFileUUID, GetWeight(file.FullName));
                if (prevFileName == curFileName)
                {
                    locaFiles.Add(curFile);
                }
                else
                {
                    locaFiles.OrderBy(c => c.Weight).ThenBy(c => c.UUID);
                    //Write(locaFiles);
                    locaFiles.Clear();
                    locaFiles.Add(curFile);
                    prevFileName = curFileName;
                }
            }
            locaFiles.OrderBy(c => c.Weight).ThenBy(c => c.UUID);
            //Write(locaFiles);
        }

        static int GetWeight(string path)
        {
            using (StreamReader stream = new StreamReader(path))
                return int.Parse(stream.ReadLine().Replace("Kb", ""));
        }



    }
}

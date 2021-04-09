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
            Console.Write("Input = ");
            //string inpPath = Console.ReadLine();
            Console.Write("Output = ");
            //string outPath = Console.ReadLine();
            //Get(inpPath, outPath);
            Get("1", "2");

            Console.ReadKey();
        }

        static void Get(string startPath, string copyPath)
        {
            DirectoryInfo mainDirectory = new DirectoryInfo(startPath);
            DirectoryInfo[] directories = mainDirectory.GetDirectories();
            ProcessFiles(mainDirectory.GetFiles(), copyPath);

        }

        static void ProcessFiles(FileInfo[] files, string copyPath)
        {
            string prevFileName = "";
            string curFileName = "";
            string curFileUUID = "";
            List<LocaFileInfo> locaFiles = new List<LocaFileInfo>();
            foreach (FileInfo file in files)
            {
                curFileName = file.Name.Remove(file.Name.LastIndexOf(' '));
                curFileUUID = file.Name.Remove(file.Name.LastIndexOf('.')).Remove(0, file.Name.LastIndexOf(' '));
                LocaFileInfo curFile = new LocaFileInfo(curFileName, curFileUUID, 
                    GetWeight(file.FullName), file.FullName, 
                    file.Name.Remove(0, file.Name.LastIndexOf('.')));

                if (prevFileName == curFileName)
                {
                    locaFiles.Add(curFile);
                }
                else
                {
                    locaFiles = locaFiles.OrderByDescending(c => c.Weight).ThenBy(c => c.UUID).ToList();
                    CopyLocalFiles(locaFiles, copyPath);
                    locaFiles.Clear();
                    locaFiles.Add(curFile);
                    prevFileName = curFileName;
                }
            }
            locaFiles = locaFiles.OrderByDescending(c => c.Weight).ThenBy(c => c.UUID).ToList();
            CopyLocalFiles(locaFiles, copyPath);
        }

        static void CopyLocalFiles(List<LocaFileInfo> locaFiles, string path)
        {
            int i = 0;
            foreach (LocaFileInfo file in locaFiles)
            {
                File.Copy(file.FullName, path + (i == 0 ? $"\\{file.Name}{file.FileType}" : $"\\{file.Name} ({i}){file.FileType}"), true);
                i++;
            }
        }

        static int GetWeight(string path)
        {
            using (StreamReader stream = new StreamReader(path))
                return int.Parse(stream.ReadLine().Replace("Kb", ""));
        }



    }
}

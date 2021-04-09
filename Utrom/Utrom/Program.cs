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
            Console.Write("InputDirectory = ");
            string inpPath = Console.ReadLine();
            Console.Write("OutputDirectory = ");
            string outPath = Console.ReadLine();
            JustDoIt(inpPath, outPath);

            Console.ReadKey();
        }
        static void JustDoIt(string startPath, string copyPath)
        {
            DirectoryInfo mainDirectory = new DirectoryInfo(startPath);
            ProcessDirectories(mainDirectory.GetDirectories(), copyPath);
            ProcessFiles(mainDirectory.GetFiles(), copyPath);
        }
        static void ProcessDirectories(DirectoryInfo[] directories, string copyPath)
        {
            string prevDirectoryName = "";
            string curDirectoryName = "";
            string curDirectoryUUID = "";
            List<LocalDirectoryInfo> locaDirectories = new List<LocalDirectoryInfo>();
            foreach (DirectoryInfo directory in directories)
            {
                curDirectoryName = directory.Name.Remove(directory.Name.LastIndexOf(' '));
                curDirectoryUUID = directory.Name.Remove(0, directory.Name.LastIndexOf(' '));
                LocalDirectoryInfo curDirectory = new LocalDirectoryInfo(curDirectoryName, curDirectoryUUID, 
                    directory.GetFiles().Sum(c => GetWeight(c.FullName)), directory.FullName);

                if (prevDirectoryName == curDirectoryName)
                {
                    locaDirectories.Add(curDirectory);
                }
                else
                {
                    locaDirectories = locaDirectories.OrderByDescending(c => c.Weight).ThenBy(c => c.UUID).ToList();
                    CopyLocalDirectories(locaDirectories, copyPath);
                    locaDirectories.Clear();
                    locaDirectories.Add(curDirectory);
                    prevDirectoryName = curDirectoryName;
                }
            }
            locaDirectories = locaDirectories.OrderByDescending(c => c.Weight).ThenBy(c => c.UUID).ToList();
            CopyLocalDirectories(locaDirectories, copyPath);
        }
        static void ProcessFiles(FileInfo[] files, string copyPath)
        {
            string prevFileName = "";
            string prevFileType = "";
            string curFileName = "";
            string curFileUUID = "";
            string curFileType = "";
            List<LocaFileInfo> locaFiles = new List<LocaFileInfo>();
            foreach (FileInfo file in files)
            {
                curFileName = file.Name.Remove(file.Name.LastIndexOf(' '));
                curFileUUID = file.Name.Remove(file.Name.LastIndexOf('.')).Remove(0, file.Name.LastIndexOf(' '));
                curFileType = file.Name.Remove(0, file.Name.LastIndexOf('.') + 1);
                LocaFileInfo curFile = new LocaFileInfo(curFileName, curFileUUID, GetWeight(file.FullName), file.FullName, curFileType);

                if (prevFileName == curFileName && prevFileType == curFileType)
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
                    prevFileType = curFileType;
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
                File.Copy(file.FullName, path + (i == 0 ? $"\\{file.Name}.{file.FileType}" : $"\\{file.Name} ({i}).{file.FileType}"), true);
                i++;
            }
        }
        static void CopyLocalDirectories(List<LocalDirectoryInfo> localDirectories, string path)
        {
            Directory.CreateDirectory(path);
            int i = 0;
            foreach (LocalDirectoryInfo directory in localDirectories)
            {
                string newPath = i == 0 ? $"{path}\\{directory.Name}" : $"{path}\\{directory.Name} ({i})";
                Directory.CreateDirectory(newPath);
                foreach (FileInfo file in new DirectoryInfo(directory.FullName).GetFiles())
                    File.Copy(file.FullName, ($"{newPath}\\{file.Name}"), true);
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
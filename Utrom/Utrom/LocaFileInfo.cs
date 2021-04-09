using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utrom
{
    class LocaFileInfo
    {
        public string Name { get; }
        public string UUID { get; }
        public int Weight { get; }
        public string FullName { get; }
        public string FileType { get; }

        public LocaFileInfo(string name, string UUID, int weight, string fullName, string fileType)
        {
            Name = name;
            this.UUID = UUID;
            Weight = weight;
            FullName = fullName;
            FileType = fileType;
        }
    }
}

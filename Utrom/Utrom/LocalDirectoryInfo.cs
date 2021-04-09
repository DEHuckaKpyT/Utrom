using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utrom
{
    class LocalDirectoryInfo
    {
        public string Name { get; }
        public string UUID { get; }
        public int Weight { get; }
        public string FullName { get; }

        public LocalDirectoryInfo(string name, string UUID, int weight, string fullName)
        {
            Name = name;
            this.UUID = UUID;
            Weight = weight;
            FullName = fullName;
        }
    }
}

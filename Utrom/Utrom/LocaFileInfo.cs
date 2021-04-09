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

        public LocaFileInfo(string name, string UUID, int weight)
        {
            Name = name;
            this.UUID = UUID;
            Weight = weight;
        }
    }
}

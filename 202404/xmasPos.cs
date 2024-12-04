using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _202404
{
    internal class xmasPos
    {
        public int x { get; set; }
        public int y { get; set; }

        public bool North { get; set; }

        public bool NorthEast { get; set; }
        public bool East { get; set; }
        public bool SouthEast { get; set; }
        public bool South { get; set; }
        public bool SouthWest { get; set; }
        public bool West { get; set; }
        public bool NorthWest { get; set; }

    }

    public enum direction
    {
        North = 0,
        NorthEast = 1,
        East = 2,
        SouthEast = 3,
        South = 4,
        SouthWest = 5,
        West = 6,
        NorthWest = 7

    }
}

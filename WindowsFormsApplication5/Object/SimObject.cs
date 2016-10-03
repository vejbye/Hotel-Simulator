using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulator.Object
{
    class SimObject
    {
        public Image Image;
        public int Width;
        public int Height;
        public Space Position;

        public Dictionary<Neighbour.Neighbours, Space> neighbours = new Dictionary<Neighbour.Neighbours, Space>();

        public void CreateNeighbours(ref Space neighbour, Neighbour.Neighbours n)
        {
            neighbours.Add(n, neighbour);
        }
    }
}

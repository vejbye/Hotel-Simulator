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
        public Image image;
        public int width;
        public int height;
        public Dictionary<Neighbour.Neighbours, SimObject> Neighbours = new Dictionary<Neighbour.Neighbours, SimObject>();

        public void CreateNeighbours(ref SimObject Neighbour, Neighbour.Neighbours n)
        {
            Neighbours.Add(n, Neighbour);
        }
    }
}

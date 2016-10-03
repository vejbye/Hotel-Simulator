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
        public SimObject CurrentObject;

        public Dictionary<Neighbour.Neighbours, SimObject> neighbours = new Dictionary<Neighbour.Neighbours, SimObject>();

        public void CreateNeighbours(ref SimObject neighbour, Neighbour.Neighbours n)
        {
            neighbours.Add(n, neighbour);
        }
    }
}

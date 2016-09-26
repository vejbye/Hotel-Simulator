using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulator.Object
{
    class Space
    {
        public GameObject currentObject { get; set; }
        public Dictionary<Neighbour.Neighbours, Space> neighbours = new Dictionary<Neighbour.Neighbours, Space>();

        public void CreateNeighbours(ref Space neighbour, Neighbour.Neighbours n)
        {
            neighbours.Add(n, neighbour);
        }
        


        

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulator.Object
{
    class GameObject
    {
        public Image image;

        public GameObject currentObject { get; set; }
        public Dictionary<Neighbour.Neighbours, GameObject> neighbours = new Dictionary<Neighbour.Neighbours, GameObject>();

        public void CreateNeighbours(ref GameObject neighbour, Neighbour.Neighbours n)
        {
            neighbours.Add(n, neighbour);
        }
    }
}

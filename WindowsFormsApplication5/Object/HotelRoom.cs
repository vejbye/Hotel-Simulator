using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulator.Object
{
    public enum Neighbours
    {
        North,
        East,
        South,
        West
    }

    public class HotelRoom : SimObject
    {
        public HotelRoom CurrentRoom;
        public Dictionary<Neighbours, HotelRoom> Neighbours = new Dictionary<Neighbours, HotelRoom>();
        public List<Guest> Guests;
        public List<Maid> Maids;
        public HotelRoom Previous;
        public int Distance;
        public int Id;

        public HotelRoom()
        {
                Width = 1;
                Height = 1;
            Maids = new List<Maid>();
            Guests = new List<Guest>();
        }
        public void CreateNeighbours(ref HotelRoom neighbour, Neighbours n)
        {
            Neighbours.Add(n, neighbour);
            Previous = null;
            Distance = Int32.MaxValue;
        }

    }
}

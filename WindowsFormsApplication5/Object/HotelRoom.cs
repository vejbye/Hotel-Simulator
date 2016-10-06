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
        public Guest Guest;
        public HotelRoom Previous;
        public int Distance;
        public int Id;

        public void CreateNeighbours(ref HotelRoom neighbour, Neighbours n)
        {
            Neighbours.Add(n, neighbour);
            Previous = null;
            Distance = Int32.MaxValue;
        }

    }
}

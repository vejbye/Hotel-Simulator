using System;
using System.Collections.Generic;
using System.Drawing;
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
        public Dictionary<Neighbours, HotelRoom> Neighbours = new Dictionary<Neighbours, HotelRoom>();
        public HotelRoom CurrentRoom;
        public HotelRoom Previous;
        public List<Guest> Guests;
        public List<Maid> Maids;
        public Point RoomPosition;

        //General information of a hotelroom.
        public int Distance;
        public int Id;
        public int Classification;
        public int Floor;
        public string Dimensions;

        public HotelRoom()
        {
            Width = 100;
            Height = 50;
            Maids = new List<Maid>();
            Guests = new List<Guest>();
        }
        
        /// <summary>
        /// Adds neighbours to a hotelroom. 
        /// </summary>
        /// <param name="neighbour">The room you want to add neighbours to.</param>
        /// <param name="n">Direction of the neighbour.</param>
        public void CreateNeighbours(ref HotelRoom neighbour, Neighbours n)
        {
            Neighbours.Add(n, neighbour);
            Previous = null;
            Distance = Int32.MaxValue;
        }


    }
}

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
        public HotelRoom CurrentRoom { get; set; }
        public HotelRoom Previous { get; set; }
        public List<Guest> Guests { get; set; }
        public List<Maid> Maids { get; set; }
        public Point RoomPosition { get; set; }

        //General information of a hotelroom.
        public int Distance { get; set; }
        public int Id { get; set; }
        public int Classification { get; set; }
        public int Floor { get; set; }
        public string Dimensions { get; set; }

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

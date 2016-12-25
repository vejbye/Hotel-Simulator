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

    public abstract class HotelRoom : SimObject
    {
        public Dictionary<Neighbours, HotelRoom> Neighbours = new Dictionary<Neighbours, HotelRoom>();
        public HotelRoom CurrentRoom { get; set; } //what type of room is this
        public HotelRoom Previous { get; set; } //this is necessary for creating paths for guests/maids, memorizes wich hotelroom has been used so far
        public List<Guest> Guests { get; set; } //a list of guests currently in the room
        public List<Maid> Maids { get; set; }// a lis of maids currently in the room
        public Point RoomPosition { get; set; }//the position of the room in the bitmap

        //General information of a hotelroom.
        public int Distance { get; set; } // this is necessary for creating paths for guests/maids, memorizes how long the path is so the shortest path can be made
        public int Id { get; set; } // the id of the hotelroom
        public int Classification { get; set; } // how many stars the hotelroom has if applicable
        public int Floor { get; set; }// the floor the room is on
        public string Dimensions { get; set; } //the size of the room (1x1, 2x2, etc)

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

using HotelSimulator.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulator.Object
{
    public class Person: SimObject
    {
        public enum Directions
        {
            LEFT,
            RIGHT,
            UP,
            DOWN,
            NONE
        }

        public Directions Direction { get; set; }
        public HotelRoom Current; // current location of guest
        public List<HotelRoom> Path; // for storing the path to the guests destination
        public bool Dead = false;
        public int WaitTime = 0;
        public int MoveDistance;
        public int RoomPositioning;
        public int HeightPositioning;
        public int HteDuration;

        public Person()
        {
            MoveDistance = 10;
            RoomPositioning = 4;
            HeightPositioning = 10;
            HteDuration = 1;
        }
        


        }
   
}

using HotelSimulator.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulator.Object
{
    public abstract class Person : SimObject
    {
        public enum Directions
        {
            North,
            East,
            South,
            West,
            LEFT,
            RIGHT,
            UP,
            DOWN,
            NONE
        }

        public HotelRoom Current { get; set; } // current location of guest
        public List<HotelRoom> Path { get; set; }// for storing the path to the guests destination
        public int HteDuration { get; set; }
        public bool Dead;
        protected int WaitTime;
        protected int MoveDistance;
        protected int RoomPositioning;
        protected int HeightPositioning;
        protected Directions Direction;

        public Person()
        {
            MoveDistance = 10;
            RoomPositioning = 4;
            HeightPositioning = 10;
            HteDuration = 1;
            WaitTime = 0;
            Dead = false;
        }

        public abstract void Walk(Hotel hotel);

    }

}

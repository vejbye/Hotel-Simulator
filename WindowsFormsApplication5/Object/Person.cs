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
        }

        public HotelRoom Current { get; set; } // current location of guest
        public List<HotelRoom> Path { get; set; }// for storing the path to the guests destination
        public int HteDuration { get; set; } //How fast the person will work
        public bool Dead; 
        public int WaitTime; //How long the person waits before he/she dies
        protected int MoveDistance;//The amount of pixels it moves in the hotel
        protected int RoomPositioning;//Where to make the guest walk, changing this will change how the guest will walk
        protected int HeightPositioning;//Same as above 
        protected Directions Direction;//The direction this person will go
        public HotelRoom Destination { get; set; }//The destination where the guest will go to
        public bool InQueue { get; set; }
        public int CurrentFloor { get; set; }//The current floor the guest is on

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

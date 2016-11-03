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
        public HotelRoom Current; // current location of guest
        public List<HotelRoom> Path; // for storing the path to the guests destination
        public int MoveDistance = 10;
        public int RoomPositioning = 4;
        public int HeightPositioning = 10;
        public bool dead = false;
        public int waitTime = 0;

        public void Update()
        {

        }
    }
   
}

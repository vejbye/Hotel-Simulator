using HotelSimulator.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulator.Object
{
    public class Moveable: SimObject
    {
        public HotelRoom Current; // current location of guest
        public List<HotelRoom> Path; // for storing the path to the guests destination

        public int MoveDistance;
        public int RoomPositioning;
        public int HeightPositioning;
        public int hteDuration;

        public Moveable()
        {
            MoveDistance = 10;
            RoomPositioning = 4;
            HeightPositioning = 10;
            hteDuration = 1;
        }
        

        
    }
   
}

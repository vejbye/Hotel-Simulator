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
        public int MoveDistance = 10;

        public void Update()
        {

        }
    }
   
}

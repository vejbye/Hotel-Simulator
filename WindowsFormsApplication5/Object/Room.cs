using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using HotelSimulator;

namespace HotelSimulator.Object
{
    class Room : GameObject
    {
        bool taken { get; set; }
        int roomNr { get; set; }

        public Room() : base()
        {
            roomSize = 1;
            image = Image.FromFile(@"C:\Users\iCalvin\Source\Repos\Hotel-Simulator\WindowsFormsApplication5\Resources\Room.png");
        }
        
    }
}

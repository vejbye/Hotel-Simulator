using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using WindowsFormsApplication5.Properties;

namespace HotelSimulator.Object
{
    class Room : GameObject
    {
        bool taken { get; set; }
        int roomNr { get; set; }

        public Room() : base()
        {
            image = Resources.Room;
        }
        
    }
}

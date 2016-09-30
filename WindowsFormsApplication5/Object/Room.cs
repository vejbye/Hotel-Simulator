using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using HotelSimulator;
using WindowsFormsApplication5.Properties;

namespace HotelSimulator.Object
{
    class Room : HotelRoom
    {
        bool taken { get; set; }
        int roomNr { get; set; }

        public Room() : base()
        {
            
            image = Resources.Room;
            taken = false;
            width = 1;
            height = 1;
            
        }

        public bool getTaken()
        {
            return taken;
        }

        public void setTaken(bool taken)
        {
            this.taken = taken;

        }

    }
}

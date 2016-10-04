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
        private bool Taken { get; set; }
        private int RoomNr { get; set; }

        public Room() : base()
        {
            Image = Resources.Room;
            Taken = false;
            Width = 1;
            Height = 1;
        }

        public bool getTaken()
        {
            return Taken;
        }

        public void setTaken(bool taken)
        {
            this.Taken = taken;

        }

    }
}

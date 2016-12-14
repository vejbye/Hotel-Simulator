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
    public class Room : HotelRoom
    {
        private bool _taken;
        public bool Dirty { get; set; }
        public bool BeingCleaned { get; set; } = false;

        public Room() : base()
        {
            _taken = false;
            Dirty = false;
        }

        public bool getTaken()
        {
            return _taken;
        }
        
        public void setTaken(bool taken)
        {
            _taken = taken;
        }

        public void SetRoomImage(string[] RoomDimension)
        {
            if (int.Parse(RoomDimension[0]) == 2)
            {
                if (int.Parse(RoomDimension[1]) == 2)
                    Image = Resources.Room5;
                else
                    Image = Resources.Room3;
            }
            else
                Image = Resources.Room;
        }
    }
}

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
        private bool Taken { get; set; }
        public bool Dirty { get; set; }
        public bool BeingCleaned { get; set; } = false;

        public Room() : base()
        {
            Taken = false;
            Dirty = false;
        }

        public bool getTaken()
        {
            return Taken;
        }
        
        public void setTaken(bool taken)
        {
            Taken = taken;
        }
    }
}

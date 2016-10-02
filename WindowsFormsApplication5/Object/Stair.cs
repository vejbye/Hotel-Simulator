using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication5.Properties;

namespace HotelSimulator.Object
{
    class Stair: SimObject
    {
        public Stair()
        {
            Image = Resources.Stairs;
            Width = 1;
            Height = 1;
        }
    }
}

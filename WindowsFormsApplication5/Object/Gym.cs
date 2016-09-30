using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication5.Properties;

namespace HotelSimulator.Object
{
    class Gym: HotelRoom
    {
        public Gym()
        {
            image = Resources.Gym;
            width = 1;
            height = 1;
        }
    }
}

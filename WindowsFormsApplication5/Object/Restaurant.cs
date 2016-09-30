using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication5.Properties;

namespace HotelSimulator.Object
{
    class Restaurant: HotelRoom
    {
        int capacity { get; set; }

        public Restaurant()
        {
            image = Resources.Restaurant;
            width = 1;
            height = 1;
        }
        
    }
}

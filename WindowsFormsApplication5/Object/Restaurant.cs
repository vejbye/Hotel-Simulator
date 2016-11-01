using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication5.Properties;

namespace HotelSimulator.Object
{
    public class Restaurant: HotelRoom
    {
        public int Capacity { get; set; }

        public Restaurant()
        {
            Image = Resources.Restaurant;
        }
        
    }
}

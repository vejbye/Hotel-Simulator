using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication5.Properties;

namespace HotelSimulator.Object
{
    class Cinema : HotelRoom
    {
        bool playing { get; set; }
        
        public Cinema()
        {
            Image = Resources.Cinema;
            Width = 1;
            Height = 1;
        }
    }
}

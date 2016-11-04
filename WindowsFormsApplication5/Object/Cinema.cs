using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication5.Properties;

namespace HotelSimulator.Object
{
    public class Cinema : HotelRoom
    {
        public bool Playing { get; set; }
        private int MovieAt;
        
        public Cinema()
        {
            Image = Resources.Cinema;
            Playing = false;
        }

        public void PlayMovie(int duration)
        {
            if (MovieAt == duration)
            {
                Playing = false;
                MovieAt = 1;
            }
            else
                MovieAt++;
        }
    }
}

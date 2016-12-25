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
        public bool Playing { get; set; }// check wether the cinema is busy
        private int _movieAt; //Private integer that counts at how long the movie has been going for.

        public Cinema()
        {
            Image = Resources.Cinema;
            Playing = false;
        }

        /// <summary>
        /// Enjoy and watch.
        /// </summary>
        /// <param name="duration">The movielength in hte.</param>
        public void PlayMovie(int duration)
        {
            if (_movieAt == duration)
            {
                Playing = false;
                _movieAt = 1;
            }
            else
                _movieAt++;
        }
    }
}

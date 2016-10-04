using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication5.Properties;

namespace HotelSimulator.Object
{
    public class Guest: SimObject
    {
        Room room;
        public HotelRoom current;
        HotelRoom[,] plans;
        Graphics gfx;
        int xstart, ystart;

        public Guest(HotelRoom current)
        {
            this.current = current;
            Image = Resources.Guest;
            Width = 30;
            Height = 30;



        }
        public void Walk(Hotel hotel)
        {
            if (current.Neighbours.ContainsKey(Neighbours.East))
            {
                current.guest = null;
                current.Neighbours[Neighbours.East].guest = this;
                current = current.Neighbours[Neighbours.East];
                hotel.Draw(hotel.map);

            }
            else if (current.Neighbours.ContainsKey(Neighbours.South))
            {
                current.guest = null;
                current.Neighbours[Neighbours.South].guest = this;
                current = current.Neighbours[Neighbours.South];
                hotel.Draw(hotel.map);
            }
        }
    }
}

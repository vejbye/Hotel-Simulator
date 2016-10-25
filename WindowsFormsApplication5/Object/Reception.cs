using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using WindowsFormsApplication5.Properties;

namespace HotelSimulator.Object
{
    public class Reception: HotelRoom
    {
        public Reception() : base()
        {
            Image = Resources.Reception;
        }

        public Room findEmptyRoom(Hotel hotel, string stars)
        {
            for (int i = 0; i < hotel.GetMap().GetLength(0); i++)
            {
                for (int j = 0; j < hotel.GetMap().GetLength(1); j++)
                {
                    if (hotel.GetMap()[i, j] is Room && ((Room)hotel.GetMap()[i,j]).Stars.ToString() == stars)
                    {
                        if (!((Room)hotel.GetMap()[i, j]).getTaken())
                        {
                            ((Room)hotel.GetMap()[i, j]).setTaken(true);
                            return (Room)hotel.GetMap()[i, j];
                        }
                    }
                }
            }
            return null;
        }

        public void checkOut(Guest guest)
        {
            guest.Room.setTaken(false);
            guest.Room.Dirty = true;
            guest.Room = null;
        }
    }
}

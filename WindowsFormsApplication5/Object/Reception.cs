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

        public Room findEmptyRoom(Hotel hotel, string stars) //search for empty room if a guest wants to check in
        {
            for (int i = 0; i < hotel.GetMap().GetLength(0); i++)
            {
                for (int j = 0; j < hotel.GetMap().GetLength(1); j++)
                {
                    if (hotel.GetMap()[i, j] is Room && ((Room)hotel.GetMap()[i,j]).Classification.ToString() == stars)
                    {
                        if (!((Room)hotel.GetMap()[i, j]).getTaken() && !((Room)hotel.GetMap()[i, j]).Dirty && !((Room)hotel.GetMap()[i, j]).BeingCleaned)
                        {
                            ((Room)hotel.GetMap()[i, j]).setTaken(true, guest);
                            return (Room)hotel.GetMap()[i, j];
                        }
                    }
                }
            }
            return null;
        }

        public void checkOut(Guest guest)
        {
            guest.Room.setTaken(false); // Make the room empty
            guest.Room.Dirty = true; // Make the room dirty after checkout
            guest.Room = null; //remove room from guest
        }
    }
}

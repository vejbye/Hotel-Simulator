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
        /// <summary>
        /// find a empty room for the guest that is checking in
        /// </summary>
        /// <param name="hotel">Give the hotel that belongs to this Reception</param>
        /// <param name="guest">Give the guest that want to check in</param>
        /// <returns></returns>
        public Room findEmptyRoom(Hotel hotel, Guest guest) //search for empty room if a guest wants to check in
        {
            int stars = int.Parse(guest.Preference);
            while (stars < 6)
            {
                for (int i = 0; i < hotel.GetMap().GetLength(1); i++)
                {
                    for (int j = 0; j < hotel.GetMap().GetLength(0); j++)
                    {
                        if (hotel.GetMap()[j,i] is Room && ((Room)hotel.GetMap()[j, i]).Classification.ToString() == stars.ToString())
                        {
                            if (!((Room)hotel.GetMap()[j, i]).getTaken() && !((Room)hotel.GetMap()[j, i]).Dirty && !((Room)hotel.GetMap()[j, i]).BeingCleaned)
                            {
                                ((Room)hotel.GetMap()[j, i]).setTaken(true);
                                return (Room)hotel.GetMap()[j, i];
                            }
                        }
                    }
                }
                stars++;
           }
            return null;
        }
        /// <summary>
        /// check out the guest
        /// </summary>
        /// <param name="guest">Give the guest that is cheking out</param>
        public void checkOut(Guest guest)
        {
            guest.Room.setTaken(false); // Make the room empty
            guest.Room.Dirty = true; // Make the room dirty after checkout
            guest.Room = null; //remove room from guest
        }
    }
}

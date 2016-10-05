using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using WindowsFormsApplication5.Properties;

namespace HotelSimulator.Object
{
    class Reception: HotelRoom
    {
        public Reception() : base()
        {
            Image = Resources.Reception;
            Width = 1;
            Height = 1;
        }

        public Room findEmptyRoom(Hotel hotel)
        {
            for (int i = 0; i < hotel.getMap().GetLength(0); i++)
            {
                for (int j = 0; j < hotel.getMap().GetLength(1); j++)
                {
                    if (hotel.getMap()[i, j] is Room)
                    {
                        if (!((Room)hotel.getMap()[i, j]).getTaken())
                        {
                            ((Room)hotel.getMap()[i, j]).setTaken(true);
                            return (Room)hotel.getMap()[i, j];
                        }
                    }
                }
            }
            return null;
        }
    }
}

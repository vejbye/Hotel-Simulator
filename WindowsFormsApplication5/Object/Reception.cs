using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using WindowsFormsApplication5.Properties;

namespace HotelSimulator.Object
{
    class Reception: GameObject
    {
        public Reception() : base()
        {
            image = Resources.Reception;
        }

        public Room findEmptyRoom(Hotel hotel)
        {
            for (int i = 0; i < hotel.getMap().GetLength(0); i++)
            {
                for (int j = 0; j < hotel.getMap().GetLength(1); i++)
                {
                    if (hotel.getMap()[i, j].currentObject is Room)
                    {
                        if (!((Room)hotel.getMap()[i, j].currentObject).getTaken())
                        {
                            ((Room)hotel.getMap()[i, j].currentObject).setTaken(true);
                            return (Room)hotel.getMap()[i, j].currentObject;
                        }
                    }
                }
            }
            return null;
        }
    }
}

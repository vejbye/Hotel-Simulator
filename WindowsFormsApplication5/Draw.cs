using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using HotelSimulator.Object;
using WindowsFormsApplication5.Properties;

namespace WindowsFormsApplication5
{
    public class Draw
    {
        public Bitmap DrawHotel(HotelRoom[,] map, Bitmap _hotel)
        {
            //Drawing the hotel on a bitmap
            Graphics gfx = Graphics.FromImage(_hotel);
            
            int xStartPosition = 500;
            int yStartPosition = 735;
            int standardRoomWidth = 100;
            int standardRoomHeight = 50;
            
            //Background image o the hotel
            gfx.DrawImage(Resources.SimulatorBG, 1, 1, 2000, 800);

            //Fills space with a room if there is one
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y] != null && map[x, y] is SimObject)
                    {
                        if (map[x, y].Image != null)
                        {
                            gfx.DrawImage(map[x, y].Image, xStartPosition, yStartPosition - map[x, y].Height, map[x, y].Width, map[x, y].Height);
                            map[x, y].BoundingBox = new Rectangle(xStartPosition, yStartPosition - map[x, y].Height, map[x, y].Width, map[x, y].Height);
                        }
                        foreach (Guest guest in map[x, y].Guests)
                            gfx.DrawImage(guest.Image, xStartPosition, yStartPosition - map[x, y].Height, guest.Width, guest.Height);
                        foreach (Maid maid in map[x, y].Maids)
                            gfx.DrawImage(maid.Image, xStartPosition, yStartPosition - map[x, y].Height, maid.Width, maid.Height);
                    }

                    //Builds down
                    yStartPosition -= standardRoomHeight;
                }

                //Builds to right and sets start position on the ground again.
                xStartPosition += standardRoomWidth;
                yStartPosition = 735;
            }




            //Returns the drawn bitmap
            return _hotel;

        }
    }
}

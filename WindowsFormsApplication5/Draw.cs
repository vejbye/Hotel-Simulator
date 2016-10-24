using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using WindowsFormsApplication5.Properties;

namespace HotelSimulator.Object
{
    public class Draw
    {
        public int yStartPosition = 735;
        public int xStartPosition = 500;

        public int currentXPos;
        public int currentYPos;

        public int standardRoomWidth = 100;
        public int standardRoomHeight = 50;

        public Bitmap DrawHotel(HotelRoom[,] map, Bitmap hotel, Elevator hotelElevator, bool newHotel)
        {
            //Drawing the hotel on a bitmap
            Graphics gfx = Graphics.FromImage(hotel);

            currentXPos = xStartPosition;
            currentYPos = yStartPosition;
            
            //Background image of the hotel
            gfx.DrawImage(Resources.SimulatorBG, (xStartPosition - xStartPosition), (yStartPosition - yStartPosition), hotel.Width, hotel.Height);
            
            //Fills space with a room if there is one
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y] != null && map[x, y] is SimObject)
                    {
                        if (map[x, y].Image != null)
                        {
                            if (map[x, y] is Elevator)
                                gfx.DrawImage(map[x, y].Image, hotelElevator.Position.X, hotelElevator.Position.Y, hotelElevator.Width, hotelElevator.Height);
                            else
                            {
                                gfx.DrawImage(map[x, y].Image, currentXPos, currentYPos - map[x, y].Height, map[x, y].Width, map[x, y].Height);

                                if (newHotel)
                                    DrawBoundingBoxes(map, x, y);
                            }
                        }

                        foreach (Guest guest in map[x, y].Guests)
                        {
                            if (map[x, y].Height == standardRoomHeight)
                                gfx.DrawImage(guest.Image, currentXPos, currentYPos - map[x, y].Height, guest.Width, guest.Height);
                            else
                                gfx.DrawImage(guest.Image, currentXPos, currentYPos - map[x, y].Height + standardRoomHeight, guest.Width, guest.Height);
                        }

                        foreach (Maid maid in map[x, y].Maids)
                            gfx.DrawImage(maid.Image, currentXPos, currentYPos - map[x, y].Height, maid.Width, maid.Height);
                    }

                    //Builds down
                    currentYPos -= standardRoomHeight;
                }

                //Builds to right and sets start position on the ground again.
                currentXPos += standardRoomWidth;
                currentYPos = yStartPosition;
            }
            
            //Returns the drawn bitmap
            return hotel;

        }

        public Bitmap MoveElevator(Hotel hotel, Elevator hotelElevator, int move)
        {
            hotelElevator.Position.Y = hotelElevator.Position.Y - 100;
            return DrawHotel(hotel.GetMap(), hotel._hotel, hotelElevator, false);
        }

        private void DrawBoundingBoxes(HotelRoom[,] map, int x, int y)
        {
            map[x, y].BoundingBox = new Rectangle(currentXPos, currentYPos - map[x, y].Height, map[x, y].Width, map[x, y].Height);
        }
    }
}

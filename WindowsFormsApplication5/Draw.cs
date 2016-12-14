using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using WindowsFormsApplication5.Properties;
using System.Windows.Forms;

namespace HotelSimulator.Object
{
    public class Draw
    {
        public Image Img = Resources.SimulatorBG;
        public Graphics Gfx;
        public int YStartPosition = 735;
        public int XStartPosition = 500;

        public int CurrentXPos;
        public int CurrentYPos;

        public int StandardRoomWidth = 100;
        public int StandardRoomHeight = 50;

        public Bitmap DrawHotel(Hotel hotel, bool newHotel)
        {
            //Drawing the hotel on a bitmap
            Gfx = Graphics.FromImage(hotel.HotelBitmap);

            CurrentXPos = XStartPosition;
            CurrentYPos = YStartPosition;

            //Background image of the hotel
            Gfx.DrawImage(Img, (XStartPosition - XStartPosition), (YStartPosition - YStartPosition), hotel.HotelBitmap.Width, hotel.HotelBitmap.Height);

            //Fills space with a room if there is one
            for (int x = 0; x < hotel.Map.GetLength(0); x++)
            {
                for (int y = 0; y < hotel.Map.GetLength(1); y++)
                {
                    if (hotel.Map[x, y] != null && hotel.Map[x, y] is SimObject)
                    {
                        if (hotel.Map[x, y].Image != null)
                        {
                            if (hotel.Map[x, y] is Room)
                            {
                                Room currentRoom = (Room)hotel.Map[x, y];
                                if (currentRoom.Dirty) ;
                                  //  Gfx.DrawImage(((Room)hotel.Map[x, y]).DirtyRoom, CurrentXPos, CurrentYPos - hotel.Map[x, y].Height, hotel.Map[x, y].Width, hotel.Map[x, y].Height);
                            }

                            if (hotel.Map[x, y] is Elevator)
                                Gfx.DrawImage(hotel.Map[x, y].Image, hotel.Elevator.ElevatorPosition.X, hotel.Elevator.ElevatorPosition.Y, hotel.Elevator.Width, hotel.Elevator.Height);
                            else
                            {
                                Gfx.DrawImage(hotel.Map[x, y].Image, CurrentXPos, CurrentYPos - hotel.Map[x, y].Height, hotel.Map[x, y].Width, hotel.Map[x, y].Height);

                                if (hotel.Map[x, y] is Reception)
                                {
                                    if (newHotel)
                                        DrawBoundingBoxes(hotel.Map, x, y);
                                }
                            }
                        }

                        if (hotel.Map[x, y].RoomPosition == new Point(0, 0))
                            hotel.Map[x, y].RoomPosition = new Point(CurrentXPos, CurrentYPos - hotel.Map[x, y].Height);
                    }

                    //Builds down
                    CurrentYPos -= StandardRoomHeight;
                }

                //Builds to right and sets start position on the ground again.
                CurrentXPos += StandardRoomWidth;
                CurrentYPos = YStartPosition;
            }

            CurrentXPos = 500;

            foreach (Guest guest in hotel.Guests)
                Gfx.DrawImage(guest.Image, guest.Position.X, guest.Position.Y, guest.Width, guest.Height);

            foreach (Maid maid in hotel.Maids)
                Gfx.DrawImage(maid.Image, maid.Position.X, maid.Position.Y, maid.Width, maid.Height);

            return hotel.HotelBitmap;
        }


        private void DrawBoundingBoxes(HotelRoom[,] map, int x, int y)
        {
            map[x, y].BoundingBox = new Rectangle(CurrentXPos, CurrentYPos - map[x, y].Height, map[x, y].Width, map[x, y].Height);
        }
    }
}

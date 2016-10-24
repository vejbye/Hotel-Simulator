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
        public Image img = Resources.SimulatorBG;
        Graphics gfx;
      //  public Bitmap DrawHotel(HotelRoom[,] map, Bitmap _hotel, List<Guest> guests, List<Maid> maids);
        public int yStartPosition = 735;
        public int xStartPosition = 500;

        public int currentXPos;
        public int currentYPos;

        public int standardRoomWidth = 100;
        public int standardRoomHeight = 50;

        public Bitmap DrawHotel(Hotel hotel, Elevator hotelElevator, bool newHotel)
        {
            //Drawing the hotel on a bitmap
            gfx = Graphics.FromImage(hotel._hotel);

            //Background image o the hotel
            if (img != null)
            {
                gfx.DrawImage(img, 0, 0, 2000, 800);
            }
            currentXPos = xStartPosition;
            currentYPos = yStartPosition;

            //Background image of the hotel
            gfx.DrawImage(Resources.SimulatorBG, (xStartPosition - xStartPosition), (yStartPosition - yStartPosition), hotel._hotelWidth, hotel._hotelHeight);

            //Fills space with a room if there is one
            for (int x = 0; x < hotel.Map.GetLength(0); x++)
            {
                for (int y = 0; y < hotel.Map.GetLength(1); y++)
                {
                    if (hotel.Map[x, y] != null && hotel.Map[x, y] is SimObject)
                    {
                        if (hotel.Map[x, y].Image != null)
                        {
                            if (hotel.Map[x, y] is Elevator)
                                gfx.DrawImage(hotel.Map[x, y].Image, hotelElevator.Position.X, hotelElevator.Position.Y, hotelElevator.Width, hotelElevator.Height);
                            else
                            {
                                gfx.DrawImage(hotel.Map[x, y].Image, currentXPos, currentYPos - hotel.Map[x, y].Height, hotel.Map[x, y].Width, hotel.Map[x, y].Height);

                                if (newHotel)
                                    DrawBoundingBoxes(hotel.Map, x, y);
                            }
                        }
                    }

                    //Builds down
                    currentYPos -= standardRoomHeight;
                }

                //Builds to right and sets start position on the ground again.
                currentXPos += standardRoomWidth;
                currentYPos = yStartPosition;
            }

            xStartPosition = 500;
            foreach (Guest guest in hotel.Guests)
            {
                gfx.DrawImage(guest.Image, guest.Position.X, guest.Position.Y, guest.Width, guest.Height);
            }

            foreach (Maid maid in hotel.maids)
            {
                gfx.DrawImage(maid.Image, maid.Position.X, maid.Position.Y, maid.Width, maid.Height);
            }

            return hotel._hotel;
        }
        public Bitmap MoveElevator(Hotel hotel, Elevator hotelElevator, int move)
        {
            hotelElevator.Position.Y = hotelElevator.Position.Y - 100;
            return DrawHotel(hotel, hotelElevator, false);
        }

        private void DrawBoundingBoxes(HotelRoom[,] map, int x, int y)
        {
            map[x, y].BoundingBox = new Rectangle(currentXPos, currentYPos - map[x, y].Height, map[x, y].Width, map[x, y].Height);
        }

        public void drawPersons(Hotel hotel, SimObject person, Elevator hotelElevator, HotelSimulator hs )
        {
            Point point;
            if (person is Guest || person is Maid) {
                if (person.Direction ==  Direction.RIGHT)
                {
                     point = new Point(person.Position.X + standardRoomWidth, person.Position.Y);
                    while (person.Position.X < point.X)
                    {
                        person.Position.X += 10;
                        xStartPosition = 500;
                        yStartPosition = 735;
                        DrawHotel(hotel, hotelElevator, false);
                        hs.Refresh();
                        //Application.DoEvents();
                    }
                }
                if (person.Direction == Direction.UP)
                {
                    point = new Point(person.Position.X, person.Position.Y + standardRoomHeight);
                    while (person.Position.Y < point.Y)
                    {
                        person.Position.Y += 10;
                        xStartPosition = 500;
                        yStartPosition = 735;
                        DrawHotel(hotel, hotelElevator, false);
                        hs.Refresh();
                    }
                }
                else if (person.Direction == Direction.LEFT)
                {
                    point = new Point(person.Position.X - standardRoomWidth, person.Position.Y);
                    while (person.Position.X > point.X)
                    {
                        person.Position.X -= 10;
                        xStartPosition = 500;
                        yStartPosition = 735;
                        DrawHotel(hotel, hotelElevator, false);
                        hs.Refresh();
                    }
                }
                if (person.Direction == Direction.DOWN)
                {
                    point = new Point(person.Position.X, person.Position.Y - standardRoomHeight);
                    while (person.Position.Y > point.Y)
                    {
                        person.Position.Y -= 10;
                        xStartPosition = 500;
                        yStartPosition = 735;
                        DrawHotel(hotel, hotelElevator, false);
                        hs.Refresh();
                    }
                }
            }


        }
    }
}

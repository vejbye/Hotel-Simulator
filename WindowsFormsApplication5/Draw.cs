using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using HotelSimulator.Object;
using WindowsFormsApplication5.Properties;
using System.Windows.Forms;

namespace HotelSimulator
{
    public class Draw
    {
        public int xStartPosition = 500;
       public int yStartPosition = 735;
       public int standardRoomWidth = 100;
       public int standardRoomHeight = 50;
        public Image img = Resources.SimulatorBG;
        Graphics gfx;
        public Bitmap DrawHotel(HotelRoom[,] map, Bitmap _hotel, List<Guest> guests, List<Maid> maids)
        {
            //Drawing the hotel on a bitmap
             gfx = Graphics.FromImage(_hotel);
            
            //Background image o the hotel
            if (img != null)
            {
               gfx.DrawImage(img, 0, 0, 2000, 800);
            }

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
                    }

                    //Builds down
                    yStartPosition -= standardRoomHeight;
                }

                //Builds to right and sets start position on the ground again.
                xStartPosition += standardRoomWidth;
                yStartPosition = 735;
            }

            xStartPosition = 500;
            foreach(Guest guest in guests)
            {
                gfx.DrawImage(guest.Image, guest.Position.X, guest.Position.Y, guest.Width, guest.Height);
            }

            foreach(Maid maid in maids)
            {
                gfx.DrawImage(maid.Image, maid.Position.X, maid.Position.Y, maid.Width, maid.Height);
            }

            //Returns the drawn bitmap
            return _hotel;

        }

        public void drawPersons(Hotel hotel, SimObject person, HotelSimulator hs )
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
                        DrawHotel(hotel.Map, hotel._hotel, hotel.Guests, hotel.maids);
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
                        DrawHotel(hotel.Map, hotel._hotel, hotel.Guests, hotel.maids);
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
                        DrawHotel(hotel.Map, hotel._hotel, hotel.Guests, hotel.maids);
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
                        DrawHotel(hotel.Map, hotel._hotel, hotel.Guests, hotel.maids);
                        hs.Refresh();
                    }
                }
            }


        }
    }
}

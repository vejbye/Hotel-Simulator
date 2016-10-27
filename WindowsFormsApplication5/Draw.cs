﻿using System;
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
        public Graphics gfx;
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

            currentXPos = xStartPosition;
            currentYPos = yStartPosition;

            //Background image of the hotel
            gfx.DrawImage(img, (xStartPosition - xStartPosition), (yStartPosition - yStartPosition), hotel._hotel.Width, hotel._hotel.Height);

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
                                gfx.DrawImage(hotel.Map[x, y].Image, hotelElevator.ElevatorPosition.X, hotelElevator.ElevatorPosition.Y, hotelElevator.Width, hotelElevator.Height);
                            else
                            {
                                gfx.DrawImage(hotel.Map[x, y].Image, currentXPos, currentYPos - hotel.Map[x, y].Height, hotel.Map[x, y].Width, hotel.Map[x, y].Height);

                                if (newHotel)
                                    DrawBoundingBoxes(hotel.Map, x, y);
                            }
                        }
                        if(hotel.Map[x,y].RoomPosition == new Point(0,0))
                        {
                            hotel.Map[x, y].RoomPosition = new Point(currentXPos, currentYPos - hotel.Map[x, y].Height);
                        }
                    }

                    //Builds down
                    currentYPos -= standardRoomHeight;
                }

                //Builds to right and sets start position on the ground again.
                currentXPos += standardRoomWidth;
                currentYPos = yStartPosition;
            }

            currentXPos = 500;
            foreach (Guest guest in hotel.Guests)
            {
                gfx.DrawImage(guest.Image, guest.Position.X, guest.Position.Y, guest.Width, guest.Height);
            }

            foreach (Maid maid in hotel.Maids)
            {
                gfx.DrawImage(maid.Image, maid.Position.X, maid.Position.Y, maid.Width, maid.Height);
            }

            return hotel._hotel;
        }
        public Bitmap MoveElevator(Hotel hotel, Elevator hotelElevator, bool moveUp)
        {
            if(moveUp)
                hotelElevator.ElevatorPosition.Y = hotelElevator.ElevatorPosition.Y - 20;
            if(!moveUp)
                hotelElevator.ElevatorPosition.Y = hotelElevator.ElevatorPosition.Y + 20;

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
                        person.Position.X += 10;
                      //  DrawHotel(hotel, hotelElevator, false);
                        hs.Refresh();
                        //Application.DoEvents(); p
                   
                }
                if (person.Direction == Direction.UP)
                {
                        person.Position.Y += 10;
                      //  DrawHotel(hotel, hotelElevator, false);
                        hs.Refresh();
                    
                }
                else if (person.Direction == Direction.LEFT)
                {
                        person.Position.X -= 10;
                       // DrawHotel(hotel, hotelElevator, false);
                        hs.Refresh();
                }
                if (person.Direction == Direction.DOWN)
                {
                        person.Position.Y -= 10;
                       // DrawHotel(hotel, hotelElevator, false);
                        hs.Refresh();
                }
            }


        }
    }
}

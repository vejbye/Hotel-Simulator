﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication5.Properties;
using System.Linq;

namespace HotelSimulator.Object
{
    class Hotel
    {
        List<int> hotelWidth;
        List<int> hotelHeight;
        
        List<SimObject> objects; 
        Space[,] map = new Space[10, 10];

        public Bitmap Build(List<LayoutFormat> layout)
        {
            Bitmap hotel = new Bitmap(2000,1000);
            objects = new List<SimObject>();

            //Creates a space for objects to be placed in
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] = new Space();
                }
            }

            //Gets every kind of room there is and places them in the spaces
            foreach (LayoutFormat l in layout)
            {
                string[] positions = l.Position.Split(',');
                string[] dimensions = l.Dimension.Split(',');

                switch (l.AreaType)
                {
                    case "Room":
                        {
                            Room current = new Room();
                            current.width = int.Parse(dimensions[0]);
                            current.height = int.Parse(dimensions[1]);
                            objects.Add(current);
                            map[int.Parse(positions[0]), int.Parse(positions[1])].currentObject = current;
                            break;
                        }

                    case "Cinema":
                        {
                            Cinema current = new Cinema();
                            current.width = int.Parse(dimensions[0]);
                            current.height = int.Parse(dimensions[1]);
                            objects.Add(current);
                            map[int.Parse(positions[0]), int.Parse(positions[1])].currentObject = current;
                            break;
                        }

                    case "Restaurant":
                        {
                            Restaurant current = new Restaurant();
                            current.width = int.Parse(dimensions[0]);
                            current.height = int.Parse(dimensions[1]);
                            objects.Add(current);
                            map[int.Parse(positions[0]), int.Parse(positions[1])].currentObject = current;
                            break;
                        }

                    case "Fitness":
                        {
                            Gym current = new Gym();
                            current.width = int.Parse(dimensions[0]);
                            current.height = int.Parse(dimensions[1]);
                            objects.Add(current);
                            map[int.Parse(positions[0]), int.Parse(positions[1])].currentObject = current;
                            break;
                        }

                }
            }


            //Drawing the hotel on a bitmap
            Graphics gfx = Graphics.FromImage(hotel);

            int xStartPosition = 1;
            int yStartPosition = 1;
            int standardRoomWidth = 100;
            int standardRoomHeight = 50;

            gfx.DrawImage(Resources.SimulatorBG, 1 , 1 , 2000, 800);

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y].currentObject != null)
                    {
                        if (map[x, y].currentObject is Reception)
                        {
                            gfx.DrawImage(map[x, y].currentObject.image, xStartPosition, yStartPosition, (standardRoomWidth * map[x, y].currentObject.width), (standardRoomHeight * map[x, y].currentObject.height));
                        }
                        if (map[x, y].currentObject is Restaurant)
                        {
                            gfx.DrawImage(map[x, y].currentObject.image, xStartPosition, yStartPosition, (standardRoomWidth * map[x, y].currentObject.width), (standardRoomHeight * map[x, y].currentObject.height));
                        }
                        if (map[x, y].currentObject is Gym)
                        {
                            gfx.DrawImage(map[x, y].currentObject.image, xStartPosition, yStartPosition, (standardRoomWidth * map[x, y].currentObject.width), (standardRoomHeight * map[x, y].currentObject.height));
                        }
                        if (map[x, y].currentObject is Cinema)
                        {
                            gfx.DrawImage(map[x, y].currentObject.image, xStartPosition, yStartPosition, (standardRoomWidth * map[x, y].currentObject.width), (standardRoomHeight * map[x, y].currentObject.height));
                        }
                        if (map[x, y].currentObject is ElevatorShaft)
                        {
                            gfx.DrawImage(map[x, y].currentObject.image, xStartPosition, yStartPosition, (standardRoomWidth * map[x, y].currentObject.width), (standardRoomHeight * map[x, y].currentObject.height));
                        }
                        if (map[x, y].currentObject is Room)
                        {
                            gfx.DrawImage(map[x, y].currentObject.image, xStartPosition, yStartPosition, (standardRoomWidth * map[x, y].currentObject.width), (standardRoomHeight * map[x, y].currentObject.height));
                        }


                    }

                    //Builds up
                    yStartPosition += standardRoomHeight;
                }

                //Builds to right and sets start position on the ground again.
                xStartPosition += standardRoomWidth;
                yStartPosition = 1;
            }

            //Returns the drawn bitmap
            return hotel;
            
        }

        public Space[,] getMap()
        {
            return map;
        }

    }
}

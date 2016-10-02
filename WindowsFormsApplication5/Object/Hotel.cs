using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication5.Properties;
using System.Linq;

namespace HotelSimulator.Object
{
    class Hotel
    {
        private List<int> hotelWidthList;
        private List<int> hotelHeightList;
        private int hotelWidth;
        private int hotelHeight;

        private Space[,] map;  

        public Bitmap Build(List<LayoutFormat> layout)
        {
            Bitmap Hotel = new Bitmap(2000, 1000);

            hotelHeightList = new List<int>();
            hotelWidthList = new List<int>();

            //Looks at the width and height of the hotel
            foreach (LayoutFormat l in layout)
            {
                string[] positions = l.Position.Split(',');

                hotelWidthList.Add(int.Parse(positions[0]));
                hotelHeightList.Add(int.Parse(positions[1]));

                for (int i = 0; i < hotelWidthList.Count; i++)
                {
                    if (hotelWidthList[i] > hotelWidth)
                        hotelWidth = hotelWidthList[i];
                }

                for (int i = 0; i < hotelHeightList.Count; i++)
                {
                    if (hotelHeightList[i] > hotelHeight)
                        hotelHeight = hotelHeightList[i];
                }
            }

            //Creates double array based on the width and height and adds 1 to width for elevator and stairs
            map = new Space[hotelWidth + 2, hotelHeight + 1];

            //Creates a space for objects to be placed in
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] = new Space();
                }
            }

            //Looks for every room in the layout file and gives it a position in the hotel
            foreach (LayoutFormat l in layout)
            {
                string[] positions = l.Position.Split(',');
                string[] dimensions = l.Dimension.Split(',');

                switch (l.AreaType)
                {
                    case "Room":
                        {
                            Room current = new Room();
                            current.Width = int.Parse(dimensions[0]);
                            current.Height = int.Parse(dimensions[1]);
                            map[int.Parse(positions[0]), int.Parse(positions[1])].currentObject = current;
                            break;
                        }

                    case "Cinema":
                        {
                            Cinema current = new Cinema();
                            current.Width = int.Parse(dimensions[0]);
                            current.Height = int.Parse(dimensions[1]);
                            map[int.Parse(positions[0]), int.Parse(positions[1])].currentObject = current;
                            break;
                        }

                    case "Restaurant":
                        {
                            Restaurant current = new Restaurant();
                            current.Width = int.Parse(dimensions[0]);
                            current.Height = int.Parse(dimensions[1]);
                            map[int.Parse(positions[0]), int.Parse(positions[1])].currentObject = current;
                            break;
                        }

                    case "Fitness":
                        {
                            Gym current = new Gym();
                            current.Width = int.Parse(dimensions[0]);
                            current.Height = int.Parse(dimensions[1]);
                            map[int.Parse(positions[0]), int.Parse(positions[1])].currentObject = current;
                            break;
                        }

                }
            }
           
            //Drawing the hotel on a bitmap
            Graphics gfx = Graphics.FromImage(Hotel);

            int xStartPosition = 500;
            int yStartPosition = 735;
            int standardRoomWidth = 100;
            int standardRoomHeight = 50;

            //Background image o the hotel
            gfx.DrawImage(Resources.SimulatorBG, 1 , 1 , 2000, 800);

            //Places a reception as big as the hotel without the stairs and elevators
            for (int lobbyStart = 1; lobbyStart <= hotelWidth; lobbyStart++)
            {
                map[lobbyStart, 0].currentObject = new Reception();
            }

            //Adds elevatorshafts to left side of hotel
            for (int infrastructureStart = 0; infrastructureStart <= hotelHeight; infrastructureStart++)
            {
                map[0, infrastructureStart].currentObject = new ElevatorShaft();
                map[hotelWidth + 1, infrastructureStart].currentObject = new Stair();
            }
            
            //Fills space with a room if there is one
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y].currentObject != null && map[x, y].currentObject is SimObject)
                    {
                        gfx.DrawImage(map[x, y].currentObject.Image, xStartPosition, yStartPosition - (standardRoomHeight * map[x, y].currentObject.Height), (standardRoomWidth * map[x, y].currentObject.Width), (standardRoomHeight * map[x, y].currentObject.Height));
                    }

                    
                    //Builds down
                    yStartPosition -= standardRoomHeight;
                }

              
                
                //Builds to right and sets start position on the ground again.
                xStartPosition += standardRoomWidth;
                yStartPosition = 735;
            }

            Guest guest = null;
            foreach (Space space in map)
            {
                if (space.currentObject != null)
                {
                    guest = new Guest(space);
                    guest.Draw(gfx, map, xStartPosition, yStartPosition);
                    guest.Walk();
                }
            }

            AddNeighbours(map);
            
            //Returns the drawn bitmap
            return Hotel;
            
        }

        private void AddNeighbours(Space [,] map)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (y > 0)
                    {
                        //North
                        map[x, y].CreateNeighbours(ref map[x, y - 1], Neighbour.Neighbours.North);
                    }
                    if (x < map.GetLength(0) - 1)
                    {
                        //East
                        map[x, y].CreateNeighbours(ref map[x + 1, y], Neighbour.Neighbours.East);
                    }
                    if (y < map.GetLength(1) - 1)
                    {
                        //South
                        map[x, y].CreateNeighbours(ref map[x, y + 1], Neighbour.Neighbours.South);
                    }
                    if (x > 0)
                    {
                        //West
                        map[x, y].CreateNeighbours(ref map[x - 1, y], Neighbour.Neighbours.West);
                    }

                    if (map[x, y].currentObject != null)
                    {
                        map[x, y].currentObject.Position = map[x, y];
                    }
                }
            }
        }

        public Space[,] getMap()
        {
            return map;
        }

    }
}

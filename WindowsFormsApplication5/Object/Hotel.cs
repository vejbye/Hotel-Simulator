using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication5.Properties;

namespace HotelSimulator.Object
{
    class Hotel
    {
        private List<int> _hotelWidthList;
        private List<int> _hotelHeightList;
        private List<Rectangle> _rectangles;
        private int _hotelWidth;
        private int _hotelHeight;
        private Bitmap _hotel = new Bitmap(2000, 1000);

        public SimObject[,] map;

        public void Build(List<LayoutFormat> layout)
        {
            _hotelHeightList = new List<int>();
            _hotelWidthList = new List<int>();

            //Looks at the width and height of the hotel
            foreach (LayoutFormat l in layout)
            {
                string[] positions = l.Position.Split(',');

                _hotelWidthList.Add(int.Parse(positions[0]));
                _hotelHeightList.Add(int.Parse(positions[1]));

                for (int i = 0; i < _hotelWidthList.Count; i++)
                {
                    if (_hotelWidthList[i] > _hotelWidth)
                        _hotelWidth = _hotelWidthList[i];
                }

                for (int i = 0; i < _hotelHeightList.Count; i++)
                {
                    if (_hotelHeightList[i] > _hotelHeight)
                        _hotelHeight = _hotelHeightList[i];
                }
            }

            //Creates double array based on the width and height and adds 1 to width for elevator and stairs
            map = new SimObject[_hotelWidth + 2, _hotelHeight + 1];

            //Creates a space for objects to be placed in
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] = new SimObject();
                }
            }

            //Looks for every room in the layout file and gives it a position in the hotel
            foreach (LayoutFormat l in layout)
            {
                string[] dimensions = l.Dimension.Split(',');
                string[] positions = l.Position.Split(',');

                switch (l.AreaType)
                {
                    case "Room":
                        {
                            Room current = new Room();
                            current.Width = int.Parse(dimensions[0]);
                            current.Height = int.Parse(dimensions[1]);
                            
                            map[int.Parse(positions[0]), int.Parse(positions[1])] = current;
                            break;
                        }

                    case "Cinema":
                        {
                            Cinema current = new Cinema();
                            current.Width = int.Parse(dimensions[0]);
                            current.Height = int.Parse(dimensions[1]);
                            map[int.Parse(positions[0]), int.Parse(positions[1])] = current;
                            break;
                        }

                    case "Restaurant":
                        {
                            Restaurant current = new Restaurant();
                            current.Width = int.Parse(dimensions[0]);
                            current.Height = int.Parse(dimensions[1]);
                            map[int.Parse(positions[0]), int.Parse(positions[1])] = current;
                            break;
                        }

                    case "Fitness":
                        {
                            Gym current = new Gym();
                            current.Width = int.Parse(dimensions[0]);
                            current.Height = int.Parse(dimensions[1]);
                            map[int.Parse(positions[0]), int.Parse(positions[1])] = current;
                            break;
                        }

                }
            }

        }

        public Bitmap Draw()
        {
            //Drawing the hotel on a bitmap
            Graphics gfx = Graphics.FromImage(_hotel);

            int xStartPosition = 500;
            int yStartPosition = 735;
            int standardRoomWidth = 100;
            int standardRoomHeight = 50;

            //Background image o the hotel
            gfx.DrawImage(Resources.SimulatorBG, 1, 1, 2000, 800);

            //Places a reception as big as the hotel without the stairs and elevators
            for (int lobbyStart = 1; lobbyStart <= _hotelWidth; lobbyStart++)
            {
                map[lobbyStart, 0] = new Reception();
            }

            //Adds elevatorshafts to left side of hotel
            for (int infrastructureStart = 0; infrastructureStart <= _hotelHeight; infrastructureStart++)
            {
                map[0, infrastructureStart] = new ElevatorShaft();
                map[_hotelWidth + 1, infrastructureStart] = new Stair();
            }

            //Fills space with a room if there is one
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y] != null && map[x, y] is SimObject)
                    {
                        gfx.DrawImage(map[x, y].Image, xStartPosition, yStartPosition - (standardRoomHeight * map[x, y].Height), (standardRoomWidth * map[x, y].Width), (standardRoomHeight * map[x, y].Height));
                    }


                    //Builds down
                    yStartPosition -= standardRoomHeight;
                }



                //Builds to right and sets start position on the ground again.
                xStartPosition += standardRoomWidth;
                yStartPosition = 735;
            }

            Guest guest = null;

            foreach (SimObject space in map)
            {
                if (space != null)
                {
                    guest = new Guest();
                    guest.Draw(gfx, map, xStartPosition, yStartPosition);
                    guest.Walk();
                }
            }

            AddNeighbours(map);

            //Returns the drawn bitmap
            return _hotel;

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

        public SimObject[,] getMap()
        {
            return map;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication5.Properties;

namespace HotelSimulator.Object
{
    public class Hotel
    {
        private List<int> _hotelWidthList;
        private List<int> _hotelHeightList;
        private List<Rectangle> _rectangles;
        private int _hotelWidth;
        private int _hotelHeight;
        private Bitmap _hotel = new Bitmap(2000, 1000);

        public HotelRoom[,] map;

        public void Build(List<LayoutFormat> layout)
        {
            _hotelHeightList = new List<int>();
            _hotelWidthList = new List<int>();

            for (int i  = 0; i  < _hotelHeight * _hotelWidth; i ++)
            {
                Rectangle rectangle = new Rectangle();
                _rectangles.Add(rectangle);
            }

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
            map = new HotelRoom[_hotelWidth + 2, _hotelHeight + 1];

            //Creates a space for objects to be placed in
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] = new HotelRoom();
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
                            map[int.Parse(positions[0]), int.Parse(positions[1])]  = current;
                            break;
                        }

                }
            
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
            }

        }

        public Bitmap Draw(HotelRoom[,] map)
        {
            //Drawing the hotel on a bitmap
            Graphics gfx = Graphics.FromImage(_hotel);

            int xStartPosition = 500;
            int yStartPosition = 735;
            int guestxpos = xStartPosition;
            int guestypos = yStartPosition;
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
                        if (map[x, y].Image == null) ;

                        else {
                            gfx.DrawImage(map[x, y].Image, xStartPosition, yStartPosition - (standardRoomHeight * map[x, y].Height), (standardRoomWidth * map[x, y].Width), (standardRoomHeight * map[x, y].Height));
                            if(((HotelRoom)map[x,y]).guest != null)
                            {
                                int dimension = -25;
                                if (((HotelRoom)map[x, y]).Height == 1)
                                {
                                    gfx.DrawImage(((HotelRoom)map[x, y]).guest.Image, x * (((HotelRoom)map[x, y]).Width + 100) + guestxpos, -y * (((HotelRoom)map[x, y]).Height + 50) + guestypos + dimension, ((HotelRoom)map[x, y]).guest.Width, ((HotelRoom)map[x, y]).guest.Height);
                                }
                                else
                                {
                                    gfx.DrawImage(((HotelRoom)map[x, y]).guest.Image, x * (((HotelRoom)map[x, y]).Width + 100) + guestxpos, -y * (((HotelRoom)map[x, y]).Height + 50) + guestypos + dimension + (dimension * ((HotelRoom)map[x, y]).Height), ((HotelRoom)map[x, y]).guest.Width, ((HotelRoom)map[x, y]).guest.Height);
                                }
                            }
                        }
                    }
                    
                    //Builds down
                    yStartPosition -= standardRoomHeight;
                }



                //Builds to right and sets start position on the ground again.
                xStartPosition += standardRoomWidth;
                yStartPosition = 735;
            }


            AddNeighbours(map);

            //Returns the drawn bitmap
            return _hotel;

        }

        public void Action()
        {

            Guest guest = null;

            foreach (HotelRoom space in map)
            {
                if (space == map[0,0])
                {
                    guest = new Guest(space);
                    //guest.Draw(gfx, map, xStartPosition, yStartPosition);
                    //guest.Walk(this);
                }
            }

        }
    

        private void AddNeighbours(HotelRoom [,] map)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (y > 0)
                    {
                        //North
                        map[x, y].CreateNeighbours(ref map[x, y - 1], Neighbours.North);
                    }
                    if (x < map.GetLength(0) - 1)
                    {
                        //East
                        map[x, y].CreateNeighbours(ref map[x + 1, y], Neighbours.East);
                    }
                    if (y < map.GetLength(1) - 1)
                    {
                        //South
                        map[x, y].CreateNeighbours(ref map[x, y + 1], Neighbours.South);
                    }
                    if (x > 0)
                    {
                        //West
                        map[x, y].CreateNeighbours(ref map[x - 1, y], Neighbours.West);
                    }

                    if (map[x, y] != null)
                    {
                        map[x, y].CurrentRoom = map[x, y];
                    }
                }
            }
        }

        public HotelRoom[,] getMap()
        {
            return map;
        }

    }
}

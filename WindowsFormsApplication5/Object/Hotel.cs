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
        public Bitmap _hotel;
        public HotelRoom[,] Map;
        public Elevator Elevator;
        public Draw DrawMe;

        private List<int> _hotelWidthList;
        private List<int> _hotelHeightList;
        public List<Guest> Guests;
        public List<Maid> Maids;

        private const int BITMAPHEIGHT = 800;
        private const int BITMAPWIDTH = 2000;

        private int amountOfInfrastructure = 3;
        private int _lastArrayDimension = 0;
        public int HotelWidth;
        public int HotelHeight;

        public bool Added = false;
        private bool _layoutStartsAt0 = false;

        public Hotel()
        {
            Guests = new List<Guest>();
            Maids = new List<Maid>();
        }

        /// <summary>
        /// Builds up the hotel as written in the layout file.
        /// </summary>
        /// <param name="layout">List of every room in the hotel</param>
        public void Build(List<LayoutFormat> layout)
        {
            _hotelHeightList = new List<int>();
            _hotelWidthList = new List<int>();
            _hotel = new Bitmap(BITMAPWIDTH, BITMAPHEIGHT);
            DrawMe = new Draw();
            Elevator = new Elevator();

            //Looks at the width and height of the hotel
            foreach (LayoutFormat l in layout)
            {
                string[] positions = l.Position.Split(',');
                string[] dimensions = l.Dimension.Split(',');

                _hotelWidthList.Add(int.Parse(positions[0]));
                _hotelHeightList.Add(int.Parse(positions[1]));

                for (int i = 0; i < _hotelWidthList.Count; i++)
                {
                    if (_hotelWidthList[i] > HotelWidth)
                    {
                        HotelWidth = _hotelWidthList[i];
                        _lastArrayDimension = int.Parse(dimensions[0]);
                    }
                }

                for (int i = 0; i < _hotelHeightList.Count; i++)
                {
                    if (_hotelHeightList[i] > HotelHeight)
                        HotelHeight = _hotelHeightList[i];
                }

                if (_hotelWidthList.Contains(0))
                {
                    _layoutStartsAt0 = true;
                }
            }

            //Creates double array based on the width and height
            //Adds height: 1 for reception
            Map = new HotelRoom[HotelWidth + amountOfInfrastructure + _lastArrayDimension, HotelHeight + 1];

            //Creates a space for objects to be placed in
            for (int x = 0; x < Map.GetLength(0); x++)
            {
                for (int y = 0; y < Map.GetLength(1); y++)
                    Map[x, y] = new HotelRoom();
            }

            //Looks for every room in the layout file and gives it a position in the hotel
            foreach (LayoutFormat l in layout)
            {
                string[] dimensions = l.Dimension.Split(',');
                string[] positions = l.Position.Split(',');
                int stairXpos = HotelWidth + _lastArrayDimension;
                int xPos = int.Parse(positions[0]);

                int roomImg = 1;

                //If the layout has a room that starts at x = 0 then add them in the next array.
                if (_layoutStartsAt0 == true)
                {
                    xPos++;
                    stairXpos++;
                    _lastArrayDimension++;
                }

                //Looks for every type of room in the layout and assigns the information accordingly.
                switch (l.AreaType)
                {
                    case "Room":
                        {
                            Room current = new Room();
                            current.Width = current.Width * int.Parse(dimensions[0]);
                            current.Height = current.Height * int.Parse(dimensions[1]);
                            current.Id = l.ID;
                            current.Classification = int.Parse(l.Classification.Substring(0, 1));
                            current.Dimensions = String.Format("{0} x {1}", int.Parse(dimensions[0]), int.Parse(dimensions[1]));
                            current.Floor = int.Parse(positions[1]);
                            Map[xPos, int.Parse(positions[1])] = current;
                            roomImg = int.Parse(dimensions[0]) * int.Parse(dimensions[1]);

                            switch (roomImg)
                            {
                                case 1:
                                    {
                                        current.Image = Resources.Room;
                                        break;
                                    }
                                case 2:
                                    {
                                        current.Image = Resources.Room3;
                                        break;
                                    }
                                case 4:
                                    {
                                        current.Image = Resources.Room5;
                                        break;
                                    }
                                default:
                                    {
                                        Console.WriteLine("There is no image of this room.");
                                        break;
                                    }
                            }

                            break;
                        }

                    case "Cinema":
                        {
                            Cinema current = new Cinema();
                            current.Width = current.Width * int.Parse(dimensions[0]);
                            current.Height = current.Height * int.Parse(dimensions[1]);
                            current.Id = l.ID;
                            current.Dimensions = String.Format("{0} x {1}", int.Parse(dimensions[0]), int.Parse(dimensions[1]));
                            current.Floor = int.Parse(positions[1]);
                            Map[xPos, int.Parse(positions[1])] = current;
                            break;
                        }

                    case "Restaurant":
                        {
                            Restaurant current = new Restaurant();
                            current.Width = current.Width * int.Parse(dimensions[0]);
                            current.Height = current.Height * int.Parse(dimensions[1]);
                            current.Id = l.ID;
                            current.Dimensions = String.Format("{0} x {1}", int.Parse(dimensions[0]), int.Parse(dimensions[1]));
                            current.Floor = int.Parse(positions[1]);
                            current.Capacity = l.Capacity;
                            Map[xPos, int.Parse(positions[1])] = current;
                            break;
                        }

                    case "Fitness":
                        {
                            Gym current = new Gym();
                            current.Width = current.Width * int.Parse(dimensions[0]);
                            current.Height = current.Height * int.Parse(dimensions[1]);
                            current.Id = l.ID;
                            current.Dimensions = String.Format("{0} x {1}", int.Parse(dimensions[0]), int.Parse(dimensions[1]));
                            current.Floor = int.Parse(positions[1]);
                            Map[xPos, int.Parse(positions[1])] = current;
                            break;
                        }
                    case "Pool":
                        {
                            Pool current = new Pool();
                            current.Width = current.Width * int.Parse(dimensions[0]);
                            current.Height = current.Height * int.Parse(dimensions[1]);
                            current.Id = l.ID;
                            current.Dimensions = String.Format("{0} x {1}", int.Parse(dimensions[0]), int.Parse(dimensions[1]));
                            current.Floor = int.Parse(positions[1]);
                            Map[xPos, int.Parse(positions[1])] = current;
                            break;
                        }

                }

                if (!Added)
                {
                    for (int lobbyStart = 1; lobbyStart <= HotelWidth + _lastArrayDimension; lobbyStart++)
                        Map[lobbyStart, 0] = new Reception();

                    //Adds elevatorshafts to left side of hotel, and stairs to the right side of the hotel.
                    for (int infrastructureStart = 0; infrastructureStart <= HotelHeight; infrastructureStart++)
                    {
                        Map[0, infrastructureStart] = new ElevatorShaft();
                        Map[0, infrastructureStart].Floor = infrastructureStart;

                        Map[stairXpos, infrastructureStart] = new Stair();
                        Map[stairXpos, infrastructureStart].Floor = infrastructureStart;
                    }

                    //Places the elevator in the empty space of the array since I need it there >:(
                    Map[Map.GetLength(0) - 1, 0] = Elevator;

                    Added = true;
                }
            }


            AddNeighbours(Map);
        }

        public void Action()
        {
            Maid maid1 = new Maid(Map[0, 0]);
            Maid maid2 = new Maid(Map[0, 0]);
            maid1.Position = new Point(DrawMe.xStartPosition + maid1.Width, DrawMe.yStartPosition - maid1.Height);
            maid2.Position = new Point(DrawMe.xStartPosition + maid2.Width, DrawMe.yStartPosition - maid2.Height);

            Maids.Add(maid1);
            Maids.Add(maid2);
        }

        //Adds neighbours for every direction possible in the layout.
        private void AddNeighbours(HotelRoom[,] map)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (y > 0 && (map[x, y] is ElevatorShaft || map[x, y] is Stair))
                        map[x, y].CreateNeighbours(ref map[x, y - 1], Neighbours.North);

                    if (y < map.GetLength(1) - 1 && (map[x, y] is ElevatorShaft || map[x, y] is Stair))
                        map[x, y].CreateNeighbours(ref map[x, y + 1], Neighbours.South);

                    if (x < map.GetLength(0) - 1)
                        map[x, y].CreateNeighbours(ref map[x + 1, y], Neighbours.East);

                    if (x > 0)
                        map[x, y].CreateNeighbours(ref map[x - 1, y], Neighbours.West);

                    if (map[x, y] != null)
                        map[x, y].CurrentRoom = map[x, y];

                }
            }
        }

        public HotelRoom[,] GetMap()
        {
            return Map;
        }

        //Resets every list and values
        public void Reset()
        {
            _hotelWidthList.Clear();
            _hotelHeightList.Clear();
            Guests.Clear();
            Maids.Clear();
            HotelWidth = 0;
            HotelHeight = 0;
            _lastArrayDimension = 0;
            Added = false;
            _layoutStartsAt0 = false;
        }

    }
}

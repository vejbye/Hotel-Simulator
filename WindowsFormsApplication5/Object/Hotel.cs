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
        private static Hotel hotel = new Hotel();
        public Bitmap HotelBitmap;
        public HotelRoom[,] Map;
        public Elevator Elevator;
        public Draw DrawMe;
        private HotelRoomFactory _hrFactory;

        private List<int> _hotelRoomWidths;
        private List<int> _hotelRoomHeights;
        public List<Guest> Guests;
        public List<Maid> Maids;

        private const int BITMAPHEIGHT = 800;
        private const int BITMAPWIDTH = 2000;

        private int _amountOfInfrastructure = 3;
        private int _lastArrayDimension = 0;
        private int _hotelWidth;
        private int _hotelHeight;

        public bool Added = false;
        private bool _layoutStartsAt0 = false;  

        private Hotel()
        {
            Guests = new List<Guest>();
            Maids = new List<Maid>();
            _hrFactory = new HotelRoomFactory();
        }

        public static Hotel getHotel()
        {
            return hotel;
        }

        /// <summary>
        /// Builds up the hotel as written in the layout file.
        /// </summary>
        /// <param name="layout">List of every room in the hotel</param>
        public void Build(List<LayoutFormat> layout)
        {
            _hotelRoomHeights = new List<int>();
            _hotelRoomWidths = new List<int>();
            HotelBitmap = new Bitmap(BITMAPWIDTH, BITMAPHEIGHT);
            DrawMe = new Draw();
            Elevator = new Elevator();

            //Looks at the width and height of the hotel
            foreach (LayoutFormat l in layout)
            {
                string[] positions = l.Position.Split(',');
                string[] dimensions = l.Dimension.Split(',');

                _hotelRoomWidths.Add(int.Parse(positions[0]));
                _hotelRoomHeights.Add(int.Parse(positions[1]));

                for (int i = 0; i < _hotelRoomWidths.Count; i++)
                {
                    if (_hotelRoomWidths[i] > _hotelWidth)
                    {
                        _hotelWidth = _hotelRoomWidths[i];
                        _lastArrayDimension = int.Parse(dimensions[0]);
                    }
                }

                for (int i = 0; i < _hotelRoomHeights.Count; i++)
                {
                    if (_hotelRoomHeights[i] > _hotelHeight)
                        _hotelHeight = _hotelRoomHeights[i];
                }

                if (_hotelRoomWidths.Contains(0))
                {
                    _layoutStartsAt0 = true;
                }
            }

            if (_lastArrayDimension == 1)
                Map = new HotelRoom[_hotelWidth + _amountOfInfrastructure, _hotelHeight + 1];
            else
                //Creates double array based on the width and height
                //Adds height: 1 for reception
                Map = new HotelRoom[_hotelWidth + _amountOfInfrastructure + _lastArrayDimension, _hotelHeight + 1];

            
            //Looks for every room in the layout file and gives it a position in the hotel
            foreach (LayoutFormat l in layout)
            {
                HotelRoom currentRoom;
                string[] dimensions = l.Dimension.Split(',');
                string[] positions = l.Position.Split(',');
                int stairXpos = _hotelWidth + _lastArrayDimension;
                int xPos = int.Parse(positions[0]);
                
                //If the layout has a room that starts at x = 0 then add them in the next array.
                if (_layoutStartsAt0 == true)
                {
                    xPos++;
                    stairXpos++;
                    _lastArrayDimension++;
                }

                //Creates the corresponding room and makes a new object of it.
                currentRoom = _hrFactory.CreateHotelRoom(l.AreaType, dimensions, l.Classification, l.Capacity);

                //Assigns all the basic information to the hotelroom.
                currentRoom.Width = currentRoom.Width * int.Parse(dimensions[0]);
                currentRoom.Height = currentRoom.Height * int.Parse(dimensions[1]);
                currentRoom.Id = l.ID;
                currentRoom.Dimensions = String.Format("{0} x {1}", int.Parse(dimensions[0]), int.Parse(dimensions[1]));
                currentRoom.Floor = int.Parse(positions[1]);
               
                //Assigns the position of the room in the hotel.
                Map[xPos, int.Parse(positions[1])] = currentRoom;

                if (!Added)
                {
                    for (int lobbyStart = 1; lobbyStart <= _hotelWidth + _lastArrayDimension; lobbyStart++)
                    {
                        Map[lobbyStart, 0] = new Reception();
                        Map[lobbyStart, 0].Dimensions = String.Format("{0} x {1}", _hotelWidth + _lastArrayDimension - 1, 1);
                    }
                    //Adds elevatorshafts to left side of hotel, and stairs to the right side of the hotel.
                    for (int infrastructureStart = 0; infrastructureStart <= _hotelHeight; infrastructureStart++)
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

            //Checks for empty rooms in the hotel and creates a node for the guests to walk on.
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] == null)
                        Map[i, j] = new Node();
                }
            }

            AddNeighbours(Map);
        }
        
        /// <summary>
        /// Adds maids to the hotel
        /// </summary>
        public void AddMaids(int cleaningHTE)
        {
            Maid maid1 = new Maid(Map[0, 0]);
            Maid maid2 = new Maid(Map[0, 0]);
            maid1.Position = new Point(DrawMe.XStartPosition + maid1.Width, DrawMe.YStartPosition - maid1.Height);
            maid2.Position = new Point(DrawMe.XStartPosition + maid2.Width, DrawMe.YStartPosition - maid2.Height);
            maid1.CleaningHTE = cleaningHTE;
            maid2.CleaningHTE = cleaningHTE;

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
            _hotelRoomWidths.Clear();
            _hotelRoomHeights.Clear();
            Guests.Clear();
            Maids.Clear();
            _hotelWidth = 0;
            _hotelHeight = 0;
            _lastArrayDimension = 0;
            Added = false;
            _layoutStartsAt0 = false;
        }

    }
}

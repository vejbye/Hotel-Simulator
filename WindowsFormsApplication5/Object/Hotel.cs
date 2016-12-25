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
        private static Hotel _hotel = new Hotel(); //Singleton hotel
        public Bitmap HotelBitmap; //This is where the hotel will be drawed on
        public HotelRoom[,] Map; //The double array to give positions to every room
        public Elevator Elevator; //A hotel naturally has an elevator!
        public Draw DrawMe; //To get standard information from like standard room height
        private HotelRoomFactory _hrFactory; //A factory to create all kinds of rooms

        public List<Guest> Guests { get; set; } //The guests that resides in the hotel
        public List<Maid> Maids { get; set; }// The maids that are working in the hotel
        private List<int> _hotelRoomWidths; // A list of all the widths of every room, used to calculate the width of the hotel
        private List<int> _hotelRoomHeights;// A list of all the heights of every room, used to calculate the height of the hotel

        private const int BITMAPHEIGHT = 800; 
        private const int BITMAPWIDTH = 2000;

        private int _amountOfInfrastructure = 3; //This will be added to the double array in map. 1 for the shaft, 1 for the stairs, and 1 for the elevator
        private int _lastArrayDimension = 0; //The dimension of the most right room. This is to know if we have to make the hotel bigger or not because otherwise the room would stick out.
        private int _hotelWidth; //Width of the hotel
        private int _hotelHeight;// Height of the hotel

        public bool Added = false; //Checks if infrastructure is already added or not
        private bool _layoutStartsAt0;  //Checks if the layout files starts at 0

        private Hotel()
        {
            Guests = new List<Guest>();
            Maids = new List<Maid>();
            _hrFactory = new HotelRoomFactory();
            _layoutStartsAt0 = false;
        }

        public static Hotel GetHotel()
        {
            return _hotel;
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
                //Splits dimensions and positions and make the numbers useable
                string[] positions = l.Position.Split(',');
                string[] dimensions = l.Dimension.Split(',');

                _hotelRoomWidths.Add(int.Parse(positions[0]));
                _hotelRoomHeights.Add(int.Parse(positions[1]));

                //Checks for the widest room
                for (int i = 0; i < _hotelRoomWidths.Count; i++)
                {
                    if (_hotelRoomWidths[i] > _hotelWidth)
                    {
                        _hotelWidth = _hotelRoomWidths[i];
                        _lastArrayDimension = int.Parse(dimensions[0]);
                    }
                }

                //Checks for the highest room
                for (int i = 0; i < _hotelRoomHeights.Count; i++)
                {
                    if (_hotelRoomHeights[i] > _hotelHeight)
                        _hotelHeight = _hotelRoomHeights[i];
                }

                //Checks if layout starts at 0
                if (_hotelRoomWidths.Contains(0))
                {
                    _layoutStartsAt0 = true;
                }
            }

            //If the last room has a width of 1, you can add the infrastructure normally to the hotel
            if (_lastArrayDimension == 1)
                Map = new HotelRoom[_hotelWidth + _amountOfInfrastructure, _hotelHeight + 1];
            //Else add more space in the hotel for the last room
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
        /// Add maids to the hotel.
        /// </summary>
        /// <param name="cleaningHTE">How fast the maids need to work.</param>
        public void AddMaids(int cleaningHTE)
        {
            //Adds 2 maids, giving them a position, and how fast they work
            Maid maid1 = new Maid(Map[0, 0]);
            Maid maid2 = new Maid(Map[0, 0]);
            maid1.Position = new Point(DrawMe.XStartPosition + maid1.Width, DrawMe.YStartPosition - maid1.Height);
            maid2.Position = new Point(DrawMe.XStartPosition + maid2.Width, DrawMe.YStartPosition - maid2.Height);
            maid1.CleaningHTE = cleaningHTE;
            maid2.CleaningHTE = cleaningHTE;

            Maids.Add(maid1);
            Maids.Add(maid2);
        }

        /// <summary>
        /// Adds neighbours for every single direction there is.
        /// </summary>
        /// <param name="map">Give the room for neighbours to assign.</param>
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

        /// <summary>
        /// Resets every list and value to their original state.
        /// </summary>
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

        public bool LayoutAtZero()
        {
            return _layoutStartsAt0;
        }

    }
}

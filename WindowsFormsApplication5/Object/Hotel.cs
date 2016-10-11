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
        private int _hotelWidth;
        private int _hotelHeight;

        private Bitmap _hotel = new Bitmap(2000, 1000);
        public HotelRoom[,] Map;
        public BoundaryBox BoundaryBox;

        public void Build(List<LayoutFormat> layout)
        {
            _hotelHeightList = new List<int>();
            _hotelWidthList = new List<int>();
            BoundaryBox = new BoundaryBox();
                
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
            Map = new HotelRoom[_hotelWidth + 2, _hotelHeight + 1];

            //Creates a space for objects to be placed in
            for (int x = 0; x < Map.GetLength(0); x++)
            {
                for (int y = 0; y < Map.GetLength(1); y++)
                    Map[x, y] = new HotelRoom();
            }

            AddNeighbours(Map);
            AddBoxes(Map);

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
                            current.Id = l.ID;
                            Map[int.Parse(positions[0]), int.Parse(positions[1])] = current;
                            break;
                        }

                    case "Cinema":
                        {
                            Cinema current = new Cinema();
                            current.Width = int.Parse(dimensions[0]);
                            current.Height = int.Parse(dimensions[1]);
                            current.Id = l.ID;
                            Map[int.Parse(positions[0]), int.Parse(positions[1])] = current;
                            break;
                        }

                    case "Restaurant":
                        {
                            Restaurant current = new Restaurant();
                            current.Width = int.Parse(dimensions[0]);
                            current.Height = int.Parse(dimensions[1]);
                            current.Id = l.ID;
                            Map[int.Parse(positions[0]), int.Parse(positions[1])] = current;
                            break;
                        }

                    case "Fitness":
                        {
                            Gym current = new Gym();
                            current.Width = int.Parse(dimensions[0]);
                            current.Height = int.Parse(dimensions[1]);
                            current.Id = l.ID;
                            Map[int.Parse(positions[0]), int.Parse(positions[1])]  = current;
                            break;
                        }

                }
            
                //Places a reception as big as the hotel without the stairs and elevators
                for (int lobbyStart = 1; lobbyStart <= _hotelWidth; lobbyStart++)
                    Map[lobbyStart, 0] = new Reception();
                

                //Adds elevatorshafts to left side of hotel
                for (int infrastructureStart = 0; infrastructureStart <= _hotelHeight; infrastructureStart++)
                {
                    Map[0, infrastructureStart] = new ElevatorShaft();
                    Map[_hotelWidth + 1, infrastructureStart] = new Stair();
                }
            }
            AddNeighbours(Map);

        }

        public Bitmap Draw(HotelRoom[,] map)
        {
            //Drawing the hotel on a bitmap
            Graphics gfx = Graphics.FromImage(_hotel);

            int xStartPosition = 500;
            int yStartPosition = 735;
            int standardRoomWidth = 100;
            int standardRoomHeight = 50;
            int dimension = 10;

            //Background image o the hotel
            gfx.DrawImage(Resources.SimulatorBG, 1, 1, 2000, 800);

            //Fills space with a room if there is one
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y] != null && map[x, y] is SimObject)
                    {
                        if (map[x, y].Image != null)
                            gfx.DrawImage(map[x, y].Image, xStartPosition, yStartPosition - (standardRoomHeight * map[x, y].Height), (standardRoomWidth * map[x, y].Width), (standardRoomHeight * map[x, y].Height));
                        foreach (Guest guest in map[x, y].Guests)
                        {
                            gfx.DrawImage(guest.Image, xStartPosition, yStartPosition - (standardRoomHeight * map[x, y].Height) + dimension * map[x,y].Height * map[x, y].Height + map[x, y].Height, (guest.Width), (guest.Height));
                        }
                        foreach(Maid maid in map[x, y].Maids)
                        {
                            gfx.DrawImage(maid.Image, xStartPosition, yStartPosition - (standardRoomHeight * map[x, y].Height) + dimension * map[x, y].Height * map[x, y].Height + map[x, y].Height, (maid.Width), (maid.Height));
                        }
                    }
                    
                    //Builds down
                    yStartPosition -= standardRoomHeight;
                }



                //Builds to right and sets start position on the ground again.
                xStartPosition += standardRoomWidth;
                yStartPosition = 735;
            }


          

            //Returns the drawn bitmap
            return _hotel;

        }

        public Guest Action()
        {

            Guest guest = null;

            foreach (HotelRoom space in Map)
            {
                if (space == Map[0,0])
                {
                    guest = new Guest(space);
                    space.Guests.Add(guest);
                    Draw(Map);                  
                }
                else if (space == Map[2, 2])
                {
                    Maid maid = new Maid(space);
                    space.Maids.Add(maid);
                    Draw(Map);
                }
                else if(space == Map[8, 1])
                {
                    if(space is Room)
                    {
                        ((Room)space).Dirty = true;
                    }
                }
            }
            return guest;

        }


        private void AddNeighbours(HotelRoom[,] map)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (y > 0 && (map[x, y] is ElevatorShaft || map[x, y] is Stair))
                    {
                        //North
                        map[x, y].CreateNeighbours(ref map[x, y - 1], Neighbours.North);
                    
                    if (x < map.GetLength(0) - 1)
                        map[x, y].CreateNeighbours(ref map[x + 1, y], Neighbours.East);
                    }
                    if (y < map.GetLength(1) - 1 && (map[x, y] is ElevatorShaft || map[x, y] is Stair))
                    {
                        //South
                        map[x, y].CreateNeighbours(ref map[x, y + 1], Neighbours.South);
                    }
                        if (x > 0)
                            map[x, y].CreateNeighbours(ref map[x - 1, y], Neighbours.West);


                        if (map[x, y] != null)
                            map[x, y].CurrentRoom = map[x, y];

                    
                }
            }
        }

        public void AddBoxes(HotelRoom[,] map)
        {
            int xStartPosition = 500;
            int yStartPosition = 735;
            int standardRoomWidth = 100;
            int standardRoomHeight = 50;

            for (int x = 0; x< map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    Rectangle r = new Rectangle(xStartPosition, yStartPosition - standardRoomHeight, standardRoomWidth, standardRoomHeight);
                    BoundaryBox.AddBox(r, map, x, y);

                    yStartPosition -= standardRoomHeight;
                }

                xStartPosition += standardRoomWidth;
                yStartPosition = 735;
            }
        }

        public HotelRoom[,] getMap()
        {
            return Map;
        }

    }
}

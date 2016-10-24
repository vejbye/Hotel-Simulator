﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication5;

namespace HotelSimulator.Object
{
    public class Hotel
    {
        private List<int> _hotelWidthList;
        private List<int> _hotelHeightList;

        public int _hotelWidth;
        public int _hotelHeight;
        public bool _added = false;

        public Bitmap _hotel;
        public HotelRoom[,] Map;
        public Elevator Elevator;
        public Draw DrawMe;
        public List<Guest> Guests;
        public List<Maid> maids;
        

        public List<Rectangle> TEST = new List<Rectangle>();

        public Hotel()
        {
            Guests = new List<Guest>();
            maids = new List<Maid>();
        }

        public void Build(List<LayoutFormat> layout)
        {
            _hotelHeightList = new List<int>();
            _hotelWidthList = new List<int>();
            _hotel = new Bitmap(2000, 800);
            DrawMe = new Draw();
            Elevator = new Elevator();
                
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
            Map = new HotelRoom[_hotelWidth + 3, _hotelHeight + 1];

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

                if (!_added)
                {
                    
                    for (int lobbyStart = 1; lobbyStart <= _hotelWidth; lobbyStart++)
                        Map[lobbyStart, 0] = new Reception();

                    //Adds elevatorshafts to left side of hotel
                    for (int infrastructureStart = 0; infrastructureStart <= _hotelHeight; infrastructureStart++)
                    {
                        Map[0, infrastructureStart] = new ElevatorShaft();
                        Map[_hotelWidth + 1, infrastructureStart] = new Stair();
                    }
                    Map[_hotelWidth + 2, 0] = Elevator;
                    _added = true;
                }

                switch (l.AreaType)
                {
                    case "Room":
                        {
                            Room current = new Room();
                            current.Width = current.Width * int.Parse(dimensions[0]);
                            current.Height = current.Height * int.Parse(dimensions[1]);
                            current.Id = l.ID;
                            current.Stars = l.Classification.Substring(0,1);
                            Map[int.Parse(positions[0]), int.Parse(positions[1])] = current;
                            break;
                        }

                    case "Cinema":
                        {
                            Cinema current = new Cinema();
                            current.Width = current.Width * int.Parse(dimensions[0]);
                            current.Height = current.Height * int.Parse(dimensions[1]);
                            current.Id = l.ID;
                            Map[int.Parse(positions[0]), int.Parse(positions[1])] = current;
                            break;
                        }

                    case "Restaurant":
                        {
                            Restaurant current = new Restaurant();
                            current.Width = current.Width * int.Parse(dimensions[0]);
                            current.Height = current.Height * int.Parse(dimensions[1]);
                            current.Id = l.ID;
                            Map[int.Parse(positions[0]), int.Parse(positions[1])] = current;
                            break;
                        }

                    case "Fitness":
                        {
                            Gym current = new Gym();
                            current.Width = current.Width * int.Parse(dimensions[0]);
                            current.Height = current.Height * int.Parse(dimensions[1]);
                            current.Id = l.ID;
                            Map[int.Parse(positions[0]), int.Parse(positions[1])] = current;
                            break;
                        }
            
            }

        }

            AddNeighbours(Map);
                    
                }

        public void Action()
        {
            Maid maid1 = new Maid(Map[0,0]);
            maid1.Position = new System.Drawing.Point(DrawMe.xStartPosition, DrawMe.yStartPosition - DrawMe.standardRoomHeight);
            maids.Add(maid1);
        }
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

    }
}

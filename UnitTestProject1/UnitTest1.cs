using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulator.Object;
using System.Collections.Generic;
using HotelSimulator;
using System.Drawing;

namespace HotelSimulatorUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        Hotel hotel;
        HotelRoom[,] map;
        HotelRoom current;
        List<LayoutFormat> layoutFormatList;
        List<int> hotelWidth;
        List<int> hotelPosition;

        [TestInitialize]
        public void Initialize()
        {
            hotel = new Hotel();
            layoutFormatList = new List<LayoutFormat>();
            map = new HotelRoom[10, 10];
            hotelWidth = new List<int>();
            hotelPosition = new List<int>();
        }

        [TestMethod]
        public void TestGuestDying()
        {
            Guest guest = new Guest();
            guest.WaitTime = 7;
            guest.InLine();
            Assert.AreEqual(guest.Dead, true);
        }
        [TestMethod]
        public void testcheckin()
        {
            Initialize();
            AddNeighbours();
            hotel.Map = map;
            Reception r = new Reception();
            Guest guest = new Guest();
            guest.Preference = "1";           
            guest.Room = r.findEmptyRoom(hotel, guest);
            Assert.IsNotNull(guest.Room);
        }

        [TestMethod]
        public void testpathfind()
        {
            Initialize();
            AddNeighbours();
            hotel.Map = map;
            Guest guest = new Guest();
            guest.Current = hotel.Map[0, 0];
            Point Position = guest.Position;
            guest.setPath(hotel, hotel.Map[9, 0]);
            Assert.AreEqual(10, guest.Path.Count);
        }

        [TestMethod]
        public void TestWalkRight()
        {
            Initialize();
            AddNeighbours();
            hotel.Map = map;
            Guest guest = new Guest();
            guest.Current = hotel.Map[0, 0];
            Point Position = guest.Position;
            guest.setPath(hotel, hotel.Map[9, 0]);
            guest.Walk(hotel);
            Assert.AreEqual(Position.X + 10, guest.Position.X);

        }
        [TestMethod]
        public void TestWalkDown()
        {
            Initialize();
            AddNeighbours();
            hotel.Map = map;
            Guest guest = new Guest();
            guest.Current = hotel.Map[0, 9];
            Point Position = guest.Position;
            guest.setPath(hotel, hotel.Map[0, 0]);
            guest.Walk(hotel);
            Assert.AreEqual(Position.Y + 10, guest.Position.Y);

        }
        [TestMethod]
        public void TestWalkLeft()
        {
            Initialize();
            AddNeighbours();
            hotel.Map = map;
            Guest guest = new Guest();
            guest.Current = hotel.Map[9, 0];
            Point Position = guest.Position;
            guest.setPath(hotel, hotel.Map[0, 0]);
            guest.Walk(hotel);
            Assert.AreEqual(Position.X - 10, guest.Position.X);

        }
        [TestMethod]
        public void TestWalkUp()
        {
            Initialize();
            AddNeighbours();
            hotel.Map = map;
            Guest guest = new Guest();
            guest.Current = hotel.Map[0, 0];
            Point Position = guest.Position;
            guest.setPath(hotel, hotel.Map[0,9]);
            guest.Walk(hotel);
            Assert.AreEqual(Position.Y - 10, guest.Position.Y);

        }

        [TestMethod]
        public void CreateHotel()
        {
            Initialize();
            Assert.IsNotNull(hotel);
        }


        [TestMethod]
        public void CreateHotelBitmap()
        {
            hotel.Build(layoutFormatList);
            Assert.IsNotNull(hotel.HotelBitmap);
        }

        [TestMethod]
        public void Create2DArrayOfRooms()
        {
            map = new HotelRoom[10, 10];

            //Creates a space for objects to be placed in
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                    map[x, y] = new HotelRoom();
            }

            Assert.IsNotNull(map);
        }

        [TestMethod]
        public void SplitString()
        {
            string Dimension = "2, 1";
            string Position = "8, 5";

            string[] positions = Position.Split(',');
            string[] dimensions = Dimension.Split(',');

            hotelWidth.Add(int.Parse(dimensions[0]));
            hotelPosition.Add(int.Parse(positions[0]));
            
            Assert.AreEqual(hotelWidth[0], 2);
            Assert.AreEqual(hotelPosition[0], 8);
        }

        [TestMethod]
        public void CreateRoom()
        {
            bool thisIsARoom = false;
            string RoomType = "Room";

            switch (RoomType)
            {
                case "Room":
                    {
                        current = new Room();
                        break;
                    }

                case "Cinema":
                    {
                        current = new Cinema();
                        break;
                    }

                case "Restaurant":
                    {
                        current = new Restaurant();
                        break;
                    }

                case "Fitness":
                    {
                        current = new Gym();
                        break;
                    }
                case "Pool":
                    {
                        current = new Pool();
                        break;
                    }


            }

            if (current is Room)
                thisIsARoom = true;

            Assert.IsTrue(thisIsARoom);
        }

        [TestMethod]
        public void Reset()
        {
            bool resetted = false;

            hotelWidth.Add(1);
            hotelPosition.Add(2);

            hotelWidth.Clear();
            hotelPosition.Clear();

            if (hotelWidth.Count == 0 && hotelPosition.Count == 0)
                resetted = true;

            Assert.IsTrue(resetted);
        }

        [TestMethod]
        public void AddNeighbours()
        {
            bool neighboursCreated = false;

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                    if (x == 0)
                        map[x, y] = new ElevatorShaft();                   
                     else if(x == 4 && y == 4){
                        map[x, y] = new Room();
                        ((Room)map[x, y]).Classification = 2;
                    }

                    else
                         map[x, y] = new HotelRoom();

            }

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

            if (map[2, 5].Neighbours.Count == 2)
                neighboursCreated = true;

            Assert.IsTrue(neighboursCreated);
        }
    }

    
}

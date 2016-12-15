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
        List<LayoutFormat> layoutFormatList;
        List<int> hotelWidth;
        List<int> hotelPosition;

        [TestInitialize]
        public void Initialize()
        {
            hotel = Hotel.getHotel();
            layoutFormatList = new List<LayoutFormat>();
            map = new HotelRoom[10, 10];
            hotelWidth = new List<int>();
            hotelPosition = new List<int>();
            //Creating a little example hotel to test for adding neighbours
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (x == 0)
                        map[x, y] = new ElevatorShaft();
                    else if (x == 4 && y == 4)
                    {
                        map[x, y] = new Room();
                        ((Room)map[x, y]).Classification = 2;
                    }else if(x == 1 && y == 1)
                    {
                        map[x, y] = new Restaurant();
                    }
                    else if (x == 2 && y == 2)
                    {
                        map[x, y] = new Cinema();
                    }
                    else
                        map[x, y] = new Node();
                }
            }
        }
        [TestMethod]
        public void TestMovieStart()
        {
            ((Cinema)map[2, 2]).Playing = true;
            ((Cinema)map[2, 2]).PlayMovie(0);
            Assert.AreEqual(false, ((Cinema)map[2, 2]).Playing);
        }

        [TestMethod]
        public void TestRestaurantwaitingline()
        {
            Guest guest = new Guest();
            ((Restaurant)map[1, 1]).Capacity = 1;
            ((Restaurant)map[1, 1]).Waitingline.Enqueue(guest);
            ((Restaurant)map[1, 1]).HandleWaitingline();
            Assert.AreEqual(0, ((Restaurant)map[1, 1]).Waitingline.Count);
        }

        [TestMethod]
        public void TestGuestDying()
        {
            Guest guest = new Guest();
            //guest.WaitTime = 7;
            guest.InLine();
            //Assert.AreEqual(guest.Dead, true);
        }
        [TestMethod]
        public void Testcheckin()
        {
            Initialize();
            hotel.Map = map;
            AddNeighbours();
            Reception r = new Reception();
            Guest guest = new Guest();
            guest.Preference = "1";           
            guest.Room = r.FindEmptyRoom(hotel, guest);
            Assert.IsNotNull(guest.Room);
        }

        [TestMethod]
        public void TestGuestpathfind()
        {
            Initialize();
            hotel.Map = map;
            AddNeighbours();
            Guest guest = new Guest();
            guest.Current = hotel.Map[0, 0];
            Point Position = guest.Position;
            guest.setPath(hotel, hotel.Map[9, 0]);
            Assert.AreEqual(10, guest.Path.Count);
        }

        [TestMethod]
        public void TestMaidpathfind()
        {
            Initialize();
            hotel.Map = map;
            AddNeighbours();
            Maid maid = new Maid(hotel.Map[0, 0]);
            maid.CleaningHTE = 1;
            ((Room)hotel.Map[4,4]).Dirty = true;
            Point Position = maid.Position;
            maid.SetPath(hotel);
            Assert.AreEqual(9, maid.Path.Count);
        }

        [TestMethod]
        public void TestWalkRight()
        {
            Initialize();
            hotel.Map = map;
            AddNeighbours();
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
            hotel.Map = map;
            AddNeighbours();
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
            hotel.Map = map;
            AddNeighbours();
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
            hotel.Map = map;
            AddNeighbours();
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
        public void AddNeighbours()
        {
            bool neighboursCreated = false;

            //Adding neighbours to every hotelroom.
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

        [TestMethod]
        public void TestMovingElevator()
        {
            bool elevatorArrived = false;
            hotel.Build(layoutFormatList);
            //Testing if the elevator movement is correct. Given are the hotel the elevator is in, the requested floor (3), and the speed of the elevator. (1)

            while (!elevatorArrived)
            {
                hotel.Elevator.MoveElevator(hotel, 3, 1);

                if (hotel.Elevator.ElevatorPosition.Y == hotel.DrawMe.YStartPosition - (3 * hotel.DrawMe.StandardRoomHeight))
                {
                    elevatorArrived = true;
                }
            }

            

            Assert.IsTrue(elevatorArrived);
        }

        [TestMethod]
        public void TestResetMethod()
        {
            //Only the guestlist is tested since every other list gets resetted exactly the same way.
            bool hotelCleared = false;
            Guest testGuest = new Guest();
            hotel.Guests.Add(testGuest);

            hotel.Reset();

            if (hotel.Guests.Count == 0)
                hotelCleared = true;

            Assert.IsTrue(hotelCleared);
        }

        [TestMethod]
        public void TestAddingMaid()
        {
            hotel.Build(layoutFormatList);
            hotel.AddMaids(1);
            Assert.AreEqual(hotel.Maids.Count, 2);
        }


    }

    
}

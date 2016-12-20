using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulator.Object;
using System.Collections.Generic;
using HotelSimulator;
using System.Drawing;
using WindowsFormsApplication5.Properties;

namespace HotelSimulatorUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        Hotel hotel;
        LayoutReader reader;
        string standardLayout = @"..\..\..\WindowsFormsApplication5\Resources\Hotel3.layout";

        [TestInitialize]
        public void Initialize()
        {
            hotel = Hotel.getHotel();
            reader = new LayoutReader();
            hotel.Build(reader.ReadLayout(standardLayout));
            /*map = new HotelRoom[10, 10];
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
                    }
                    else if (x == 1 && y == 1)
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
            }*/
        }

        [TestMethod]
        public void TestMovieStart()
        {
            ((Cinema)hotel.GetMap()[2, 2]).Playing = true;
            ((Cinema)hotel.GetMap()[2, 2]).PlayMovie(0);
            Assert.AreEqual(false, ((Cinema)hotel.GetMap()[2, 2]).Playing);
        }

        [TestMethod]
        public void TestRestaurantwaitingline()
        {
            Guest guest = new Guest();
            ((Restaurant)hotel.GetMap()[1, 1]).Capacity = 1;
            ((Restaurant)hotel.GetMap()[1, 1]).Waitingline.Enqueue(guest);
            ((Restaurant)hotel.GetMap()[1, 1]).HandleWaitingline();
            Assert.AreEqual(0, ((Restaurant)hotel.GetMap()[1, 1]).Waitingline.Count);
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
            Guest guest = new Guest();
            guest.Current = hotel.GetMap()[0, 0];
            Point Position = guest.Position;
            guest.setPath(hotel, hotel.GetMap()[9, 0]);
            Assert.AreEqual(10, guest.Path.Count);
        }

        [TestMethod]
        public void TestMaidpathfind()
        {
            Initialize();
            Maid maid = new Maid(hotel.GetMap()[0, 0]);
            maid.CleaningHTE = 1;
            ((Room)hotel.GetMap()[2, 1]).Dirty = true;
            Point Position = maid.Position;
            maid.SetPath(hotel);
            Assert.AreEqual(9, maid.Path.Count);
        }

        [TestMethod]
        public void TestMaidWalk()
        {
            Initialize();
            Maid maid = new Maid(hotel.GetMap()[0, 0]);
            maid.CleaningHTE = 1;
            ((Room)hotel.GetMap()[2, 1]).Dirty = true;
            Point Position = maid.Position;
            maid.SetPath(hotel);
            maid.Walk(hotel);
            Assert.AreEqual(Position.Y - 10, maid.Position.Y);
        }

        [TestMethod]
        public void TestWalkRight()
        {
            Initialize();
            Guest guest = new Guest();
            guest.Current = hotel.GetMap()[0, 0];
            Point Position = guest.Position;
            guest.setPath(hotel, hotel.GetMap()[9, 0]);
            guest.Walk(hotel);
            Assert.AreEqual(Position.X + 10, guest.Position.X);

        }
        [TestMethod]
        public void TestWalkDown()
        {
            Initialize();
            Guest guest = new Guest();
            guest.Current = hotel.GetMap()[0, 9];
            Point Position = guest.Position;
            guest.setPath(hotel, hotel.GetMap()[0, 0]);
            guest.Walk(hotel);
            Assert.AreEqual(Position.Y + 10, guest.Position.Y);

        }
        [TestMethod]
        public void TestWalkLeft()
        {
            Initialize();
            Guest guest = new Guest();
            guest.Current = hotel.GetMap()[9, 0];
            Point Position = guest.Position;
            guest.setPath(hotel, hotel.GetMap()[0, 0]);
            guest.Walk(hotel);
            Assert.AreEqual(Position.X - 10, guest.Position.X);

        }
        [TestMethod]
        public void TestWalkUp()
        {
            Initialize();
            Guest guest = new Guest();
            guest.Current = hotel.GetMap()[0, 0];
            Point Position = guest.Position;
            guest.setPath(hotel, hotel.GetMap()[0, 4]);
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
            hotel.Build(reader.ReadLayout(standardLayout));
            Assert.IsNotNull(hotel.HotelBitmap);
        }

        [TestMethod]
        public void AddNeighbours()
        {
            bool neighboursCreated = false;

            //Adding neighbours to every hotelroom.
            if (hotel.GetMap()[2, 5].Neighbours.Count == 2)
                neighboursCreated = true;

            Assert.IsTrue(neighboursCreated);
        }

        [TestMethod]
        public void TestMovingElevator()
        {
            bool elevatorArrived = false;
            hotel.Build(reader.ReadLayout(standardLayout));
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
            hotel.Build(reader.ReadLayout(standardLayout));
            hotel.AddMaids(1);
            Assert.AreEqual(hotel.Maids.Count, 2);
        }

        [TestMethod]
        public void TestBoundingBox()
        {
            bool hasDrawingBoxes = false;

            hotel.DrawMe.DrawHotel(hotel, true);
            for (int i = 0; i < hotel.Map.GetLength(0); i++)
            {
                if (hotel.Map[i, 0].BoundingBox.Height != 0 && hotel.Map[i, 0].BoundingBox.Width != 0)
                    hasDrawingBoxes = true;
            }

            Assert.IsTrue(hasDrawingBoxes);

        }


    }


}

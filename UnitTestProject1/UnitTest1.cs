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
            hotel.DrawMe.DrawHotel(hotel, true);
        }

        [TestMethod]
        public void TestMovieStart()
        {
            ((Cinema)hotel.Map[3, 3]).Playing = true;
            ((Cinema)hotel.Map[3, 3]).PlayMovie(0);
            Assert.AreEqual(false, ((Cinema)hotel.Map[3, 3]).Playing);
        }

        [TestMethod]
        public void TestBoundingBox()
        {
            bool hasDrawingBoxes = false;
            for (int i = 0; i < hotel.Map.GetLength(0); i++)
            {
                if (hotel.Map[i, 0].BoundingBox.Height != 0 && hotel.Map[i, 0].BoundingBox.Width != 0)
                {
                    hasDrawingBoxes = true;
                    break;
                }
            }

            Assert.IsTrue(hasDrawingBoxes);

        }

        [TestMethod]
        public void TestRestaurantwaitingline()
        {
            Guest guest = new Guest();
            ((Restaurant)hotel.Map[1, 1]).Capacity = 1;
            ((Restaurant)hotel.Map[1, 1]).Waitingline.Enqueue(guest);
            ((Restaurant)hotel.Map[1, 1]).HandleWaitingline();
            Assert.AreEqual(0, ((Restaurant)hotel.Map[1, 1]).Waitingline.Count);
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
            Reception r = new Reception();
            Guest guest = new Guest();
            guest.Preference = "1";
            guest.Room = r.FindEmptyRoom(hotel, guest);
            Assert.IsNotNull(guest.Room);
        }

        [TestMethod]
        public void TestGuestpathfind()
        {
            Guest guest = new Guest();
            guest.Current = hotel.Map[0, 0];
            Point Position = guest.Position;
            guest.setPath(hotel, hotel.Map[9, 0]);
            Assert.AreEqual(10, guest.Path.Count);
        }

        [TestMethod]
        public void TestMaidpathfind()
        {
            Maid maid = new Maid(hotel.Map[0, 0]);
            maid.CleaningHTE = 1;
            ((Room)hotel.Map[2, 1]).Dirty = true;
            Point Position = maid.Position;
            maid.SetPath(hotel);
            Assert.AreEqual(9, maid.Path.Count);
        }

        [TestMethod]
        public void TestMaidWalk()
        {
            Maid maid = new Maid(hotel.Map[0, 0]);
            maid.CleaningHTE = 1;
            ((Room)hotel.Map[2, 1]).Dirty = true;
            Point Position = maid.Position;
            maid.SetPath(hotel);
            maid.Walk(hotel);
            Assert.AreEqual(Position.Y - 10, maid.Position.Y);
        }

        [TestMethod]
        public void TestWalkRight()
        {
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
            Guest guest = new Guest();
            guest.Current = hotel.Map[0, 5];
            Point Position = guest.Position;
            guest.setPath(hotel, hotel.Map[0, 0]);
            guest.Walk(hotel);
            Assert.AreEqual(Position.Y + 10, guest.Position.Y);

        }
        [TestMethod]
        public void TestWalkLeft()
        {
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
            Guest guest = new Guest();
            guest.Current = hotel.Map[0, 0];
            Point Position = guest.Position;
            guest.setPath(hotel, hotel.Map[0, 4]);
            guest.Walk(hotel);
            Assert.AreEqual(Position.Y - 10, guest.Position.Y);

        }

        [TestMethod]
        public void CreateHotel()
        {
            Assert.IsNotNull(hotel);
        }

        [TestMethod]
        public void CreateHotelBitmap()
        {
            Assert.IsNotNull(hotel.HotelBitmap);
        }

        [TestMethod]
        public void AddNeighbours()
        {
            bool neighboursCreated = false;

            //Adding neighbours to every hotelroom.
            if (hotel.Map[2, 5].Neighbours.Count == 2)
                neighboursCreated = true;

            Assert.IsTrue(neighboursCreated);
        }

        [TestMethod]
        public void TestMovingElevator()
        {
            bool elevatorArrived = false;
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
            hotel.AddMaids(1);
            Assert.AreEqual(hotel.Maids.Count, 2);
        }



    }


}

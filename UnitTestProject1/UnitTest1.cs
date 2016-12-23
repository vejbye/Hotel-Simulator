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
        string standardLayout = @"..\..\..\WindowsFormsApplication5\Resources\Hotel5.layout";
        HotelRoom[,] map;

        [TestInitialize]
        public void Initialize()
        {
            hotel = Hotel.getHotel();
            var hs = new HotelSimulator.HotelSimulator();
            reader = new LayoutReader();

            if (hotel.GetMap() == null)
            {
                hotel.Build(reader.ReadLayout(standardLayout));
            }

            map = hotel.GetMap();
            hotel.DrawMe.DrawHotel(hotel, true);

        }
        
        [TestMethod]
        public void TestEating()
        {
            Guest guest = new Guest();
            guest.EatingHTE = 2;
            guest.EatingDuration = 1;
            guest.Eating();
            Assert.AreEqual(3, guest.EatingHTE);
        }
        [TestMethod]
        public void TestArrival1()
        {
            Guest guest = new Guest();
            guest.Current = map[2, 3];
            Point Position = guest.Position;
            guest.setPath(hotel, hotel.GetMap()[2, 3]);
            guest.Walk(hotel);
            Assert.AreEqual(1, hotel.GetMap()[2, 3].Guests.Count);
            hotel.GetMap()[2, 3].Guests.Clear();

        }
        [TestMethod]
        public void TestArrival2()
        {
            Guest guest = new Guest();
            Guest guest2 = new Guest();
            hotel.GetMap()[2, 3].Guests.Add(guest2);
            guest.Current = map[2, 3];
            Point Position = guest.Position;
            guest.setPath(hotel, hotel.GetMap()[2, 3]);
            ((Restaurant)hotel.GetMap()[2, 3]).Capacity = 1;
            guest.Walk(hotel);
            Assert.AreEqual(1, ((Restaurant)hotel.GetMap()[2, 3]).Waitingline.Count);
            ((Restaurant)hotel.GetMap()[2, 3]).Waitingline.Clear();
            hotel.GetMap()[2, 3].Guests.Clear();
            

        }

        [TestMethod]
        public void TestArrival3()
        {
            Guest guest = new Guest();
            guest.Current = hotel.GetMap()[1, 0];
            guest.Room = ((Room)hotel.GetMap()[1, 1]);
            Point Position = guest.Position;
            guest.setPath(hotel, hotel.GetMap()[1, 0]);
            guest.Walk(hotel);
            Assert.AreEqual(false, guest.CheckedIn);
            hotel.GetMap()[2, 3].Guests.Clear();
        }

        [TestMethod]
        public void TestCheckout()
        {
            Guest guest = new Guest();
            guest.Room = ((Room)hotel.GetMap()[1, 1]);
            hotel.Guests.Add(guest);
            ((Reception)hotel.GetMap()[1, 0]).CheckOut(guest);
            Assert.AreEqual(null, guest.Room);
            ((Room)hotel.GetMap()[1, 1]).Dirty = false;
        }


        [TestMethod]
        public void TestMovieStart()
        {
            ((Cinema)map[4, 3]).Playing = true;
            ((Cinema)map[4, 3]).PlayMovie(0);
            Assert.AreEqual(false, ((Cinema)map[4, 3]).Playing);
        }

        [TestMethod]
        public void TestBoundingBox()
        {
            bool hasDrawingBoxes = false;

            for (int i = 0; i < map.GetLength(0); i++)
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
            ((Restaurant)map[2, 3]).Capacity = 1;
            ((Restaurant)map[2, 3]).Waitingline.Enqueue(guest);
            ((Restaurant)map[2, 3]).HandleWaitingline();
            Assert.AreEqual(0, ((Restaurant)map[2, 3]).Waitingline.Count);
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
        public void TestMaidDying()
        {
            Maid maid = new Maid(hotel.GetMap()[2,2]);
            maid.WaitTime = 7;
            maid.InLine(hotel);
            Assert.AreEqual(hotel.GetMap()[0, 0], maid.Current);

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
            guest.Current = map[0, 0];
            Point Position = guest.Position;
            guest.setPath(hotel, map[9, 0]);
            Assert.AreEqual(10, guest.Path.Count);
        }

        [TestMethod]
        public void TestMaidpathfind()
        {
            Maid maid = new Maid(hotel.GetMap()[0, 0]);
            maid.CleaningHTE = 1;
            ((Room)hotel.Map[4, 1]).Dirty = true;
            Point Position = maid.Position;
            maid.SetPath(hotel);
            Assert.AreEqual(6, maid.Path.Count);
        }

        [TestMethod]
        public void TestMaidWalk1()
        {
            Maid maid = new Maid(hotel.GetMap()[0, 0]);
            maid.CleaningHTE = 1;
            ((Room)hotel.GetMap()[4, 1]).Dirty = true;
            Point Position = maid.Position;
            maid.SetPath(hotel);
            maid.Walk(hotel);
            Assert.AreEqual(Position.Y - 10, maid.Position.Y);
        }

        [TestMethod]
        public void TestMaidWalkEvacuation()
        {
            Maid maid = new Maid(hotel.GetMap()[0, 0]);
            maid.Evacuation = true;
            maid.CleaningHTE = 1;
            Point Position = maid.Position;
            maid.SetPath(hotel);
            maid.Walk(hotel);
            Assert.AreEqual(Position.X + 10, maid.Position.X);
            maid.Evacuation = false;
        }
        [TestMethod]
        public void TestMaidEvacuation2()
        {
            Maid maid = new Maid(hotel.GetMap()[hotel.GetMap().GetLength(0) - 2, 0]);
            maid.Evacuation = true;
            maid.CleaningHTE = 1;
            Point Position = maid.Position;
            maid.SetPath(hotel);
            maid.Walk(hotel);
            Assert.AreEqual(Position.X - 10, maid.Position.X);
            maid.Evacuation = false;
        }

        [TestMethod]
        public void TestWalkRight()
        {
            Guest guest = new Guest();
            guest.Current = map[0, 0];
            Point Position = guest.Position;
            guest.setPath(hotel, hotel.GetMap()[9, 0]);
            guest.Walk(hotel);
            Assert.AreEqual(Position.X + 10, guest.Position.X);

        }
        [TestMethod]
        public void TestWalkDown()
        {
            Guest guest = new Guest();
            guest.Current = map[0, 5];
            Point Position = guest.Position;
            guest.setPath(hotel, map[0, 0]);
            guest.Walk(hotel);
            Assert.AreEqual(Position.Y + 10, guest.Position.Y);

        }
        [TestMethod]
        public void TestWalkLeft()
        {
            Guest guest = new Guest();
            guest.Current = map[9, 0];
            Point Position = guest.Position;
            guest.setPath(hotel, hotel.GetMap()[0, 0]);
            guest.Walk(hotel);
            Assert.AreEqual(Position.X - 10, guest.Position.X);

        }
        [TestMethod]
        public void TestWalkUp()
        {
            Guest guest = new Guest();
            guest.Current = map[0, 0];
            Point Position = guest.Position;
            guest.setPath(hotel, map[0, 4]);
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
            hotel.Build(reader.ReadLayout(standardLayout));
            Assert.IsNotNull(hotel.HotelBitmap);
        }

        [TestMethod]
        public void AddNeighbours()
        {
            bool neighboursCreated = false;

            //Adding neighbours to every hotelroom.
            if (map[2, 5].Neighbours.Count == 2)
                neighboursCreated = true;

            Assert.IsTrue(neighboursCreated);
        }

        [TestMethod]
        public void TestMovingElevatorUp()
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

        //NEEDS TO BE EDITED
        [TestMethod]
        public void TestMovingElevatorDown()
        {
            bool elevatorArrived = false;
            hotel.Build(reader.ReadLayout(standardLayout));
            hotel.Elevator.PreviousFloor = 6;
            hotel.Elevator.Position.Y = hotel.DrawMe.YStartPosition - (6 * hotel.DrawMe.StandardRoomHeight);
            //Testing if the elevator movement is correct. Given are the hotel the elevator is in, the requested floor (3), and the speed of the elevator. (1)

            while (!elevatorArrived)
            {
                hotel.Elevator.MoveElevator(hotel,3, 1);

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
        public void TestCheckOut()
        {
            Reception r = new Reception();
            Guest guest = new Guest();
            guest.Preference = "1";
            guest.Room = r.FindEmptyRoom(hotel, guest);

            r.CheckOut(guest);
            Assert.IsNull(guest.Room);
        }

        [TestMethod]
        public void TestFailingLayout()
        {
            LayoutReader lr = new LayoutReader();
            List<LayoutFormat> _formats;
            _formats = lr.ReadLayout(@"..\..\..\WindowsFormsApplication5\Resources\Room.png");


            Assert.IsNull(_formats);
        }



    }


}

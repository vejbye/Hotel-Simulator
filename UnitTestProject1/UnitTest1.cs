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
            hotel = Hotel.getHotel();
            layoutFormatList = new List<LayoutFormat>();
            map = new HotelRoom[10, 10];
            hotelWidth = new List<int>();
            hotelPosition = new List<int>();
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
        public void testcheckin()
        {
            Initialize();
            hotel.Map = map;
            Reception r = new Reception();
            Guest guest = new Guest();
            guest.Preference = "1";           
            guest.Room = r.FindEmptyRoom(hotel, guest);
            Assert.IsNotNull(guest.Room);
        }

        [TestMethod]
        public void testGuestpathfind()
        {
            Initialize();
            hotel.Map = map;
            Guest guest = new Guest();
            guest.Current = hotel.Map[0, 0];
            Point Position = guest.Position;
            guest.setPath(hotel, hotel.Map[9, 0]);
            Assert.AreEqual(10, guest.Path.Count);
        }


        public void testMaidpathfind()
        {
            Initialize();
            hotel.Map = map;
            Maid maid = new Maid(hotel.Map[0, 0]);
            Point Position = maid.Position;
            maid.SetPath(hotel);
            Assert.AreEqual(10, maid.Path.Count);
        }

        [TestMethod]
        public void TestWalkRight()
        {
            Initialize();
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
                for (int y = 0; y < map.GetLength(1); y++) ;
                   // map[x, y] = new HotelRoom();
            }

            Assert.IsNotNull(map);
        }

        [TestMethod]
        public void Neighbours()
        {
            
        }
       


       
    }

    
}

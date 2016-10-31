using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HotelSimulator.Object;
using System.Collections.Generic;
using HotelSimulator;

namespace HotelSimulatorUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Guest guest = new Guest(null);
            guest.waitTime = 7;
            guest.inLine();
            Assert.AreEqual(guest.dead, true);
            
        }
    }
}

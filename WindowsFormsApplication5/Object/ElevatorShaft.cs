﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using WindowsFormsApplication5.Properties;

namespace HotelSimulator.Object
{
    public class ElevatorShaft: HotelRoom
    {
        public ElevatorShaft()
        {
            Image = Resources.ElevatorShaft;
        }
    }
}

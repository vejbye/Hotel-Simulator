﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace HotelSimulator.Object
{
    public abstract class SimObject
    {
        public Rectangle BoundingBox { get; set; }
        public Image Image;
        public int Width;
        public int Height;
    }
}

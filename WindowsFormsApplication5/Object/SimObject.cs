using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using WindowsFormsApplication5;

namespace HotelSimulator.Object
{
    public enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN,
        NONE

    }
    public abstract class SimObject
    {
        public Rectangle BoundingBox { get; set; }
        public Draw DrawMe;
        public Image Image;
        public int Width;
        public int Height;
        public Point Position;
        public Direction Direction;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using WindowsFormsApplication5;

namespace HotelSimulator.Object
{
    public abstract class SimObject
    {
        public Draw DrawMe;
        public Point Position;
        public Rectangle BoundingBox { get; set; }
        public Image Image { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        
    }
}

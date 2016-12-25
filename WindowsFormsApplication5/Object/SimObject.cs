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
        public Draw DrawMe { get; set; } //Uses this to get information for standard purposes. For example, standard room height.
        public Point Position; //The position of an object
        public Rectangle BoundingBox { get; set; } //Clickbox for an object
        public Image Image { get; set; } //The image of the object
        public int Width { get; set; } //The width of an object
        public int Height { get; set; } //The height of an object
        
    }
}

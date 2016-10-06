using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulator.Object
{
    public class BoundaryBox
    {
        public Rectangle[,] BoundaryBoxes;

        public Rectangle[,] AddBox(Rectangle r, HotelRoom[,] map, int x, int y)
        {
            if (BoundaryBoxes == null)
                BoundaryBoxes = new Rectangle[map.GetLength(0), map.GetLength(1)];
            

            BoundaryBoxes[x, y] = r;
            return BoundaryBoxes;
        }


    }
}

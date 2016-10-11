using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelSimulator.Object;

namespace WindowsFormsApplication5
{
    // REMOVE ME!
    public class BoundaryBox
    {
        public void AddBox(Rectangle r, HotelRoom[,] map, Rectangle[,] BoundaryBoxes, int x, int y)
        {
            BoundaryBoxes[x, y] = r;
        }

        public Rectangle[,] AddBoxes(HotelRoom[,] map, Rectangle[,] BoundaryBoxes)
        {
            int xStartPosition = 500;
            int yStartPosition = 685;
            int standardRoomWidth = 100;
            int standardRoomHeight = 50;

            if (BoundaryBoxes == null)
                BoundaryBoxes = new Rectangle[map.GetLength(0), map.GetLength(1)];

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    Rectangle r = new Rectangle(xStartPosition, yStartPosition, standardRoomWidth, standardRoomHeight);
                    AddBox(r, map, BoundaryBoxes, x, y);

                    yStartPosition -= standardRoomHeight;
                }

                xStartPosition += standardRoomWidth;
                yStartPosition = 685;
            }

            return BoundaryBoxes;
        }



    }
}

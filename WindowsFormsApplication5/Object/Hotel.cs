using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulator.Object
{
    class Hotel
    {
        List<object> Spaces = new List<object>();

        public Bitmap Build(int width, int height)
        {
            Bitmap hotel = new Bitmap(width, height);
            Space[,] map = new Space[width, height];

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] = new Space();
                }
            }

            Room test = new Room();
            Room test2 = new Room();
            Room test3 = new Room();
            Reception test4 = new Reception();
            map[0, 0].currentObject = test;
            map[0, 1].currentObject = test2;
            map[1, 0].currentObject = test3;
            map[1, 1].currentObject = test4;


            Graphics gfx = Graphics.FromImage(hotel);

            int xas = 1;
            int yas = 1;
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y].currentObject != null)
                    {
                        if (map[x,y].currentObject is Reception)
                            gfx.DrawImage(map[x, y].currentObject.image, xas, yas, 200, 50);
                        else
                            gfx.DrawImage(map[x, y].currentObject.image, xas, yas, 100, 50);
                    }

                    yas += 50;
                }

                xas += 100;
                yas = 1;
            }

            return hotel;

            
        }

    }
}

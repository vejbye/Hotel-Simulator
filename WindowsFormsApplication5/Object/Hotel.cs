using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication5.Properties;

namespace HotelSimulator.Object
{
    class Hotel
    {
        List<GameObject> objects = new List<GameObject>();
        Space[,] map = new Space[10, 10];

        public Bitmap Build()
        {
            Bitmap hotel = new Bitmap(2000,1000);

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
            ElevatorShaft test5 = new ElevatorShaft();

            objects.Add(test);
            objects.Add(test2);
            objects.Add(test3);
            objects.Add(test4);
            objects.Add(test5);

            map[0, 0].currentObject = test;
            map[0, 1].currentObject = test2;
            map[1, 0].currentObject = test3;
            map[1, 1].currentObject = test4;
            map[2, 1].currentObject = test5;


            Graphics gfx = Graphics.FromImage(hotel);

            int xas = 700;
            int yas = 635;
            gfx.DrawImage(Resources.SimulatorBG, 1 , 1 , 2000, 800);
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    if (map[x, y].currentObject != null)
                    {
                        if (map[x, y].currentObject is Reception)
                        {
                            gfx.DrawImage(map[x, y].currentObject.image, xas, yas, 200, 50);
                            xas += 100;
                        }
                        if (map[x, y].currentObject is ElevatorShaft)
                        {
                            gfx.DrawImage(map[x, y].currentObject.image, xas, yas, 100, 50);
                        }
                        if (map[x, y].currentObject is Room)
                        {
                            gfx.DrawImage(map[x, y].currentObject.image, xas, yas, 100, 50);
                        }
                    }

                    yas += 50;
                }

                xas += 100;
                yas = 635;
            }

            return hotel;
            
        }

        public Space[,] getMap()
        {
            return map;
        }

    }
}

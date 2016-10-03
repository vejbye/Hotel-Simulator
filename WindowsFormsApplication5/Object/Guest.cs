using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication5.Properties;

namespace HotelSimulator.Object
{
    class Guest: SimObject
    {
        Room room;
        SimObject current;
        SimObject[,] plans;
        Graphics gfx;

        public Guest(SimObject current)
        {
            this.current = current;
            Image = Resources.Guest;
            
            
            
        }
        public void Draw(Graphics gfx, SimObject[,] map, int xstart, int ystart)
        {
            int xpos = 1;
            int ypos = 1;
            this.gfx = gfx;
            plans = map;
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == current)
                    {
                        xpos = i;
                        ypos = j;
                        break;
                    }
                }
            }
            gfx.DrawImage(Image, xpos * (current.Width + 100)+ xstart, ypos * (current.Height + 50)+ ystart, 30, 30);
        }
        public void Walk()
        {
            foreach(KeyValuePair<Neighbour.Neighbours, Space> direction in current.neighbours)
            {
                if(direction.Key == Neighbour.Neighbours.East && direction.Value != null)
                {
                    current = direction.Value.currentObject;
                    //Draw(gfx,plans);
                }
                else if(direction.Key == Neighbour.Neighbours.North && direction.Value != null)
                {
                    current = direction.Value.currentObject;
                }
            }


        }
    }
}

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
        Space current;
        Space[,] plans;
        Graphics gfx;

        public Guest(Space current)
        {
            this.current = current;
            image = Resources.Guest;
            
            
            
        }
        public void Draw(Graphics gfx, Space[,] map, int xstart, int ystart)
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
            gfx.DrawImage(image, xpos * (current.currentObject.width + 100)+ xstart, ypos * (current.currentObject.height + 50)+ ystart, 30, 30);
        }
        public void Walk()
        {
            foreach (KeyValuePair<Neighbour.Neighbours,Space> direction in current.neighbours)
            {
                if(direction.Key == Neighbour.Neighbours.East && direction.Value != null)
                {
                    current = direction.Value;
                    //Draw(gfx,plans);
                }
                else if(direction.Key == Neighbour.Neighbours.North && direction.Value != null)
                {
                    current = direction.Value;
                }
            }


        }
    }
}

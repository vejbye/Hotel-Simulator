using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication5.Properties;

namespace HotelSimulator.Object
{
    public class Guest: SimObject
    {
        Room room;
        public HotelRoom current;
        List<HotelRoom> path;
        public Guest(HotelRoom current)
        {
            this.current = current;
            Image = Resources.Guest;
            Width = 30;
            Height = 30;
            path = new List<HotelRoom>();
        }
        public void Walk(Hotel hotel, HotelSimulator hs)
        {
            while (current.Neighbours.ContainsKey(Neighbours.East))
            {
                if (current is Reception && room == null)
                {
                   room = ((Reception)current).findEmptyRoom(hotel);
                    break;
                }
                    current.guest = null;
                    current.Neighbours[Neighbours.East].guest = this;
                    current = current.Neighbours[Neighbours.East];
                    hotel.Draw(hotel.map);
                    hs.Refresh();
            }
            HotelRoom stair = hotel.map[7,6];
            PathFind pf = new PathFind();
            pf.shortestPathDijkstra(this,current, stair);
            HotelRoom cur = stair;
            while (cur != current)
            {
                path.Add(cur);
                cur = cur.Previous;
            }
            path.Add(cur);
            foreach(HotelRoom hr in path)
            Console.WriteLine(hr+ ",");
            for (int i = path.Count - 1; i > -1; i--)
            {                
                if (i - 1 >= 0)
                {
                    path[i].guest = null;
                    path[i - 1].guest = this;
                    current = path[i - 1];
                    hotel.Draw(hotel.map);
                    hs.Refresh();
                }               
            }
        }
    }
}

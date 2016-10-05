using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication5.Properties;

namespace HotelSimulator.Object
{
    public class Maid: SimObject
    {
        public HotelRoom current;
        List<HotelRoom> path;
        public Maid(HotelRoom current)
        {
            this.current = current;
            Image = Resources.Maid;
            Width = 30;
            Height = 30;
            path = new List<HotelRoom>();
        }
        public void Walk(Hotel hotel, HotelSimulator hs)
        {
            HotelRoom stair = hotel.map[7, 6];
            PathFind pf = new PathFind();
            pf.shortestPathDijkstra(this, current, stair);
            HotelRoom cur = stair;
            while (cur != current)
            {
                path.Add(cur);
                cur = cur.Previous;
            }
            path.Add(cur);
            foreach (HotelRoom hr in path)
                Console.WriteLine(hr + ",");
            for (int i = path.Count - 1; i > -1; i--)
            {
                if (i - 1 >= 0)
                {
                    path[i].maid = null;
                    path[i - 1].maid = this;
                    current = path[i - 1];
                    hotel.Draw(hotel.map);
                    hs.Refresh();
                }
            }
        }
    }
}

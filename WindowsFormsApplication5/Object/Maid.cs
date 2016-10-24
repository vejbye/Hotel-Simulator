using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication5.Properties;
using WindowsFormsApplication5;

namespace HotelSimulator.Object
{
    public class Maid: SimObject
    {
        public HotelRoom Current;
        List<HotelRoom> Path;
        public bool moved = false;
        public Maid(HotelRoom current)
        {
            this.Current = current;
            Image = Resources.Maid;
            Width = 40;
            Height = 40;
            Path = new List<HotelRoom>();
            DrawMe = new Draw();
        }
        public void Walk(Hotel hotel, HotelSimulator hs)
        {
            foreach(HotelRoom hm in hotel.Map)
            {
                if(hm is Room && ((Room)hm).Dirty == true){
                    PathFind pf = new PathFind();
                    pf.shortestPathDijkstra(this, Current, hm);
                    HotelRoom cur = hm;
                    while (cur != Current)
                    {
                        Path.Add(cur);
                        cur = cur.Previous;
                    }
                    Path.Add(cur);
                    for (int i = Path.Count - 1; i > -1; i--)
                    {
                        if (i - 1 >= 0)
                        {
                            Path[i].Maids.Remove(this);
                            Path[i - 1].Maids.Add(this);
                            Current = Path[i - 1];
                            //DrawMe.DrawHotel(hotel.Map, hotel._hotel, hotel.Elevator);
                            //hs.Refresh();
                        }
                    }
                    foreach (HotelRoom hr in hotel.Map)
                    {
                        hr.Previous = null;
                        hr.Distance = Int32.MaxValue;
                    }
                    ((Room)hm).Dirty = false;
                    Path.Clear();
                }
            }
        }
    }
}

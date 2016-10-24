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
        public HotelRoom Current; // current location of maid
        List<HotelRoom> Path; // for storing the path to the maids destination
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
                    pf.shortestPathDijkstra(Current, hm);
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
                            if (Current.Neighbours.ContainsKey(Neighbours.East) && Path[i - 1] == Current.Neighbours[Neighbours.East])
                            {
                                Direction = Direction.RIGHT;
                            }
                            else if (Current.Neighbours.ContainsKey(Neighbours.West) && Path[i - 1] == Current.Neighbours[Neighbours.West])
                            {
                                Direction = Direction.LEFT;
                            }
                            else if (Current.Neighbours.ContainsKey(Neighbours.South) && Path[i - 1] == Current.Neighbours[Neighbours.South])
                            {
                                Direction = Direction.DOWN;
                            }
                            else if (Current.Neighbours.ContainsKey(Neighbours.North) && Path[i - 1] == Current.Neighbours[Neighbours.North])
                            {
                                Direction = Direction.UP;
                            }
                            DrawMe.drawPersons(hotel, this, hotel.Elevator, hs);
                            Current = Path[i - 1];
                            
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

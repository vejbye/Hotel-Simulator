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
        List<HotelRoom> open;
        List<HotelRoom> path;

        public Guest(HotelRoom current)
        {
            this.current = current;
            Image = Resources.Guest;
            Width = 30;
            Height = 30;
            open = new List<HotelRoom>();
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
            HotelRoom stair = hotel.map[9, 0];
            shortestPathDijkstra(this,current, stair);
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

        public HotelRoom shortestPathDijkstra(Guest guest, HotelRoom start, HotelRoom end)
        {
            HotelRoom current = start;
            while (Completed(current, end) == false)
            {if (open.Count > 0)
                {
                    current = open.Aggregate((l, r) => l.Width < r.Width ? l : r);
                }
                else
                {
                    current = null;
                    break;
                }
            }
            //guest.current = current;
            return current;
        }

        public bool Completed(HotelRoom current, HotelRoom end)
        {
            if (current == end)
            {
                return true;
            }
            if (open.Contains(current))
            {
                
                open.Remove(current);
            }
            foreach (KeyValuePair<Neighbours, HotelRoom> weight in current.Neighbours)
            {
                int newDistance = current.distance + weight.Value.Height;
                if (!((current is ElevatorShaft || current is Stair) && (weight.Value is ElevatorShaft || weight.Value is Stair)))
                {
                   
                    if (newDistance < weight.Value.distance)
                    {
                        weight.Value.distance = newDistance;
                        if (weight.Value.Previous == null)
                        {
                            weight.Value.Previous = current;
                            open.Add(weight.Value);
                        }
                        
                    }
                }
                else
                {
                    newDistance = current.distance + weight.Value.Height;
                    if (newDistance < weight.Value.distance)
                    {
                        weight.Value.distance = newDistance;
                        if (weight.Value.Previous == null)
                        {
                            weight.Value.Previous = current;
                            open.Add(weight.Value);
                        }
                       
                    }
                }
            }
            return false;
        }
    }
}

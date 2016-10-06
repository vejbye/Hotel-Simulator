using HotelSimulator.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HotelSimulator
{
    class PathFind
    {
        List<HotelRoom> open;

        public PathFind()
        {
            open = new List<HotelRoom>();
        }

        public HotelRoom shortestPathDijkstra(SimObject guest, HotelRoom start, HotelRoom end)
        {
            HotelRoom current = start;
            while (Completed(current, end) == false)
            {
                if (open.Count > 0)
                {
                    current = open.Aggregate((l, r) => l.Distance < r.Distance ? l : r);
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
                /* int newDistance = 0;
                 if (weight.Key == Neighbours.East || weight.Key == Neighbours.West)
                 {
                      newDistance = current.distance + weight.Value.Height;
                 }
                 else
                 {
                     newDistance = current.distance + weight.Value.Width;
                 }
                 /*if (!((current is ElevatorShaft || current is Stair) && (weight.Value is ElevatorShaft || weight.Value is Stair)))
                 {*/
                int newDistance = current.Distance + 1;
                if (newDistance < weight.Value.Distance)
                {
                    weight.Value.Distance = newDistance;
                    if (weight.Value.Previous == null)
                    {
                        weight.Value.Previous = current;
                        open.Add(weight.Value);
                    }

                }
                /* }
                 else
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
                 }*/
            }
            return false;
        }
    }
}

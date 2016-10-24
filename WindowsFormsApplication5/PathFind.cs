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

        public HotelRoom shortestPathDijkstra(HotelRoom start, HotelRoom end)
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

                int newDistance = current.Distance + 1;//weight.Value.Distance;
                if (newDistance < weight.Value.Distance)
                {
                    weight.Value.Distance = newDistance;
                   if (weight.Value.Previous == null)
                    {
                        weight.Value.Previous = current;
                        open.Add(weight.Value);
                    }

                }
            }
            return false;
        }
    }
}

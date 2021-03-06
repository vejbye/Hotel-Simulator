﻿using HotelSimulator.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HotelSimulator
{
    class PathFind
    {
        private List<HotelRoom> _open; //this is iterated through to define the shortest path

        public PathFind()
        {
            _open = new List<HotelRoom>();
        }
        
        /// <summary>
        /// Calaculates the shortest path
        /// </summary>
        /// <param name="start">Give the start of the path</param>
        /// <param name="end">Give the end of the path</param>
        public void ShortestPathDijkstra(HotelRoom start, HotelRoom end)
        {
            HotelRoom current = start;
            while (Completed(current, end) == false)
            {
                if (_open.Count > 0)
                {
                    current = _open.Aggregate((l, r) => l.Distance < r.Distance ? l : r);
                }
                else
                {
                    current = null;
                    break;
                }
            }
        }
        
        /// <summary>
        /// Checks if the end has been reached
        /// </summary>
        /// <param name="current">Give the current position in the path</param>
        /// <param name="end">Give the end of the Path</param>
        /// <returns>true if the end has been reached, false if not</returns>
        public bool Completed(HotelRoom current, HotelRoom end)
        {
            if (current == end)
            {
                return true;
            }
            if (_open.Contains(current))
            {

                _open.Remove(current);
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
                        _open.Add(weight.Value);
                    }

                }
            }
            return false;
        }
    }
}

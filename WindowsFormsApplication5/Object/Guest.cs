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
        public HotelRoom current;
        private Room _room;
        private List<HotelRoom> _open;
        private List<HotelRoom> _path;

        public Guest(HotelRoom current)
        {
            this.current = current;
            Image = Resources.Guest;
            Width = 30;
            Height = 30;
            _open = new List<HotelRoom>();
            _path = new List<HotelRoom>();



        }
        public void Walk(Hotel hotel, HotelSimulator hs)
        {
            while (current.Neighbours.ContainsKey(Neighbours.East))
            {
                if (current is Reception && _room == null)
                {
                   _room = ((Reception)current).findEmptyRoom(hotel);
                    break;
                }
                    current.Guest = null;
                    current.Neighbours[Neighbours.East].Guest = this;
                    current = current.Neighbours[Neighbours.East];
                    hotel.Draw(hotel.Map);
                    hs.Refresh();
            }
            HotelRoom stair = hotel.Map[9, 0];
            shortestPathDijkstra(this,current, stair);
            HotelRoom cur = stair;
            while (cur != current)
            {
                _path.Add(cur);
                cur = cur.Previous;
            }
            _path.Add(cur);
            foreach(HotelRoom hr in _path)
            Console.WriteLine(hr+ ",");
            for (int i = _path.Count - 1; i > -1; i--)
            {
                
                if (i - 1 >= 0)
                {
                    _path[i].Guest = null;
                    _path[i - 1].Guest = this;
                    current = _path[i - 1];
                    hotel.Draw(hotel.Map);
                    hs.Refresh();
                }
               
            }


    }

        public HotelRoom shortestPathDijkstra(Guest guest, HotelRoom start, HotelRoom end)
        {
            HotelRoom current = start;
            while (Completed(current, end) == false)
            {if (_open.Count > 0)
                {
                    current = _open.Aggregate((l, r) => l.Width < r.Width ? l : r);
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
            if (_open.Contains(current))
            {
                
                _open.Remove(current);
            }
            foreach (KeyValuePair<Neighbours, HotelRoom> weight in current.Neighbours)
            {
                int newDistance = current.Distance + weight.Value.Height;
                if (!((current is ElevatorShaft || current is Stair) && (weight.Value is ElevatorShaft || weight.Value is Stair)))
                {
                   
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
                else
                {
                    newDistance = current.Distance + weight.Value.Height;
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
            }
            return false;
        }
    }
}

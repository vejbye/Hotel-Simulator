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
        Room Room;
        public HotelRoom Current;
        List<HotelRoom> Path;
        public HotelRoom LastDestination;
        public Guest(HotelRoom current)
        {
            this.Current = current;
            Image = Resources.Guest;
            Width = 40;
            Height = 40;
            Path = new List<HotelRoom>();
        }

        public HotelRoom setDestination(Hotel hotel)
        {
            Random rand = new Random();
            int no = rand.Next(4);
            HotelRoom destination = null;
            if (Room == null)
            {
                foreach (HotelRoom hr in hotel.map)
                {
                    if (hr is Reception)
                    {
                        destination = hr;
                    }
                }
            }
            else if (LastDestination != Room)
            {
                destination = Room;
            }
            else {
                switch (no)
                {
                    case 0:
                        foreach (HotelRoom hr in hotel.map)
                        {
                            if (hr is Cinema)
                            {
                                destination = hr;
                            }
                        }; break;
                    case 1:
                        foreach (HotelRoom hr in hotel.map)
                        {
                            if (hr is Restaurant)
                            {
                                destination = hr;
                            }
                        }; break;
                    case 2:
                        foreach (HotelRoom hr in hotel.map)
                        {
                            if (hr is Gym)
                            {
                                destination = hr;
                            }
                        }; break;
                    case 3:
                        foreach (HotelRoom hr in hotel.map)
                        {
                            if (hr is Reception)
                            {
                                destination = hr;
                            }
                        }; break;
                    default: destination = null; break;
                }
            }

            if(destination != null)
            {
                LastDestination = destination;
            }

            return destination;
            
        }

        public void Walk(Hotel hotel, HotelSimulator hs, HotelRoom destination)
        {
            PathFind pf = new PathFind();           
            pf.shortestPathDijkstra(this,Current, destination);
            HotelRoom cur = destination;
            while (cur != Current)
            {
                Path.Add(cur);
                cur = cur.Previous;
            }
            Path.Add(cur);
            foreach(HotelRoom hr in Path)
            Console.WriteLine(hr+ ",");
            for (int i = Path.Count - 1; i > -1; i--)
            {                
                if (i - 1 >= 0)
                {
                    Path[i].Guests.Remove(this);
                    Path[i - 1].Guests.Add(this);
                    Current = Path[i - 1];
                    hotel.Draw(hotel.map);
                    hs.Refresh();
                }               
            }
            if(Room == null && destination is Reception)
            {
                Room = ((Reception)destination).findEmptyRoom(hotel);
            }
            foreach(HotelRoom hr in hotel.map)
            {
                hr.Previous = null;
                hr.distance = Int32.MaxValue;
            }
            Path.Clear();
        }
    }
}

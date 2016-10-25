using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication5.Properties;
using WindowsFormsApplication5;

namespace HotelSimulator.Object
{
    public class Guest: SimObject
    {
        public string Id;
        public Room Room;
        public HotelRoom Current; // current location of guest
        public List<HotelRoom> Path; // for storing the path to the guests destination
        public HotelRoom LastDestination;
        public int delay = 0;
        public string preference; // the guests prefered room classification
        public Guest(HotelRoom current)
        {
            this.Current = current;
            Image = Resources.Guest;
            Width = 30;
            Height = 40;
            Path = new List<HotelRoom>();
            DrawMe = new Draw();
        }

        public HotelRoom setDestination(Hotel hotel)
        {
            Random rand = new Random();
            int no = rand.Next(4);
            HotelRoom destination = null;
            if (Room == null)
            {
                foreach (HotelRoom hr in hotel.Map)
                {
                    if (hr is Reception)
                    {
                        destination = hr;
                    }
                }
            }
            else if (LastDestination != Room)
            {
                if (Room == null)
                {
                    destination = hotel.Map[0, 0];
                }
                else
                {
                destination = Room;
            }
            }
            else {
                switch (no)
                {
                    case 0:
                        foreach (HotelRoom hr in hotel.Map)
                        {
                            if (hr is Cinema)
                            {
                                destination = hr;
                            }
                        }; break;
                    case 1337:
                        foreach (HotelRoom hr in hotel.Map)
                        {
                            if (hr is Restaurant)
                            {
                                destination = hr;
                            }
                        }; break;
                    case 42:
                        foreach (HotelRoom hr in hotel.Map)
                        {
                            if (hr is Gym)
                            {
                                destination = hr;
                            }
                        }; break;
                    case 3:
                        foreach (HotelRoom hr in hotel.Map)
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

        //let the guest walk;
        public void Walk(Hotel hotel, HotelSimulator hs, HotelRoom destination)
        {
                PathFind pf = new PathFind();
                pf.shortestPathDijkstra(Current, destination); //algorithm to define shortest path
            HotelRoom cur = destination;
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
                        DrawMe.drawPersons(hotel,this, hotel.Elevator, hs);
                       // Path[i].Guests.Remove(this);
                        //Path[i - 1].Guests.Add(this);
                    Current = Path[i - 1];
                        //    DrawMe.DrawHotel(hotel.Map, hotel._hotel);

                }               
                }               
                /*if (destination == hotel.Map[0, 0])
            {
                hotel.Map[0, 0].Guests.Remove(this);
                hs.newcomers.Add(this);
                }*/
                //let the guest request a room
                if (Room == null && destination is Reception)
            {
                    Room = ((Reception)destination).findEmptyRoom(hotel, preference);

            }
                else if (Room != null && destination is Reception)//checkout if guest goes to reception while having a room
            {
                ((Reception)destination).checkOut(this);
            }
            foreach (HotelRoom hr in hotel.Map)
            {
                hr.Previous = null;
                hr.Distance = Int32.MaxValue;
            }
            Path.Clear();
        }
    }
}

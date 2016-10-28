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
    public class Guest : Moveable
    {
        public string Id;
        public Room Room;
        public HotelRoom Destination;
        public int Delay = 0;
        public string Preference; // the guests prefered room classification
        public bool CheckedIn = false;
        public Guest(HotelRoom current)
        {
            this.Current = current;
            Image = Resources.Guest;
            Width = 30;
            Height = 40;
            Path = new List<HotelRoom>();
            DrawMe = new Draw();
        }

        public void setPath(Hotel hotel, HotelRoom destination)
        {
            PathFind pf = new PathFind();
            pf.shortestPathDijkstra(Current, destination); //algorithm to define shortest path
            HotelRoom cur = destination;
            while (cur != Current)
            {
                Path.Add(cur);
                cur = cur.Previous;
            }
            foreach (HotelRoom hr in hotel.Map)
            {
                hr.Previous = null;
                hr.Distance = Int32.MaxValue;
            }
            Path.Add(cur);
            Destination = destination;
        }

        //let the guest walk;
        public void Walk(Hotel hotel, HotelRoom destination)
        {
            if (Current != Path.ElementAt(0))
            {
                if (Current.Neighbours.ContainsKey(Neighbours.East) && Path[Path.IndexOf(Current) - 1] == Current.Neighbours[Neighbours.East])
                {
                    Direction = Direction.RIGHT;                 
                    if (Position.X > Path[Path.IndexOf(Current) - 1].RoomPosition.X + 10 )
                    {
                        Current = Path[Path.IndexOf(Current) - 1];
                    }

                }
                else if (Current.Neighbours.ContainsKey(Neighbours.West) && Path[Path.IndexOf(Current) - 1] == Current.Neighbours[Neighbours.West])
                {
                    Direction = Direction.LEFT;                   
                    if (Position.X < Path[Path.IndexOf(Current) - 1].RoomPosition.X + 20)
                    {
                        Current = Path[Path.IndexOf(Current) - 1];
                    }
                }
                else if (Current.Neighbours.ContainsKey(Neighbours.South) && Path[Path.IndexOf(Current) - 1] == Current.Neighbours[Neighbours.South])
                {
                    Direction = Direction.DOWN;              
                    if (Position.Y < Path[Path.IndexOf(Current) - 1].RoomPosition.Y + 30)
                    {
                        Current = Path[Path.IndexOf(Current) - 1];
                    }
                }
                else if (Current.Neighbours.ContainsKey(Neighbours.North) && Path[Path.IndexOf(Current) - 1] == Current.Neighbours[Neighbours.North])
                {
                    Direction = Direction.UP;
                    if (Position.Y > Path[Path.IndexOf(Current) - 1].RoomPosition.Y - 5)
                    {
                        Current = Path[Path.IndexOf(Current) - 1];
                    }
                }

                if (Direction == Direction.RIGHT)
                    Position.X += 10;
                if (Direction == Direction.UP)
                    Position.Y += 10;
                if (Direction == Direction.DOWN)
                    Position.Y -= 10;
                if (Direction == Direction.LEFT)
                    Position.X -= 10;
            }

            //let the guest request a room
            if (Room == null && Destination is Reception && Current == Destination)
            {
                Room = ((Reception)Destination).findEmptyRoom(hotel, Preference);
                Path.Clear();
                setPath(hotel, Room);
                CheckedIn = true;

            }
            else if (Room != null && Destination is Reception && Current == Destination)//checkout if guest goes to reception while having a room
            {
                ((Reception)Destination).checkOut(this);
                Path.Clear();
                setPath(hotel, hotel.Map[0, 0]);
                CheckedIn = false;
            }

            if (Destination == hotel.Map[0, 0] && Current == Destination && !CheckedIn)
            {
                hotel.Guests.Remove(this);
            }

        }
    }
}

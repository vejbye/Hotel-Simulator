using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication5.Properties;
using WindowsFormsApplication5;
using System.Windows.Forms;

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
        public bool dead = false;
        public int waitTime = 0;
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
            while (cur != Current)// store path in list so guest can walk through it
            {
                Path.Add(cur);
                cur = cur.Previous;
            }
            foreach (HotelRoom hr in hotel.Map)
            {
                //clear any path related values after path has been stored
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
                //give direction and update current room
                if (Current.Neighbours.ContainsKey(Neighbours.East) && Path[Path.IndexOf(Current) - 1] == Current.Neighbours[Neighbours.East])
                {
                    Direction = Direction.RIGHT;
                    if (Position.X > Path[Path.IndexOf(Current) - 1].RoomPosition.X + MoveDistance * (Path[Path.IndexOf(Current) - 1].Width / DrawMe.standardRoomWidth))
                    {
                        Current = Path[Path.IndexOf(Current) - 1];
                    }

                }
                else if (Current.Neighbours.ContainsKey(Neighbours.West) && Path[Path.IndexOf(Current) - 1] == Current.Neighbours[Neighbours.West])
                {
                    Direction = Direction.LEFT;                   
                    if (Position.X < Path[Path.IndexOf(Current) - 1].RoomPosition.X + MoveDistance * (Path[Path.IndexOf(Current) - 1].Width / DrawMe.standardRoomWidth))
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
                //move guest accordingly
                if (Direction == Direction.RIGHT)
                    Position.X += MoveDistance;
                if (Direction == Direction.UP)
                    Position.Y += MoveDistance;
                if (Direction == Direction.DOWN)
                    Position.Y -= MoveDistance;
                if (Direction == Direction.LEFT)
                    Position.X -= MoveDistance;
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

            if (Destination == hotel.Map[0, 0] && Current == Destination && !CheckedIn)// remove guest from hotel when checked out
            {
                hotel.Guests.Remove(this);
            }

            if(Destination is Cinema && Current == Destination)
            {
                if (((Cinema)destination).playing)
                {
                    Path.Clear();
                    setPath(hotel, Room);
                }
            }

        }

        public void inLine()
        {
            waitTime++;
            if(waitTime > 6)
            {
                dead = true;                
            }
        }
    }
}

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
    public class Guest : Person
    {
        public string Id;
        public Room Room;
        public HotelRoom Destination;
        public int Delay = 0;
        public string Preference; // the guests prefered room classification
        public bool CheckedIn = false;
        public bool Evacuation = true;
        public bool _newMoveDistanceCalculated = false;
        public Guest(HotelRoom current)
        {
            this.Current = current;
            Image = Resources.Guest;
            Width = 30;
            Height = 40;
            Path = new List<HotelRoom>();
            DrawMe = new Draw();

        }
        /// <summary>
        /// calculate the shrotest path to the guests destination
        /// </summary>
        /// <param name="hotel">Give the hotel the guest resides in</param>
        /// <param name="destination">Give the hotelarea the guest want to go to</param>
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
            Current.Guests.Remove(this);
        }

        /// <summary>
        /// Moves the guest to it's destination.
        /// </summary>
        /// <param name="hotel">Give the hotel the guest resides in.</param>
        public void Walk(Hotel hotel, int hte)
        {
            if (Current != Path.ElementAt(0))
            {
                //give direction and update current room
                if (Current.Neighbours.ContainsKey(Neighbours.East) && Path[Path.IndexOf(Current) - 1] == Current.Neighbours[Neighbours.East])
                {
                    Direction = Direction.RIGHT;
                    if (Position.X > Path[Path.IndexOf(Current) - 1].RoomPosition.X + (DrawMe.standardRoomWidth / RoomPositioning))
                    {
                        Current = Path[Path.IndexOf(Current) - 1];
                    }

                }
                else if (Current.Neighbours.ContainsKey(Neighbours.West) && Path[Path.IndexOf(Current) - 1] == Current.Neighbours[Neighbours.West])
                {
                    Direction = Direction.LEFT;
                    if (Position.X < Path[Path.IndexOf(Current) - 1].RoomPosition.X + (DrawMe.standardRoomWidth / RoomPositioning))
                    {
                        Current = Path[Path.IndexOf(Current) - 1];
                    }
                }
                else if (Current.Neighbours.ContainsKey(Neighbours.South) && Path[Path.IndexOf(Current) - 1] == Current.Neighbours[Neighbours.South])
                {
                    Direction = Direction.DOWN;
                    if (Position.Y < Path[Path.IndexOf(Current) - 1].RoomPosition.Y + (DrawMe.standardRoomHeight / 2))
                    {
                        Current = Path[Path.IndexOf(Current) - 1];
                    }
                }
                else if (Current.Neighbours.ContainsKey(Neighbours.North) && Path[Path.IndexOf(Current) - 1] == Current.Neighbours[Neighbours.North])
                {
                    Direction = Direction.UP;
                    if (Position.Y > Path[Path.IndexOf(Current) - 1].RoomPosition.Y - (DrawMe.standardRoomHeight / HeightPositioning))
                    {
                        Current = Path[Path.IndexOf(Current) - 1];
                    }
                }


                if (!_newMoveDistanceCalculated) //TO DO
                {
                    MoveDistance = MoveDistance * hte;
                    _newMoveDistanceCalculated = true;
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

            if (Current == Destination)
            {
                //let the guest request a room
                if (Room == null && Destination is Reception)
                {
                    Room = ((Reception)Destination).findEmptyRoom(hotel, this);
                    if (Room == null)
                    {
                        Path.Clear();
                        setPath(hotel, hotel.Map[0, 0]);
                        hotel.Guests.Remove(this);
                    }
                    else {
                        Path.Clear();
                        setPath(hotel, Room);
                        CheckedIn = true;
                    }

                }
                else if (Room != null && Destination is Reception)//checkout if guest goes to reception while having a room
                {
                    ((Reception)Destination).checkOut(this);
                    Path.Clear();
                    setPath(hotel, hotel.Map[0, 0]);
                    CheckedIn = false;
                }

                //guest returns to room if cinema has started
                if (Destination is Cinema && !Destination.Guests.Contains(this))
                {
                    if (((Cinema)Destination).playing)
                    {
                        Path.Clear();
                        setPath(hotel, Room);
                    }
                }
                //guest returns to room if restaurant is full
                if (Destination is Restaurant && Destination.Guests.Count >= ((Restaurant)Destination).Capacity)
                {
                    Path.Clear();
                    setPath(hotel, Room);

                }

                if (!Current.Guests.Contains(this) && !(Destination is Cinema && ((Cinema)Destination).playing))
                {
                    Current.Guests.Add(this);
                }

                if (Current == hotel.Map[0, 0] && !CheckedIn)// remove guest from hotel when checked out
                {
                    Current.Guests.Remove(this);
                    hotel.Guests.Remove(this);
                }

                if (Current == hotel.Map[hotel.Map.GetLength(0) - 2, 0]) //-2 because elevator takes the last column in array  
                {
                    Path.Clear();
                    setPath(hotel, hotel.Map[0, 0]);
                }
            }
        }

        public void inLine()
        {
            waitTime++;
            if (waitTime > 6)
            {
                dead = true;
            }
        }
    }
}

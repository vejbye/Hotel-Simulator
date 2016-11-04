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
        public string GuestName { get; set; }
        public string Id { get; set; }
        public Room Room { get; set; }
        public HotelRoom Destination { get; set; }
        public string Preference { get; set; }// the guests prefered room classification
        protected bool CheckedIn { get; set; }
        public bool InQueue { get; set; }
        private int _hteCount;

        public Guest()
        {
            Image = Resources.Guest;
            Width = 30;
            Height = 40;
            Path = new List<HotelRoom>();
            DrawMe = new Draw();
            InQueue = false;
            CheckedIn = false;
            HteDuration = 1;
            _hteCount = 1;
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
        public void Walk(Hotel hotel)
        {
            if (_hteCount == HteDuration)
            {
                if (Current != Path.ElementAt(0))
                {
                    //give direction and update current room
                    if (Current.Neighbours.ContainsKey(Neighbours.East) && Path[Path.IndexOf(Current) - 1] == Current.Neighbours[Neighbours.East])
                    {
                        Direction = Directions.RIGHT;
                        if (Position.X > Path[Path.IndexOf(Current) - 1].RoomPosition.X + (DrawMe.StandardRoomWidth / RoomPositioning))
                        {
                            Current = Path[Path.IndexOf(Current) - 1];
                        }

                    }
                    else if (Current.Neighbours.ContainsKey(Neighbours.West) && Path[Path.IndexOf(Current) - 1] == Current.Neighbours[Neighbours.West])
                    {
                        Direction = Directions.LEFT;
                        if (Position.X < Path[Path.IndexOf(Current) - 1].RoomPosition.X + (DrawMe.StandardRoomWidth / RoomPositioning))
                        {
                            Current = Path[Path.IndexOf(Current) - 1];
                        }
                    }
                    else if (Current.Neighbours.ContainsKey(Neighbours.South) && Path[Path.IndexOf(Current) - 1] == Current.Neighbours[Neighbours.South])
                    {
                        Direction = Directions.DOWN;
                        if (Position.Y < Path[Path.IndexOf(Current) - 1].RoomPosition.Y + (DrawMe.StandardRoomHeight / 2))
                        {
                            Current = Path[Path.IndexOf(Current) - 1];
                        }
                    }
                    else if (Current.Neighbours.ContainsKey(Neighbours.North) && Path[Path.IndexOf(Current) - 1] == Current.Neighbours[Neighbours.North])
                    {
                        Direction = Directions.UP;
                        if (Position.Y > Path[Path.IndexOf(Current) - 1].RoomPosition.Y - (DrawMe.StandardRoomHeight / HeightPositioning))
                        {
                            Current = Path[Path.IndexOf(Current) - 1];
                        }
                    }


                    //move guest accordingly
                    if (Direction == Directions.RIGHT)
                        Position.X += MoveDistance;
                    if (Direction == Directions.UP)
                        Position.Y += MoveDistance;
                    if (Direction == Directions.DOWN)
                        Position.Y -= MoveDistance;
                    if (Direction == Directions.LEFT)
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
                        else
                        {
                            Path.Clear();
                            setPath(hotel, Room);
                            CheckedIn = true;
                        }

                    }
                    else if (Room != null && Destination is Reception)//checkout if guest goes to reception while having a room
                    {
                        ((Reception)Destination).CheckOut(this);
                        Path.Clear();
                        setPath(hotel, hotel.Map[0, 0]);
                        CheckedIn = false;
                    }

                    //guest returns to room if cinema has started
                    if (Destination is Cinema && !Destination.Guests.Contains(this))
                    {
                        if (((Cinema)Destination).Playing)
                        {
                            Path.Clear();
                            setPath(hotel, Room);
                        }
                    }
                    //guest returns to room if restaurant is full
                    if (Destination is Restaurant && Destination.Guests.Count >= ((Restaurant)Destination).Capacity)
                    {
                        if (((Restaurant)Destination).Waitingline.Count < 3)
                        {
                            if (((Restaurant)Destination).Waitingline.Contains(this))
                            {
                                ((Restaurant)Destination).Waitingline.Enqueue(this);
                                InQueue = true;
                            }
                        }
                        else
                        {
                            Path.Clear();
                            setPath(hotel, Room);
                        }

                    }

                    if (!Current.Guests.Contains(this) && !(Destination is Cinema && ((Cinema)Destination).Playing))
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

                _hteCount = 1;
            }
            else
                _hteCount++;
        }

        public void InLine()
        {
            WaitTime++;
            if (WaitTime > 6)
            {
                Dead = true;
                if (Current != null)
                {
                    Current.Guests.Remove(this);
                }
            }
        }
    }
}

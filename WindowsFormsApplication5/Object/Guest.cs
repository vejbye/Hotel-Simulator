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
        public string GuestName { get; set; }// the name of the guest
        public string Id { get; set; } //the guests unqiue ID
        public Room Room { get; set; } //the room the guest obtained
        public string Preference { get; set; }// the guests prefered room classification
        public bool CheckedIn { get; set; }// for checking if a guest has checked in
        public int EatingDuration { get; set; } //how long the guest does over eating
        private int _hteCount;
        private int _queueCount; //how long the guest is waiting in line
        public int EatingHTE; //how long before the guest is finished
        public int FitnessHTE; // how long the guest does over fitness


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
            EatingHTE = 1;
            _hteCount = 1;
            _queueCount = 3;
            FitnessHTE = 0;
            CurrentFloor = 0;
        }
        /// <summary>
        /// calculate the shrotest path to the guests destination
        /// </summary>
        /// <param name="hotel">Give the hotel the guest resides in</param>
        /// <param name="destination">Give the hotelarea the guest want to go to</param>
        public void setPath(Hotel hotel, HotelRoom destination)
        {
            PathFind pf = new PathFind();
            pf.ShortestPathDijkstra(Current, destination); //algorithm to define shortest path
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

        // provides the guest with a direction to walk in
        public void setDirection()
        {
            foreach (KeyValuePair<Neighbours, HotelRoom> kvp in Current.Neighbours)
            {
                    if (Path[Path.IndexOf(Current) - 1] == kvp.Value)
                    {

                        Direction = (Directions)kvp.Key;
                    }
            }
        }

        /// <summary>
        /// Moves the guest to it's destination.
        /// </summary>
        /// <param name="hotel">Give the hotel the guest resides in.</param>
        public override void Walk(Hotel hotel)
        {
            if (_hteCount == HteDuration)
            {
                if (Current != Path.ElementAt(0))
                {
                    //give direction
                    setDirection();


                    //move guest accordingly and update current room
                    if (Direction == Directions.East)
                    {
                        if (Position.X > Path[Path.IndexOf(Current) - 1].RoomPosition.X + (DrawMe.StandardRoomWidth / RoomPositioning))
                        {
                            Current = Path[Path.IndexOf(Current) - 1];
                        }
                        Position.X += MoveDistance;
                    }
                    if (Direction == Directions.North) //Actually south
                    {
                        if (CurrentFloor == hotel.Elevator.Floor && Current is ElevatorShaft)
                        {
                            if (!hotel.Elevator.PersonsInElevator.Contains(this))
                            {
                                InQueue = true;// guest starts waiting for elevator
                                //guest requests the elevator here
                                if (hotel.LayoutAtZero())
                                {
                                    hotel.Elevator.AddRequest(Current.Floor);
                                    hotel.Elevator.AddRequest(Destination.Floor + 1);
                                    hotel.Elevator.Destination -= DrawMe.StandardRoomHeight * 2;
                                }
                                else
                                {
                                    hotel.Elevator.AddRequest(Current.Floor);
                                    hotel.Elevator.AddRequest(Destination.Floor);
                                }
                            }
                            if (hotel.Elevator.ElevatorPosition.Y == hotel.Elevator.Destination + MoveDistance)// when destination is reached move guest accordingly
                            {
                                for (int i = Path.Count - 1; i > 0; i--)
                                {
                                    if (Path[i].Floor != Destination.Floor)
                                    {
                                        if (Path[i] is ElevatorShaft)
                                            if (hotel.LayoutAtZero())
                                                Position.Y += DrawMe.StandardRoomHeight; //change guests position to the next floor
                                            else
                                                Position.Y += DrawMe.StandardRoomHeight;
                                    }
                                    else
                                    {
                                        Current = Path[i];
                                        hotel.Elevator.PersonsInElevator.Remove(this); //let the guest exit
                                        break;
                                    }
                                }

                            }
                        }
                        else if (Current is Stair)
                        {
                            if (Position.Y < Path[Path.IndexOf(Current) - 1].RoomPosition.Y + (DrawMe.StandardRoomHeight / 2))
                            {
                                Current = Path[Path.IndexOf(Current) - 1];
                            }
                            Position.Y += MoveDistance;
                        }
                    }
                        if (Direction == Directions.South) //Actually north
                    {
                        if (CurrentFloor == hotel.Elevator.Floor && Current is ElevatorShaft)
                        {
                            if (!hotel.Elevator.PersonsInElevator.Contains(this))
                            {
                                InQueue = true; // guest starts waiting for elevator
                                //guest requests the elevator here
                                if (hotel.LayoutAtZero())
                                {
                                    hotel.Elevator.AddRequest(Current.Floor);
                                    hotel.Elevator.AddRequest(Destination.Floor + 1);
                                    hotel.Elevator.Destination -= DrawMe.StandardRoomHeight * 2;
                                }
                                else
                                {
                                    hotel.Elevator.AddRequest(Current.Floor);
                                    hotel.Elevator.AddRequest(Destination.Floor);
                                }
                            }
                            if (hotel.Elevator.ElevatorPosition.Y == hotel.Elevator.Destination + MoveDistance) // when destination is reached move guest accordingly
                            {
                                for (int i = Path.Count -1; i > 0; i--)
                                {
                                    if(Path[i].Floor != Destination.Floor)
                                    {
                                        if(Path[i] is ElevatorShaft)
                                             if (hotel.LayoutAtZero())
                                                Position.Y -= DrawMe.StandardRoomHeight;
                                            else
                                        Position.Y -= DrawMe.StandardRoomHeight;
                                    }
                                    else
                                    {
                                        Current = Path[i];
                                        hotel.Elevator.PersonsInElevator.Remove(this); //let the guest exit
                                        break;
                                    }
                                }
                               
                            }
                        }
                        else if (Current is Stair)
                        {
                            if (Position.Y < Path[Path.IndexOf(Current) - 1].RoomPosition.Y + (DrawMe.StandardRoomHeight / 2))
                            {
                                Current = Path[Path.IndexOf(Current) - 1];
                            }
                            Position.Y -= MoveDistance;
                        }
                    }
                    if (Direction == Directions.West)
                    {
                        if (Position.X < Path[Path.IndexOf(Current) - 1].RoomPosition.X + (DrawMe.StandardRoomWidth / RoomPositioning))
                        {
                            Current = Path[Path.IndexOf(Current) - 1];
                        }
                        Position.X -= MoveDistance;
                    }
                }

                if (Current == Destination)
                {
                    //let the guest request a room
                    if (Room == null && Destination is Reception)
                    {
                        Room = ((Reception)Destination).FindEmptyRoom(hotel, this);
                        if (Room == null) //if there is no room guests leaves
                        {
                            Path.Clear();
                            setPath(hotel, hotel.Map[0, 0]);
                            hotel.Guests.Remove(this);
                        }
                        else //guest goes to room
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
                        if (((Restaurant)Destination).Waitingline.Count < _queueCount)
                        {
                            if (!((Restaurant)Destination).Waitingline.Contains(this))
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

                    if (Destination is Gym && FitnessHTE > 0) //Guest starts fitness;
                    {
                        HteDuration = FitnessHTE;
                        FitnessHTE = 0;
                    }

                    //adds guest to a hotelroom if there is room for him (and if a movie ís not playing in case of cinema)
                    if (!Current.Guests.Contains(this) && !(Destination is Cinema && ((Cinema)Destination).Playing) && !(Destination is Restaurant && ((Restaurant)Destination).Guests.Count >= ((Restaurant)Destination).Capacity))
                    {
                        Current.Guests.Add(this);
                    }

                    if (Destination is Restaurant && ((Restaurant)Destination).Guests.Count < ((Restaurant)Destination).Capacity) //guests starts eating
                    {
                        Eating();
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

        /// <summary>
        /// Time for lunch!
        /// </summary>
        public void Eating()
        {
            if (EatingHTE == EatingDuration)
            {
                Console.WriteLine("Guest finished eating.");
                EatingHTE = 1;
            }
            else
                EatingHTE++;
        }
    }
}

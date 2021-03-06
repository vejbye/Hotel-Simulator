﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication5.Properties;
using WindowsFormsApplication5;

namespace HotelSimulator.Object
{
    public class Maid : Person
    {
        public bool Evacuation { get; set; } // let the maid know if he needs to evacuate
        public int CleaningDuration { get; set; } // how much time the maid already spent cleaning the room
        public int CleaningHTE { get; set; } //  how long it takes to clean the room
        public Maid(HotelRoom current)
        {
            Current = current;
            Image = Resources.Maid;
            Width = 20;
            Height = 40;
            Path = new List<HotelRoom>();
            DrawMe = new Draw();
            Evacuation = false;
            CleaningDuration = 1;
        }
        
        /// <summary>
        /// Calculate the shrotest path to the maids destination
        /// </summary>
        /// <param name="hotel">Give the hotel the maid works in</param>
        public void SetPath(Hotel hotel)
        {
            PathFind pf = new PathFind();
            if (Evacuation)
            {
                HotelRoom cur = null;
                if (Current == hotel.Map[hotel.Map.GetLength(0) - 2, 0])
                {
                    pf.ShortestPathDijkstra(Current, hotel.Map[0, 0]);//algorithm to define shortest path}
                    cur = hotel.Map[0, 0];
                }
                else
                {
                    pf.ShortestPathDijkstra(Current, hotel.Map[hotel.Map.GetLength(0) - 2, 0]);//algorithm to define shortest path}
                    cur = hotel.Map[hotel.Map.GetLength(0) - 2, 0];
                }
                while (cur != Current)// store path in list so maid can walk through it
                {
                    Path.Add(cur);
                    cur = cur.Previous;
                }
                Path.Add(cur);
            }
            else
            {
                if (CleaningDuration == CleaningHTE)
                {
                    foreach (HotelRoom hm in hotel.Map)// search for dirty room
                    {
                        if (hm is Room && (((Room)hm).Dirty == true))
                        {
                            pf.ShortestPathDijkstra(Current, hm);//algorithm to define shortest path
                            HotelRoom cur = hm;
                            Destination = hm;
                            while (cur != Current)// store path in list so maid can walk through it
                            {
                                Path.Add(cur);
                                cur = cur.Previous;
                            }
                            Path.Add(cur);
                            ((Room)hm).BeingCleaned = true;
                            ((Room)hm).Dirty = false;
                            break;
                        }
                    }
                    CleaningDuration = 1;
                }
                else
                    CleaningDuration++;
            }

                foreach (HotelRoom hr in hotel.Map)
                {
                    //clear any path related values after path has been stored
                    hr.Previous = null;
                    hr.Distance = Int32.MaxValue;
                }
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
        /// Moves the maid to it's destination.
        /// </summary>
        /// <param name="hotel">Give the hotel the maid works for.</param>
        public override void Walk(Hotel hotel)
        {
            if (Path.Count > 0 && Current != Path.ElementAt(0))
            {
                //give direction
                setDirection();


                //move maid accordingly and update current room
                if (Direction == Directions.East)
                {
                    if (Position.X > Path[Path.IndexOf(Current) - 1].RoomPosition.X + (DrawMe.StandardRoomWidth / RoomPositioning))
                    {
                        Current = Path[Path.IndexOf(Current) - 1];
                    }
                    Position.X += MoveDistance;
                }
                if (Direction == Directions.North)
                {
                    if (CurrentFloor == hotel.Elevator.Floor && Current is ElevatorShaft)
                    {
                        if (!hotel.Elevator.PersonsInElevator.Contains(this))
                        {
                            InQueue = true; // miaid starts waiting for elevator
                            //maid requests the elevator here
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
                        if (hotel.Elevator.ElevatorPosition.Y == hotel.Elevator.Destination + MoveDistance) //when elevator reaches destination move maid accordingly
                        {
                            for (int i = Path.Count - 1; i > 0; i--)
                            {
                                if (Path[i].Floor != Destination.Floor)
                                {
                                    if (Path[i] is ElevatorShaft)
                                        if (hotel.LayoutAtZero())
                                            Position.Y += DrawMe.StandardRoomHeight;
                                        else
                                            Position.Y += DrawMe.StandardRoomHeight;
                                }
                                else
                                {
                                    Current = Path[i];
                                    hotel.Elevator.PersonsInElevator.Remove(this); //let the maid exit the elevator
                                    break;
                                }
                            }

                        }
                    }
                }
                    if (Direction == Directions.South) //Actually north
                {
                    if (CurrentFloor == hotel.Elevator.Floor && Current is ElevatorShaft)
                    {
                        if (!hotel.Elevator.PersonsInElevator.Contains(this))
                        {
                            InQueue = true;// miaid starts waiting for elevator
                            //maid requests the elevator here
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
                        if (hotel.Elevator.ElevatorPosition.Y == hotel.Elevator.Destination + MoveDistance)  //when elevator reaches destination move maid accordingly
                        {
                            for (int i = Path.Count - 1; i > 0; i--)
                            {
                                if (Path[i].Floor != Destination.Floor)
                                {
                                    if (Path[i] is ElevatorShaft)
                                        if (hotel.LayoutAtZero())
                                            Position.Y -= DrawMe.StandardRoomHeight;
                                        else
                                            Position.Y -= DrawMe.StandardRoomHeight;
                                }
                                else
                                {
                                    Current = Path[i];
                                    hotel.Elevator.PersonsInElevator.Remove(this); //let the maid exit the elevator
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

            if(Path.Count > 0 && Current == Path.ElementAt(0) && !(Evacuation && Current == hotel.Map[0,0])) //start cleaning the room if maid has arrived
            {
                if (Current is Room)
                    ((Room)Path.ElementAt(0)).BeingCleaned = false;

                Path.Clear();
                SetPath(hotel);
            }

            if(Path.Count <= 0) //look for a new dirty room when finished
            {
                Path.Clear();
                SetPath(hotel);
            }
        }

        public void InLine(Hotel hotel)
        {
            WaitTime++;
            if (WaitTime > 6)
            {
                Dead = true;
                Path.Clear();
                Position = new System.Drawing.Point(DrawMe.XStartPosition + Width, DrawMe.YStartPosition - Height);
                Current = hotel.GetMap()[0, 0];
                Dead = false;
                SetPath(hotel);
            }
        }
    }
}

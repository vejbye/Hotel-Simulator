using System;
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
        public bool Evacuation { get; set; }
        public int CleaningDuration { get; set; }
        public int CleaningHTE { get; set; }
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
                    if (Position.Y > Path[Path.IndexOf(Current) - 1].RoomPosition.Y - (DrawMe.StandardRoomHeight / HeightPositioning))
                    {
                        Current = Path[Path.IndexOf(Current) - 1];
                    }
                    Position.Y += MoveDistance;
                }
                if (Direction == Directions.South)
                {
                    if (CurrentFloor == hotel.Elevator.Floor && Current is ElevatorShaft)
                    {
                        if (!hotel.Elevator.PersonsInElevator.Contains(this))
                        {
                            InQueue = true;
                            //hotel.Elevator.AddRequest(Current.Floor);
                            if (hotel.LayoutAtZero())
                            {
                                //Destination.Floor++;
                                hotel.Elevator.AddRequest(Destination.Floor + 1);
                                hotel.Elevator.Destination -= DrawMe.StandardRoomHeight * 2;
                            }
                            else
                                hotel.Elevator.AddRequest(Destination.Floor);
                        }
                        if (hotel.Elevator.ElevatorPosition.Y == hotel.Elevator.Destination + MoveDistance)
                        {
                            for (int i = Path.Count - 1; i > 0; i--)
                            {
                                if (Path[i].Floor != Destination.Floor)
                                {
                                    if (Path[i] is ElevatorShaft)
                                        if (hotel.LayoutAtZero())
                                            Position.Y = (int)(hotel.Elevator.Destination - (hotel.Elevator.Destination * 0.023) + (DrawMe.StandardRoomHeight));
                                        else
                                            Position.Y = (int)(hotel.Elevator.Destination - (hotel.Elevator.Destination * 0.023));
                                }
                                else
                                {
                                    Current = Path[i];
                                    Position.Y -= (DrawMe.StandardRoomHeight / 2);
                                    hotel.Elevator.PersonsInElevator.Remove(this);
                                    break;
                                }
                            }

                        }
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

            if(Path.Count > 0 && Current == Path.ElementAt(0) && !(Evacuation && Current == hotel.Map[0,0]))
            {
                if (Current is Room)
                    ((Room)Path.ElementAt(0)).BeingCleaned = false;

                Path.Clear();
                SetPath(hotel);
            }

            if(Path.Count <= 0)
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

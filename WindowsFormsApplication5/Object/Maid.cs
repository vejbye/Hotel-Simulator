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
                    pf.shortestPathDijkstra(Current, hotel.Map[0, 0]);//algorithm to define shortest path}
                    cur = hotel.Map[0, 0];
                }
                else
                {
                    pf.shortestPathDijkstra(Current, hotel.Map[hotel.Map.GetLength(0) - 2, 0]);//algorithm to define shortest path}
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
                            pf.shortestPathDijkstra(Current, hm);//algorithm to define shortest path
                            HotelRoom cur = hm;
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

        /// <summary>
        /// Moves the maid to it's destination.
        /// </summary>
        /// <param name="hotel">Give the hotel the maid works for.</param>
        public void Walk(Hotel hotel)
        {
            if (Path.Count > 0 && Current != Path.ElementAt(0))
            {
                //Give direction and update current room
                if (Current.Neighbours.ContainsKey(Neighbours.East) && Path[Path.IndexOf(Current) - 1] == Current.Neighbours[Neighbours.East])
                {
                    Direction = Directions.RIGHT;
                    if (Position.X > Path[Path.IndexOf(Current) - 1].RoomPosition.X + (DrawMe.StandardRoomWidth / RoomPositioning))
                        Current = Path[Path.IndexOf(Current) - 1];
                    }
                else if (Current.Neighbours.ContainsKey(Neighbours.West) && Path[Path.IndexOf(Current) - 1] == Current.Neighbours[Neighbours.West])
                {
                    Direction = Directions.LEFT;
                    if (Position.X < Path[Path.IndexOf(Current) - 1].RoomPosition.X + (DrawMe.StandardRoomWidth / RoomPositioning))
                        Current = Path[Path.IndexOf(Current) - 1];
                    }
                else if (Current.Neighbours.ContainsKey(Neighbours.South) && Path[Path.IndexOf(Current) - 1] == Current.Neighbours[Neighbours.South])
                {
                    Direction = Directions.DOWN;
                    if (Position.Y < Path[Path.IndexOf(Current) - 1].RoomPosition.Y + (DrawMe.StandardRoomHeight / 2))
                        Current = Path[Path.IndexOf(Current) - 1];
                    }
                else if (Current.Neighbours.ContainsKey(Neighbours.North) && Path[Path.IndexOf(Current) - 1] == Current.Neighbours[Neighbours.North])
                {
                    Direction = Directions.UP;
                    if (Position.Y > Path[Path.IndexOf(Current) - 1].RoomPosition.Y - (DrawMe.StandardRoomHeight / HeightPositioning))
                        Current = Path[Path.IndexOf(Current) - 1];
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
                Dead = false;
                SetPath(hotel);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication5.Properties;
using WindowsFormsApplication5;

namespace HotelSimulator.Object
{
    public class Maid : Moveable
    {
        public bool isBusy = false;
        public Maid(HotelRoom current)
        {
            this.Current = current;
            Image = Resources.Maid;
            Width = 20;
            Height = 40;
            Path = new List<HotelRoom>();
            DrawMe = new Draw();
        }

        public void setPath(Hotel hotel)
        {
            foreach (HotelRoom hm in hotel.Map)// search for dirty room
            {
                if (hm is Room && ((Room)hm).Dirty == true)
                {
                    PathFind pf = new PathFind();
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
                    foreach (HotelRoom hr in hotel.Map)
                    {
                        //clear any path related values after path has been stored
                        hr.Previous = null;
                        hr.Distance = Int32.MaxValue;
                    }
                    break;
                }
            }
        }
        public void Walk(Hotel hotel)
        {
            if (Path.Count > 0 && Current != Path.ElementAt(0))
            {
                //give direction and update current room
                if (Current.Neighbours.ContainsKey(Neighbours.East) && Path[Path.IndexOf(Current) - 1] == Current.Neighbours[Neighbours.East])
                {
                    Direction = Direction.RIGHT;
                    if (Position.X > Path[Path.IndexOf(Current) - 1].RoomPosition.X + 10)
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

            if(Path.Count > 0 && Current == Path.ElementAt(0))
            {
                isBusy = false;
                ((Room)Path.ElementAt(0)).BeingCleaned = false;
            }


        }
    }
}

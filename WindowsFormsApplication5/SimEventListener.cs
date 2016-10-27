using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelEvents;
using HotelSimulator.Object;

namespace HotelSimulator
{
    public class SimEventListener : HotelEventListener
    {
        Hotel Hotel;
        HotelSimulator hs;
        public List<Guest> Guests;
        public Queue<HotelEvent> events;
        public SimEventListener(Hotel hotel, HotelSimulator hs)
        {
            this.Hotel = hotel;
            this.hs = hs;
            Guests = new List<Guest>();
            events = new Queue<HotelEvent>();
        
        }
        public void Notify(HotelEvent evt)
        {

            events.Enqueue(evt);

        }

        public void DoEvent() {
            if (events.Count != 0)
            {
                HotelEvent evt = events.Peek();
                switch (evt.EventType)
                {

                    case HotelEventType.CHECK_IN:
                        foreach (KeyValuePair<string, string> data in evt.Data)
                        {

                            Console.WriteLine(data.Key.Substring(4) + "," + data.Value);
                        }
                        Guest guest = new Guest(null);
                        guest.Id = evt.Data.Keys.ElementAt(0).Substring(4);
                        guest.preference = evt.Data.Values.ElementAt(0).Substring(8,1);
                        Hotel.Guests.Add(guest);
                        guest.Current = ((HotelRoom)Hotel.Map[0, 0]);
                        guest.Position = new System.Drawing.Point(Hotel.DrawMe.xStartPosition + guest.Width, Hotel.DrawMe.yStartPosition - guest.Height);
                        Reception r = null;
                        foreach (HotelRoom hr in Hotel.Map)
                        {
                            if (hr is Reception)
                            {
                                r = (Reception)hr;
                                break;
                            }
                        }
                        guest.setPath(Hotel, r);
                        //guest.Walk(Hotel, hs, r);
                        //hs.Refresh();
                        Console.WriteLine("Guest has checked in");
                        break;

                    case HotelEventType.CHECK_OUT:
                           foreach (Guest g in Hotel.Guests)
                           {
                               if (g.Id == evt.Data.Values.ElementAt(0))
                               {
                                   foreach (HotelRoom room in Hotel.Map)
                                   {
                                       if (room is Reception)
                                       {
                                        g.Path.Clear();
                                        g.setPath(Hotel, room);
                                           break;
                                       }
                                   }
                                break;
                               }
                           }
                        Console.WriteLine("Guest has checked out");
                        break;
                    case HotelEventType.CLEANING_EMERGENCY:
                        foreach(HotelRoom room in Hotel.Map)
                        {
                            foreach (KeyValuePair<string, string> data in evt.Data)
                            {

                                if (data.Key.Equals("kamer") && room.Id == Int32.Parse(data.Value))
                                {
                                    ((Room)room).Dirty = true;
                                }
                            }
                        }

                        foreach (Maid maid in Hotel.Maids)
                        {
                            maid.Path.Clear();
                            maid.setPath(Hotel);
                        }

                        Console.WriteLine("there is a cleaning emergency");
                        break;
                    case HotelEventType.EVACUATE:
                        Console.WriteLine(evt.Message);
                        foreach (Guest g in Hotel.Guests)
                        {
                                        g.Path.Clear();
                                        g.setPath(Hotel, Hotel.Map[0,0]);
                                        break;                                
                        }
                        Console.WriteLine("fly, you fools!");
                        break;
                    case HotelEventType.GODZILLA:
                        Console.WriteLine("it will kill us all!");
                        break;
                    case HotelEventType.GOTO_CINEMA:
                             foreach (Guest g in Hotel.Guests)
                             {
                                 if (g.Id == evt.Data.Values.ElementAt(0))
                                 {
                                     foreach (HotelRoom room in Hotel.Map)
                                     {
                                         if (room is Cinema)
                                         {
                                        g.Path.Clear();
                                        g.setPath(Hotel, room);
                                        g.Walk(Hotel, hs, room);
                                             break;
                                         }
                                     }
                                 }
                             }
                        Console.WriteLine("Guest is going to cinema");
                        break;
                    case HotelEventType.GOTO_FITNESS:
                          foreach (Guest g in Hotel.Guests)
                          {
                              if (g.Id == evt.Data.Values.ElementAt(0))
                              {
                                  foreach (HotelRoom room in Hotel.Map)
                                  {
                                      if (room is Gym)
                                      {
                                        g.Path.Clear();
                                        g.setPath(Hotel, room);
                                        g.Walk(Hotel, hs, room);
                                          break;
                                      }
                                  }
                              }
                          }
                        Console.WriteLine("Guest is going to the gym");
                        break;
                    case HotelEventType.NEED_FOOD:
                        foreach(Guest g in Hotel.Guests)
                        {
                            if(g.Id == evt.Data.Values.ElementAt(0))
                            {
                                foreach(HotelRoom room in Hotel.Map)
                                {
                                    if(room is Restaurant)
                                    {
                                        g.Path.Clear();
                                        g.setPath(Hotel, room);
                                        g.Walk(Hotel, hs, room);
                                        break;
                                    }
                                }
                            }
                        }
                        Console.WriteLine(evt.Message);
                        Console.WriteLine("Guest is hungry and is going to the restaurant");
                        break;
                    case HotelEventType.START_CINEMA:
                        foreach (KeyValuePair<string, string> data in evt.Data)
                        {

                            Console.WriteLine(data.Key + "," + data.Value);
                        }
                        Console.WriteLine(evt.Message);
                        Console.WriteLine("a movie has started");
                        break;
                    case HotelEventType.NONE:
                        Console.WriteLine(evt.Message);
                        Console.WriteLine("nothing happens");
                        break;
                    default:
                        Console.WriteLine("this should not happen");
                        break;
                }

                events.Dequeue();
            }
        }
    }
}

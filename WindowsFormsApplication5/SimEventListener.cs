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
        private Hotel _hotel;
        private HotelSimulator _hs;
        public Queue<HotelEvent> events;
        private int _guestCount = 0;

        public SimEventListener(Hotel hotel, HotelSimulator hs)
        {
            _hotel = hotel;
            _hs = hs;
            events = new Queue<HotelEvent>();
        }
        /// <summary>
        /// gets events and stores it in a queue
        /// </summary>
        /// <param name="evt">Give the event to store</param>
        public void Notify(HotelEvent evt)
        {
            events.Enqueue(evt);
        }
        /// <summary>
        /// handle the hotelevents stored in the queue
        /// </summary>
        public void DoEvent(int hteDuration)
        {
            if (events.Count != 0)
            {
                HotelEvent evt = events.Peek();
                switch (evt.EventType)
                {
                    case HotelEventType.CHECK_IN:
                        Guest guest = new Guest(null);
                        _guestCount++;
                        guest.guestName = String.Format("Guest {0}", _guestCount);
                        guest.Id = evt.Data.Keys.ElementAt(0).Substring(4);
                        guest.Preference = evt.Data.Values.ElementAt(0).Substring(8, 1);
                        _hotel.Guests.Add(guest);
                        guest.Current = ((HotelRoom)_hotel.Map[0, 0]);
                        guest.Position = new System.Drawing.Point(_hotel.DrawMe.XStartPosition + guest.Width, _hotel.DrawMe.YStartPosition - guest.Height);
                        guest.HteDuration = hteDuration;
                        Reception r = null;
                        foreach (HotelRoom hr in _hotel.Map)
                        {
                            if (hr is Reception)
                            {
                                r = (Reception)hr;
                                break;
                            }
                        }
                        guest.setPath(_hotel, r);
                        Console.WriteLine("Guest has checked in");
                        break;
                    case HotelEventType.CHECK_OUT:
                        foreach (Guest g in _hotel.Guests)
                        {
                            if (g.Id == evt.Data.Values.ElementAt(0))
                            {
                                _guestCount--;
                                foreach (HotelRoom room in _hotel.Map)
                                {
                                    if (room is Reception)
                                    {
                                        if (g.Current == g.Destination)
                                        {
                                            g.Path.Clear();
                                            g.setPath(_hotel, room);
                                            break;
                                        }
                                    }
                                }
                                break;
                            }
                        }
                        Console.WriteLine("Guest has checked out");
                        break;
                    case HotelEventType.CLEANING_EMERGENCY:
                        foreach (HotelRoom room in _hotel.Map)
                        {
                            foreach (KeyValuePair<string, string> data in evt.Data)
                            {
                                if (data.Key.Equals("kamer") && room.Id == Int32.Parse(data.Value))
                                {
                                    ((Room)room).Dirty = true;
                                }
                            }
                        }
                        Console.WriteLine("there is a cleaning emergency");
                        break;
                    case HotelEventType.EVACUATE:
                        Console.WriteLine(evt.Message);
                        foreach (Guest g in _hotel.Guests)
                        {
                            g.Path.Clear();
                            g.setPath(_hotel, _hotel.Map[_hotel.Map.GetLength(0) - 2, 0]); //-2 because elevator takes the last column in array
                        }
                        foreach(Maid maid in _hotel.Maids)
                        {
                            maid.IsBusy = false;
                            maid.Evacuation = true;
                            maid.Path.Clear();
                            maid.setPath(_hotel);
                        }
                        _hotel.Elevator.Requests.Clear();
                        _hs.CurrentElement = 0;
                        _hotel.Elevator.Requests.Add(1);
                        Console.WriteLine("fly, you fools!");
                        break;
                    case HotelEventType.GODZILLA:
                        Console.WriteLine("it will kill us all!");
                        break;
                    case HotelEventType.GOTO_CINEMA:
                        foreach (Guest g in _hotel.Guests)
                        {
                            if (g.Id == evt.Data.Values.ElementAt(0))
                            {
                                foreach (HotelRoom room in _hotel.Map)
                                {
                                    if (room is Cinema && !((Cinema)room).playing)
                                    {
                                        if (g.Current == g.Destination)
                                        {
                                            g.Path.Clear();
                                            g.setPath(_hotel, room);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        Console.WriteLine("Guest is going to cinema");
                        break;
                    case HotelEventType.GOTO_FITNESS:
                        foreach (Guest g in _hotel.Guests)
                        {
                            if (g.Id == evt.Data.Values.ElementAt(0))
                            {
                                foreach (HotelRoom room in _hotel.Map)
                                {
                                    if (room is Gym)
                                    {
                                        if (g.Current == g.Destination)
                                        {
                                            g.Path.Clear();
                                            g.setPath(_hotel, room);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        Console.WriteLine("Guest is going to the gym");
                        break;
                    case HotelEventType.NEED_FOOD:
                        foreach (Guest g in _hotel.Guests)
                        {
                            if (g.Id == evt.Data.Values.ElementAt(0))
                            {
                                foreach (HotelRoom room in _hotel.Map)
                                {
                                    if (room is Restaurant)
                                    { if (g.Current == g.Destination)
                                        {
                                            g.Path.Clear();
                                            g.setPath(_hotel, room);
                                            break;
                                        }
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
                            foreach(HotelRoom room in _hotel.Map)
                            {
                                if(room is Cinema && ((Cinema)room).Id == Int32.Parse(data.Value))
                                {
                                    ((Cinema)room).playing = true;
                                }
                            }
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

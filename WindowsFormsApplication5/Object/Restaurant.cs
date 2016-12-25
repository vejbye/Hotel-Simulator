using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication5.Properties;

namespace HotelSimulator.Object
{
    public class Restaurant : HotelRoom
    {
        public int Capacity { get; set; }
        public Queue<Guest> Waitingline { get; set; }

        public Restaurant()
        {
            Image = Resources.Restaurant;
            Waitingline = new Queue<Guest>();
        }

        public void HandleWaitingline()
        {
            //As long as there is space in the restaurant, a guest can eat
            while (Guests.Count < Capacity)
            {
                //The first one in the queue gets a seat in the restaurant and starts eating
                if (Waitingline.Count > 0)
                {
                    Guest guest = Waitingline.Peek();
                    Guests.Add(guest);
                    guest.Eating();
                    Waitingline.Dequeue();
                }
                else
                {
                    break;
                }
            }
        }

    }
}

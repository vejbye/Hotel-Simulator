using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication5.Properties;

namespace HotelSimulator.Object
{
    public class Restaurant: HotelRoom
    {
        public int Capacity { get; set; }
        public Queue<Guest> waitingline { get; set; }

        public Restaurant()
        {
            Image = Resources.Restaurant;
            waitingline = new Queue<Guest>();
        }

        public void handleWaitingline()
        {
            while(Guests.Count < Capacity)
            {
                if (waitingline.Count > 0)
                {
                    Guest guest = waitingline.Peek();
                    Guests.Add(guest);
                    waitingline.Dequeue();
                }
                else
                {
                    break;
                }
            }
        }
        
    }
}

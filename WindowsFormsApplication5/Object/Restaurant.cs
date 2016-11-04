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
        public Queue<Guest> Waitingline { get; set; }

        public Restaurant()
        {
            Image = Resources.Restaurant;
            Waitingline = new Queue<Guest>();
        }

        public void HandleWaitingline()
        {
            while(Guests.Count < Capacity)
            {
                if (Waitingline.Count > 0)
                {
                    Guest guest = Waitingline.Peek();
                    Guests.Add(guest);
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication5.Properties;
using System.Drawing;

namespace HotelSimulator.Object
{
    public class Elevator: HotelRoom
    {
        public int Speed { get; set; }  
        public Point Position;

        private new Draw DrawMe;
        private List<int> _requests;

        public Elevator()
        {
            Image = Resources.Elevator;
            Height = 50;
            Width = 35;
            DrawMe = new Draw();
            Position = new Point((int)(DrawMe.xStartPosition + (DrawMe.standardRoomWidth * 0.65)), (DrawMe.yStartPosition - DrawMe.standardRoomHeight));
            

            _requests = new List<int>();
            _requests.Add(3);
            _requests.Add(2);
            _requests.Add(6);
            _requests.Add(1);
            _requests.Add(5);
            _requests.Sort();
            _requests.ForEach(i => Console.Write("{0}\n", i));
        }

        private void CalculateSeekingTime()
        {

        }

        
    }
}

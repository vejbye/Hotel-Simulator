using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApplication5.Properties;
using System.Drawing;

namespace HotelSimulator.Object
{
    public class Elevator : HotelRoom
    {
        public int Speed { get; set; }
        public Point ElevatorPosition;
        public int Destination;
        public int RequestedFloor;
        public ElevatorState CurrentState;
        public int PreviousFloor = 0;

        public List<int> Requests;

        public enum ElevatorState
        {
            MovingUp,
            MovingDown,
            Idle
        }

        public Elevator()
        {
            Image = Resources.Elevator;
            CurrentState = ElevatorState.Idle;
            Height = 50;
            Width = 35;
            DrawMe = new Draw();
            ElevatorPosition = new Point((int)(DrawMe.xStartPosition + (DrawMe.standardRoomWidth * 0.65)), (DrawMe.yStartPosition - DrawMe.standardRoomHeight));


            Requests = new List<int>();
            Requests.Add(3);
            Requests.Add(2);
            Requests.Add(6);
            Requests.Add(1);
            Requests.Add(5);
            Requests.Add(3);

            //_requests.Sort();
            //_requests.ForEach(i => Console.Write("{0}\n", i));
        }

        private void CalculateSeekingTime()
        {

        }


    }
}

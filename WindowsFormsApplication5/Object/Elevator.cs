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
        public List<Person> PersonsInElevator { get; set; }
        public Point ElevatorPosition;
        public int Destination;
        public int RequestedFloor;
        public ElevatorState CurrentState;

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
            Speed = 1;
            DrawMe = new Draw();
            ElevatorPosition = new Point((int)(DrawMe.XStartPosition + (DrawMe.StandardRoomWidth * 0.65)), (DrawMe.YStartPosition - DrawMe.StandardRoomHeight));
            PersonsInElevator = new List<Person>();


            Requests = new List<int>();
            
            
        }


        /// <summary>
        /// Moves the elevator in steps.
        /// </summary>
        /// <param name="hotel">The hotel where the elevator is in.</param>
        /// <param name="hotelElevator"></param>
        /// <param name="floor"></param>
        public void MoveElevator(Hotel hotel, int floor, int elevatorHTE)
        {
            if (!hotel.LayoutAtZero())
                floor++; ; //Starts at floor 0, so add 1 to cancel this effect out

            if (Speed == elevatorHTE)
            {
                if (hotel.Elevator.ElevatorPosition.Y != DrawMe.YStartPosition - (floor * DrawMe.StandardRoomHeight))
                {
                    if (hotel.Elevator.ElevatorPosition.Y > DrawMe.YStartPosition - (floor * DrawMe.StandardRoomHeight))
                    {
                        hotel.Elevator.CurrentState = Elevator.ElevatorState.MovingUp;
                        hotel.Elevator.ElevatorPosition.Y -= 10;
                    }

                    if (hotel.Elevator.ElevatorPosition.Y < DrawMe.YStartPosition - (floor * DrawMe.StandardRoomHeight) && floor < Floor)
                    {
                        hotel.Elevator.CurrentState = Elevator.ElevatorState.MovingDown;
                        hotel.Elevator.ElevatorPosition.Y += 10;
                    }
                }

                Speed = 1;
            }
            else
                Speed++;
        }

        public void AddRequest(int RequestedFloor)
        {
            Requests.Add(RequestedFloor);
        }


    }
}

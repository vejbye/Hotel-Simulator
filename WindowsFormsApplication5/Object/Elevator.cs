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
        public int Speed { get; set; } //How fast the elevator goes
        public List<Person> PersonsInElevator { get; set; } //List of the people who are in the elevator
        public Point ElevatorPosition; //The position of the elevator
        public int Destination { get; set; } //The destination in height
        public int RequestedFloor; //The requested floor of the guest 
        public ElevatorState CurrentState; //The currentstate of the elevator

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
                floor++;  //Starts at floor 0, so add 1 to cancel this effect out

            if (Speed == elevatorHTE)
            {
                //If the elevator has not reached destination
                if (hotel.Elevator.ElevatorPosition.Y != Destination)
                {
                    //Move up if the position of the elevator is lower than destination
                    if (hotel.Elevator.ElevatorPosition.Y > Destination)
                    {
                        hotel.Elevator.CurrentState = Elevator.ElevatorState.MovingUp;
                        hotel.Elevator.ElevatorPosition.Y -= 10;
                    }

                    //Move down if the position of the elevator is higher than the destination
                    if (hotel.Elevator.ElevatorPosition.Y < Destination && floor < Floor)
                    {
                        hotel.Elevator.CurrentState = Elevator.ElevatorState.MovingDown;
                        hotel.Elevator.ElevatorPosition.Y += 10;

                    }

                    if (hotel.Elevator.ElevatorPosition.Y == Destination)
                    {
                        Floor = floor;
                        hotel.Elevator.Requests.RemoveAt(hotel.Elevator.Requests.Count - 1);
                    }
                }

                Speed = 1;
            }
            else
                Speed++;
        }

        public void AddRequest(int RequestedFloor)
        {
            //Adds a persons request to the list
            Requests.Add(RequestedFloor);
        }


    }
}

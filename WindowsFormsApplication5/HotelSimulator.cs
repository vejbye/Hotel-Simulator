using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HotelSimulator.Object;
using WindowsFormsApplication5;
using System.Threading;
using System.Timers;
using HotelEvents;

namespace HotelSimulator
{
    public partial class HotelSimulator : Form
    {
        private Hotel Hotel { get; set; }
        private System.Windows.Forms.Timer HotelEventTimer;
        public Draw DrawMe;
        private SimEventListener EventListener;
        public bool godzilla = false;

        //Parameters for panning
        private Point _startingPoint = Point.Empty;
        private Point _movingPoint = Point.Empty;
        private Point _original = new Point(0, 0);
        private bool _panning = false;

        //Max pan box settings (TO SEE RESULT, ADJUST BITMAP TOO IN HOTEL)
        private int _maxPanX = -800; //Adjust to right (greater negative, is more space to right)
        private int _minPanX = 0; //Adjust to left (greater negative, is more space to left)
        private int _maxPanY = 0; //Adjust upwards (greater integer, is more space upwards)
        private int _minPanY = -250; //Adjust downwards (greater integer, is more space downwards)
        private bool _initialized = false;

        //Current element in list request list.
        public int CurrentElement = 0;

        //Standard hte settings
        public int GuestHteDuration = 1;
        public int ElevatorHteDuration = 1;
        public int MaidCleaningDuration = 1;
        public int MovieDuration = 1;
        public int EatingDuration = 1;
        private int _standardMovieLength = 10;


        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public HotelSimulator()
        {
            InitializeComponent();
            Hotel = Hotel.GetHotel();
            DrawMe = new Draw();

            timer.Interval = (1000 / 120); // Refreshes 120 times a second
            timer.Tick += new EventHandler(timer_Tick);
        }

        //If left mousebutton is clicked, go in panning mode.
        private void screenPB_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _panning = true;
                _startingPoint = new Point(e.Location.X - _movingPoint.X,
                                          e.Location.Y - _movingPoint.Y);
            }

        }

        //If left mousebutton isn't clicked, stop panning mode.
        private void screenPB_MouseUp(object sender, MouseEventArgs e)
        {
            _panning = false;
        }

        //Calculates the amount of movement in the world when mouse button is clicked.
        private void screenPB_MouseMove(object sender, MouseEventArgs e)
        {
            if (_panning)
            {
                _movingPoint = new Point(e.Location.X - _startingPoint.X,
                                        e.Location.Y - _startingPoint.Y);
                screenPB.Invalidate();
            }
        }

        //Moves the image based on mouse position when clicked.
        private void screenPB_Paint(object sender, PaintEventArgs e)
        {
            if (_initialized)
            {
                e.Graphics.Clear(Color.DeepSkyBlue);

                if (_movingPoint.X < _maxPanX)
                    _movingPoint.X = _maxPanX;

                if (_movingPoint.X > _minPanX)
                    _movingPoint.X = _minPanX;

                if (_movingPoint.Y < _minPanY)
                    _movingPoint.Y = _minPanY;

                if (_movingPoint.Y > _maxPanY)
                    _movingPoint.Y = _maxPanY;

                e.Graphics.DrawImage(screenPB.Image, _movingPoint);
            }

        }

        //When clicked it will show a dialog where you can choose a layout file.
        private void loadlayoutBTN_Click(object sender, EventArgs e)
        {
            OpenFileDialog chosenFile = new OpenFileDialog();
            chosenFile.Filter = "Layout Files|*.LAYOUT*";
            chosenFile.FilterIndex = 1;
            chosenFile.Multiselect = false;

            if (chosenFile.ShowDialog() == DialogResult.OK)
            {
                string json = chosenFile.FileName;
                LayoutReader reader = new LayoutReader();
                if (reader.ReadLayout(json) != null)
                {
                    _initialized = true;
                    Hotel.Build(reader.ReadLayout(json));
                    //Hotel.Reset();
                    CurrentElement = 0;
                    Hotel.AddMaids(MaidCleaningDuration);
                    screenPB.Image = DrawMe.DrawHotel(Hotel, true);

                    EventListener = new SimEventListener(Hotel, this);
                    HotelEventManager.Register(EventListener);
                    HotelEventManager.Start();
                    timer.Start();
                    HotelEventTimer = new System.Windows.Forms.Timer();
                    HotelEventTimer.Interval = 1000;
                    HotelEventTimer.Tick += new EventHandler(onTimedEvent);
                    HotelEventTimer.Start();
                }
            }
            else
                MessageBox.Show("No file loaded.");
        }

        //When clicked on, you can change the settings of the hotel. TO DO
        private void settingsBTN_Click(object sender, EventArgs e)
        {
            SettingsForm settings = new SettingsForm();

            if (settings.ShowDialog() == DialogResult.OK)
            {
                //Gets all the hte values from the settings form. If nothing is chosen the standard value will be 1.
                if (!settings.hteCB.Text.Equals(""))
                    GuestHteDuration = int.Parse(settings.hteCB.Text);
                if (!settings.elevatorCB.Text.Equals(""))
                    ElevatorHteDuration = int.Parse(settings.elevatorCB.Text);
                if (!settings.cleaningCB.Text.Equals(""))
                    MaidCleaningDuration = int.Parse(settings.cleaningCB.Text);
                if (!settings.moviedurationCB.Text.Equals(""))
                    MovieDuration = int.Parse(settings.moviedurationCB.Text);
                if (!settings.eatingdurationCB.Text.Equals(""))
                    EatingDuration = int.Parse(settings.eatingdurationCB.Text);
                if (!settings.checkinoutCB.Text.Equals(""))
                    EatingDuration = int.Parse(settings.checkinoutCB.Text);
            }
            else
                MessageBox.Show("Nothing changed.");

        }

        //If right mousebutton is clicked show information about the room the mouse is in.
        private void screenPB_MouseClick(object sender, MouseEventArgs e)
        {
            if (_initialized)
            {
                if (e.Button == MouseButtons.Right)
                {
                    Point boxPosition = new Point(e.Location.X - _movingPoint.X, e.Location.Y - _movingPoint.Y);
                    foreach (HotelRoom s in Hotel.GetMap())
                    {
                        if (s.BoundingBox.Contains(boxPosition))
                        {
                            //Pauses the dll and the form timer when reception is clicked.
                            HotelEventTimer.Stop();
                            timer.Stop();
                            InfoScreen infoScreen = new InfoScreen(Hotel.Guests, Hotel.GetMap(), Hotel.Elevator);
                            var result = infoScreen.ShowDialog();
                        }
                    }
                }

                HotelEventTimer.Start();
                timer.Start();
            }
        }
        //when interval is reached, execute next hotelevent
        private void onTimedEvent(object source, EventArgs e)
        {
            EventListener.DoEvent(GuestHteDuration);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            int godzillatimer = 0;
            try
            {
                //Gets the requestedfloor and calculates what y coordinate the floor is in.
                Hotel.Elevator.RequestedFloor = Hotel.Elevator.Requests.ElementAt(CurrentElement);
            }
            catch
            {
                Hotel.Elevator.CurrentState = Elevator.ElevatorState.Idle;
            }

            if (Hotel.Elevator.Destination == DrawMe.YStartPosition && Hotel.Elevator.Requests.Count > 0) 
                Hotel.Elevator.Destination -= DrawMe.StandardRoomHeight;
            else
                Hotel.Elevator.Destination = (DrawMe.YStartPosition - (Hotel.Elevator.RequestedFloor * DrawMe.StandardRoomHeight)) - DrawMe.StandardRoomHeight ;

            Hotel.Elevator.MoveElevator(Hotel, Hotel.Elevator.RequestedFloor, ElevatorHteDuration);


            if (Hotel.Elevator.ElevatorPosition.Y == Hotel.Elevator.Destination)
            {
                Hotel.Elevator.Floor = Hotel.Elevator.RequestedFloor;
                

                if (CurrentElement < Hotel.Elevator.Requests.Count - 1)
                    CurrentElement++;
                else
                {
                    Hotel.Elevator.CurrentState = Elevator.ElevatorState.Idle;
                    CurrentElement = 0;
                }
               
            }


            //Let each guest/maid/elevator move one step each * milliseconds
            for (int i = 0; i < Hotel.Guests.Count; i++)
            {  if(Hotel.Guests[i].InQueue && Hotel.Elevator.Floor == Hotel.Guests[i].CurrentFloor && Hotel.Guests[i].Current is ElevatorShaft)
                {
                    Hotel.Guests[i].InQueue = false;
                    Hotel.Elevator.PersonsInElevator.Add(Hotel.Guests[i]);
                }

                if (!Hotel.Guests[i].InQueue)
                    if (Hotel.Guests[i].Current != Hotel.Guests[i].Destination)
                    {
                        Hotel.Guests[i].Walk(Hotel);
                    }
            }
            for (int i = 0; i < Hotel.Maids.Count; i++)
            {
                if (Hotel.Maids[i].InQueue && Hotel.Elevator.Floor == Hotel.Maids[i].CurrentFloor && Hotel.Maids[i].Current is ElevatorShaft)
                {
                    Hotel.Maids[i].InQueue = false;
                    Hotel.Elevator.PersonsInElevator.Add(Hotel.Maids[i]);
                }

                if (!Hotel.Maids[i].InQueue)
                Hotel.Maids[i].Walk(Hotel);
            }

            foreach (HotelRoom hr in Hotel.Map)
            {
                if (hr is Restaurant)
                {
                    ((Restaurant)hr).HandleWaitingline();
                }
            }

            foreach (HotelRoom hr in Hotel.Map)
            {
                if (hr is Cinema)
                {
                    if (((Cinema)hr).Playing)
                        ((Cinema)hr).PlayMovie(MovieDuration * _standardMovieLength);
                }
            }

            if (godzilla)
            {

                if (HotelEventManager.Running)
                {
                    HotelEventManager.Stop();
                }
                godzillatimer++;
                if (godzillatimer == 10)
                {
                    for (int x = 0; x < Hotel.Map.GetLength(0); x++)
                    {
                        for (int y = 0; y < Hotel.Map.GetLength(1); y++)
                        {

                            if (Hotel.Map[Hotel.Map.GetLength(0) - 1, Hotel.Map.GetLength(1) - 1] == null)
                            {
                                break;
                            }
                            else if (Hotel.Map[x, y] != null && Hotel.Map[x, y].RoomPosition.Y != 1000)
                            {
                                Hotel.Map[x, y] = null;
                            }
                        }

                    }
                    godzillatimer = 0;
                }
            }



            Refresh();

            DrawMe.DrawHotel(Hotel, false);
        }

        private void warningLBL_Click(object sender, EventArgs e)
        {

        }
    }
}

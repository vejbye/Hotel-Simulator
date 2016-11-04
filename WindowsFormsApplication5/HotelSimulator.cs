﻿using System;
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
        public Hotel Hotel;
        public System.Windows.Forms.Timer HotelEventTimer;
        private Draw DrawMe;
        public SimEventListener EventListener;
        
        //Parameters for panning
        private Point _startingPoint = Point.Empty;
        private Point _movingPoint = Point.Empty;
        private Point _original = new Point(0, 0);
        private bool _panning = false;

        //Max pan box settings
        private int _maxPanX = -800;
        private int _minPanX = 0;
        private int _maxPanY = 0;
        private int _minPanY = -250;
        private bool _initialized = false;

        //Current element in list request list.
        public int CurrentElement = 0;

        public int guestHteDuration = 1;

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        public HotelSimulator()
        {
            InitializeComponent();
            Hotel = new Hotel();
            DrawMe = new Draw();

            timer.Interval = 1; // 000.1 sec
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
                _initialized = true;
                LayoutReader reader = new LayoutReader();
                Hotel.Build(reader.ReadLayout(json));
                Hotel.Reset();
                CurrentElement = 0;
                Hotel.AddMaids();
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
            else
                MessageBox.Show("No file loaded.");
        }

        //When clicked on, you can change the settings of the hotel. TO DO
        private void settingsBTN_Click(object sender, EventArgs e)
        {
            SettingsForm settings = new SettingsForm();

            if (settings.ShowDialog() == DialogResult.OK)
            {
                guestHteDuration = int.Parse(settings.hteCB.Text);
            }
            else
                MessageBox.Show("Nothing changed.");

        }

        //If right mousebutton is clicked show information about the room the mouse is in.
        private void screenPB_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point boxPosition = new Point(e.Location.X - _movingPoint.X, e.Location.Y - _movingPoint.Y);
                foreach (HotelRoom s in Hotel.GetMap())
                {
                    if (s.BoundingBox.Contains(boxPosition))
                    {
                        HotelEventTimer.Stop();
                        timer.Stop(); 
                        InfoScreen infoScreen = new InfoScreen(Hotel.Guests);
                        var result = infoScreen.ShowDialog();
                    }
                }
            }

            HotelEventTimer.Start();
            timer.Start();
        }
        //when interval is reached, execute next hotelevent
        private void onTimedEvent(object source, EventArgs e)
        {
            EventListener.DoEvent(guestHteDuration);
        }

        private void timer_Tick(object sender, EventArgs e)
        {   
            //Let each guest/maid/elevator move one step each * milliseconds
            for (int i = 0; i < Hotel.Guests.Count; i++)
            {
                Hotel.Guests[i].Walk(Hotel);
            }
            for (int i = 0; i < Hotel.Maids.Count; i++)
            {
                Hotel.Maids[i].Walk(Hotel);
            }

            Hotel.Elevator.RequestedFloor = Hotel.Elevator.Requests.ElementAt(CurrentElement);
            Hotel.Elevator.Destination = DrawMe.YStartPosition - (Hotel.Elevator.RequestedFloor * DrawMe.StandardRoomHeight);

            DrawMe.MoveElevator(Hotel, Hotel.Elevator.RequestedFloor);
            
            if(Hotel.Elevator.ElevatorPosition.Y == Hotel.Elevator.Destination)
            {
                Hotel.Elevator.PreviousFloor = Hotel.Elevator.RequestedFloor;

                if (CurrentElement < Hotel.Elevator.Requests.Count - 1)
                    CurrentElement++;
                else
                {
                    Hotel.Elevator.CurrentState = Elevator.ElevatorState.Idle;
                    CurrentElement = 0;
                }
            }

            Refresh();

            Hotel.DrawMe.DrawHotel(Hotel, false);
        }



    }
}

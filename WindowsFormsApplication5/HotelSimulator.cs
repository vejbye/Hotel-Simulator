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
using WindowsFormsApplication5.Properties;

namespace HotelSimulator
{
   

    public partial class HotelSimulator : Form
    {
        public Hotel Hotel;
        SimObject[,] Map;
        public List<Guest> newcomers;
        public System.Windows.Forms.Timer HotelEventTimer;
        private Draw DrawMe;
        public SimEventListener sl;
        private Point _startingPoint = Point.Empty;
        private Point _movingPoint = Point.Empty;
        private Point _original = new Point(0, 0);
        private bool _panning = false;

        private int _maxPanX = -800;
        private int _minPanX = 0;
        private int _maxPanY = 0;
        private int _minPanY = -250;
        private bool _initialized = false;

        public HotelSimulator()
        {
            InitializeComponent();
            Hotel = new Hotel();
            DrawMe = new Draw();
            Map = Hotel.Map;
            newcomers = new List<Guest>();
            for(int i = 0; i < 2; i++)
            {
                newcomers.Add(new Guest(null));
        }
        }

        private void screenPB_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                _panning = true;
                _startingPoint = new Point(e.Location.X - _movingPoint.X,
                                          e.Location.Y - _movingPoint.Y);
            }

        }

        private void screenPB_MouseUp(object sender, MouseEventArgs e)
        {
            _panning = false;
        }

        private void screenPB_MouseMove(object sender, MouseEventArgs e)
        {
            if (_panning)
            {
                _movingPoint = new Point(e.Location.X - _startingPoint.X,
                                        e.Location.Y - _startingPoint.Y);
                screenPB.Invalidate();
            }
        }

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
                Hotel.Action();
                screenPB.Image = DrawMe.DrawHotel(Hotel.GetMap(), Hotel._hotel, Hotel.Guests, Hotel.maids);

                sl = new SimEventListener(Hotel, this);
                HotelEventManager.Register(sl);
                HotelEventManager.Start();

                HotelEventTimer = new System.Windows.Forms.Timer();
                HotelEventTimer.Interval = 1000;
                HotelEventTimer.Tick += new EventHandler(OnTimedEvent);
                HotelEventTimer.Start();
            }
            else
                MessageBox.Show("Couldn't load file");
        }

        private void settingsBTN_Click(object sender, EventArgs e)
        {
            SettingsForm settings = new SettingsForm();
            var result = settings.ShowDialog();
            
            if (result == DialogResult.OK)
            {
                
            }
            
            if (result == DialogResult.Cancel)
            {
                
            }

        }

        private void screenPB_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                    Point boxPosition = new Point(e.Location.X - _movingPoint.X, e.Location.Y - _movingPoint.Y);
                foreach (SimObject s in Hotel.GetMap())
                    {
                    if (s.BoundingBox.Contains(boxPosition))
                        {
                        InfoScreen infoScreen = new InfoScreen(s);
                            var result = infoScreen.ShowDialog();
                        }

                }
            }

        }

        //when interval is reached, execute next hotelevent
        private void OnTimedEvent(object source, EventArgs e)
        {
           // HotelEventManager.Pauze();
            sl.DoEvent();
        }

        private void Movement()
        {
            if (Hotel.Map != null)
            {
                {
                    try
                    {
                        foreach (Guest guest in Hotel.Guests)
                        {
                            HotelRoom destination = guest.setDestination(Hotel);
                            guest.Walk(Hotel, this, destination);
                        }
                    }
                    catch (Exception e)
                    {

                    }        
                }
            }
        }
    }
}

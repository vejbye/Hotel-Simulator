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

namespace HotelSimulator
{
   

    public partial class HotelSimulator : Form
    {
        Hotel Hotel;
        SimObject[,] Map;
        Guest Guest;
        public List<Guest> newcomers;
        System.Windows.Forms.Timer aTimer;
        System.Windows.Forms.Timer SpawnTimer;


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
            Map = Hotel.Map;
            newcomers = new List<Guest>();
            for(int i = 0; i < 10; i++)
            {
                newcomers.Add(new Guest(null));
            }
        }

        private void OnSpawn(object source, EventArgs e)
        {
                if (newcomers.Count > 0)
                {
                    ((HotelRoom)Hotel.Map[0, 0]).Guests.Add(newcomers.ElementAt(0));
                    newcomers.ElementAt(0).Current = ((HotelRoom)Hotel.Map[0, 0]);
                    newcomers.RemoveAt(0);
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
                screenPB.Image = Hotel.Draw(Hotel.Map);
                Guest = Hotel.Action();
                
                aTimer = new System.Windows.Forms.Timer();
                aTimer.Interval = 2000;
                aTimer.Tick += new EventHandler(OnTimedEvent);
                aTimer.Start();
                SpawnTimer = new System.Windows.Forms.Timer();
                SpawnTimer.Interval = 5000;
                SpawnTimer.Tick += new EventHandler(OnSpawn);
                SpawnTimer.Start();
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
                if (Hotel.Map != null)
                {
                    Point boxPosition = new Point(e.Location.X - _movingPoint.X, e.Location.Y - _movingPoint.Y);
                    foreach (Rectangle r in Hotel.BoundaryBox.BoundaryBoxes)
                    {
                        if (r.Contains(boxPosition))
                        {
                            InfoScreen infoScreen = new InfoScreen();
                            var result = infoScreen.ShowDialog();
                        }

                    }

                    Console.WriteLine("Actual Click: {0} \nClick in World: {1}", e.Location, boxPosition);
                }
            }
        }

        private void OnTimedEvent(object source, EventArgs e)
        {
            aTimer.Stop();
           Movement();
            aTimer.Start();
        }

        private void Movement()
        {
            if (Hotel.Map != null)
            {
                {
                    foreach (HotelRoom hr in Hotel.Map)
                    {
                        try
                        {
                            for(int i = 0; i < hr.Guests.Count; i++)
                            {
                                if (hr.Guests.ElementAt(i).moved == false)
                                {
                                    hr.Guests.ElementAt(i).moved = true;
                                    hr.Guests.ElementAt(i).delay = 3;
                                    HotelRoom destination = hr.Guests.ElementAt(i).setDestination(Hotel);
                                    hr.Guests.ElementAt(i).Walk(Hotel, this, destination);
                                    i--;
                                }else
                                {
                                    hr.Guests.ElementAt(i).delay--;
                                    if (hr.Guests.ElementAt(i).delay == 0)
                                    {
                                        hr.Guests.ElementAt(i).moved = false;
                                    }
                                }
                            }
                        }
                        catch (InvalidOperationException ex)
                        {

                        }
                    }
                  /*  foreach (HotelRoom hr in Hotel.Map)
                    {
                        try
                        {
                            foreach (Maid maid in hr.Maids)
                            {
                                if (maid.moved == false)
                                {
                                    maid.Walk(Hotel, this);
                                    maid.moved = true;
                                }
                            }
                            foreach (Maid maid in hr.Maids)
                            {
                                maid.moved = false;

                            }
                        }
                        catch (InvalidOperationException ex)
                        {

                        }
                    }*/
                    //screenPB.Invalidate();
                    //screenPB.Refresh();
                }
            }
        }
    }
}

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

namespace HotelSimulator
{
    public partial class HotelSimulator : Form
    {
        Hotel Hotel;
        Space[, ] map;
        private Point startingPoint = Point.Empty;
        private Point movingPoint = Point.Empty;
        private bool panning = false;
        private Point original = new Point(0, 0);

        private int maxPanX = -800;
        private int minPanX = 0;
        private int maxPanY = 0;
        private int minPanY = -250;
        bool initialized = false;

        public HotelSimulator()
        {
            InitializeComponent();
            Hotel = new Hotel();
            map = Hotel.map;
        }


        private void screenPB_MouseDown(object sender, MouseEventArgs e)
        {
            panning = true;
            startingPoint = new Point(e.Location.X - movingPoint.X,
                                      e.Location.Y - movingPoint.Y);

        }

        private void screenPB_MouseUp(object sender, MouseEventArgs e)
        {
            panning = false;
        }

        private void screenPB_MouseMove(object sender, MouseEventArgs e)
        {
            if (panning)
            {

                movingPoint = new Point(e.Location.X - startingPoint.X,
                                        e.Location.Y - startingPoint.Y);
                screenPB.Invalidate();

            }
        }

        private void screenPB_Paint(object sender, PaintEventArgs e)
        {
            if (initialized)
            {
                e.Graphics.Clear(Color.DeepSkyBlue);

                if (movingPoint.X < maxPanX)
                    movingPoint.X = maxPanX;

                if (movingPoint.X > minPanX)
                    movingPoint.X = minPanX;

                if (movingPoint.Y < minPanY)
                    movingPoint.Y = minPanY;

                if (movingPoint.Y > maxPanY)
                    movingPoint.Y = maxPanY;

                e.Graphics.DrawImage(screenPB.Image, movingPoint);
            }


        }

        private void loadlayoutBTN_Click(object sender, EventArgs e)
        {
            OpenFileDialog chosenFile = new OpenFileDialog();
            chosenFile.Filter = "All Files (*.*)|*.*";
            chosenFile.FilterIndex = 1;
            chosenFile.Multiselect = false;

            if (chosenFile.ShowDialog() == DialogResult.OK)
            {
                string json = chosenFile.FileName;

                initialized = true;
                LayoutReader reader = new LayoutReader();
                Hotel.Build(reader.ReadLayout(json));
                screenPB.Image = Hotel.Draw();
            }
            else
                MessageBox.Show("Couldn't load file");
        }

        private void HotelSimulator_Load(object sender, EventArgs e)
        {

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
            foreach(Space s in map)
            {
               // if(s.)
            }
        }
    }
}

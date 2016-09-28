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

namespace HotelSimulator
{
    public partial class HotelSimulator : Form
    {
        Hotel hotel;
        private Point startingPoint = Point.Empty;
        private Point movingPoint = Point.Empty;
        private bool panning = false;
        private Point original = new Point(0, 0);

        int maxPanX = -800;
        int minPanX = 1;
        int maxPanY = 0;
        int minPanY = -250;
        

        public HotelSimulator()
        {
            InitializeComponent();
            hotel = new Hotel();
            screenPB.Image = hotel.Build();
        }

        
        void screenPB_MouseDown(object sender, MouseEventArgs e)
        {
            panning = true;
            startingPoint = new Point(e.Location.X - movingPoint.X,
                                      e.Location.Y - movingPoint.Y);
            
        }

        void screenPB_MouseUp(object sender, MouseEventArgs e)
        {
            panning = false;
        }

        void screenPB_MouseMove(object sender, MouseEventArgs e)
        {
            if (panning)
            {
                
                    movingPoint = new Point(e.Location.X - startingPoint.X,
                                            e.Location.Y - startingPoint.Y);
                    screenPB.Invalidate();
                
            }
        }

        void screenPB_Paint(object sender, PaintEventArgs e)
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
}

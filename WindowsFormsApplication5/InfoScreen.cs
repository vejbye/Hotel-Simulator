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

namespace WindowsFormsApplication5
{
    public partial class InfoScreen : Form
    {
        public InfoScreen(HotelRoom s)
        {
            InitializeComponent();
            string[] roomName = s.ToString().Split('.');
            string standardDimension = "1 x 1";

            if (s is Room)
            {
                int star = s.Classification;
                unknownLBL.Text = String.Format("{0} {1} Star", roomName[2], star.ToString());
            }

            else
                unknownLBL.Text = roomName[2];

            unknownLBL2.Text = s.Guests.Count().ToString();

            if(s is Stair || s is ElevatorShaft)
                unknownLBL3.Text = standardDimension;
            else
                unknownLBL3.Text = s.Dimensions;

            unknownLBL4.Text = s.Floor.ToString();
        }

        private void InfoScreen_Load(object sender, EventArgs e)
        {

        }
    }
}

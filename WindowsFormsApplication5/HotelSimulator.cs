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

        public HotelSimulator()
        {
            InitializeComponent();
            hotel = new Hotel();
            screenPB.Image = hotel.Build(screenPB.Width, screenPB.Height);
        }

        private void HotelSimulator_Load(object sender, EventArgs e)
        {

        }
    }
}

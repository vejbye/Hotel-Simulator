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
        public InfoScreen(SimObject s)
        {
            InitializeComponent();
            string[] roomName = s.ToString().Split('.');
            unknownLBL.Text = roomName[2];
        }

        private void InfoScreen_Load(object sender, EventArgs e)
        {

        }
    }
}

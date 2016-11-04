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
        public InfoScreen(List<Guest> guests)
        {
            InitializeComponent();
            

            DataTable guestInfo = new DataTable();
            guestInfo.Columns.Add("Guest Name", typeof(string));
            guestInfo.Columns.Add("Destination", typeof(HotelRoom));

            foreach(Guest g in guests)
            {
                guestInfo.Rows.Add(g.guestName);
            }

            guestsDG.DataSource = guestInfo;
        }

        private void InfoScreen_Load(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}

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
        List<string> guestsNames;
        public InfoScreen(List<Guest> guests)
        {
            InitializeComponent();
            guestsNames = new List<string>();
            foreach (Guest g in guests)
            {
                string[] guestName = guests.ToString().Split('.');
                guestsNames.Add(guestName[5]);
            }

            guestsDG.DataSource = guestsNames;
        }

        private void InfoScreen_Load(object sender, EventArgs e)
        {

        }

        
    }
}

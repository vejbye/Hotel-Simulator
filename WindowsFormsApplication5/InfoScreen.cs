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
        public InfoScreen(List<Guest> guests, HotelRoom[,] map, Elevator elevator)
        {
            InitializeComponent();
            string[] roomName;
            DataView guestDV;
            DataView facilityDV;

            //Creates tables for information about the guests in the hotel.
            DataTable guestInfo = new DataTable();
            guestInfo.Columns.Add("Guest Name", typeof(string));
            guestInfo.Columns.Add("Going to", typeof(string));
            guestInfo.Columns.Add("On floor", typeof(int));
            guestInfo.Columns.Add("Room ID", typeof(int));

            foreach(Guest g in guests)
            {
                roomName = g.Destination.ToString().Split('.');
                if(g.Destination is Room)
                    guestInfo.Rows.Add(g.GuestName, roomName[2] + String.Format(" {0} stars", g.Destination.Classification), g.Destination.Floor, g.Destination.Id);
                else
                    guestInfo.Rows.Add(g.GuestName, roomName[2], g.Destination.Floor, g.Destination.Id);
            }

            guestDV = guestInfo.DefaultView;
            guestDV.Sort = "Guest Name";
            guestsDG.DataSource = guestDV.ToTable();


            //Creates tables for information about the facilities in the hotel.
            DataTable facilitiesInfo = new DataTable();
            facilitiesInfo.Columns.Add("Room Name", typeof(string));
            facilitiesInfo.Columns.Add("Floor", typeof(int));
            facilitiesInfo.Columns.Add("Guests", typeof(int));
            facilitiesInfo.Columns.Add("Room ID", typeof(int));

            foreach (HotelRoom hr in map)
            {
                if (hr.Id != 0)
                {
                    roomName = hr.ToString().Split('.');
                    if (hr is Room)
                        facilitiesInfo.Rows.Add(roomName[2] + String.Format(" {0} stars", hr.Classification), hr.Floor, hr.Guests.Count, hr.Id);
                    else
                        facilitiesInfo.Rows.Add(roomName[2], hr.Floor, hr.Guests.Count, hr.Id);
                }
            }

            facilityDV = facilitiesInfo.DefaultView;
            facilityDV.Sort = "Room Name";
            facilitiesDG.DataSource = facilityDV.ToTable();


            //Creates tables for information about the facilities in the hotel.
            DataTable elevatorInfo = new DataTable();
            elevatorInfo.Columns.Add("Going to floor", typeof(string));
            elevatorInfo.Columns.Add("People in lift", typeof(int));

            elevatorInfo.Rows.Add(elevator.RequestedFloor, elevator.Guests.Count);
            elevatorDG.DataSource = elevatorInfo;
        }
    }
}

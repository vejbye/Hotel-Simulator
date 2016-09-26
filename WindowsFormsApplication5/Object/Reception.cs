using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace HotelSimulator.Object
{
    class Reception: GameObject
    {
        public Reception() : base()
        {
            image = Image.FromFile(@"C:\Users\iCalvin\Source\Repos\Hotel-Simulator\WindowsFormsApplication5\Resources\Reception.png");
        }
    }
}

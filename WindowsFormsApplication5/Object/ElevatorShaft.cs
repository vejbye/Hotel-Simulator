using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulator.Object
{
    class ElevatorShaft: GameObject
    {
        ElevatorShaft elevatorShaft;

        public ElevatorShaft(ElevatorShaft shaft)
        {
            this.elevatorShaft = shaft;
        }
    }
}

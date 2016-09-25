using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulator.Object
{
    class Liftschacht: GameObject
    {
        Lift lift;

        public Liftschacht(Lift lift)
        {
            this.lift = lift;
        }
    }
}

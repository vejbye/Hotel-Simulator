using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication5
{
    class Liftschacht: Ruimte
    {
        Lift lift;

        public Liftschacht(Lift lift)
        {
            this.lift = lift;
        }
    }
}

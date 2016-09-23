﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication5
{
    class Hotel
    {
        int height;
        int width;
        List<object> ruimtes = new List<object>();

        public void maakHotel()
        {
            Kamer kamer1 = new Kamer();
            Kamer kamer2 = new Kamer();
            Kamer kamer3 = new Kamer();
            Kamer kamer4 = new Kamer();
            Receptie rc = new Receptie();
            Bioscoop b = new Bioscoop();
            Fitnessruimte f = new Fitnessruimte();
            Restaurant rs = new Restaurant();
            Trap t1 = new Trap();
            Trap t2 = new Trap();
            Trap t3 = new Trap();
            Liftschacht l1 = new Liftschacht(new Lift());
            Liftschacht l2 = new Liftschacht(null);
            Liftschacht l3 = new Liftschacht(null);

            b.neighbours.Add(f);
            kamer1.neighbours.Add(kamer2);
            kamer4.neighbours.Add(kamer3);
            kamer2.neighbours.Add(kamer1);
            kamer2.neighbours.Add(t2);
            kamer3.neighbours.Add(kamer4);
            kamer3.neighbours.Add(l3);
            f.neighbours.Add(b);
            f.neighbours.Add(t3);
            t2.neighbours.Add(kamer2);
            t3.neighbours.Add(f);
            t2.neighbours.Add(l2);
            t3.neighbours.Add(l3);
            rs.neighbours.Add(l2);
            rc.neighbours.Add(l1);
            t1.neighbours.Add(l1);
            l2.neighbours.Add(t2);
            l2.neighbours.Add(rs);
            l1.neighbours.Add(t1);
            l1.neighbours.Add(rc);
            l3.neighbours.Add(t3);
            l3.neighbours.Add(kamer3);


        }
    }
}

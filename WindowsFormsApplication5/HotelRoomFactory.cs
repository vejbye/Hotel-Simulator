using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSimulator.Object
{
    public class HotelRoomFactory
    {
        private HotelRoom _current;

        public HotelRoom CreateHotelRoom(string AreaType, string[] Dimensions, string Classification, int Capacity)
        {
            switch (AreaType)
            {
                case "Room":
                    {
                        Room _current = new Room();
                        _current.Classification = int.Parse(Classification.Substring(0, 1));
                        _current.SetRoomImage(Dimensions);
                        return _current;
                    }

                case "Cinema":
                    {
                        Cinema _current = new Cinema();
                        return _current;
                    }

                case "Restaurant":
                    {
                        Restaurant _current = new Restaurant();
                        _current.Capacity = Capacity;
                        return _current;
                    }

                case "Fitness":
                    {
                        Gym _current = new Gym();
                        return _current;
                    }
                case "Pool":
                    {
                        Pool _current = new Pool();
                        return _current;
                    }

                default:
                    {
                        _current = null;
                        return _current;
                    }

                    
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace HotelSimulator.Object
{
class LayoutReader
    {
        List<LayoutFormat> formats;

        public List<LayoutFormat> ReadLayout(string jsonPath)
        {
            formats = new List<LayoutFormat>();

            string jsonText = File.ReadAllText(jsonPath);
            LayoutFormat[] layouts = JsonConvert.DeserializeObject<LayoutFormat[]>(jsonText);

            foreach(LayoutFormat l in layouts)
            {
                LayoutFormat layout = new LayoutFormat();

                switch (l.AreaType)
                {
                    case "Room":
                        {
                            layout.AreaType = l.AreaType;
                            layout.Classification = l.Classification;
                            layout.Position = l.Position;
                            layout.Dimension = l.Dimension;
                            formats.Add(l);
                            
                            break;
                        }

                    case "Cinema":
                        {
                            layout.AreaType = l.AreaType;
                            layout.Position = l.Position;
                            layout.Dimension = l.Dimension;
                            formats.Add(l);

                            break;
                        }

                    case "Restaurant":
                        {
                            layout.AreaType = l.AreaType;
                            layout.Position = l.Position;
                            layout.Dimension = l.Dimension;
                            formats.Add(l);

                            break;
                        }

                    case "Fitness":
                        {
                            layout.AreaType = l.AreaType;
                            layout.Position = l.Position;
                            layout.Dimension = l.Dimension;
                            formats.Add(l);

                            break;
                        }
                }

            }

            return formats;

        }
    }

}

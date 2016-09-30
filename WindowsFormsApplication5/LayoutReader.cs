using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace HotelSimulator.Object
{
    public class LayoutReader
    {
        List<LayoutFormat> Formats;

        public List<LayoutFormat> ReadLayout(string jsonPath)
        {
            Formats = new List<LayoutFormat>();

            string jsonText = File.ReadAllText(jsonPath);
            Formats = JsonConvert.DeserializeObject<List<LayoutFormat>>(jsonText);

            return Formats;
        }
    }

}

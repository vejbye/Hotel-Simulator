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
        private List<LayoutFormat> _formats;

        public List<LayoutFormat> ReadLayout(string jsonPath)
        {
            _formats = new List<LayoutFormat>();

            string jsonText = File.ReadAllText(jsonPath);
            _formats = JsonConvert.DeserializeObject<List<LayoutFormat>>(jsonText);

            return _formats;
        }
    }

}

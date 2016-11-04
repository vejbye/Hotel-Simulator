using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using WindowsFormsApplication5.Properties;

namespace HotelSimulator.Object
{
    public class LayoutReader
    {
        private List<LayoutFormat> _formats;

        public List<LayoutFormat> ReadLayout(string jsonPath)
        {
            _formats = new List<LayoutFormat>();

            string jsonText = File.ReadAllText(jsonPath);
            try
            {
                _formats = JsonConvert.DeserializeObject<List<LayoutFormat>>(jsonText);
            }
            catch
            {
                Console.WriteLine("This file contains unsupported characters! Please select another file.");
                _formats = null;
            }

            return _formats;
        }
    }

}

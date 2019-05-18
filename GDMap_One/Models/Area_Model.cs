using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GDMap_One.Models
{
    public class Area_Model
    {
        public int id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public string center { get; set; }
        public string title { get; set; }
        public string intro { get; set; }

    }
    public class position {
        public double Lng { get; set; }
        public double Lat { get; set; }
    }

    
}
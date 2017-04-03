using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleManager.API.Models
{
    public class Vehicle
    {
        //Scalar
        public int VehicleID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Color { get; set; }
        public string VehicleType { get; set; }
        public int RetailPrice { get; set; }

        //Nav
        public virtual ICollection<Sale> Sales {get; set;}
    }
}
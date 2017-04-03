using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleManager.API.Models
{
    public class Color
    {
        //Scalar
        public int ColorID { get; set; }
        public string Description { get; set; }

        //Nav
        public virtual ICollection<Vehicle> Vehicle { get; set; }
    }
}
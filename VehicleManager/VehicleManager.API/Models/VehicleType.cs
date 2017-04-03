using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleManager.API.Models
{
    public class VehicleType
    {
        //Scalar
        public int VehicleTypeID { get; set; }
        public string Description { get; set; }

        //Nav
        public virtual ICollection<Vehicle> Vehicle { get; set; }
    }
}
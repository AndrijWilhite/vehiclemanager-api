using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleManager.API.Models
{
    public class Customer
    {
        //Scalar
        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAdress { get; set; }
        public string Telephone { get; set; }
        public DateTime DateOfBirth { get; set; }

        //Nav
        public virtual ICollection<Sale> Sales {get; set;}
}
}
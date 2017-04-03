using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VehicleManager.API.Models
{
    public class Sale
    {
        //Scalar
        public int SaleID { get; set; }
        public int VehicleID { get; set; }
        public int CustomerID { get; set; }
        public decimal SalePrice { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? PaymentRecivedDate { get; set; }

        //Nav
        public virtual Vehicle Vehicle { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
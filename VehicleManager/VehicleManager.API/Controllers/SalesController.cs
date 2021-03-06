﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using VehicleManager.API.Data;
using VehicleManager.API.Models;

namespace VehicleManager.API.Controllers
{
    public class SalesController : ApiController
    {
        private VehicleManagerDataContext db = new VehicleManagerDataContext();

        // GET: api/Sales
        public IHttpActionResult GetSales()
        {
            var resultSet = db.Sales.Select(sales => new
            {
                sales.SaleID,
                sales.SalePrice,
                sales.InvoiceDate,
                sales.PaymentRecivedDate,
                sales.Vehicle,
                CustomerName = sales.Customer.FirstName + " "+sales.Customer.LastName,
                Whip = sales.Vehicle.Year+" "+sales.Vehicle.Make+" "+sales.Vehicle.Model
            });

            return Ok(resultSet);
        }

        // GET: api/Sales/5
        [ResponseType(typeof(Sale))]
        public IHttpActionResult GetSale(int id)
        {
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                sale.SaleID,
                sale.SalePrice,
                sale.InvoiceDate,
                sale.PaymentRecivedDate,
                sale.Vehicle,
                sale.Customer
            });
        }

        // PUT: api/Sales/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSale(int id, Sale sale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sale.SaleID)
            {
                return BadRequest();
            }

            var dbSale = db.Sales.Find(id);
            dbSale.SaleID = sale.SaleID;
            dbSale.SalePrice = sale.SalePrice;
            dbSale.InvoiceDate = sale.InvoiceDate;
            dbSale.PaymentRecivedDate = sale.PaymentRecivedDate;
            
            db.Entry(dbSale).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Sales
        [ResponseType(typeof(Sale))]
        public IHttpActionResult PostSale(Sale sale)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sales.Add(sale);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = sale.SaleID }, sale);
        }

        // DELETE: api/Sales/5
        [ResponseType(typeof(Sale))]
        public IHttpActionResult DeleteSale(int id)
        {
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return NotFound();
            }

            db.Sales.Remove(sale);
            db.SaveChanges();

            return Ok(new
            {
                sale.SaleID,
                sale.SalePrice,
                sale.InvoiceDate,
                sale.PaymentRecivedDate,
                sale.Vehicle,
                sale.Customer
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SaleExists(int id)
        {
            return db.Sales.Count(e => e.SaleID == id) > 0;
        }
    }
}
using System;
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
    public class VehiclesController : ApiController
    {
        private VehicleManagerDataContext db = new VehicleManagerDataContext();

        // GET: api/Vehicles
        public IHttpActionResult GetVehicles()
        {
            var resultSet = db.Vehicles.Select(vehicle => new
            {
                vehicle.VehicleID,
                vehicle.VehicleTypeID,
                vehicle.Make,
                vehicle.Model,
                vehicle.Year,
                vehicle.RetailPrice,
                vehicle.Color,
                vehicle.VehicleType.Description

            });

            return Ok(resultSet);
        }

        // GET: api/Vehicles/5
        [ResponseType(typeof(Vehicle))]
        public IHttpActionResult GetVehicle(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                vehicle.VehicleID,
                vehicle.VehicleTypeID,
                vehicle.Make,
                vehicle.Model,
                vehicle.Year,
                vehicle.RetailPrice,
                vehicle.Color,
                vehicle.VehicleType.Description

            });
        }

        // PUT: api/Vehicles/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVehicle(int id, Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehicle.VehicleID)
            {
                return BadRequest();
            }
            var dbVehicle = db.Vehicles.Find(id);
            dbVehicle.VehicleID = vehicle.VehicleID;
            dbVehicle.VehicleTypeID = vehicle.VehicleTypeID;
            dbVehicle.Make = vehicle.Make;
            dbVehicle.Model = vehicle.Model;
            dbVehicle.Year = vehicle.Year;
            dbVehicle.RetailPrice = vehicle.RetailPrice;
            dbVehicle.Color = vehicle.Color;
            dbVehicle.VehicleTypeID = vehicle.VehicleTypeID;

            db.Entry(dbVehicle).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
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

        // POST: api/Vehicles
        [ResponseType(typeof(Vehicle))]
        public IHttpActionResult PostVehicle(Vehicle vehicle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vehicles.Add(vehicle);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vehicle.VehicleID }, vehicle);
        }

        // DELETE: api/Vehicles/5
        [ResponseType(typeof(Vehicle))]
        public IHttpActionResult DeleteVehicle(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            db.Vehicles.Remove(vehicle);
            db.SaveChanges();

            return Ok(new
            {
                vehicle.VehicleID,
                vehicle.VehicleTypeID,
                vehicle.Make,
                vehicle.Model,
                vehicle.Year,
                vehicle.RetailPrice,
                vehicle.Color,
                vehicle.VehicleType.Description

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

        private bool VehicleExists(int id)
        {
            return db.Vehicles.Count(e => e.VehicleID == id) > 0;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dealership2.Models;


namespace Dealership2.Controllers
{
    public class VehiclesController : Controller
    {
        private DealerShipDBEntities db = new DealerShipDBEntities();

        // GET: Vehicles
        public ActionResult Index()
        {
            var vehicles = db.Vehicles.Include(v => v.Customer);
            return View(vehicles.ToList());
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            ViewBag.Customers = db.Customers.ToList();
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "VehicleID,Make,Model,Year,Color,Type,Price,CustomerID,SalesDID")] Vehicle vehicle)
        {
            List<Vehicle> vehicles = db.Vehicles.ToList();
            //THIS IS THE CODE FOR DUPLICATION CHECKING (v can be anything)
            if (vehicles.Any(v => v.SalesDID == vehicle.SalesDID))
            {
                ModelState.AddModelError(nameof(SalesDept.SalesDID), "Please enter a unique Sales ID");
            }

            if (vehicles.Any(v => v.VehicleID == vehicle.VehicleID))
            {
                ModelState.AddModelError(nameof(Vehicle.VehicleID), "Please enter a unique Vehicle ID");
            }

            if (ModelState.IsValid)
            {
                db.Vehicles.Add(vehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", vehicle.CustomerID);
            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", vehicle.CustomerID);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "VehicleID,Make,Model,Year,Color,Type,Price,CustomerID,SalesDID")] Vehicle vehicle)
        {
            List<Vehicle> vehicles = db.Vehicles.ToList();

            if (vehicles.Any(v => v.SalesDID == vehicle.SalesDID))
            {
                ModelState.AddModelError(nameof(SalesDept.SalesDID), "Please enter a unique Sales ID");
            }

            if (ModelState.IsValid)
            {
                //db.Entry(vehicle).State = EntityState.Modified;
                Vehicle v= db.Vehicles.Find(vehicle.VehicleID);
                db.Vehicles.Remove(v);
                db.Vehicles.Add(vehicle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", vehicle.CustomerID);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            db.Vehicles.Remove(vehicle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

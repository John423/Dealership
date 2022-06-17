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
    public class SalesDeptsController : Controller
    {
        private DealerShipDBEntities db = new DealerShipDBEntities();

        // GET: SalesDepts
        public ActionResult Index()
        {
            return View(db.SalesDepts.ToList());
        }

        // GET: SalesDepts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesDept salesDept = db.SalesDepts.Find(id);
            if (salesDept == null)
            {
                return HttpNotFound();
            }
            return View(salesDept);
        }

        // GET: SalesDepts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SalesDepts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalesDID,Title,CustomerID,Condition")] SalesDept salesDept)
        {

            List<SalesDept> salesDepts = db.SalesDepts.ToList();
            //THIS IS THE CODE FOR DUPLICATION CHECKING 
            if (salesDepts.Any(s => s.SalesDID == salesDept.SalesDID))
            {
                ModelState.AddModelError(nameof(SalesDept.SalesDID), "Please enter a unique Sales ID");
            }

            if (ModelState.IsValid)
            {
                db.SalesDepts.Add(salesDept);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(salesDept);
        }

        // GET: SalesDepts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesDept salesDept = db.SalesDepts.Find(id);
            if (salesDept == null)
            {
                return HttpNotFound();
            }
            return View(salesDept);
        }

        // POST: SalesDepts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalesDID,Title,CustomerID,Condition")] SalesDept salesDept)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salesDept).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(salesDept);
        }

        // GET: SalesDepts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesDept salesDept = db.SalesDepts.Find(id);
            if (salesDept == null)
            {
                return HttpNotFound();
            }
            return View(salesDept);
        }

        // POST: SalesDepts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalesDept salesDept = db.SalesDepts.Find(id);
            db.SalesDepts.Remove(salesDept);
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

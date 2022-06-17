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
    public class TransactionsController : Controller
    {
        private DealerShipDBEntities db = new DealerShipDBEntities();

        // GET: Transactions
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.Customer).Include(t => t.Service).Include(t => t.Vehicle);
            return View(transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName");
            ViewBag.ServiceID = new SelectList(db.Services, "ServiceID", "ServiceType");
            ViewBag.VehicleID = new SelectList(db.Vehicles, "VehicleID", "Make");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransactionID,AccountNumber,BankName,DateOfTransaction,CustomerID,VehicleID,ServiceID")] Transaction transaction)
        {
            List<Transaction> transactions = db.Transactions.ToList();
            //THIS IS THE CODE FOR DUPLICATION CHECKING 
            if (transactions.Any(t => t.TransactionID == transaction.TransactionID))
            {
                ModelState.AddModelError(nameof(Transaction.TransactionID), "Please enter a unique Transaction ID");
            }


            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", transaction.CustomerID);
            ViewBag.ServiceID = new SelectList(db.Services, "ServiceID", "ServiceType", transaction.ServiceID);
            ViewBag.VehicleID = new SelectList(db.Vehicles, "VehicleID", "Make", transaction.VehicleID);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", transaction.CustomerID);
            ViewBag.ServiceID = new SelectList(db.Services, "ServiceID", "ServiceType", transaction.ServiceID);
            ViewBag.VehicleID = new SelectList(db.Vehicles, "VehicleID", "Make", transaction.VehicleID);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransactionID,AccountNumber,BankName,DateOfTransaction,CustomerID,VehicleID,ServiceID")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "FirstName", transaction.CustomerID);
            ViewBag.ServiceID = new SelectList(db.Services, "ServiceID", "ServiceType", transaction.ServiceID);
            ViewBag.VehicleID = new SelectList(db.Vehicles, "VehicleID", "Make", transaction.VehicleID);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FaithAG.Models;
using Microsoft.AspNet.Identity;
using Rotativa;

namespace FaithAG.Controllers
{
    public class TithePaymentsController : Controller
    {
        private hpsadmin_dfaithagEntities db = new hpsadmin_dfaithagEntities();

        // GET: TithePayments
        [Authorize]
        public ActionResult Index(int? id)
        {
            ViewBag.MemID = id;
            var tithePayments = db.TithePayments.Include(t => t.AspNetUser).Include(t => t.MemebershipT);
            return View(tithePayments.Where(x => x.MemberIdN == id).OrderByDescending(x=>x.RecordDate).ToList());
        }

        // Access all Tithe Payments
        [Authorize]
        public ActionResult ViewAll()
        {           
            var tithePayments = db.TithePayments.Include(t => t.AspNetUser).Include(t => t.MemebershipT);
            return View(tithePayments.ToList());
        }

        // Access all Tithe Payments
        [Authorize]
        public ActionResult DailyTransaction()
        {
            DateTime dt = DateTime.Now ;
            dt = dt.AddDays(-0.5);
            var tithePayments = db.TithePayments.Include(t => t.AspNetUser).Include(t => t.MemebershipT);
            return View(tithePayments.Where(x=>x.RecordDate >dt).ToList());
        }

        // GET: TithePayments/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TithePayment tithePayment = db.TithePayments.Find(id);
            if (tithePayment == null)
            {
                return HttpNotFound();
            }
            return View(tithePayment);
        }

        // GET: TithePayments/Create
        public ActionResult Create(int? id)
        {
            ViewBag.MemID = id;
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Firstname");
            ViewBag.MemberIdN = new SelectList(db.MemebershipTs.Where(x => x.Id == id), "Id", "Fullname");
            return View();
        }

        // POST: TithePayments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MemberIdN,RecordDate,UserId,Amt")] TithePayment tithePayment, int? id)
        {
            if (ModelState.IsValid)
            {
                ViewBag.MemID = id;
                tithePayment.RecordDate = DateTime.UtcNow;
                tithePayment.UserId = User.Identity.GetUserId();
                db.TithePayments.Add(tithePayment);
                db.SaveChanges();

                //Adding Tithe to Log
                FaithBillLog BLog = new FaithBillLog();
                BLog.BillRef = ViewBag.MemID;
                BLog.BillDate = DateTime.UtcNow;
                BLog.BillItemTyp = "Tithe";
                BLog.BillPaymentType  = "Cash";
                BLog.PaidInAmt  = tithePayment.Amt;                 
                BLog.PaidOutAmt  = 0;
                BLog.TotalAmt = (decimal)(tithePayment.Amt) ;
                BLog.BillUID = User.Identity.GetUserId();
                BLog.PaymentStatus = false;
                BLog.CancellationStatus = false;
                db.FaithBillLogs.Add(BLog);
                db.SaveChanges();


                return RedirectToAction("Index" ,  new { id = id });
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Firstname", tithePayment.UserId);
            ViewBag.MemberIdN = new SelectList(db.MemebershipTs.Where(x => x.Id == id), "Id", "Fullname", tithePayment.MemberIdN);
            return View(tithePayment);
        }

        // GET: TithePayments/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TithePayment tithePayment = db.TithePayments.Find(id);
            if (tithePayment == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Firstname", tithePayment.UserId);
            ViewBag.MemberIdN = new SelectList(db.MemebershipTs, "Id", "Fullname", tithePayment.MemberIdN);
            return View(tithePayment);
        }

        // POST: TithePayments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MemberIdN,RecordDate,UserId,Amt")] TithePayment tithePayment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tithePayment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index",  new { id = tithePayment.Id });
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Firstname", tithePayment.UserId);
            ViewBag.MemberIdN = new SelectList(db.MemebershipTs, "Id", "Fullname", tithePayment.MemberIdN);
            return View(tithePayment);
        }

        // GET: TithePayments/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TithePayment tithePayment = db.TithePayments.Find(id);
            if (tithePayment == null)
            {
                return HttpNotFound();
            }
            return View(tithePayment);
        }

        // POST: TithePayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TithePayment tithePayment = db.TithePayments.Find(id);
            db.TithePayments.Remove(tithePayment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: TithePayments/Edit/5      
        public ActionResult TitheRec(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TithePayment tithePayment = db.TithePayments.Find(id);
            if (tithePayment == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Firstname", tithePayment.UserId);
            ViewBag.MemberIdN = new SelectList(db.MemebershipTs, "Id", "Fullname", tithePayment.MemberIdN);
            return View(tithePayment);
        }

        [Authorize]
        public ActionResult PrintTitheRec(int? id)
        {
            var y = new ActionAsPdf("TitheRec", new { id = id });
            return y;
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

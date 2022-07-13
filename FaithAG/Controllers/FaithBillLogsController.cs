using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FaithAG.Models;

namespace FaithAG.Controllers
{
    public class FaithBillLogsController : Controller
    {
        private hpsadmin_dfaithagEntities db = new hpsadmin_dfaithagEntities();

        // GET: FaithBillLogs
        public async Task<ActionResult> Index()
        {
            var faithBillLogs = db.FaithBillLogs.Include(f => f.AspNetUser);
            return View(await faithBillLogs.ToListAsync());
        }

        // GET: FaithBillLogs/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FaithBillLog faithBillLog = await db.FaithBillLogs.FindAsync(id);
            if (faithBillLog == null)
            {
                return HttpNotFound();
            }
            return View(faithBillLog);
        }

        // GET: FaithBillLogs/Create
        public ActionResult Create()
        {
            ViewBag.BillUID = new SelectList(db.AspNetUsers, "Id", "Firstname");
            return View();
        }

        // POST: FaithBillLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,BillRef,BillItemType,BillPaymentType,BillUID,BillDate,PaidInAmt,PaidOutAmt,TotalAmt,PaymentStatus,CancellationStatus")] FaithBillLog faithBillLog)
        {
            if (ModelState.IsValid)
            {
                db.FaithBillLogs.Add(faithBillLog);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BillUID = new SelectList(db.AspNetUsers, "Id", "Firstname", faithBillLog.BillUID);
            return View(faithBillLog);
        }

        // GET: FaithBillLogs/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FaithBillLog faithBillLog = await db.FaithBillLogs.FindAsync(id);
            if (faithBillLog == null)
            {
                return HttpNotFound();
            }
            ViewBag.BillUID = new SelectList(db.AspNetUsers, "Id", "Firstname", faithBillLog.BillUID);
            return View(faithBillLog);
        }

        // POST: FaithBillLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,BillRef,BillItemType,BillPaymentType,BillUID,BillDate,PaidInAmt,PaidOutAmt,TotalAmt,PaymentStatus,CancellationStatus")] FaithBillLog faithBillLog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(faithBillLog).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BillUID = new SelectList(db.AspNetUsers, "Id", "Firstname", faithBillLog.BillUID);
            return View(faithBillLog);
        }

        // GET: FaithBillLogs/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FaithBillLog faithBillLog = await db.FaithBillLogs.FindAsync(id);
            if (faithBillLog == null)
            {
                return HttpNotFound();
            }
            return View(faithBillLog);
        }

        // POST: FaithBillLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            FaithBillLog faithBillLog = await db.FaithBillLogs.FindAsync(id);
            db.FaithBillLogs.Remove(faithBillLog);
            await db.SaveChangesAsync();
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

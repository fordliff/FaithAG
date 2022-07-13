using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FaithAG.Models;

namespace FaithAG.Controllers
{
    public class childrenDetailsController : Controller
    {
        private hpsadmin_dfaithagEntities db = new hpsadmin_dfaithagEntities();

        // GET: childrenDetails
        [Authorize]
        public ActionResult Index()
        {
            var childrenDetails = db.childrenDetails.Include(c => c.MemebershipT);
            return View(childrenDetails.ToList());
        }

        // GET: childrenDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            childrenDetail childrenDetail = db.childrenDetails.Find(id);
            if (childrenDetail == null)
            {
                return HttpNotFound();
            }
            return View(childrenDetail);
        }

        // GET: childrenDetails/Create
        [Authorize]
        public ActionResult Create(int? id)
        {
            ViewBag.MembershipIDNo = new SelectList(db.MemebershipTs.Where(x => x.Id == id), "Id", "Fullname");
            return View();
        }

        // POST: childrenDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MembershipIDNo,Firstname,Middlename,Lastname,DOB,PhoneNo,EmailAddress,WorkSch,Fullname")] childrenDetail childrenDetail, int? id)
        {
            if (ModelState.IsValid)
            {
                childrenDetail.Fullname = (childrenDetail.Firstname + " " + childrenDetail.Middlename + " " + childrenDetail.Lastname).Trim();
                db.childrenDetails.Add(childrenDetail);
                db.SaveChanges();
                //return RedirectToAction("Index");
                TempData["SuccessMsg"] = "Successfully Saved";
                //return RedirectToAction("Index");
                ModelState.Clear();
            }
            else
            {
                TempData["SuccessMsg"] = "";
                ModelState.AddModelError("", "Unsuccessfully Saved");
            }

            ViewBag.MembershipIDNo = new SelectList(db.MemebershipTs.Where(x => x.Id == id), "Id", "Fullname", childrenDetail.MembershipIDNo);
            return View(childrenDetail);
        }

        // GET: childrenDetails/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            childrenDetail childrenDetail = db.childrenDetails.Find(id);
            if (childrenDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.MembershipIDNo = new SelectList(db.MemebershipTs, "Id", "Firstname", childrenDetail.MembershipIDNo);
            return View(childrenDetail);
        }

        // POST: childrenDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MembershipIDNo,Firstname,Middlename,Lastname,DOB,PhoneNo,EmailAddress,WorkSch,Fullname")] childrenDetail childrenDetail)
        {
            if (ModelState.IsValid)
            {
                childrenDetail.Fullname = (childrenDetail.Firstname + " " + childrenDetail.Middlename + " " + childrenDetail.Lastname).Trim();
                db.Entry(childrenDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MembershipIDNo = new SelectList(db.MemebershipTs, "Id", "Firstname", childrenDetail.MembershipIDNo);
            return View(childrenDetail);
        }

        // GET: childrenDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            childrenDetail childrenDetail = db.childrenDetails.Find(id);
            if (childrenDetail == null)
            {
                return HttpNotFound();
            }
            return View(childrenDetail);
        }

        // POST: childrenDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            childrenDetail childrenDetail = db.childrenDetails.Find(id);
            db.childrenDetails.Remove(childrenDetail);
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

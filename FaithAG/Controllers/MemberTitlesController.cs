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
    public class MemberTitlesController : Controller
    {
        private hpsadmin_dfaithagEntities db = new hpsadmin_dfaithagEntities();

        // GET: MemberTitles
        public ActionResult Index()
        {
            return View(db.MemberTitles.ToList());
        }

        // GET: MemberTitles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberTitle memberTitle = db.MemberTitles.Find(id);
            if (memberTitle == null)
            {
                return HttpNotFound();
            }
            return View(memberTitle);
        }

        // GET: MemberTitles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MemberTitles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TitleName")] MemberTitle memberTitle)
        {
            if (ModelState.IsValid)
            {
                db.MemberTitles.Add(memberTitle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(memberTitle);
        }

        // GET: MemberTitles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberTitle memberTitle = db.MemberTitles.Find(id);
            if (memberTitle == null)
            {
                return HttpNotFound();
            }
            return View(memberTitle);
        }

        // POST: MemberTitles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TitleName")] MemberTitle memberTitle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberTitle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(memberTitle);
        }

        // GET: MemberTitles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberTitle memberTitle = db.MemberTitles.Find(id);
            if (memberTitle == null)
            {
                return HttpNotFound();
            }
            return View(memberTitle);
        }

        // POST: MemberTitles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemberTitle memberTitle = db.MemberTitles.Find(id);
            db.MemberTitles.Remove(memberTitle);
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

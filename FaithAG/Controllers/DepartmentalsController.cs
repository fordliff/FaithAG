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
    public class DepartmentalsController : Controller
    {
        private hpsadmin_dfaithagEntities db = new hpsadmin_dfaithagEntities();

        // List of Departmentals in the Church
        [Authorize]
        public ActionResult Index()
        {
            return View(db.Departmentals.ToList());
        }

        // GET: Departmentals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departmental departmental = db.Departmentals.Find(id);
            if (departmental == null)
            {
                return HttpNotFound();
            }
            return View(departmental);
        }

        // creating Departmentals for the church
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Departmentals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,Desc")] Departmental departmental)
        {
            if (ModelState.IsValid)
            {
                db.Departmentals.Add(departmental);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(departmental);
        }

        // Editing Departmentals for the church
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departmental departmental = db.Departmentals.Find(id);
            if (departmental == null)
            {
                return HttpNotFound();
            }
            return View(departmental);
        }

        // POST: Departmentals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,Desc")] Departmental departmental)
        {
            if (ModelState.IsValid)
            {
                db.Entry(departmental).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(departmental);
        }

        // Deleting Departmentals for the church
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departmental departmental = db.Departmentals.Find(id);
            if (departmental == null)
            {
                return HttpNotFound();
            }
            return View(departmental);
        }

        // Departmentals
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Departmental departmental = db.Departmentals.Find(id);
            db.Departmentals.Remove(departmental);
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

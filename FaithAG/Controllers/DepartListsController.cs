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
    public class DepartListsController : Controller
    {
        private hpsadmin_dfaithagEntities db = new hpsadmin_dfaithagEntities();

        // GET: DepartLists
        [Authorize]
        public ActionResult Index()
        {
            var departLists = db.DepartLists.Include(d => d.Departmental).Include(d => d.MemebershipT);
            return View(departLists.ToList());
        }

        // GET: DepartLists/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartList departList = db.DepartLists.Find(id);
            if (departList == null)
            {
                return HttpNotFound();
            }
            return View(departList);
        }

        // GET: DepartLists/Create
        [Authorize]
        public ActionResult Create(int? id)
        {
            ViewBag.DeptId = new SelectList(db.Departmentals, "Id", "name");
            ViewBag.MemberId = new SelectList(db.MemebershipTs.Where(x => x.Id == id), "Id", "Fullname");
            return View();
        }

        // POST: DepartLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MemberId,DeptId")] DepartList departList, int? id)
        {
            if (ModelState.IsValid)
            {
                db.DepartLists.Add(departList);
                db.SaveChanges();
                //return RedirectToAction("Index");
                ModelState.Clear();
                TempData["SuccessMsg"] = "Successfully Saved";
                //return RedirectToAction("Index");
            }
            else
            {               
                TempData["SuccessMsg"] = "";
                ModelState.AddModelError("", "Unsuccessfully Saved");
            }

            ViewBag.DeptId = new SelectList(db.Departmentals, "Id", "name", departList.DeptId);
            ViewBag.MemberId = new SelectList(db.MemebershipTs.Where(x => x.Id == id), "Id", "Fullname", departList.MemberId);
            return View(departList);
        }

        // GET: DepartLists/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartList departList = db.DepartLists.Find(id);
            if (departList == null)
            {
                return HttpNotFound();
            }
            ViewBag.DeptId = new SelectList(db.Departmentals, "Id", "name", departList.DeptId);
            ViewBag.MemberId = new SelectList(db.MemebershipTs, "Id", "Firstname", departList.MemberId);
            return View(departList);
        }

        // POST: DepartLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,MemberId,DeptId")] DepartList departList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(departList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DeptId = new SelectList(db.Departmentals, "Id", "name", departList.DeptId);
            ViewBag.MemberId = new SelectList(db.MemebershipTs, "Id", "Firstname", departList.MemberId);
            return View(departList);
        }

        // GET: DepartLists/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartList departList = db.DepartLists.Find(id);
            if (departList == null)
            {
                return HttpNotFound();
            }
            return View(departList);
        }

        // POST: DepartLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DepartList departList = db.DepartLists.Find(id);
            db.DepartLists.Remove(departList);
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

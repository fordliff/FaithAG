using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FaithAG.Models;
using Microsoft.AspNet.Identity;

namespace FaithAG.Controllers
{
    public class MemebershipTsController : Controller
    {
        private hpsadmin_dfaithagEntities db = new hpsadmin_dfaithagEntities();

        // GET: MemebershipTs
        [Authorize]
        public ActionResult Index()
        {
            var memebershipTs = db.MemebershipTs.Include(m => m.MaritalStatu).Include(m => m.Nationality).Include(m => m.AspNetUser).Include(m => m.MemberTitle).Include(m => m.TbIdentification).Include(m => m.Gender);
            return View(memebershipTs.ToList());
        }

        // GET: MemebershipTs/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemebershipT memebershipT = db.MemebershipTs.Find(id);
            if (memebershipT == null)
            {
                return HttpNotFound();
            }
            return View(memebershipT);
        }

        // GET: MemebershipTs/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.MaritalStatusID = new SelectList(db.MaritalStatus, "Id", "StatusName");
            ViewBag.NationalityID = new SelectList(db.Nationalities, "Id", "Country");
            ViewBag.UID = new SelectList(db.AspNetUsers, "Id", "Firstname");
            ViewBag.MemTitleID = new SelectList(db.MemberTitles, "Id", "TitleName");
            ViewBag.IDType = new SelectList(db.TbIdentifications, "Id", "name");
            ViewBag.GenderIDn = new SelectList(db.Genders, "Id", "StatusName");
            return View();
        }

        // POST: MemebershipTs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Firstname,Middlename,Lastname,DOB,PhoneNo,EmailAddress,NationalityID,ResidentialAddress,Address,MaritalStatusID,WorkPlace,UID,RecordDate,Fullname,City,HomeTown,SpouseId,SpouseFullname,Profession,GenderIDn,Profile_Img,MemTitleID,NextofKin,KinContact,DigitalAddress,IDType,IDNumber")] MemebershipT memebershipT, HttpPostedFileBase Profile_Pic)
        {
            if (ModelState.IsValid)
            {
                MemebershipT MT = db.MemebershipTs.Find(memebershipT.SpouseId);
                if (MT != null)
                {
                    memebershipT.SpouseFullname = MT.Fullname;
                }

                string path = LoadImageFile(Profile_Pic);
                if (path.Equals("-1"))
                {
                    memebershipT.Profile_Img = "~/Images/ProfilePics/NoPic.jpg";
                }
                else
                {
                    memebershipT.Profile_Img = path;
                }

                memebershipT.RecordDate = DateTime.UtcNow;
                memebershipT.UID = User.Identity.GetUserId();
                memebershipT.Fullname = (memebershipT.Firstname + " " + memebershipT.Middlename + " " + memebershipT.Lastname).Trim();

                db.MemebershipTs.Add(memebershipT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaritalStatusID = new SelectList(db.MaritalStatus, "Id", "StatusName", memebershipT.MaritalStatusID);
            ViewBag.NationalityID = new SelectList(db.Nationalities, "Id", "Country", memebershipT.NationalityID);
            ViewBag.UID = new SelectList(db.AspNetUsers, "Id", "Firstname", memebershipT.UID);
            ViewBag.MemTitleID = new SelectList(db.MemberTitles, "Id", "TitleName", memebershipT.MemTitleID);
            ViewBag.IDType = new SelectList(db.TbIdentifications, "Id", "name", memebershipT.IDType);
            ViewBag.GenderIDn = new SelectList(db.Genders, "Id", "StatusName", memebershipT.GenderIDn);
            return View(memebershipT);
        }

        // GET: MemebershipTs/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemebershipT memebershipT = db.MemebershipTs.Find(id);
            if (memebershipT == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaritalStatusID = new SelectList(db.MaritalStatus, "Id", "StatusName", memebershipT.MaritalStatusID);
            ViewBag.NationalityID = new SelectList(db.Nationalities, "Id", "Country", memebershipT.NationalityID);
            ViewBag.UID = new SelectList(db.AspNetUsers, "Id", "Firstname", memebershipT.UID);
            ViewBag.MemTitleID = new SelectList(db.MemberTitles, "Id", "TitleName", memebershipT.MemTitleID);
            ViewBag.IDType = new SelectList(db.TbIdentifications, "Id", "name", memebershipT.IDType);
            ViewBag.GenderIDn = new SelectList(db.Genders, "Id", "StatusName", memebershipT.GenderIDn);
            return View(memebershipT);
        }

        // POST: MemebershipTs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Firstname,Middlename,Lastname,DOB,PhoneNo,EmailAddress,NationalityID,ResidentialAddress,Address,MaritalStatusID,WorkPlace,UID,RecordDate,Fullname,City,HomeTown,SpouseId,SpouseFullname,Profession,GenderIDn,Profile_Img,MemTitleID,NextofKin,KinContact,DigitalAddress,IDType,IDNumber")] MemebershipT memebershipT, HttpPostedFileBase Profile_Pic)
        {
            if (ModelState.IsValid)
            {
                MemebershipT MT = db.MemebershipTs.Find(memebershipT.SpouseId);
                if (MT != null)
                {
                    memebershipT.SpouseFullname = MT.Fullname;
                }

                string path = LoadImageFile(Profile_Pic);
                if (path.Equals("-1"))
                {
                    //Nothing  
                    memebershipT.Profile_Img = "~/Images/ProfilePics/NoPic.jpg";
                }
                else
                {
                    memebershipT.Profile_Img = path;
                }
                memebershipT.Fullname = (memebershipT.Firstname + " " + memebershipT.Middlename + " " + memebershipT.Lastname).Trim();

                db.Entry(memebershipT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaritalStatusID = new SelectList(db.MaritalStatus, "Id", "StatusName", memebershipT.MaritalStatusID);
            ViewBag.NationalityID = new SelectList(db.Nationalities, "Id", "Country", memebershipT.NationalityID);
            ViewBag.UID = new SelectList(db.AspNetUsers, "Id", "Firstname", memebershipT.UID);
            ViewBag.MemTitleID = new SelectList(db.MemberTitles, "Id", "TitleName", memebershipT.MemTitleID);
            ViewBag.IDType = new SelectList(db.TbIdentifications, "Id", "name", memebershipT.IDType);
            ViewBag.GenderIDn = new SelectList(db.Genders, "Id", "StatusName", memebershipT.GenderIDn);
            return View(memebershipT);
        }

        // GET: MemebershipTs/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemebershipT memebershipT = db.MemebershipTs.Find(id);
            if (memebershipT == null)
            {
                return HttpNotFound();
            }
            return View(memebershipT);
        }

        // POST: MemebershipTs/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MemebershipT memebershipT = db.MemebershipTs.Find(id);
            db.MemebershipTs.Remove(memebershipT);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Uploading Membership Profile Picture
        public string LoadImageFile(HttpPostedFileBase file)
        {
            Random Rm = new Random();
            string path = "-1";
            int Rand = Rm.Next();

            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(file.FileName);
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png") || extension.ToLower().Equals(".tiff") || extension.ToLower().Equals(".gif"))
                {
                    try
                    {
                        path = Path.Combine(Server.MapPath("~/Images/ProfilePics/"), Rand + Path.GetFileName(file.FileName));
                        file.SaveAs(path);
                        path = "~/Images/ProfilePics/" + Rand + Path.GetFileName(file.FileName);
                    }
                    catch (Exception rs)
                    {
                        path = "-1";
                    }
                }
                else
                {
                    Response.Write("<script>alert('Only jpg or jpeg or png or tiff format are accepted');</script>");
                }
            }


            return path;
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

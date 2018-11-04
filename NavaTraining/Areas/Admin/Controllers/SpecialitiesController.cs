using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NavaTraining.Models;
using PagedList;

namespace NavaTraining.Areas.Admin.Controllers
{
    public class SpecialitiesController : Controller
    {
        private Nava_DBEntities db = new Nava_DBEntities();

        // GET: Admin/Specialities
        public ActionResult Index(int? page)
        {
          
            var speciality = db.Speciality.Include(s => s.Fields).OrderBy(a => a.SpecialID).ToPagedList(page ?? 1, 4);
            return View(speciality);
        }

        // GET: Admin/Specialities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Speciality speciality = db.Speciality.Find(id);
            if (speciality == null)
            {
                return HttpNotFound();
            }
            return View(speciality);
        }

        // GET: Admin/Specialities/Create
        public ActionResult Create()
        {
            ViewBag.FieldID = new SelectList(db.Fields, "FieldID", "FieldName");
            return View();
        }

        // POST: Admin/Specialities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SpecialID,FieldID,SpecialName")] Speciality speciality)
        {
            if (ModelState.IsValid)
            {
                db.Speciality.Add(speciality);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FieldID = new SelectList(db.Fields, "FieldID", "FieldName", speciality.FieldID);
            return View(speciality);
        }

        // GET: Admin/Specialities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Speciality speciality = db.Speciality.Find(id);
            if (speciality == null)
            {
                return HttpNotFound();
            }
            ViewBag.FieldID = new SelectList(db.Fields, "FieldID", "FieldName", speciality.FieldID);
            return View(speciality);
        }

        // POST: Admin/Specialities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SpecialID,FieldID,SpecialName")] Speciality speciality)
        {
            if (ModelState.IsValid)
            {
                db.Entry(speciality).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FieldID = new SelectList(db.Fields, "FieldID", "FieldName", speciality.FieldID);
            return View(speciality);
        }

        // GET: Admin/Specialities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Speciality speciality = db.Speciality.Find(id);
            if (speciality == null)
            {
                return HttpNotFound();
            }
            return View(speciality);
        }

        // POST: Admin/Specialities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Speciality speciality = db.Speciality.Find(id);
            db.Special_Fields.Where(t => t.SpecialID == id).ToList().ForEach(t => db.Special_Fields.Remove(t));
            db.Speciality.Remove(speciality);
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

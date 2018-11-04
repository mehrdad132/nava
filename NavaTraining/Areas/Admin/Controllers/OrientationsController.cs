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
    public class OrientationsController : Controller
    {
        private Nava_DBEntities db = new Nava_DBEntities();

        // GET: Admin/Orientations
        public ActionResult Index(int? page)
        {
            var orientation = db.Orientation.Include(o => o.Fields).OrderBy(a=>a.OrientationID).ToPagedList(page ?? 1, 4);
            return View(orientation);
        }

        // GET: Admin/Orientations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orientation orientation = db.Orientation.Find(id);
            if (orientation == null)
            {
                return HttpNotFound();
            }
            return View(orientation);
        }

        // GET: Admin/Orientations/Create
        public ActionResult Create()
        {
            ViewBag.FieldID = new SelectList(db.Fields, "FieldID", "FieldName");
            return View();
        }

        // POST: Admin/Orientations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrientationID,FieldID,OrientationName")] Orientation orientation)
        {
            if (ModelState.IsValid)
            {
                db.Orientation.Add(orientation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FieldID = new SelectList(db.Fields, "FieldID", "FieldName", orientation.FieldID);
            return View(orientation);
        }

        // GET: Admin/Orientations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orientation orientation = db.Orientation.Find(id);
            if (orientation == null)
            {
                return HttpNotFound();
            }
            ViewBag.FieldID = new SelectList(db.Fields, "FieldID", "FieldName", orientation.FieldID);
            return View(orientation);
        }

        // POST: Admin/Orientations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrientationID,FieldID,OrientationName")] Orientation orientation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orientation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FieldID = new SelectList(db.Fields, "FieldID", "FieldName", orientation.FieldID);
            return View(orientation);
        }

        // GET: Admin/Orientations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orientation orientation = db.Orientation.Find(id);
            if (orientation == null)
            {
                return HttpNotFound();
            }
            return View(orientation);
        }

        // POST: Admin/Orientations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.EducatInfo.Where(t => t.OrientationID == id).ToList().ForEach(t => db.EducatInfo.Remove(t));
            Orientation orientation = db.Orientation.Find(id);
            db.Orientation.Remove(orientation);
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

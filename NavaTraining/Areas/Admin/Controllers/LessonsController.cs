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
    public class LessonsController : Controller
    {
        private Nava_DBEntities db = new Nava_DBEntities();

        // GET: Admin/Lessons
        public ActionResult Index(int? page)
        {
            var lessons = db.Lessons.Include(l => l.Fields).OrderBy(a => a.LessonID).ToPagedList(page ?? 1, 4);
            return View(lessons);
        }

        // GET: Admin/Lessons/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lessons lessons = db.Lessons.Find(id);
            if (lessons == null)
            {
                return HttpNotFound();
            }
            return View(lessons);
        }

        // GET: Admin/Lessons/Create
        public ActionResult Create()
        {
            ViewBag.FieldID = new SelectList(db.Fields, "FieldID", "FieldName");
            return View();
        }

        // POST: Admin/Lessons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LessonID,LessonCode,FieldID,LessonName")] Lessons lessons)
        {
            if (ModelState.IsValid)
            {
                db.Lessons.Add(lessons);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FieldID = new SelectList(db.Fields, "FieldID", "FieldName", lessons.FieldID);
            return View(lessons);
        }

        // GET: Admin/Lessons/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lessons lessons = db.Lessons.Find(id);
            if (lessons == null)
            {
                return HttpNotFound();
            }
            ViewBag.FieldID = new SelectList(db.Fields, "FieldID", "FieldName", lessons.FieldID);
            return View(lessons);
        }

        // POST: Admin/Lessons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LessonID,LessonCode,FieldID,LessonName")] Lessons lessons)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lessons).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FieldID = new SelectList(db.Fields, "FieldID", "FieldName", lessons.FieldID);
            return View(lessons);
        }

        // GET: Admin/Lessons/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lessons lessons = db.Lessons.Find(id);
            if (lessons == null)
            {
                return HttpNotFound();
            }
            return View(lessons);
        }

        // POST: Admin/Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.Field_Lessons.Where(t => t.LessonID == id).ToList().ForEach(t => db.Field_Lessons.Remove(t));
            Lessons lessons = db.Lessons.Find(id);
            db.Lessons.Remove(lessons);
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

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
    public class FieldsController : Controller
    {
        private Nava_DBEntities db = new Nava_DBEntities();

        // GET: Admin/Fields
        public ActionResult Index(int? page)
        {
            var q = db.Fields.OrderBy(a => a.FieldID).ToPagedList(page ?? 1, 4);
            return View(q);
        }

        // GET: Admin/Fields/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fields fields = db.Fields.Find(id);
            if (fields == null)
            {
                return HttpNotFound();
            }
            return View(fields);
        }

        // GET: Admin/Fields/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Fields/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FieldID,FieldName")] Fields fields)
        {
            if (ModelState.IsValid)
            {
                db.Fields.Add(fields);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fields);
        }

        // GET: Admin/Fields/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fields fields = db.Fields.Find(id);
            if (fields == null)
            {
                return HttpNotFound();
            }
            return View(fields);
        }

        // POST: Admin/Fields/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FieldID,FieldName")] Fields fields)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fields).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fields);
        }

        // GET: Admin/Fields/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Fields fields = db.Fields.Find(id);
            if (fields == null)
            {
                return HttpNotFound();
            }
            return View(fields);
        }

        // POST: Admin/Fields/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.EducatInfo.Where(t => t.FieldID == id).ToList().ForEach(t => db.EducatInfo.Remove(t));
            db.Lessons.Where(t => t.FieldID == id).ToList().ForEach(t => db.Lessons.Remove(t));
            db.Orientation.Where(t => t.FieldID == id).ToList().ForEach(t => db.Orientation.Remove(t));
            db.Special_Fields.Where(t => t.FieldID == id).ToList().ForEach(t => db.Special_Fields.Remove(t));
            Fields fields = db.Fields.Find(id);
            db.Fields.Remove(fields);
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

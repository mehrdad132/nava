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
    public class UniverCitiesController : Controller
    {
        private Nava_DBEntities db = new Nava_DBEntities();

        // GET: Admin/UniverCities
        public ActionResult Index(int? page)
        {
            return View(db.UniverCity.OrderBy(a => a.UniID).ToPagedList(page ?? 1, 4));
        }

        // GET: Admin/UniverCities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UniverCity univerCity = db.UniverCity.Find(id);
            if (univerCity == null)
            {
                return HttpNotFound();
            }
            return View(univerCity);
        }

        // GET: Admin/UniverCities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/UniverCities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UniID,UniName")] UniverCity univerCity)
        {
            if (ModelState.IsValid)
            {
                db.UniverCity.Add(univerCity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(univerCity);
        }

        // GET: Admin/UniverCities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UniverCity univerCity = db.UniverCity.Find(id);
            if (univerCity == null)
            {
                return HttpNotFound();
            }
            return View(univerCity);
        }

        // POST: Admin/UniverCities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UniID,UniName")] UniverCity univerCity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(univerCity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(univerCity);
        }

        // GET: Admin/UniverCities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UniverCity univerCity = db.UniverCity.Find(id);
            if (univerCity == null)
            {
                return HttpNotFound();
            }
            return View(univerCity);
        }

        // POST: Admin/UniverCities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UniverCity univerCity = db.UniverCity.Find(id);
            //var qeducat = db.EducatInfo.Select(a => a).Where(a => a.UniID == univerCity.UniID);
            //db.EducatInfo.RemoveRange(qeducat.AsEnumerable());
            db.EducatInfo.Where(t => t.UniID == id).ToList().ForEach(t => db.EducatInfo.Remove(t));
           
            db.UniverCity.Remove(univerCity);
           
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

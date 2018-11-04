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
    public class ResultItemsController : Controller
    {
        private Nava_DBEntities db = new Nava_DBEntities();

        // GET: Admin/ResultItems
        public ActionResult Index(int? page)
        {
            var q = db.ResultItem.OrderBy(a => a.ItemID).ToPagedList(page ?? 1, 4);
            return View(q);
        }

        // GET: Admin/ResultItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResultItem resultItem = db.ResultItem.Find(id);
            if (resultItem == null)
            {
                return HttpNotFound();
            }
            return View(resultItem);
        }

        // GET: Admin/ResultItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/ResultItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemID,ValueItem")] ResultItem resultItem)
        {
            if (ModelState.IsValid)
            {
                db.ResultItem.Add(resultItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(resultItem);
        }

        // GET: Admin/ResultItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResultItem resultItem = db.ResultItem.Find(id);
            if (resultItem == null)
            {
                return HttpNotFound();
            }
            return View(resultItem);
        }

        // POST: Admin/ResultItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemID,ValueItem")] ResultItem resultItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resultItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(resultItem);
        }

        // GET: Admin/ResultItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResultItem resultItem = db.ResultItem.Find(id);
            if (resultItem == null)
            {
                return HttpNotFound();
            }
            return View(resultItem);
        }

        // POST: Admin/ResultItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ResultItem resultItem = db.ResultItem.Find(id);
            db.ResultItem.Remove(resultItem);
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

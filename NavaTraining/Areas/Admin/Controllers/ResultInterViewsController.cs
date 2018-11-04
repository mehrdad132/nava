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
    public class ResultInterViewsController : Controller
    {
        private Nava_DBEntities db = new Nava_DBEntities();

        // GET: Admin/ResultInterViews
        public ActionResult Index(int? page, string fieldname, string orientationname,string result, int Item = 1)
        {

            //var quser = db.RegisterUser.Where(a => a.NationalNumber == meli).FirstOrDefault();
            //if (Item == 1)
            //{


            //    if (meli == null)
            //    {
            //        var resultInterView =
            //            db.ResultInterView.Include(r => r.ResultItem)
            //                .OrderBy(a => a.ResultID)
            //                .ToPagedList(page ?? 1, 4);
            //        return View(resultInterView);
            //    }
            //    else
            //    {
            //        var resultInterView =
            //        db.ResultInterView.Include(r => r.ResultItem)
            //            .Where(a => a.UserID == quser.UserID)
            //            .OrderBy(a => a.ResultID)
            //            .ToPagedList(page ?? 1, 4);
            //        return View(resultInterView);
            //    }
            //}
            //else
            //{
            //    var resultInterView =
            //        db.ResultInterView.Include(r => r.ResultItem)
            //            .Where(a => a.ItemID == Item)
            //            .OrderBy(a => a.ResultID)
            //            .ToPagedList(page ?? 1, 4);

            //    return View(resultInterView);
            //}
            if(Item == 1)
            {


                var query = db.ResultInterView.Select(a => new FormEndResultViewModel
                {
                    ResultID = a.ResultID,
                    UserID = a.UserID,
                    Name =
                        db.RegisterUser.Where(e => e.UserID == a.UserID)
                            .Select(m => m.UserName)
                            .DefaultIfEmpty("")
                            .FirstOrDefault(),
                    Family =
                        db.RegisterUser.Where(e => e.UserID == a.UserID)
                            .Select(m => m.FullName)
                            .DefaultIfEmpty("")
                            .FirstOrDefault(),
                    Mobile =
                        db.RegisterUser.Where(e => e.UserID == a.UserID)
                            .Select(m => m.Mobile)
                            .DefaultIfEmpty("")
                            .FirstOrDefault(),

                    ItemID = db.ResultInterView.Where(e => e.UserID == a.UserID).Select(m => m.ItemID).FirstOrDefault(),
                    ValueItem = db.ResultInterView.Where(e => e.UserID == a.UserID).FirstOrDefault().ResultItem.ValueItem,

                    FieldName = db.EducatInfo.Where(e => e.UserID == a.UserID)
                        .OrderByDescending(o => o.BeginStudy)
                        .Select(m => m.Fields.FieldName)
                        .DefaultIfEmpty("")
                        .FirstOrDefault(),
                    OrientationName = db.EducatInfo.Where(e => e.UserID == a.UserID)
                        .OrderByDescending(o => o.BeginStudy)
                        .Select(m => m.Orientation.OrientationName)
                        .DefaultIfEmpty("")
                        .FirstOrDefault(),

                }).Where(a => (string.IsNullOrEmpty(fieldname) || a.FieldName == fieldname) &&
                                (string.IsNullOrEmpty(orientationname) || a.OrientationName == orientationname) &&
                              (string.IsNullOrEmpty(result) || a.ValueItem.Contains(result))
                    ).OrderByDescending(a => a.UserID).ToPagedList(page ?? 1, 10);
                ViewBag.Orientation = orientationname;
                ViewBag.Field = fieldname;
                ViewBag.Itemid = Item;
                ViewBag.result = result;
                return View(query);

            }
            else
            {
                var query = db.ResultInterView.Select(a => new FormEndResultViewModel
                {
                    UserID = a.UserID,
                    Name =
                      db.RegisterUser.Where(e => e.UserID == a.UserID)
                          .Select(m => m.UserName)
                          .DefaultIfEmpty("")
                          .FirstOrDefault(),
                    Family =
                      db.RegisterUser.Where(e => e.UserID == a.UserID)
                          .Select(m => m.FullName)
                          .DefaultIfEmpty("")
                          .FirstOrDefault(),
                    Mobile =
                      db.RegisterUser.Where(e => e.UserID == a.UserID)
                          .Select(m => m.Mobile)
                          .DefaultIfEmpty("")
                          .FirstOrDefault(),

                    ItemID = db.ResultInterView.Where(e => e.UserID == a.UserID).Select(m => m.ItemID).FirstOrDefault(),
                    ValueItem = db.ResultInterView.Where(e=>e.UserID==a.UserID).FirstOrDefault().ResultItem.ValueItem.ToString(),

                    FieldName = db.EducatInfo.Where(e => e.UserID == a.UserID)
                      .OrderByDescending(o => o.BeginStudy)
                      .Select(m => m.Fields.FieldName)
                      .DefaultIfEmpty("")
                      .FirstOrDefault(),
                    OrientationName = db.EducatInfo.Where(e => e.UserID == a.UserID)
                      .OrderByDescending(o => o.BeginStudy)
                      .Select(m => m.Orientation.OrientationName)
                      .DefaultIfEmpty("")
                      .FirstOrDefault(),

                }).Where(a => (string.IsNullOrEmpty(fieldname) || a.FieldName == fieldname) &&
                                (string.IsNullOrEmpty(orientationname) || a.OrientationName == orientationname) && (a.ItemID == Item) 
                              
                  ).OrderByDescending(a => a.UserID).ToPagedList(page ?? 1, 10);
                ViewBag.Orientation = orientationname;
                ViewBag.Field = fieldname;
                ViewBag.Itemid = Item;
                return View(query);

            }

        }
    

        // GET: Admin/ResultInterViews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            ResultInterView resultInterView = db.ResultInterView.Find(id);
            if (resultInterView == null)
            {
                return HttpNotFound();
            }
            return View(resultInterView);
        }

        // GET: Admin/ResultInterViews/Create
        public ActionResult Create()
        {
            ViewBag.ItemID = new SelectList(db.ResultItem, "ItemID", "ValueItem");
            return View();
        }

        // POST: Admin/ResultInterViews/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResultID,ItemID,UserID,SkillSpeaking,PassionWork,Sobriety,Rhetorical,DateForWork,VideoName")] ResultInterView resultInterView)
        {
            if (ModelState.IsValid)
            {
                db.ResultInterView.Add(resultInterView);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ItemID = new SelectList(db.ResultItem, "ItemID", "ValueItem", resultInterView.ItemID);
            return View(resultInterView);
        }

        // GET: Admin/ResultInterViews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResultInterView resultInterView = db.ResultInterView.Find(id);
            if (resultInterView == null)
            {
                return HttpNotFound();
            }
            ViewBag.ItemID = new SelectList(db.ResultItem, "ItemID", "ValueItem", resultInterView.ItemID);
            return View(resultInterView);
        }

        // POST: Admin/ResultInterViews/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResultID,ItemID,UserID,SkillSpeaking,PassionWork,Sobriety,Rhetorical,DateForWork,VideoName")] ResultInterView resultInterView,
            HttpPostedFileBase fileupload)
        {
            if (ModelState.IsValid)
            {
                

                if (fileupload == null)
                {
                    var qvideo = db.ResultInterView.Where(a => a.UserID == resultInterView.UserID).Select(x=>x.VideoName).FirstOrDefault();

                    resultInterView.VideoName = qvideo;
                    db.ResultInterView.Attach(resultInterView);
                    db.Entry(resultInterView).State = EntityState.Modified;
                }
                else
                {



                    if (fileupload != null && fileupload.ContentLength > 0)
                    {
                        if (fileupload.ContentLength/1024 <= 100000)
                        {
                            if (fileupload.ContentType == "video/mp4" ||
                                fileupload.ContentType == "application/mp4" ||
                                fileupload.ContentType == "Audio/Video/mp4")
                            {
                                resultInterView.VideoName = Guid.NewGuid().ToString().Replace("-", "") +
                                                            System.IO.Path.GetExtension(fileupload.FileName);
                                string path =
                                    System.IO.Path.Combine(
                                        Server.MapPath("~/VideoUpload/UserVideo/" + resultInterView.VideoName));
                                fileupload.SaveAs(path);
                                resultInterView.VideoName = resultInterView.VideoName;

                            }
                            else
                            {
                                TempData["msg"] = "پسوند فایل باید mp4 باشد ";

                                return RedirectToAction("Edit");
                            }
                        }
                        else
                        {
                            TempData["msg"] = "حجم فایل شما باید کمتر از 100 مگابایت باشد";
                            return RedirectToAction("Edit");
                        }
                    }
                    db.Entry(resultInterView).State = EntityState.Modified;
                }

                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ItemID = new SelectList(db.ResultItem, "ItemID", "ValueItem", resultInterView.ItemID);
            return View(resultInterView);
        }

        // GET: Admin/ResultInterViews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResultInterView resultInterView = db.ResultInterView.Find(id);
            if (resultInterView == null)
            {
                return HttpNotFound();
            }
            return View(resultInterView);
        }

        // POST: Admin/ResultInterViews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ResultInterView resultInterView = db.ResultInterView.Find(id);
            
           System.IO.File.Delete(Server.MapPath("~/VideoUpload/UserVideo/" + resultInterView.VideoName));
         
            db.ResultInterView.Remove(resultInterView);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public void DeleteVideo(int id)
        {
            var video = db.ResultInterView.Find(id);
            System.IO.File.Delete(Server.MapPath("~/VideoUpload/UserVideo/" + video.VideoName));
            System.IO.File.Delete(Server.MapPath("~/VideoUpload/UserVideo/" + video.VideoName));
            video.VideoName = null;
            db.Entry(video).State = EntityState.Modified;
            db.SaveChanges();
       
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

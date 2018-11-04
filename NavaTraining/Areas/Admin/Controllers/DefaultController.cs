using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NavaTraining.Models;
using PagedList;
using NavaTraining.Classes;
using System.Data.Entity.Validation;

namespace NavaTraining.Areas.Admin.Controllers
{
    public class DefaultController : Controller
    {
        Nava_DBEntities db = new Nava_DBEntities();
        // GET: Admin/Default
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Orientation(int FieldID)
        {
            return Json(db.Orientation.Where(a => a.FieldID ==
                                                  FieldID).Select(s => new
                                                  {
                                                      OrientationID = s.OrientationID,
                                                      OrientationName = s.OrientationName
                                                  }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Lessons(int FieldID)
        {
            return Json(db.Lessons.Where(a => a.FieldID ==
                                              FieldID).Select(s => new
                                              {
                                                  LessonID = s.LessonID,
                                                  LessonName = s.LessonName
                                              }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Special(int FieldID)
        {
            return Json(db.Speciality.Where(a => a.FieldID ==
                                                 FieldID).Select(s => new
                                                 {
                                                     SpecialID = s.SpecialID,
                                                     SpecialName = s.SpecialName
                                                 }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetOrientadList(string term)
        {
        
            List<string> Orientation;
            Orientation = db.Orientation.Where(x=>x.OrientationName.StartsWith(term)).Select(a => a.OrientationName).Distinct().ToList();
            return Json(Orientation, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetFieldList(string term)
        {

            List<string> Field;
            Field = db.Fields.Where(x => x.FieldName.StartsWith(term)).Select(a => a.FieldName).Distinct().ToList();
            return Json(Field, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddListSpecial(int fieldID, string fieldName, int specialID, string specialName)
        {

            List<Special_VM> _special = new List<Special_VM>();
            if (Session["Special"] != null)
            {
                _special = Session["Special"] as List<Special_VM>;
            }



            _special.Add(new Special_VM()
            {
                FieldID = fieldID,
                FieldName = fieldName,
                SpecialID = specialID,
                SpecialName = specialName



            });

            Session["Special"] = _special;

            return PartialView("ListSpecial", _special);
        }

        public ActionResult ListSpecial()
        {
            List<Special_VM> _special = new List<Special_VM>();
            if (Session["Special"] != null)
            {
                _special = Session["Special"] as List<Special_VM>;

            }
            return PartialView(_special);
        }


        public ActionResult DeleteSpecial(int fieldID, string fieldName, int specialID, string specialName)
        {
            List<Special_VM> _special = new List<Special_VM>();
            if (Session["Special"] != null)
            {
                _special = Session["Special"] as List<Special_VM>;

                _special.Remove(_special.First(i => i.FieldID == fieldID
                                                    && i.FieldName == fieldName && i.SpecialID == specialID
                                                    && i.SpecialName == specialName));
                Session["Special"] = _special;

            }
            return PartialView("ListSpecial", _special);
        }


        public ActionResult AddListSkills(int LessonID, string LessonName, string ProblemSolving, string MakingVideo,
            string RelatedSoftware)

        {
            List<Skills_VM> _skill = new List<Skills_VM>();
            if (Session["Skill"] != null)
            {
                _skill = Session["Skill"] as List<Skills_VM>;
            }

            if (RelatedSoftware == "")
            {
                _skill.Add(new Skills_VM()
                {
                    LessonID = LessonID,
                    LessonName = LessonName,
                    ProblemSolving = ProblemSolving,
                    MakingVideo = MakingVideo,
                    RelatedSoftware = "-"

                });

            }
            else
            {
                _skill.Add(new Skills_VM()
                {
                    LessonID = LessonID,
                    LessonName = LessonName,
                    ProblemSolving = ProblemSolving,
                    MakingVideo = MakingVideo,
                    RelatedSoftware = RelatedSoftware

                });

            }

            Session["Skill"] = _skill;

            return PartialView("ListSkill", _skill);
        }

        public ActionResult ListSkill()
        {
            List<Skills_VM> _skill = new List<Skills_VM>();
            if (Session["Skill"] != null)
            {
                _skill = Session["Skill"] as List<Skills_VM>;

            }
            return PartialView(_skill);
        }



        public ActionResult DeleteSkills(int LessonID, string ProblemSolving, string MakingVideo, string RelatedSoftware)
        {
            List<Skills_VM> _skill = new List<Skills_VM>();
            if (Session["Skill"] != null)
            {
                _skill = Session["Skill"] as List<Skills_VM>;

                _skill.Remove(_skill.First(i => i.LessonID == LessonID && i.ProblemSolving == ProblemSolving
                                                && i.MakingVideo == MakingVideo && i.RelatedSoftware == RelatedSoftware));
                Session["Skill"] = _skill;

            }
            return PartialView("ListSkill", _skill);
        }


        /// <summary>
        /// چهار اکشن زیر مربوط به مدیریت کاربران اعم از جستجوی ایجکسی و
        /// جزییات کاربران می باشد
        /// </summary>
        /// <param name="ManageUsersRegister"></param>
        /// <param name="Load_UsersbySearch"></param>
        /// <param name="DetailsUsers"></param>
        /// <param name="DeleteUsers"></param>
        /// <returns></returns>
        public ActionResult ManageUsersRegister(int? page, string strcheck)
        {
            //شروع صفحات از صفحه اول
            var PageNumber = page ?? 1;

            //var users = db.RegisterUser
            //    .Where(x => string.IsNullOrEmpty(strcheck) ||  x.UserLogin.EducatInfo.Any(e => e.UniverCity.UniName.Contains(strcheck)))
            //    .OrderByDescending(a => a.UserID).ToPagedList(PageNumber, 2);
            //ViewBag.strcheck = strcheck;


            var q = (from a in db.RegisterUser

                where
                    (string.IsNullOrEmpty(strcheck) ||
                     a.UserLogin.EducatInfo.Any(e => e.UniverCity.UniName.StartsWith(strcheck))
                     ||
                     a.UserLogin.EducatInfo.Any(e => e.Fields.FieldName.StartsWith(strcheck)) &&
                     (a.UserLogin.EducatInfo.Any(e => e.BeginStudy != null && e.YearGrad == null)))
                select new ListFilterUsers
                {
                    RegID = a.RegID,
                    UserID = a.UserID,
                    UserName = a.UserName,
                    Family = a.FullName,
                    Mobile = a.Mobile,
                    Phone = a.Phone,
                    Email = a.Email
                }).OrderBy(a => a.RegID).ToPagedList(page ?? 1, 2);

            ViewBag.strcheck = strcheck;




            return View(q);
        }

        public ActionResult Load_UsersbySearch(string strcheck)
        {

            //var res = db.RegisterUser
            //    .Where(x => x.UserLogin.EducatInfo.Any(e => e.UniverCity.UniName.Contains(strcheck)))
            //     .OrderByDescending(a => a.UserID)
            //    .ToPagedList(1,2);
            var q = (from a in db.RegisterUser

                where
                    (string.IsNullOrEmpty(strcheck) ||
                     a.UserLogin.EducatInfo.Any(e => e.UniverCity.UniName.StartsWith(strcheck))
                     ||
                     a.UserLogin.EducatInfo.Any(e => e.Fields.FieldName.StartsWith(strcheck)) &&
                     (a.UserLogin.EducatInfo.Any(e => e.BeginStudy != null && e.YearGrad == null)))

                select new ListFilterUsers
                {
                    RegID = a.RegID,
                    UserID = a.UserID,
                    UserName = a.UserName,
                    Family = a.FullName,
                    Mobile = a.Mobile,
                    Phone = a.Phone,
                    Email = a.Email
                }).OrderBy(a => a.RegID).ToPagedList(1, 2);
            ViewBag.strcheck = strcheck;


            return PartialView(q);

        }

        public ActionResult DetailsUsers(int id)
        {
            
            var q = db.RegisterUser.Find(id);
            
         
                return View(q);
           
        }

        public ActionResult DeleteUsers(int id)
        {
            int userid = db.RegisterUser.Find(id).UserID;

            
            var q = db.RegisterUser.Where(a => a.UserID == userid).FirstOrDefault();
            if (q != null)
            {
                var qeducat = db.EducatInfo.Select(a => a).Where(a => a.UserID == q.UserID);
                db.EducatInfo.RemoveRange(qeducat.AsEnumerable());
                var interview = db.InterView.Select(a => a).Where(a => a.UserID == userid);
                db.InterView.RemoveRange(interview.AsEnumerable());
                var laguser = db.User_Lang.Select(a => a).Where(a => a.UserID == userid);
                db.User_Lang.RemoveRange(laguser.AsEnumerable());
                var copmuser = db.Company.Select(a => a).Where(a => a.UserID == userid);
                db.Company.RemoveRange(copmuser.AsEnumerable());
                var lessonuser = db.Field_Lessons.Select(a => a).Where(a => a.UserID == userid);
                db.Field_Lessons.RemoveRange(lessonuser.AsEnumerable());
                db.RegisterUser.Remove(q);
                if (Convert.ToBoolean(db.SaveChanges()))
                {
                    TempData["msg"] = ";کاربر با موفقییت حذف شد";
                }
                else
                {
                    TempData["msg"] = "کاربر حذف نشد لطفا دوباره تلاش نمایید";
                }

            }
            return RedirectToAction("ManageUsersRegister");
        }


        /// <summary>
        /// چهار اکشن زیر مربوط به مصاحبه می باشد
        /// </summary>
        /// <param name="InterView"></param>
        /// <param name="InterviewList"></param>
        /// <param name="ManageInterviewList"></param>

        //ثبت تاریخ رزرو
        public ActionResult InterView()
        {
            return View();
        }

        //ثبت تاریخ رزرو
        [HttpPost]
        public ActionResult InterView(InterView interview, int id)

        {
            int q = db.RegisterUser.Find(id).UserID;

            if (!db.InterView.Any(a => a.UserID == q))
            {


                //تاریخ و ساعت رو میگیره و با تاریخ اول مقایسه میکنه و بعدش با
                //ساعت های اون تاریخ و فیلد هایی رو برمیگردونه که با این ساعتت یکسان نیستن
                var qdate = (from a in db.InterView
                    where a.DateView == interview.DateView && a.ClockView != interview.ClockView
                    select a).ToList();
                //تاریخ و ساعت رو میگیره و با تاریخ اول مقایسه میکنه و بعدش 
                //با ساعت های اون تاریخ و فیلد هایی رو برمیگردونه که با این ساعتت یکسان هستن
                var qdateCopmare = (from a in db.InterView
                    where a.DateView == interview.DateView && a.ClockView == interview.ClockView
                    select a).FirstOrDefault();


                //اگر تعداد مصاحبه های یک روز بیشتر از 5 باشد اجازه ثبت ساعت در آن روز را نمیدهد.یعنی ظرفیت آن روز تکمیل است
                if (qdate.Count() > 5)
                {
                    TempData["msg"] = "همه ی ساعات تاریخ" + interview.DateView.ToShortPersianDateString() +
                                      "برای مصاحبه رزرو شده است-";

                }
                else
                {
                    //اگه تارخ ورودی برای اولین بار وارد مشود وجود ندارد
                    if (!db.InterView.Any(a => a.DateView == interview.DateView))
                    {
                        InterView view = new InterView()
                        {
                            UserID = q,
                            ClockView = interview.ClockView,
                            DateView = interview.DateView

                        };
                        db.InterView.Add(view);
                        db.SaveChanges();
                        TempData["msg"] = "تاریخ مصاحبه برای این کاربر رزرو شد";
                    }
                    else
                    {

                        if (qdateCopmare == null)
                        {
                            //اگه تعداد ساعات رزر شده در تاریخ مورد نظر کمتر از پنج تا بود
                            if (
                                db.InterView.Where(a => a.DateView == interview.DateView)
                                    .Select(a => a.ClockView)
                                    .ToList()
                                    .Count() < 5)
                            {
                                int Count = 0;
                                foreach (var item in qdate)
                                {
                                    if (item.DateView == interview.DateView)
                                    {
                                        if (item.ClockView != interview.ClockView)
                                        {

                                            Count += 1;
                                            if (Count == qdate.Count)
                                            {
                                                InterView view = new InterView()
                                                {
                                                    UserID = q,
                                                    ClockView = interview.ClockView,
                                                    DateView = interview.DateView

                                                };
                                                db.InterView.Add(view);
                                                db.SaveChanges();
                                                TempData["msg"] = "تاریخ مصاحبه برای این کاربر رزرو شد";
                                            }

                                        }


                                    }
                                }

                            }
                            else
                            {
                                TempData["msg"] = "همه ی ساعات تاریخ" + interview.DateView.ToShortPersianDateString() +
                                                  "برای مصاحبه رزرو شده است";
                            }


                        }

                        else
                        {
                            TempData["msg"] = "در تاریخ مورد نظر شما این ساعت برای مصاحبه رزرو شده است";
                        }


                    }


                }

                return RedirectToAction("InterView");
            }
            else
            {
                var qexistdate = db.InterView.Where(a => a.UserID == q).FirstOrDefault().DateView;
             
                TempData["msg"] = "برای این کاربر تاریخ" + qexistdate.ToLongPersianDateString() + "رزرو شده است";
                return RedirectToAction("InterView");
            }

        }

        //لیست افراد با تاریخ مصاحبه 
        public ActionResult InterviewList()
        {
            var q = db.InterView.OrderBy(a => a.DateView).Distinct().ToList();

            return View(q);
        }

        //مدیریت لیست مصاحبه شوندگان به صورت صفحه بندی
        public ActionResult ManageInterviewList(int? page, string meli)
        {
            //یک روش برای ریختن اطلاعات در ویو مدل
            var q = (from a in db.RegisterUser
                join b in db.InterView on a.UserID equals b.UserID
                where a.UserID == a.UserID && ((string.IsNullOrEmpty(meli) || a.NationalNumber == meli))
                select new ListDate
                {
                    ViewID = b.ViewID,
                    UserID = b.UserID,
                    Date = b.DateView,
                    Clockview = b.ClockView,
                    Username = a.UserName,
                    Family = a.FullName,
                    Mobile = a.Mobile,
                    Meli = a.NationalNumber
                }).OrderBy(a => a.Date).ToPagedList(page ?? 1, 4);
            ViewBag.NationalNumber = meli;
            //روش دوم ریختن اطلاعات در ویو مدل
            //var users = db.InterView.Select(x => new InterViewList_VM
            //{
            //    DateView = x.DateView,
            //    ClockView = x.ClockView,
            //    UserName= x.UserName



            //}).OrderBy(a => a.Date).ToPagedList(page ?? 1, 4);
            return View(q);
        }

        //حذف کاربر از لیست مصاحبه 
        public ActionResult DeleteInterviewUsers(int id)
        {
            int userid = db.InterView.Find(id).UserID;
            var q = db.InterView.Where(a => a.UserID == userid).FirstOrDefault();
            db.InterView.Remove(q);
            if (Convert.ToBoolean(db.SaveChanges()))
            {
                TempData["msg"] = "کاربر با موفقییت حذف شد";
                return RedirectToAction("ManageInterviewList");
            }
            else
            {
                TempData["msg"] = "کاربر حذف نشد لطفا دوباره تلاش نمایید";
                return RedirectToAction("ManageInterviewList");
            }



        }


        //ویرایش تاریخ رزرو مصاحبه
        public ActionResult EditInterView(int id)
        {

            var q = db.InterView.Where(a => a.ViewID == id).FirstOrDefault();

            return View(q);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //ویرایش تاریخ رزرو مصاحبه
        public ActionResult EditInterView(InterView interview)
        {
            var qstatus = db.RegisterUser.Where(a => a.UserID == interview.UserID).FirstOrDefault();
            var qdate = (from a in db.InterView
                where a.DateView == interview.DateView && a.ClockView != interview.ClockView
                select a).ToList();
            //تاریخ و ساعت رو میگیره و با تاریخ اول مقایسه میکنه و بعدش 
            //با ساعت های اون تاریخ و فیلد هایی رو برمیگردونه که با این ساعتت یکسان هستن
            var qdateCopmare = (from a in db.InterView
                where a.DateView == interview.DateView && a.ClockView == interview.ClockView
                select a).FirstOrDefault();


            var q = db.InterView.Select(a => a).Where(a => a.ViewID == interview.ViewID).FirstOrDefault();

            if (qdate.Count() >= 5)
            {
                TempData["msg"] = "همه ی ساعات تاریخ" + interview.DateView.ToShortPersianDateString() +
                                  "برای مصاحبه رزرو شده است-";

            }
            else
            {
                //اگه تارخ ورودی برای اولین بار وارد مشود وجود ندارد
                if (!db.InterView.Any(a => a.DateView == interview.DateView))
                {
                    q.UserID = interview.UserID;
                    q.ViewID = interview.ViewID;
                    q.DateView = interview.DateView;
                    q.ClockView = interview.ClockView;


                    db.InterView.Attach(q);
                    db.Entry(q).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["msg"] = "تاریخ مصاحبه برای این کاربر ویرایش شد";

                }
                else
                {
                    if (qdateCopmare == null)
                    {

                        q.UserID = interview.UserID;
                        q.ViewID = interview.ViewID;
                        q.DateView = interview.DateView;
                        q.ClockView = interview.ClockView;
                        //if (qstatus.Status == 4)
                        //{
                        //    qstatus.Status = 1;
                        //    db.RegisterUser.Attach(qstatus);
                        //    db.Entry(qstatus).State = EntityState.Modified;
                        //    db.SaveChanges();
                        //}

                        db.InterView.Attach(q);
                        db.Entry(q).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["msg"] = "تاریخ مصاحبه برای این کاربر ویرایش شد";



                    }

                    else
                    {
                        TempData["msg"] = "در تاریخ مورد نظر شما این ساعت برای مصاحبه رزرو شده است";
                    }

                }
            }



            return View("EditInterView");
        }




        /// <returns></returns>
        //لیست کامل کاربران بر اساس آخرین  به همراه جستجوهای ترکیبی 
        public ActionResult ManageSearchUsers(int? page, string username, string family, string fieldname,
            string orientationname, string meli, string result, string internship, DateTime? dateview, int status = 1,int item = 1)
        {


            if (status == 1 && item == 1)
            {


                var query = db.RegisterUser.Select(a => new ListUsersForSearch
                {
                    Family = a.FullName,
                    Status = a.Status,
                    UserName = a.UserName,
                    Mobile = a.Mobile,
                    RegID = a.RegID,
                    Internship = a.Internship,
                    NationalNumber = a.NationalNumber,
                    Stu_Graduste =
                        (from c in db.EducatInfo
                         where a.UserID == c.UserID
                         orderby c.BeginStudy descending
                         select c.Stu_Graduate).FirstOrDefault(),
                    UserID = a.UserID,
                    ValueItem =
                        db.ResultInterView.Where(e => e.UserID == a.UserID)
                            .Select(m => m.ResultItem.ValueItem)
                            .DefaultIfEmpty("")
                            .FirstOrDefault(),
                    ItemID = db.ResultInterView.Where(e => e.UserID == a.UserID)
                        .Select(m => m.ItemID)
                        .FirstOrDefault(),
                    DateView =
                        db.InterView.Where(e => e.UserID == a.UserID)
                            .Select(m => m.DateView)
                            .FirstOrDefault().Value,
                    Field = (from c in db.EducatInfo
                             where a.UserID == c.UserID
                             orderby c.BeginStudy descending
                             select c.Fields.FieldName).FirstOrDefault(),
                    //db.EducatInfo.Where(e => e.UserID == a.UserID)
                    //    .OrderByDescending(e => e.BeginStudy)
                    //    .Select(e => e.Fields.FieldName)
                    //    .DefaultIfEmpty("")
                    //    .FirstOrDefault()

                    Orientation =

                        (from c in db.EducatInfo
                         where a.UserID == c.UserID
                         orderby c.BeginStudy descending
                         select c.Orientation.OrientationName).FirstOrDefault(),
                    SectionName =

                        (from c in db.EducatInfo
                         where a.UserID == c.UserID
                         orderby c.BeginStudy descending
                         select c.CrossSection.SectionTitle).FirstOrDefault(),
                    UniName =

                        (from c in db.EducatInfo
                         where a.UserID == c.UserID
                         orderby c.BeginStudy descending
                         select c.UniverCity.UniName).FirstOrDefault()
                }).Where(a => (string.IsNullOrEmpty(username) || a.UserName == username) &&
                              (string.IsNullOrEmpty(family) || a.Family == family) &&
                              (string.IsNullOrEmpty(fieldname) || a.Field == fieldname) &&
                              (string.IsNullOrEmpty(orientationname) || a.Orientation == orientationname) &&
                              (string.IsNullOrEmpty(meli) || a.NationalNumber == meli) &&
                              (string.IsNullOrEmpty(result) || a.ValueItem.Contains(result) ||
                               a.ValueItem.EndsWith(result) || a.ValueItem.StartsWith(result)) &&
                              (string.IsNullOrEmpty(internship) || a.Internship == internship) &&
                              (dateview.Value.Equals(null) || a.DateView == dateview)



                    ).OrderBy(a => a.RegID).ToPagedList(page ?? 1, 2);
                ViewBag.Orientation = orientationname;
                ViewBag.Field = fieldname;
                ViewBag.NationalNumber = meli;
                ViewBag.Username = username;
                ViewBag.Family = family;
                ViewBag.Status = status;
                ViewBag.DateView = dateview;
                ViewBag.Internship = internship;
                ViewBag.Item = item;
                ViewBag.Result = result;
                return View(query);
            }
            else
            {

                var query = db.RegisterUser.Select(a => new ListUsersForSearch
                {
                    Family = a.FullName,
                    Status = a.Status,
                    UserName = a.UserName,
                    Mobile = a.Mobile,
                    RegID = a.RegID,
                    Internship = a.Internship,
                    NationalNumber = a.NationalNumber,
                    Stu_Graduste =
                        (from c in db.EducatInfo
                         where a.UserID == c.UserID
                         orderby c.BeginStudy descending
                         select c.Stu_Graduate).FirstOrDefault(),
                    UserID = a.UserID,
                    ValueItem =
                        db.ResultInterView.Where(e => e.UserID == a.UserID)
                            .Select(m => m.ResultItem.ValueItem)
                            .DefaultIfEmpty("")
                            .FirstOrDefault(),
                    ItemID = db.ResultInterView.Where(e => e.UserID == a.UserID)
                        .Select(m => m.ItemID)
                        .FirstOrDefault(),
                    DateView =
                        db.InterView.Where(e => e.UserID == a.UserID)
                            .Select(m => m.DateView)
                            .FirstOrDefault().Value,
                    Field = (from c in db.EducatInfo
                             where a.UserID == c.UserID
                             orderby c.BeginStudy descending
                             select c.Fields.FieldName).FirstOrDefault(),
                    //db.EducatInfo.Where(e => e.UserID == a.UserID)
                    //    .OrderByDescending(e => e.BeginStudy)
                    //    .Select(e => e.Fields.FieldName)
                    //    .DefaultIfEmpty("")
                    //    .FirstOrDefault()

                    Orientation =

                        (from c in db.EducatInfo
                         where a.UserID == c.UserID
                         orderby c.BeginStudy descending
                         select c.Orientation.OrientationName).FirstOrDefault(),
                    SectionName =

                        (from c in db.EducatInfo
                         where a.UserID == c.UserID
                         orderby c.BeginStudy descending
                         select c.CrossSection.SectionTitle).FirstOrDefault(),
                    UniName =

                        (from c in db.EducatInfo
                         where a.UserID == c.UserID
                         orderby c.BeginStudy descending
                         select c.UniverCity.UniName).FirstOrDefault()
                }).Where(a => (string.IsNullOrEmpty(username) || a.UserName == username) &&
                              (string.IsNullOrEmpty(family) || a.Family == family) &&
                              (string.IsNullOrEmpty(fieldname) || a.Field == fieldname) &&
                              (string.IsNullOrEmpty(orientationname) || a.Orientation == orientationname) &&

                              (string.IsNullOrEmpty(result) || a.ValueItem.Contains(result) ||
                               a.ValueItem.EndsWith(result) || a.ValueItem.StartsWith(result)) &&
                              (string.IsNullOrEmpty(internship) || a.Internship == internship) &&
                              (string.IsNullOrEmpty(meli) || a.NationalNumber == meli) && ((a.Status == status) ||
                                                                                           (a.ItemID == item)) &&
                              (dateview.Value.Equals(null) || a.DateView == dateview)

                    ).OrderBy(a => a.RegID).ToPagedList(page ?? 1, 2);
                ViewBag.Orientation = orientationname;
                ViewBag.Field = fieldname;
                ViewBag.NationalNumber = meli;
                ViewBag.Username = username;
                ViewBag.Family = family;
                ViewBag.Status = status;
                ViewBag.DateView = dateview;
                ViewBag.Internship = internship;
                ViewBag.Item = item;
                ViewBag.Result = result;
                return View(query);
            }

        }

        public ActionResult ChangeStatus(int id)
        {
            try
            {
                var q = db.RegisterUser.Where(a => a.RegID == id).FirstOrDefault();


                return View(q);
            }
            catch
            {
                return View("ManageSearchUsers");
            }
        }

        public ActionResult EditStatusUser(RegisterUser t)
        {

            var q = db.RegisterUser.Where(a => a.RegID == t.RegID).SingleOrDefault();
            q.Status = t.Status;

            db.RegisterUser.Attach(q);
            db.Entry(q).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ManageSearchUsers");
        }

        //اجازه دادن به کاربر جهت ویرایش پروفایل کاربری
        public ActionResult AllowEditProf(int id)

        {
            var qedit = db.RegisterUser.Find(id);
            if (qedit != null)
            {
                qedit.EditInfo = true;
                db.Entry(qedit).State = EntityState.Modified;

                db.SaveChanges();

                TempData["msg"] = "اجازه ویرایش پروفایل به کاربر مورد نظر داده شد";
                return RedirectToAction("ManageSearchUsers");
            }
            else
            {
                TempData["msg"] = "خطا";
                return RedirectToAction("ManageSearchUsers");
            }

        }

        // عدم اجازه دادن به کاربر جهت ویرایش پروفایل کاربری
        public ActionResult NotAllowEditProf(int id)
        {
            var qedit = db.RegisterUser.Find(id);
            if (qedit != null)
            {
                qedit.EditInfo = false;
                db.Entry(qedit).State = EntityState.Modified;

                db.SaveChanges();

                TempData["msg"] = " عدم اجازه ویرایش پروفایل به کاربر مورد نظر داده شد ";
                return RedirectToAction("ManageSearchUsers");
            }
            else
            {
                TempData["msg"] = "خطا";
                return RedirectToAction("ManageSearchUsers");
            }

        }

        //آخرین افراد ثبت نام شده در سه روز اخیر
        public ActionResult NewUsers(int? page)
        {
            var newusers = db.RegisterUser.ToList().OrderByDescending(a => a.DateSubmitInfo).ToPagedList(page ?? 1, 2);
            return View(newusers);
        }

        //لیست افراد رجیکت شده توسط مدیر برای عدم مصاحبه
        public ActionResult ListReject(int? page)
        {
            var lstreject = db.RejectInterView.ToList().OrderByDescending(a => a.UserID).ToPagedList(page ?? 1, 1);
            return View(lstreject);
        }

        // فرم رجیکت کاربر توسط مدیر و بیان دلیل ریجکت
        public ActionResult RejectForInterView()
        {

            return View();
        }

        // فرم رجیکت کاربر توسط مدیر و بیان دلیل ریجکت
        [HttpPost]
        public ActionResult RejectForInterView(RejectInterView reject, int id)
        {
            var userid = db.RegisterUser.Find(id).UserID;
            if (ModelState.IsValid)
            {
                if (!db.RejectInterView.Any(a => a.UserID == userid))
                {
                    RejectInterView rejectuser = new RejectInterView()
                    {
                        UserID = userid,
                        CauseReject = reject.CauseReject
                    };
                    db.RejectInterView.Add(rejectuser);
                    if (Convert.ToBoolean(db.SaveChanges()))
                    {
                        TempData["msg"] = "علت رد کاربر برای مصاحبه با مووفقیت ثبت کردید";
                        return View("Index");
                    }
                    else
                    {
                        TempData["msg"] = "خطا در ثبت کاربر بوجود آمده است";
                    }
                }
                else
                {
                    TempData["msg"] = "خطا....این کاربر قبلا ثبت شده است";
                }

            }
            else
            {
                TempData["msg"] = "خطا در ثبت....";
            }
            return View("RejectForInterView");
        }

        //حذف از لیست رجیکتی و دادن اجازه برای مصاحبه و موارد دیگر 
        public ActionResult DeleteRejectInterView(int id)
        {
            var userid = db.RegisterUser.Find(id).UserID;
            var qdelete = db.RejectInterView.Where(a => a.UserID == userid).FirstOrDefault();
            if (qdelete == null)
            {
                TempData["msg"] = "این کاربر هنوز رد مصاحبه نشده است";
                return View("ManageSearchUsers");
            }
            else
            {

                db.RejectInterView.Remove(qdelete);

                db.SaveChanges();

                TempData["msg"] = "اجازه رزرو تاریخ مصاحبه برای این کاربر داده شد";
                return View("AllowForInterView");
            }

        }


        public ActionResult InterViewResultForm(int id)
        {
            RegisterUser registeruser = db.RegisterUser.Find(id);
            ViewBag.ItemID = new SelectList(db.ResultItem, "ItemID", "ValueItem");
            ViewBag.SectionID = new SelectList(db.CrossSection, "SectionID", "SectionTitle");
            ViewBag.OrientationID = new SelectList(db.Orientation, "OrientationID", "OrientationName");
            ViewBag.FieldID = new SelectList(db.Fields, "FieldID", "FieldName");
            ViewBag.UniID = new SelectList(db.UniverCity, "UniID", "UniName");
            ViewBag.Uni = new SelectList(db.UniverCity, "UniID", "UniName");
            ViewBag.CrossSection = new SelectList(db.CrossSection, "SectionID", "SectionTitle");
            ViewBag.Field = db.Fields.ToList();
            UserLogin userlogin = db.UserLogin.Find(registeruser.UserID);
            if (userlogin.Field_Lessons.Any())
            {
                Session["Skill"] = userlogin.Field_Lessons.Select(s => new Skills_VM()
                {
                    LessonID = s.LessonID,
                    LessonName = s.Lessons.LessonName,
                    ProblemSolving = s.ProblemSolving,
                    MakingVideo = s.MakingVideo,
                    RelatedSoftware = s.RelatedSoftware
                }).ToList();

            }

            var userid = db.RegisterUser.Find(id).UserID;
            var user = db.RegisterUser.FirstOrDefault(x => x.UserID == userid);
            var education =
                db.EducatInfo.Where(a => a.UserID == userid).OrderByDescending(a => a.BeginStudy).FirstOrDefault();

            var item = new ResultInterViewModel
            {
                a = user,
                c = education

            };

            return View(item);
        }

        [HttpPost]
        public ActionResult InterViewResultForm(ResultInterViewModel model, int id, DateTime? txtdate, HttpPostedFileBase fileupload,string searchText)
        {
            //RegisterUser registeruser = db.RegisterUser.Find(id);
            //validate 

            ViewBag.SectionID = new SelectList(db.CrossSection, "SectionID", "SectionTitle", model.c.SectionID);
            ViewBag.CrossSection = new SelectList(db.CrossSection, "SectionID", "SectionTitle", model.c.SectionID);
            ViewBag.ItemID = new SelectList(db.ResultItem, "ItemID", "ValueItem", model.b.ItemID);
            ViewBag.OrientationID = new SelectList(db.Orientation, "OrientationID", "OrientationName", model.c.OrientationID);
            ViewBag.FieldID = new SelectList(db.Fields, "FieldID", "FieldName", model.c.FieldID);
            ViewBag.UniID = new SelectList(db.UniverCity, "UniID", "UniName", model.c.UniID);
            ViewBag.Uni = new SelectList(db.UniverCity, "UniID", "UniName");
            ViewBag.Field = db.Fields.ToList();

            var qOrientationID = db.Orientation.Where(a => a.OrientationName == searchText).FirstOrDefault();



            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error");
                return View(model);
            }
            if (!db.ResultInterView.Any(x => x.UserID == model.a.UserID))
            {
                if (fileupload != null && fileupload.ContentLength > 0)
                {
                    if (fileupload.ContentLength / 1024 <= 100000)
                    {
                        if (fileupload.ContentType == "	video/mp4" ||
                            fileupload.ContentType == "application/mp4")
                        {
                            string Filename = Guid.NewGuid().ToString().Replace("-", "") +
                                              System.IO.Path.GetExtension(fileupload.FileName);
                            string path =
                                System.IO.Path.Combine(Server.MapPath("~/VideoUpload/UserVideo/" + Filename));
                            fileupload.SaveAs(path);
                            model.b.VideoName = Filename;
                        }
                        else
                        {
                            TempData["msg"] = "پسوند فایل باید mp4 باشد ";

                            return RedirectToAction("InterViewResultForm");
                        }
                    }
                    else
                    {
                        TempData["msg"] = "حجم فایل شما باید کمتر از 100 مگابایت باشد";
                        return RedirectToAction("InterViewResultForm");
                    }
                }

                if (Session["Special"] != null)
                {
                    List<Special_VM> _special = Session["Special"] as List<Special_VM>;
                    foreach (var item in _special)
                    {
                        db.Special_Fields.Add(new Special_Fields()
                        {

                            UserID = model.a.UserID,
                            SpecialID = item.SpecialID,
                            FieldID = item.FieldID,



                        });

                    }

                    db.SaveChanges();
                }

                Session["Special"] = null;



                db.Field_Lessons.Where(t => t.UserID == model.a.UserID)
                    .ToList()
                    .ForEach(t => db.Field_Lessons.Remove(t));

                if (Session["Skill"] != null)
                {
                    List<Skills_VM> _listSkill = Session["Skill"] as List<Skills_VM>;
                    foreach (var item in _listSkill)
                    {
                        db.Field_Lessons.Add(new Field_Lessons()
                        {
                            LessonID = item.LessonID,
                            UserID = model.a.UserID,
                            ProblemSolving = item.ProblemSolving,
                            MakingVideo = item.MakingVideo,
                            RelatedSoftware = item.RelatedSoftware

                        });
                    }
                }


                model.a.DateSubmitInfo = model.a.DateSubmitInfo;
                model.b.UserID = (int) model.a.UserID;
                if (txtdate != null)
                {
                    model.b.DateForWork = txtdate;
                }
                else
                {
                    model.b.DateForWork = null;
                }

                if (qOrientationID.FieldID == model.c.FieldID)
                {
                    model.c.OrientationID = qOrientationID.OrientationID;
                }
                else
                {
                    TempData["msg"] = "گرایش انتخابی با رشته انتخابی مغایرت دارد";
                    return RedirectToAction("InterViewResultForm");
                }

              
                model.c.UserID = (int) model.a.UserID;
                db.Entry(model.a).State = EntityState.Modified;
                db.Entry(model.c).State = EntityState.Modified;
                db.ResultInterView.Add(model.b);
                try
                {

                    db.SaveChanges();
                    return RedirectToAction("index", model);
                }
                catch (DbEntityValidationException e)
                {

                    throw;
                }

            }
            else
            {
                TempData["msg"] = "نتیجه ی مصاحبه برای این کاربر قبلا ثبت شده است";
                return RedirectToAction("InterViewResultForm");
            }
            

        }


      


        public ActionResult EditInterViewResultform(int id)
        {
            RegisterUser registeruser = db.RegisterUser.Find(id);
            ViewBag.ItemID = new SelectList(db.ResultItem, "ItemID", "ValueItem");
            ViewBag.SectionID = new SelectList(db.CrossSection, "SectionID", "SectionTitle");
            ViewBag.OrientationID = new SelectList(db.Orientation, "OrientationID", "OrientationName");
            ViewBag.FieldID = new SelectList(db.Fields, "FieldID", "FieldName");
            ViewBag.UniID = new SelectList(db.UniverCity, "UniID", "UniName");
            ViewBag.Uni = new SelectList(db.UniverCity, "UniID", "UniName");
            ViewBag.CrossSection = new SelectList(db.CrossSection, "SectionID", "SectionTitle");
            ViewBag.Field = db.Fields.ToList();
            UserLogin userlogin = db.UserLogin.Find(registeruser.UserID);
            var userid = db.RegisterUser.Find(id).UserID;
            var user = db.RegisterUser.FirstOrDefault(x => x.UserID == userid);
            var education = db.EducatInfo.Where(a => a.UserID == userid).OrderByDescending(a => a.BeginStudy).FirstOrDefault();
            var resultinterview = db.ResultInterView.Where(a => a.UserID == userid).FirstOrDefault();
            if (userlogin.Field_Lessons.Any())
            {
                Session["Skill"] = userlogin.Field_Lessons.Select(s => new Skills_VM()
                {
                    LessonID = s.LessonID,
                    LessonName = s.Lessons.LessonName,
                    ProblemSolving = s.ProblemSolving,
                    MakingVideo = s.MakingVideo,
                    RelatedSoftware = s.RelatedSoftware
                }).ToList();

            }
            if (userlogin.Special_Fields.Any())
            {
                Session["Special"] = userlogin.Special_Fields.Select(s => new Special_VM()
                {
                    FieldID = s.FieldID,
                    FieldName = s.Fields.FieldName,
                    SpecialID =s.SpecialID,
                    SpecialName = s.Speciality.SpecialName
                   
                }).ToList();

            }
            var item = new ResultInterViewModel
            {
                a = user,
                c = education,
                b=resultinterview

            };
            return View(item);
        }

        [HttpPost]
        public ActionResult EditInterViewResultform(ResultInterViewModel model, int id,string searchText, HttpPostedFileBase fileupload)
        {
            ViewBag.SectionID = new SelectList(db.CrossSection, "SectionID", "SectionTitle", model.c.SectionID);
            ViewBag.CrossSection = new SelectList(db.CrossSection, "SectionID", "SectionTitle", model.c.SectionID);
            ViewBag.ItemID = new SelectList(db.ResultItem, "ItemID", "ValueItem", model.b.ItemID);
            ViewBag.OrientationID = new SelectList(db.Orientation, "OrientationID", "OrientationName", model.c.OrientationID);
            ViewBag.FieldID = new SelectList(db.Fields, "FieldID", "FieldName", model.c.FieldID);
            ViewBag.UniID = new SelectList(db.UniverCity, "UniID", "UniName", model.c.UniID);
            ViewBag.Uni = new SelectList(db.UniverCity, "UniID", "UniName");
            ViewBag.Field = db.Fields.ToList();

            var qOrientationID = db.Orientation.Where(a => a.OrientationName == searchText).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error");
                return View(model);
            }

         db.Special_Fields.Where(t => t.UserID == model.a.UserID)
         .ToList()
          .ForEach(t => db.Special_Fields.Remove(t));

            if (Session["Special"] != null)
            {
                List<Special_VM> _special = Session["Special"] as List<Special_VM>;
                foreach (var item in _special)
                {
                    db.Special_Fields.Add(new Special_Fields()
                    {
                        UserID = model.a.UserID,
                        SpecialID = item.SpecialID,
                        FieldID = item.FieldID,
                    });

                }

                db.SaveChanges();
            }

            Session["Special"] = null;



            db.Field_Lessons.Where(t => t.UserID == model.a.UserID)
                .ToList()
                .ForEach(t => db.Field_Lessons.Remove(t));

            if (Session["Skill"] != null)
            {
                List<Skills_VM> _listSkill = Session["Skill"] as List<Skills_VM>;
                foreach (var item in _listSkill)
                {
                    db.Field_Lessons.Add(new Field_Lessons()
                    {
                        LessonID = item.LessonID,
                        UserID = model.a.UserID,
                        ProblemSolving = item.ProblemSolving,
                        MakingVideo = item.MakingVideo,
                        RelatedSoftware = item.RelatedSoftware

                    });
                }
            }


            model.a.DateSubmitInfo = model.a.DateSubmitInfo;
            model.b.UserID = (int)model.a.UserID;
           
            if (qOrientationID.FieldID == model.c.FieldID)
            {
                model.c.OrientationID = qOrientationID.OrientationID;
            }
            else
            {
                TempData["msg"] = "خطا در ویرایش";
                return RedirectToAction("EditInterViewResultform");
            }



            if (fileupload == null)
            {
                var qvideo = db.ResultInterView.Where(a => a.UserID == model.a.UserID).Select(x => x.VideoName).FirstOrDefault();

                model.b.VideoName = qvideo;
                db.ResultInterView.Attach(model.b);
                db.Entry(model.b).State = EntityState.Modified;
            }
            else
            {



                if (fileupload != null && fileupload.ContentLength > 0)
                {
                    if (fileupload.ContentLength / 1024 <= 100000)
                    {
                        if (fileupload.ContentType == "video/mp4" ||
                            fileupload.ContentType == "application/mp4" ||
                            fileupload.ContentType == "Audio/Video/mp4")
                        {
                            model.b.VideoName = Guid.NewGuid().ToString().Replace("-", "") +
                                                        System.IO.Path.GetExtension(fileupload.FileName);
                            string path =
                                System.IO.Path.Combine(
                                    Server.MapPath("~/VideoUpload/UserVideo/" + model.b.VideoName));
                            fileupload.SaveAs(path);
                            model.b.VideoName = model.b.VideoName;

                        }
                        else
                        {
                            TempData["msg"] = "پسوند فایل باید mp4 باشد ";

                            return RedirectToAction("EditInterViewResultform");
                        }
                    }
                    else
                    {
                        TempData["msg"] = "حجم فایل شما باید کمتر از 100 مگابایت باشد";
                        return RedirectToAction("EditInterViewResultform");
                    }
                }
             
            }



            model.c.UserID = (int)model.a.UserID;
            db.Entry(model.a).State = EntityState.Modified;
            db.Entry(model.c).State = EntityState.Modified;
            db.Entry(model.b).State = EntityState.Modified;
            try
            {

                db.SaveChanges();
                return RedirectToAction("index", model);
            }
            catch (DbEntityValidationException e)
            {

                throw;
            }


            TempData["msg"] = "نتیجه ی مصاحبه برای این کاربر قبلا ثبت شده است";
            return RedirectToAction("EditInterViewResultform");
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
        public ActionResult EndResultInterView(int id)
        {
            int userid = db.RegisterUser.Find(id).UserID;

            if (db.ResultInterView.Any(a => a.UserID == userid))
            {
                var qresult = db.ResultInterView.Where(a => a.UserID == userid).FirstOrDefault();
                return View(qresult);

            }
            else
            {
                TempData["msg"] = "نتیجه ای برای کاربر ثبت نشده است";
                return View();
            }

        }

        
        //جزییات کاربر و فقط دیدین رزومه
        public ActionResult ShowRezomeh(int id)
        {
            return View(db.RegisterUser.Find(id));
        }
    }


        }

    


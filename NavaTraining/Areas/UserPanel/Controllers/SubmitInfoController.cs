using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NavaTraining.Models;
using NavaTraining.Classes;
using NavaTraining.Models.ViewModel;
using System.Globalization;
using System.Data.Entity;

namespace NavaTraining.Areas.UserPanel.Controllers
{
    public class SubmitInfoController : Controller
    {
        Nava_DBEntities db = new Nava_DBEntities();
        // GET: UserPanel/SubmitInfo
       
        public string ShamsiDate()
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime dt = DateTime.Now;
            string Today = pc.GetYear(dt).ToString("0000/") +
                           pc.GetMonth(dt).ToString("00/") +
                           pc.GetDayOfMonth(dt).ToString("00");
            return Today;

        }
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

        public ActionResult RegisterInfo()
        {
            ViewBag.CrossSection = new SelectList(db.CrossSection, "SectionID", "SectionTitle");
            ViewBag.Uni = new SelectList(db.UniverCity, "UniID", "UniName");
            ViewBag.Language = new SelectList(db.Language, "LangID", "LangName");

            //ViewBag.Field = new SelectList(db.Fields, "FieldID", "FieldName");
            ViewBag.Field = db.Fields.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult RegisterInfo(RegisterUserInformation register, HttpPostedFileBase fileupload)
        {

            int Userid = CheckUser.GetUserID();
          
            if (ModelState.IsValid)
            {
                if (!db.RegisterUser.Any(u => u.UserID == Userid))
                {


                    if (fileupload != null && fileupload.ContentLength > 0)
                    {
                        if (fileupload.ContentLength/1024 <= 30720)
                        {
                            if (fileupload.ContentType == "application/pdf" ||
                                fileupload.ContentType == "image/jpeg" ||
                                fileupload.ContentType == "image/pjpeg")
                            {
                                string Filename = Guid.NewGuid().ToString().Replace("-", "") +
                                                  System.IO.Path.GetExtension(fileupload.FileName);
                                string path =
                                    System.IO.Path.Combine(Server.MapPath("~/FileUpload/UserFiles/" + Filename));
                                fileupload.SaveAs(path);
                                register.Rezome = Filename;
                            }
                            else
                            {
                                TempData["msg"] = "پسوند فایل یکی از موارد ذیل باشد.1-zip,2-pdf,3-png,4-jpg";

                                return RedirectToAction("RegisterInfo");
                            }
                        }
                        else
                        {
                            TempData["msg"] = "حجم فایل شما باید کمتر از 30 مگابایت باشد";
                            return RedirectToAction("RegisterInfo");
                        }
                    }



                    RegisterUser user = new RegisterUser()
                    {
                        UserID = Userid,
                        UserName = register.UserName,
                        FullName = register.FullName,
                        Email = register.Email,
                        NationalNumber = register.NationalNumber,
                        Province = register.Province,
                        City = register.City,
                        BirthDay = register.BirthDay,
                        MaritalStatus = register.MaritalStatus,
                        MilitaryService = register.MilitaryService,
                        DescriptionArticles = register.DescriptionArticles,
                        TypeOfEmployment = register.TypeOfEmployment,
                        TimeInWeek = register.TimeInWeek,
                        WageFullTime = register.WageFullTime,
                        WagePartTime = register.WagePartTime,
                        Telework = register.Telework,
                        Internship = register.Internship,
                        Mobile = register.Mobile,
                        Phone = register.Phone,
                        Address = register.Address,
                        Workinprogress = register.Workinprogress,
                        DescriptionFull = register.DescriptionFull,
                        Rezome = register.Rezome,
                        DateSubmitInfo = DateTime.Now,
                        Status=2,
                        EditInfo = false



                    };

                    if (Session["Educat"] != null)
                    {
                        List<Education_VM> _education = Session["Educat"] as List<Education_VM>;
                        foreach (var item in _education)
                        {
                            db.EducatInfo.Add(new EducatInfo()
                            {
                                SectionID = item.SectionID,
                                UserID = Userid,
                                UniID = item.UniID,
                                FieldID = item.FieldID,
                                OrientationID = item.OrientationID,
                                YearGrad = item.YearGrad,
                                ThesisDiss = item.ThesisDiss,

                                Avrage = item.Avrage,
                                BeginStudy = item.BeginStudy,
                                Description = item.Description,
                                Stu_Graduate = item.Stu_Graduate

                            });

                        }

                        db.SaveChanges();
                    }

                    Session["Educat"] = null;



                    if (Session["Skill"] != null)
                    {
                        List<Skills_VM> _skill = Session["Skill"] as List<Skills_VM>;
                        foreach (var item in _skill)
                        {
                            db.Field_Lessons.Add(new Field_Lessons()
                            {
                                LessonID = item.LessonID,
                                UserID = Userid,
                                ProblemSolving = item.ProblemSolving,
                                RelatedSoftware = item.RelatedSoftware,
                                MakingVideo = item.MakingVideo


                            });

                        }

                        db.SaveChanges();
                    }

                    Session["Skill"] = null;




                    if (Session["Comp"] != null)
                    {
                        List<Company_VM> _comp = Session["Comp"] as List<Company_VM>;
                        foreach (var item in _comp)
                        {
                            db.Company.Add(new Company()
                            {
                                CompanyName = item.CompanyName,
                                Position = item.Position,
                                DurationWork = item.DurationWork,
                                DescPosition = item.DescPosition,
                                UserID = Userid
                            });

                        }

                        db.SaveChanges();
                    }

                    Session["Comp"] = null;





                    if (Session["Lang"] != null)
                    {
                        List<Lang_VM> _lang = Session["Lang"] as List<Lang_VM>;
                        foreach (var item in _lang)
                        {
                            db.User_Lang.Add(new User_Lang()
                            {
                                LangID = item.LangID,
                                UserID = Userid,
                                ScoreReading = item.ScoreReading,
                                ScoreWrite = item.ScoreWrite,
                                ScoreListening = item.ScoreListening,
                                ScoreSpeking = item.ScoreSpeking,
                                DescriptionLang = item.DescriptionLang


                            });

                        }

                        db.SaveChanges();
                    }

                    Session["Lang"] = null;

                    if (user != null)
                    {
                        db.RegisterUser.Add(user);

                        db.SaveChanges();
                        return View("SuccessRegister", user);
                    }

                }
                else
                {


                    TempData["msg"] = "شما اجازه ثبت نام مجدد را ندارید";
                }
            }


            return RedirectToAction("RegisterInfo");
        }


        public ActionResult AddListEducation(int SectionID, string SectionName, int UniID, string UniName, int FieldID,
            string FieldName, int OrientationID,
            string OrientationName, DateTime? BeginStudy, DateTime? YearGrad, string ThesisDiss, string Avrage,
            string Stu_Graduate, string Description)
        {

            List<Education_VM> _education = new List<Education_VM>();
            if (Session["Educat"] != null)
            {
                _education = Session["Educat"] as List<Education_VM>;
            }

            if (Stu_Graduate == "دانشجو" && Description != "")
            {

                ThesisDiss = "-";

                _education.Add(new Education_VM()
                {
                    SectionID = SectionID,
                    SectionName = SectionName,
                    UniID = UniID,
                    UniName = UniName,
                    FieldID = FieldID,
                    FieldName = FieldName,
                    OrientationID = OrientationID,
                    OrientationName = OrientationName,
                    BeginStudy = BeginStudy,
                    YearGrad = null,
                    ThesisDiss = ThesisDiss,
                    Avrage = Avrage,
                    Stu_Graduate = Stu_Graduate,
                    Description = Description


                });
            }
            else if (Stu_Graduate == "دانشجو" && Description == "")
            {

                ThesisDiss = "-";

                _education.Add(new Education_VM()
                {
                    SectionID = SectionID,
                    SectionName = SectionName,
                    UniID = UniID,
                    UniName = UniName,
                    FieldID = FieldID,
                    FieldName = FieldName,
                    OrientationID = OrientationID,
                    OrientationName = OrientationName,
                    BeginStudy = BeginStudy,
                    YearGrad = null,
                    ThesisDiss = ThesisDiss,
                    Avrage = Avrage,
                    Stu_Graduate = Stu_Graduate,
                    Description = "-"
                });
            }

            else if (Stu_Graduate == "فارغ التحصیل" && Description == "")
            {
                _education.Add(new Education_VM()
                {
                    SectionID = SectionID,
                    SectionName = SectionName,
                    UniID = UniID,
                    UniName = UniName,
                    FieldID = FieldID,
                    FieldName = FieldName,
                    OrientationID = OrientationID,
                    OrientationName = OrientationName,
                    BeginStudy = BeginStudy,
                    YearGrad = YearGrad,
                    ThesisDiss = ThesisDiss,
                    Avrage = Avrage,
                    Stu_Graduate = Stu_Graduate,
                    Description = "-"


                });
            }
            else if (Stu_Graduate == "فارغ التحصیل" && Description != "")
            {

                _education.Add(new Education_VM()
                {
                    SectionID = SectionID,
                    SectionName = SectionName,
                    UniID = UniID,
                    UniName = UniName,
                    FieldID = FieldID,
                    FieldName = FieldName,
                    OrientationID = OrientationID,
                    OrientationName = OrientationName,
                    BeginStudy = BeginStudy,
                    YearGrad = YearGrad,
                    ThesisDiss = ThesisDiss,
                    Avrage = Avrage,
                    Stu_Graduate = Stu_Graduate,
                    Description = Description


                });
            }
            Session["Educat"] = _education;

            return PartialView("ListEducation", _education);
        }

        public ActionResult ListEducation()
        {
            List<Education_VM> _education = new List<Education_VM>();
            if (Session["Educat"] != null)
            {
                _education = Session["Educat"] as List<Education_VM>;

            }
            return PartialView(_education);
        }


        public ActionResult DeleteEducation(int SectionID, int UniID, int FieldID,
            int OrientationID, DateTime? BeginStudy, DateTime? YearGrad, string ThesisDiss, string Avrage,
            string Stu_Graduate, string Description)
        {
            List<Education_VM> _education = new List<Education_VM>();
            if (Session["Educat"] != null)
            {
                _education = Session["Educat"] as List<Education_VM>;

                _education.Remove(_education.First(i => i.SectionID == SectionID
                                                        && i.UniID == UniID && i.FieldID == FieldID &&
                                                        i.OrientationID == OrientationID && i.BeginStudy == BeginStudy
                                                        && i.YearGrad == YearGrad && i.ThesisDiss == ThesisDiss &&
                                                        i.Avrage == Avrage));
                Session["Educat"] = _education;

            }
            return PartialView("ListEducation", _education);
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


        public ActionResult AddListCompanyes(string CompanyName, string Position, int DurationWork, string DescPosition)
        {

            List<Company_VM> _comp = new List<Company_VM>();
            if (Session["Comp"] != null)
            {
                _comp = Session["Comp"] as List<Company_VM>;
            }

            if (DescPosition == "")
            {
                _comp.Add(new Company_VM()
                {
                    CompanyName = CompanyName,
                    Position = Position,
                    DurationWork = DurationWork,
                    DescPosition = "-"

                });

            }
            else
            {
                _comp.Add(new Company_VM()
                {
                    CompanyName = CompanyName,
                    Position = Position,
                    DurationWork = DurationWork,
                    DescPosition = DescPosition

                });

            }

            Session["Comp"] = _comp;

            return PartialView("ListComp", _comp);
        }

        public ActionResult ListComp()
        {
            List<Company_VM> _comp = new List<Company_VM>();
            if (Session["Comp"] != null)
            {
                _comp = Session["Comp"] as List<Company_VM>;

            }
            return PartialView(_comp);
        }


        public ActionResult DeleteCompes(string CompanyName, string Position, int DurationWork, string DescPosition)
        {
            List<Company_VM> _comp = new List<Company_VM>();
            if (Session["Comp"] != null)
            {
                _comp = Session["Comp"] as List<Company_VM>;

                _comp.Remove(_comp.First(i => i.CompanyName == CompanyName && i.Position == Position
                                              && i.DurationWork == DurationWork && i.DescPosition == DescPosition));
                Session["Comp"] = _comp;

            }
            return PartialView("ListComp", _comp);
        }



        public ActionResult AddListLanguages(int LangID, string LangName, string ScoreSpeking, string ScoreListening,
            string ScoreWrite, string ScoreReading, string DescriptionLang)
        {
            List<Lang_VM> _lang = new List<Lang_VM>();
            if (Session["Lang"] != null)
            {
                _lang = Session["Lang"] as List<Lang_VM>;
            }

            if (DescriptionLang == "")
            {
                _lang.Add(new Lang_VM()
                {
                    LangID = LangID,
                    LangName = LangName,
                    ScoreListening = ScoreListening,
                    ScoreReading = ScoreReading,
                    ScoreSpeking = ScoreSpeking,
                    ScoreWrite = ScoreWrite,
                    DescriptionLang = "-"
                });

            }
            else
            {
                _lang.Add(new Lang_VM()
                {
                    LangID = LangID,
                    LangName = LangName,
                    ScoreListening = ScoreListening,
                    ScoreReading = ScoreReading,
                    ScoreSpeking = ScoreSpeking,
                    ScoreWrite = ScoreWrite,
                    DescriptionLang = DescriptionLang

                });

            }

            Session["Lang"] = _lang;

            return PartialView("ListLang", _lang);
        }


        public ActionResult ListLang()
        {
            List<Lang_VM> _lang = new List<Lang_VM>();
            if (Session["Lang"] != null)
            {
                _lang = Session["Lang"] as List<Lang_VM>;

            }
            return PartialView(_lang);
        }

        public ActionResult DeleteLagns(int LangID, string ScoreSpeking, string ScoreListening, string ScoreWrite,
            string ScoreReading, string DescriptionLang)
        {
            List<Lang_VM> _lang = new List<Lang_VM>();
            if (Session["Lang"] != null)
            {
                _lang = Session["Lang"] as List<Lang_VM>;

                _lang.Remove(_lang.First(i => i.LangID == LangID && i.ScoreListening == ScoreListening
                                              && i.ScoreSpeking == ScoreSpeking && i.ScoreReading == ScoreReading &&
                                              i.ScoreWrite == ScoreWrite &&
                                              i.DescriptionLang == DescriptionLang));
                Session["Lang"] = _lang;

            }
            return PartialView("ListLang", _lang);
        }

        public ActionResult EditProfile(int id = 0)
        {
            RegisterUser registeruser = db.RegisterUser.Find(id);
            if (registeruser.EditInfo == false)
            {
                return View("ErrorEdit");
            }

            else
            {

                ViewBag.CrossSection = new SelectList(db.CrossSection, "SectionID", "SectionTitle");
            ViewBag.Uni = new SelectList(db.UniverCity, "UniID", "UniName");
            ViewBag.Language = new SelectList(db.Language, "LangID", "LangName");

            //ViewBag.Field = new SelectList(db.Fields, "FieldID", "FieldName");
            ViewBag.Field = db.Fields.ToList();
           
            UserLogin userlogin = db.UserLogin.Find(registeruser.UserID);

            if (registeruser == null)
            {
                return HttpNotFound();
            }
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
            if (userlogin.User_Lang.Any())
            {
                Session["Lang"] = userlogin.User_Lang.Select(s => new Lang_VM()
                {
                    LangID = s.LangID,
                    LangName = s.Language.LangName,
                    DescriptionLang = s.DescriptionLang,
                    ScoreListening = s.ScoreListening,
                    ScoreReading = s.ScoreReading,
                    ScoreSpeking = s.ScoreSpeking,
                    ScoreWrite = s.ScoreWrite
                }).ToList();

            }
            if (userlogin.EducatInfo.Any())
            {
                Session["Educat"] = userlogin.EducatInfo.Select(s => new Education_VM()
                {
                    SectionID = s.SectionID,
                    SectionName = s.CrossSection.SectionTitle,
                    UniID = s.UniID,
                    UniName = s.UniverCity.UniName,
                    FieldID = s.FieldID,
                    FieldName = s.Fields.FieldName,
                    OrientationID = s.OrientationID,
                    OrientationName = s.Orientation.OrientationName,
                    YearGrad = s.YearGrad,
                    ThesisDiss = s.ThesisDiss,
                    BeginStudy = s.BeginStudy,
                    Avrage = s.Avrage,
                    Stu_Graduate = s.Stu_Graduate,
                    Description = s.Description
                }).ToList();

            }

            if (userlogin.Company.Any())
            {
                Session["Comp"] = userlogin.Company.Select(s => new Company_VM()
                {

                    CompanyName = s.CompanyName,
                    Position = s.Position,
                    DescPosition = s.DescPosition,
                    DurationWork = s.DurationWork

                }).ToList();

            }
                
            return View(registeruser);
        }
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(RegisterUser registeruser)
        {
           

                if (ModelState.IsValid)
                {
                    db.Field_Lessons.Where(t => t.UserID == registeruser.UserID)
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
                                UserID = registeruser.UserID,
                                ProblemSolving = item.ProblemSolving,
                                MakingVideo = item.MakingVideo,
                                RelatedSoftware = item.RelatedSoftware

                            });
                        }
                    }
                    db.EducatInfo.Where(t => t.UserID == registeruser.UserID)
                        .ToList()
                        .ForEach(t => db.EducatInfo.Remove(t));
                    if (Session["Educat"] != null)
                    {
                        List<Education_VM> _listEducat = Session["Educat"] as List<Education_VM>;
                        foreach (var item in _listEducat)
                        {
                            db.EducatInfo.Add(new EducatInfo()
                            {
                                UserID = registeruser.UserID,
                                SectionID = item.SectionID,
                                UniID = item.UniID,
                                OrientationID = item.OrientationID,
                                FieldID = item.FieldID,
                                YearGrad = item.YearGrad,
                                BeginStudy = item.BeginStudy,
                                ThesisDiss = item.ThesisDiss,
                                Avrage = item.Avrage,
                                Stu_Graduate = item.Stu_Graduate,
                                Description = item.Description

                            });
                        }
                    }
                    db.Company.Where(t => t.UserID == registeruser.UserID).ToList().ForEach(t => db.Company.Remove(t));

                    if (Session["Comp"] != null)
                    {
                        List<Company_VM> _listComp = Session["Comp"] as List<Company_VM>;
                        foreach (var item in _listComp)
                        {
                            db.Company.Add(new Company()
                            {

                                UserID = registeruser.UserID,
                                CompanyName = item.CompanyName,
                                Position = item.Position,
                                DurationWork = item.DurationWork,
                                DescPosition = item.DescPosition

                            });
                        }
                    }
                    db.User_Lang.Where(t => t.UserID == registeruser.UserID)
                        .ToList()
                        .ForEach(t => db.User_Lang.Remove(t));
                    if (Session["Lang"] != null)
                    {
                        List<Lang_VM> _listLang = Session["Lang"] as List<Lang_VM>;
                        foreach (var item in _listLang)
                        {
                            db.User_Lang.Add(new User_Lang()
                            {
                                LangID = item.LangID,
                                UserID = registeruser.UserID,
                                ScoreListening = item.ScoreListening,
                                ScoreReading = item.ScoreReading,
                                ScoreSpeking = item.ScoreSpeking,
                                ScoreWrite = item.ScoreWrite,
                                DescriptionLang = item.DescriptionLang

                            });
                        }
                    }
                    db.Entry(registeruser).State = EntityState.Modified;

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            
            ViewBag.CrossSection = new SelectList(db.CrossSection, "SectionID", "SectionTitle");
                ViewBag.Uni = new SelectList(db.UniverCity, "UniID", "UniName");
                ViewBag.Language = new SelectList(db.Language, "LangID", "LangName");

                //ViewBag.Field = new SelectList(db.Fields, "FieldID", "FieldName");
                ViewBag.Field = db.Fields.ToList();
                return View(registeruser);

            }
        }
    
}


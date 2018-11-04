using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NavaTraining.Models;
using System.Web.Security;
using SendMail;
using NavaTraining.Classes;
using System.Globalization;

namespace NavaTraining.Controllers
{
    public class AccountController : Controller
    {
        Nava_DBEntities db = new Nava_DBEntities();

        public string ShamsiDate()
        {
            PersianCalendar pc = new PersianCalendar();
            DateTime dt = DateTime.Now;
            string Today = pc.GetYear(dt).ToString("0000/") +
                pc.GetMonth(dt).ToString("00/") +
                pc.GetDayOfMonth(dt).ToString("00");

            return Today;
            
        }
        public ActionResult Register()
        {
            return View();
           
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel register,HttpPostedFileBase UserPic)
        {
            
            if(ModelState.IsValid)
            {
                if (!db.UserLogin.Any(u => u.UserName == register.UserName))
                {
                    if (UserPic != null && UserPic.ContentLength > 0)
                    {
                        if (UserPic.ContentLength / 1024 <= 3072)
                        {
                            if (UserPic.ContentType == "image/jpeg" || UserPic.ContentType == "image/png")
                            {
                                string Picname = Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(UserPic.FileName);
                                string path = System.IO.Path.Combine(Server.MapPath("~/images/Userimages/" + Picname));
                                UserPic.SaveAs(path);
                                register.ImageUser = Picname;
                            }
                            else
                            {
                                TempData["msg"] = "پسوند فایل یکی از موارد ذیل باشد.1-png,2-jpg";
                                return RedirectToAction("register");
                            }
                        }
                        else
                        {
                            TempData["msg"] = "حجم فایل شما باید کمتر از 3 مگابایت باشد";
                            return RedirectToAction("register");
                        }
                    }
                    else if (UserPic == null)
                    {

                        register.ImageUser = "0081.png";

                    }

                    UserLogin users = new UserLogin()
                    {
                       
                        UserName = register.UserName,
                        Email = register.Email.ToLower(),
                        IsActive = false,
                        ActiveCode = Guid.NewGuid().ToString().Replace("-", ""),
                        Password = FormsAuthentication.HashPasswordForStoringInConfigFile(register.Password, "MD5"),
                        RegisterDate=ShamsiDate(),
                        RoleID = 2,
                       ImageUser=register.ImageUser

                    };
                    db.UserLogin.Add(users);
                    db.SaveChanges();
                    string body = PartialToStringClass.RenderPartialView("ManageEmail", "ActiveAccount", users);
                    SendEmailGmail.Send(users.Email, "Active Email", body);
                    //کد زیر باعث می شود که وقتی ثبت نام با موفقیت انجام شد به صفحه زیر برود و حالت یک پیغام دارد
                    return View("SuccessRegister", users);
                }
                //از تمپ دیتا زمانی تستفاده می کنیم که بخواهیم پیغام موفقیت آمیز را در همان صفحه نمایش دهیم
                //TempData["msg"] = "ثبت نام شما با موفقیت انجام شد"+@register.UserName +"کاربر گرامی";
                //   return RedirectToAction("register");
                else
                {

                    TempData["msg"] = "کاریری با این نام موجود است";
                    return RedirectToAction("register");
                }

            }
            
            return View();
        }

        public ActionResult ActiveUser(string id)
        {
            var user = db.UserLogin.FirstOrDefault(u => u.ActiveCode == id);
            //وقتی کاربر لینک را کلیک کند به صفحه جدید می رود و موارد زیر فعال می شود.
            if (user != null)
            {
                user.IsActive = true;
                user.ActiveCode = Guid.NewGuid().ToString().Replace("-", "");
                db.SaveChanges();
                ViewBag.finaly = true;
            }
            else
            {

                ViewBag.finaly = false;
            }

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel login, string ReturnUrl = "/")
        {
            string pass = FormsAuthentication.HashPasswordForStoringInConfigFile(login.Password, "MD5");

            var user = db.UserLogin.FirstOrDefault(u => u.Email == login.Email && u.Password == pass);
           

                if (user != null)
                {

                    if (user.IsActive)
                    {
                        FormsAuthentication.SetAuthCookie(user.UserName, login.RememberMe);
                        return Redirect(ReturnUrl);
                        Session["UserID"] = user.UserID;
                    }
                    else
                    {
                        ModelState.AddModelError("Email", "حساب کاربری فوق فعال نشده است");
                    }
                }
                else
                {
                    ModelState.AddModelError("Email", "کاربر یافت نشد");
                }
           
            
            return View(login);
        }

        public ActionResult LoginView()
        {
            return PartialView();
        }

        public ActionResult LogOff()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return Redirect("/");
        }
        public ActionResult RecoveryPass()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RecoveryPass(RecoveryPasswordViewModel recovery)
        {
            //اول بررسی می کند کاربری با این ایمیل وجود دارد یا خیر
            var user = db.UserLogin.FirstOrDefault(u => u.Email == recovery.Email);
            if (user != null)
            {
                string body = PartialToStringClass.RenderPartialView("ManageEmail", "ResetPass", user);
                SendEmailGmail.Send(user.Email, "بازیابی کلمه عبور", body);
                ViewBag.SendEmail = true;
            }
            //اگر کاربری وجود نداشت
            else
            {
                ModelState.AddModelError("Email", "کاربری با ایمیل وارد شده وجود ندارد");


            }
            return View();
        }


        public ActionResult Resetpass(string ActiveCode)
        {
            return View(new ResetPassViewModel()
            {
                ActiveCode = ActiveCode

            });
        }

        [HttpPost]

        public ActionResult Resetpass(ResetPassViewModel reset)
        {
            // اول چک می کند آیا کاربری فعال شده که بخواهد پسوردش را تغییر دهد
            var user = db.UserLogin.FirstOrDefault(u => u.ActiveCode == reset.ActiveCode);
            if (user != null)
            {
                //پسورد جدید راجایگزین پسورد قدیم میکند 
                user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(reset.Password, "MD5");
                user.ActiveCode = Guid.NewGuid().ToString().Replace("-", "");
                db.SaveChanges();

                return Redirect("/Account/Login?ResetPass=true");
            }
            else
            {
                ModelState.AddModelError("Password", "اطلاعات ورودی صحیح نمی باشد");
            }
            return View(reset);
        }

    }
}
using NavaTraining.Classes;
using NavaTraining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace NavaTraining.Areas.UserPanel.Controllers
{
    public class DefaultController : Controller
    {
        Nava_DBEntities db = new Nava_DBEntities();
        // GET: UserPanel/Default

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {

            return View();
        }


        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel changePassword)
        {

            if (ModelState.IsValid)
            {
                //برای استفاده از یوزر کاربر رد بازیابی اطلاعات آن
                int UserID = CheckUser.GetUserID();
                //int Email = CheckUser.GetUserEmail();
                var user = db.UserLogin.Find(UserID);

                string OldPass = FormsAuthentication.HashPasswordForStoringInConfigFile(changePassword.OldPassword,
                    "MD5");
                if (user.Password == OldPass)
                {
                    user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(changePassword.Password,
                        "MD5");
                    db.SaveChanges();
                    ViewBag.IsOk = true;
                }
                else
                {
                    ModelState.AddModelError("OldPassword", "کلمه عبور فعلی صحیح نمی باشد");
                }
            }
            return View(changePassword);
        }


    }
}
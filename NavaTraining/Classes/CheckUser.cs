using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using NavaTraining.Models;

namespace NavaTraining.Classes
{
    public class CheckUser
    {

        public static int GetUserID()
        {
            using (Nava_DBEntities db = new Nava_DBEntities())
            {
                string username = HttpContext.Current.User.Identity.Name;
                return db.UserLogin.First(u => u.UserName == username).UserID;
                

            }

           
        }

        public static string GetUserEmail()
        {
            using (Nava_DBEntities db = new Nava_DBEntities())
            {
                string Email = HttpContext.Current.User.Identity.Name;
                return db.UserLogin.First(u => u.Email == Email).Email;

            }


        }

       
     
    }
}

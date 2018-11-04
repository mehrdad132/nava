using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NavaTraining.Controllers
{
    public class ManageEmailController : Controller
    {
        // GET: ManageEmail
        public ActionResult ActiveAccount()
        {
            return PartialView();
        }

        public ActionResult ResetPass()
        {
            return PartialView();
        }
    }
}
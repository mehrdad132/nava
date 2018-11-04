using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
//کلاس کاستم بیلدر در پوشه کاستم بیلدر 
using NavaTraining.CustomModelBinders;
using System.Threading;

namespace NavaTraining
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //این کد برای تقویم فارسی در قسمت رزرو مصاحبه استفاده می شود
            ModelBinders.Binders.Add(typeof(DateTime), new PersianDateModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime?), new PersianDateModelBinder());
        }
    }
}

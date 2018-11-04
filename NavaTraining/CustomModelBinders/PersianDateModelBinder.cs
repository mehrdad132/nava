using System;
using System.Globalization;
using System.Web.Mvc;

namespace NavaTraining.CustomModelBinders
{
    /// <summary>
    /// How to register it in the Application_Start method of Global.asax.cs
    /// ModelBinders.Binders.Add(typeof(DateTime), new PersianDateModelBinder());
    /// </summary>
    public class PersianDateModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
          
            var modelState = new ModelState { Value = valueResult };
            object actualValue = new DateTime(1900, 1, 1); //todo: توصيه شده تاريخ تولد خودتان را در اينجا قرار دهيد
            try
            {
                

                if ((valueResult == null || valueResult.AttemptedValue == null) && (bindingContext.Model == null || bindingContext.Model is DateTime?))
                    return null;
                if (valueResult != null && valueResult.AttemptedValue == null)
                    throw new FormatException();
                int h = 0,
                    m = 0,
                    s = 0;
                var resPartsWithSecounds = valueResult.AttemptedValue.Split(' ');
                  if(resPartsWithSecounds.Length==1)
                {
                    var parts = valueResult.AttemptedValue.Split('/'); //ex. 1391/1/19
                    if (parts.Length != 3)
                        return null;
                    var year = int.Parse(parts[0]);
                    var month = int.Parse(parts[1]);
                    var day = int.Parse(parts[2]);
                    actualValue = new DateTime(year, month, day, new PersianCalendar());
                }
                  else if (resPartsWithSecounds.Length == 2)
                {
              


                    if (resPartsWithSecounds[1] != null)
                {
                    var time = resPartsWithSecounds[1].Split(':');
                    h = time.Length == 1 ? int.Parse(time[0]) : 0;
                    m = time.Length == 2 ? int.Parse(time[1]) : 0;
                    s = time.Length == 3  ? int.Parse(time[2]) : 0;
                }

                var parts = resPartsWithSecounds[0].Split('/');
                if (parts.Length != 3)
                    return null;
                var year = int.Parse(parts[0]);
                var month = int.Parse(parts[1]);
                var day = int.Parse(parts[2]);
                actualValue = new DateTime(year, month, day, h, m, s, new PersianCalendar());

            }
                  }
            catch (FormatException e)
            {
                modelState.Errors.Add(e);
            }

            bindingContext.ModelState.Add(bindingContext.ModelName, modelState);
            return actualValue;
        }
    }
}
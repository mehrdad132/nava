using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NavaTraining.Models;
namespace NavaTraining.Areas.Admin
{
    public class ResultInterViewModel
    {
        public ResultInterViewModel()
        {
            a = new RegisterUser();
            b = new ResultInterView();
            c = new EducatInfo();
            
        }
        public RegisterUser a { get; set; }
        public ResultInterView b { get; set; }
        public EducatInfo c { get; set; }
    }
}
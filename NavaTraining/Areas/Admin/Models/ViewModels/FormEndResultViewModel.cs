using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NavaTraining.Models;

namespace NavaTraining.Areas.Admin
{
    public class FormEndResultViewModel
    {
        public int UserID { get; set; }
        public int ResultID { get; set; }
        public string Name { get; set; }
        public string ValueItem { get; set; }
        public string Family { get; set; }
        public string Mobile { get; set; }
        public int ItemID { get; set; }
        
        public string FieldName { get; set; }  
        public string OrientationName { get; set; }

    }
}
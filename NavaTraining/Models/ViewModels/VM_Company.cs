using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace NavaTraining.Models
{
    public class VM_Company
    {
     
        [DisplayName("نام شرکت")]
        public string CompanyName { get; set; }
        [DisplayName("سمت در شرکت")]
        public string Position { get; set; }
        [DisplayName("مدت زمان استخدام به ما")]
        public Nullable<int> DurationWork { get; set; }
        [DisplayName("توضیحات")]
        public string DescPosition { get; set; }
    }
}
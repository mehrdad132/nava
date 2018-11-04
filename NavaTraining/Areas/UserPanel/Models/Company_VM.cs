using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NavaTraining.Areas.UserPanel
{
    public class Company_VM
    {
        [Key]
        public int CompanyID { get; set; }
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
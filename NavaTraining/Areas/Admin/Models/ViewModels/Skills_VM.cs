using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace NavaTraining.Areas.Admin
{
    public class Skills_VM
    {

        public int LessonID { get; set; }
        [DisplayName("نام درس")]
        public string LessonName { get; set; }
        [DisplayName("حل مسئله و انجام پروژه")]
        public string ProblemSolving { get; set; }
        [DisplayName("تدریس  و ساخت ویدیو")]
        public string MakingVideo { get; set; }
        [DisplayName("نرم افزارهای تخصصی مربوطه (جهت حل مسائل و شبیه سازی )")]

        public string RelatedSoftware { get; set; }
    }
}
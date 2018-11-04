using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NavaTraining.Models.ViewModel
{
    public class Education_VM
    {
        public int SectionID { get; set; }
        [DisplayName("مقطع")]
        public string SectionName { get; set; }

        public int UniID { get; set; }
        [DisplayName("نام دانشگاه")]
        public string UniName { get; set; }

        public int FieldID { get; set; }
        [DisplayName("نام رشته")]
        public string FieldName { get; set; }

        public int OrientationID { get; set; }
        [DisplayName("گرایش ")]
        public string OrientationName { get; set; }
        [DisplayName("تاریخ شروع به تحصیل")]
        public Nullable<System.DateTime> BeginStudy { get; set; }
        [DisplayName("تاریخ اخذ مدرک")]
        public Nullable<System.DateTime> YearGrad { get; set; }
        [DisplayName("نمره پایان نامه")]
        public string ThesisDiss { get; set; }
        [DisplayName("معدل")]
        public string Avrage { get; set; }
        [DisplayName("توضیحات")]
        public string  Description { get; set; }
        [DisplayName("وضعیت تحصیلی")]
        public string Stu_Graduate { get; set; }
    }
}
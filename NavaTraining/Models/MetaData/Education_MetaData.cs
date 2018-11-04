using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NavaTraining.Models
{
    public class Education_MetaData
    {
        [Key]
        public int Educat_info_ID { get; set; }
        public int UserID { get; set; }
        [Display(Name = "مقطع")]
        
        public int SectionID { get; set; }
        [Display(Name = "نام دانشگاه")]
        
        public int UniID { get; set; }
        [Display(Name = "نام رشته")]
        public int FieldID { get; set; }
        [Display(Name = "گرایش")]
        public int OrientationID { get; set; }
        [Display(Name = "نمره پایان نامه")]
        public string ThesisDiss { get; set; }
        [Display(Name = "معدل")]
        public string Avrage { get; set; }
        [Display(Name = "وضعیت تحصیلی")]
        public string Stu_Graduate { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        [UIHint("PersianDatePicker")]
        [Display(Name = "تاریخ شروع به تحصیل")]
        public Nullable<System.DateTime> BeginStudy { get; set; }
        [Display(Name = "تاریخ فارغ التحصیلی")]
        [UIHint("PersianDatePicker")]
        public Nullable<System.DateTime> YearGrad { get; set; }
    }

    [MetadataType(typeof(Education_MetaData))]
    public partial class EducatInfo
    {

    }
}
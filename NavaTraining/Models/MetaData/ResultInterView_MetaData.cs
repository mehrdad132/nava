using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NavaTraining.Models
{
    internal class ResultInterView_MetaData
    {
        [Key]
        public int ResultID { get; set; }
        [Display(Name = "نتیجه مصاحبه")]

        public int ItemID { get; set; }
        public int UserID { get; set; }
        [Display(Name = "مهارت مکالمه")]

        public string SkillSpeaking { get; set; }
        [Display(Name = "درجه اشتیاق به کار")]
        public string PassionWork { get; set; }
        [Display(Name = "درجه شخصیت و متانت")]
        public string Sobriety { get; set; }
        [Display(Name = "روابط عمومی و فن بیان")]
        public string Rhetorical { get; set; }
        [Display(Name = "تاریخ اعلام آمادگی بکار")]
        [UIHint("PersianDatePicker")]
        public System.DateTime DateForWork { get; set; }
        [Display(Name = "ارسال ویدئو")]
        public string VideoName { get; set; }
    }
    [MetadataType(typeof(ResultInterView_MetaData))]
    public partial class ResultInterView
    {
       
    }
}
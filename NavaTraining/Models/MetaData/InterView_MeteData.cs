using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NavaTraining.Models
{
    internal class InterView_MeteData
    {
        [Key]
        public int ViewID { get; set; }

        public int UserID { get; set; }
        [Display(Name = "تاریخ مصاحبه")]
        [Required(ErrorMessage = "تاریخ مصاحبه رو وارد کنید")]
        // Location: Views\Shared\EditorTemplates\PersianDatePicker.cshtml
        [UIHint("PersianDatePicker")]

        [DisplayFormat(DataFormatString = "{0:dddd,dd MMMM yyyy}")]
        public System.DateTime DateView { get; set; }
        [Display(Name = "ساعت مصاحبه")]
        [Required(ErrorMessage = "ساعت مورد نظر را وارد نمایید")]
        public int ClockView { get; set; }

    }

    [MetadataType(typeof(InterView_MeteData))]
    public partial class InterView
    {
       

    }
}
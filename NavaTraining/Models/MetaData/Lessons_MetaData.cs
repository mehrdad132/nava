using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NavaTraining.Models
{
    internal class Lessons_MetaData
    {
        [Key]
        public int LessonID { get; set; }
        public Nullable<int> LessonCode { get; set; }
        [Display(Name = "نام رشته")]
        [Required(ErrorMessage = "نام رشته را وارد نمایید")]
        public int FieldID { get; set; }
        [Display(Name = "نام درس")]
        [Required(ErrorMessage = "نام درس را وارد نمایید")]
        public string LessonName { get; set; }
    }
    [MetadataType(typeof(Lessons_MetaData))]
    public partial class Lessons
    {

    }
}
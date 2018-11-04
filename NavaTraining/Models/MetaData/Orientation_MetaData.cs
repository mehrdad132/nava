using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NavaTraining.Models
{
    internal class Orientation_MetaData
    {
        [Key]
        public int OrientationID { get; set; }
        [Display(Name = "نام رشته")]
        [Required(ErrorMessage = "نام رشته را وارد نمایید")]
        public int FieldID { get; set; }
        [Display(Name = "نام گرایش")]
        [Required(ErrorMessage = "نام گرایش را وارد نمایید")]
        public string OrientationName { get; set; }
    }
    [MetadataType(typeof(Orientation_MetaData))]
    public partial class Orientation
    {

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NavaTraining.Models
{
    internal class Speciality_MetaData
    {
        [Key]
        public int SpecialID { get; set; }
        [Display(Name = "نام رشته")]
        [Required(ErrorMessage = "نام رشته را وارد نمایید")]
        public int FieldID { get; set; }
        [Display(Name = "تخصص")]
        [Required(ErrorMessage = "تخصص را وارد نمایید")]
        public string SpecialName { get; set; }

    }

    [MetadataType(typeof (Speciality_MetaData))]

    public partial class Speciality
    {
        
    }
}
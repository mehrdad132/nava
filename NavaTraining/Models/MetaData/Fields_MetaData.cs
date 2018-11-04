using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NavaTraining.Models
{
    internal class Fields_MetaData
    {
        [Key]
        public int FieldID { get; set; }
        [Display(Name = "نام رشته")]
        [Required(ErrorMessage = "نام رشته را وارد نمایید")]
        public string FieldName { get; set; }
    }
    [MetadataType(typeof(Fields_MetaData))]
    public partial class Fields
    {

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NavaTraining.Models
{
    internal class Langauge_MetaData
    {
        [Key]
        public int LangID { get; set; }
        [Display(Name = "زبان")]
        [Required(ErrorMessage = "زبان مورد نظر را وارد نمایید")]
        public string LangName { get; set; }

    }

    [MetadataType(typeof(Langauge_MetaData))]
    public partial class Language
    {

    }
}
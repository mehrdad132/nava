using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NavaTraining.Models
{
    internal class UniverCity_MetaData
    {
        [Key]
        public int UniID { get; set; }
        [Display(Name = "نام دانشگاه")]
        [Required(ErrorMessage = "نام دانشگاه را وارد نمایید")]
        public string UniName { get; set; }
    }


    [MetadataType(typeof(UniverCity_MetaData))]
    public partial class UniverCity
    {

    }
}
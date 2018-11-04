using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NavaTraining.Models
{
    internal class ResultItem_MetaData
    {
        [Key]
        public int ItemID { get; set; }
        [Display(Name = "نتیجه مصاحبه")]
        [Required(ErrorMessage = "نام رشته را وارد نمایید")]
        public string ValueItem { get; set; }
    }

    [MetadataType(typeof (ResultItem_MetaData))]

    public partial class ResultItem
    {
       
    }
}
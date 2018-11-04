using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NavaTraining.Models
{
    public class RejectInterView_MetaData
    {
        public int RejectID { get; set; }
        public int UserID { get; set; }
        [Display(Name = "علت رد مصاحبه")]
        [Required(ErrorMessage = "علت رد کاربر برای مصاحبه را وارد نمایید")]
        public string CauseReject { get; set; }
    }
    [MetadataType(typeof(RejectInterView_MetaData))]
    public partial class RejectInterView
    {

    }
}
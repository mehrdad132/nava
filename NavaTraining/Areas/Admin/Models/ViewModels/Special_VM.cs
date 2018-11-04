using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;

namespace NavaTraining.Areas.Admin
{
    public class Special_VM
    {

        public int FieldID { get; set; }
        [DisplayName("نام رشته")]
        public string FieldName { get; set; }
 
        public int SpecialID { get; set; }

        [DisplayName("تخصص")]
        public string SpecialName { get; set; }


    }
}
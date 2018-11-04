using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NavaTraining.Models
{
    public class VM_DetailsUsers
    {
        public string UniName { get; set; }
        public string SectionName { get; set; }
        public string FieldName { get; set; }
        public string OrientationName { get; set; }
        public Nullable<System.DateTime> BeginStudy { get; set; }
        public Nullable<System.DateTime> YearGrad { get; set; }
        public string ThesisDiss { get; set; }
        public string Avrage { get; set; }
        public string Description { get; set; }
        public string Stu_Graduate { get; set; }

    }
}
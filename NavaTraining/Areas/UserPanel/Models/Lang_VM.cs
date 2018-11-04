using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace NavaTraining.Areas.UserPanel
{
    public class Lang_VM
    {
        public int LangID { get; set; }
        [DisplayName("زبان مورد نظر")]
        public string  LangName { get; set; }
        [DisplayName("نمره اسپکینگ")]
        public string ScoreSpeking { get; set; }
        [DisplayName("نمره لسنینگ")]
        public string ScoreListening { get; set; }
        [DisplayName("نمره رایتینگ")]
        public string ScoreWrite { get; set; }
        [DisplayName("نمره ریدینگ")]
        public string ScoreReading { get; set; }
        [DisplayName("توضیحات در مورد سایر زبان ها در صورت داشتن")]
        public string DescriptionLang { get; set; }

    }
}
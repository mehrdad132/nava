//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NavaTraining.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User_Lang
    {
        public int UL_ID { get; set; }
        public int UserID { get; set; }
        public int LangID { get; set; }
        public string ScoreSpeking { get; set; }
        public string ScoreListening { get; set; }
        public string ScoreWrite { get; set; }
        public string ScoreReading { get; set; }
        public string DescriptionLang { get; set; }
    
        public virtual Language Language { get; set; }
        public virtual UserLogin UserLogin { get; set; }
    }
}
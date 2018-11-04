using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Web;

namespace NavaTraining.Areas.Admin
{
    public class ListDate
    {
        public int ViewID { get; set; }
        public int UserID { get; set; }
        public DateTime? Date { get; set; }
        public string Username { get; set; }
        public string  Family { get; set; }
        public string  Mobile { get; set; }
        public int Clockview { get; set; }
        public string  Meli { get; set; }
    }
}
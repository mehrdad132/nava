using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NavaTraining.Areas.Admin
{
    public class ListUsersForSearch
    {
        string _status = "";
        public int RegID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }

        public string NationalNumber { get; set; }
        public string Family { get; set; }
        public string Mobile { get; set; }

        public string SectionName { get; set; }
        public string Field { get; set; }
     
        public string UniName { get; set; }
        public string Stu_Graduste { get; set; }
        public string Orientation { get; set; }
        public string StatusValue
        {
            get
            {
                switch (_status)
                {
                    case "1":
                        {
                            return "";

                        }
                    case "2":
                        {
                            return "بررسی نشده";

                        }
                    case "3":
                        {
                            return "تایید شده جهت مصاحبه";

                        }
                    case "4":
                        {
                            return "بیش از حد انتظار";

                        }
                    case "5":
                        {
                            return "رد شده";

                        }

                    default:
                        return "خطا";
                }
            }
            set
            {
                _status = value;
            }
        }

        public int Status { get; set; }

        public string ValueItem { get; set; }

        public string Internship { get; set; }
        [UIHint("PersianDatePicker")]
        public DateTime DateView { get; set; }

        public int ItemID { get; set; }
    }

  
}
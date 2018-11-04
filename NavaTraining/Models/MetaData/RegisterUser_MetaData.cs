using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace NavaTraining.Models
{
    internal class RegisterUser_MetaData
    {

        [Key]
        public int RegID { get; set; }
        
        public int UserID { get; set; }
        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا  نام را وارد نمایید")]
        public string UserName { get; set; }
        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا  نام خانوادگی را وارد نمایید")]
        public string FullName { get; set; }


        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا  ایمیل را وارد نمایید")]
        [DataType(DataType.EmailAddress, ErrorMessage = "لطفا  ایمیل را  صحیح وارد نمایید")]
        public string Email { get; set; }


        [Display(Name = "شماره ملی")]
        [Required(ErrorMessage = "لطفا  شماره ملی را وارد نمایید")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "لصفا شماره ملی را به صورت عددی ده رقمی وارد نمایید مثال 238888888")]
        [Range(1, 9999999999, ErrorMessage = "شماره ملی عددی می باشد")]
        public string NationalNumber { get; set; }
        [Display(Name = "استان محل سکونت")]
        [Required(ErrorMessage = "لطفا نام استان را انتخاب نمایید")]
        public string Province { get; set; }
        [Display(Name = "شهر")]

        [Required(ErrorMessage = "لطفا  نام شهرستان را انتخاب نمایید")]
        public string City { get; set; }
        [Display(Name = "تاریخ تولد")]
        [Required(ErrorMessage = "لطفا تاریخ تولد را وارد نمایید")]
        [UIHint("PersianDatePicker")]
        public System.DateTime BirthDay { get; set; }
        [Display(Name = "وضعیت تاهل")]
        [Required(ErrorMessage = " لطفا وضعیت تاهل خود را مشخص نمایید")]
        public string MaritalStatus { get; set; }
        [Display(Name = "وضعیت نظام وظیفه")]
        [Required(ErrorMessage = " لطفا وضعیت نظام وظیفه خود را مشخص نمایید")]
        public string MilitaryService { get; set; }
        [Display(Name = "آدرس")]
        [Required(ErrorMessage = "لطفا  آدرس  را وارد نمایید")]
        public string Address { get; set; }
        [Display(Name = "شرحی از سوابق پژوهشی و مقالات ISI ")]

        public string DescriptionArticles { get; set; }
        [Display(Name = "آیا هم اکنون در جایی بصورت تمام وقت و یا پاره وقت مشغول بکار هستید؟")]
        [Required(ErrorMessage = "لطفا  فیلد  را پر کنید")]
        public string Workinprogress { get; set; }
        [Display(Name = "درخواست استخدام بصورت تمام وقت/ پاره وقت؟")]
        [Required(ErrorMessage = "لطفا  فیلد  را پر کنید")]
        public string TypeOfEmployment { get; set; }
        [Display(Name = "درصورت پاره وقت بودن حداقل تعداد ساعات کاری در هفته ؟ ")]
        [Required(ErrorMessage = "لطفا  فیلد  را پر کنید")]
        public Nullable<int> TimeInWeek { get; set; }
        [Display(Name = "میزان دستمزد درخواستی ماهیانه به تومان ( درصورت تمام وقت بودن)؟")]
        [Required(ErrorMessage = "لطفا  فیلد  را پر کنید")]
        public Nullable<int> WageFullTime { get; set; }
        [Display(Name = "میزان دستمزد درخواستی ساعتی به تومان ( درصورت پاره وقت بودن)؟")]
        [Required(ErrorMessage = "لطفا  فیلد  را پر کنید")]
        public Nullable<int> WagePartTime { get; set; }
        [Display(Name = "در صورت نیاز شرکت ، مایل به انجام کارهای محوله بصورت دورکاری  بعد از ساعات کاری روزانه و یا  پنجشنبه  و ایام تعطیلات رسمی هستید؟")]
        [Required(ErrorMessage = "لطفا  فیلد  را پر کنید")]
        public string Telework { get; set; }
        [Display(Name = "آیا درصورت پذیرفته نشدن  شما بعلت مهارت پایین  ، مایل به کارورزی هستید؟")]
        [Required(ErrorMessage = "لطفا  فیلد  را پر کنید")]
        public string Internship { get; set; }
        [Display(Name = "موبایل")]
        [Required(ErrorMessage = "لطفا موبایل خود را وارد نمایید")]
        [RegularExpression(@"^(\d{11})$", ErrorMessage = "لطفا تلفن ثابت خود را با کد شهرستان به صورت صحیح وارد نمایید")]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }
        [Display(Name = "تلفن ثابت")]
        [Required(ErrorMessage = "لطفا تلفن ثابت خود را وارد نمایید")]
        [RegularExpression(@"^(\d{11})$", ErrorMessage = "لطفا تلفن ثابت خود را با کد شهرستان به صورت صحیح وارد نمایید")]
        [DataType(DataType.PhoneNumber)]

        public string Phone { get; set; }
        [Display(Name = "مواردی را که فکر میکنید در برگزیده شدن شما برای تصدی این شغل موثر است ذکر نمایید")]
        public string DescriptionFull { get; set; }
        [Display(Name = "اگر رزومه ای دارید وارد برای ما ارسال کنید-فرمت فایل ارسالی زیپ باشد")]
        public string Rezome { get; set; }
        [UIHint("PersianDatePicker")]
        public System.DateTime DateSubmitInfo { get; set; }
        public int Status { get; set; }

    }

    [MetadataType(typeof(RegisterUser_MetaData))]
    public partial class RegisterUser
    {

    }
}
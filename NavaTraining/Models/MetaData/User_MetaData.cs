using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace NavaTraining.Models
{
    internal class User_MetaData
    {
        [Key]
       
        public int UserID { get; set; }
        [Display(Name = "RoleTitle")]
        [Required(ErrorMessage = "Please Enter {0}")]
        public int RoleID { get; set; }
        [Display(Name = "UserName")]
        [Required(ErrorMessage = "Please Enter {0}")]
        public string UserName { get; set; }
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Please Enter {0}")]
        [DataType(DataType.EmailAddress, ErrorMessage = "The Entered Email Is Not Correct")]
        public string Email { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please Enter {0}")]
        public string Password { get; set; }
        [Display(Name = "ActiveCode")]
        public string ActiveCode { get; set; }
        [Display(Name = "RegisterDate")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public System.DateTime RegisterDate { get; set; }
        [Display(Name = "IsActive/NoActive")]
        public bool IsActive { get; set; }
    }

    [MetadataType(typeof(User_MetaData))]
    public partial class UserLogin
    {

    }
}
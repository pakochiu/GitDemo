using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.ViewModel
{
    public class EmpViewModel
    {
        [DisplayName("帳號")]
        public string logAccount { get; set; }
        [DisplayName("密碼")]
        [Required(ErrorMessage ="請輸入密碼")]
        public string logPassword { get; set; }
    }

    public class EmpChangePasswordViewModel
    {
        [Required(ErrorMessage ="請輸入帳號")]
        [Display(Name = "帳號")]
        public string oldAccount { get; set; }

        [Required(ErrorMessage = "請輸入重置密碼")]
        [DataType(DataType.Password)]
        [Display(Name = "目前密碼")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "請輸入新密碼")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "新的密碼")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "需與新密碼相符")]
        [DataType(DataType.Password)]
        [Display(Name = "確認輸入密碼")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
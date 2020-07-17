using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication2.ViewModel
{
    public class EmploginViewModel
    {
        public string logAccount { get; set; }
        [Required(ErrorMessage ="請輸入密碼")]
        public string logPassword { get; set; }
        [Compare("logPassword",ErrorMessage ="兩次密碼輸入不一致")]
        [Required(ErrorMessage ="請再次輸入密碼")]
        public string PasswordChack { get; set; }
    }
}
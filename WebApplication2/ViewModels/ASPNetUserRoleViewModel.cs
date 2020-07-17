using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.ViewModels
{
    public class ASPNetUserRoleViewModel
    {
        public AspNetRole getRole { get; set; }
        public tCustomer getCustomer { get; set; }
        public AspNetUser getAspUser { get; set; }
    }
}
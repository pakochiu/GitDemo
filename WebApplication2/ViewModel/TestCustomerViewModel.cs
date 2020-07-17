using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.ViewModel
{
    public class TestCustomerViewModel
    {
        public tCustomer GetCustomer { get; set; }
        public tOrder GetOrder { get; set; }
        public tCheck GetCheck { get; set; }
    }
}
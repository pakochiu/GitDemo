using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.ViewModel
{
    public class OrderListViewModel
    {
        public Nullable<int> fCustomerID { get; set; }
        public Nullable<int> fEmployeeID { get; set; }
        public Nullable<decimal> total { get; set; }
    }
}
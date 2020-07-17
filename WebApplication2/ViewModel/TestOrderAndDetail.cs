using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.ViewModel
{
    public class TestOrderAndDetail
    {
        public tCustomer GetCustomer { get; set; }
        public tOrder GetOrder { get; set; }
        public List<tOrderDetail> GetOrderDetail 
        {          
            get;             
            set; 
        }

        public TestOrderAndDetail(string id)
        {

        }
        //public IEnumerator<tOrderDetail> GetOrderDetail { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Twilio.Rest.Insights.V1.Call;

namespace WebApplication2.ViewModel
{
    public class OrderDetialViewModel
    {
        public int fID { get; set; }
        [DisplayName("訂單編號")]
        public int fOrderID { get; set; }
        [DisplayName("產品編號")]
        public string fProductID { get; set; }
        [DisplayName("高")]
        public Nullable<decimal> fLenght { get; set; }
        [DisplayName("寬")]
        public Nullable<decimal> fWidth { get; set; }
        [DisplayName("數量")]
        public Nullable<int> fQuantity { get; set; }
        [DisplayName("玻璃型號")]
        public Nullable<int> fGlassID { get; set; }
        [DisplayName("小計")]
        public Nullable<decimal> fSubTotal { get; set; }
        public virtual tGlass tGlass { get; set; }
        public virtual tOrder tOrder { get; set; }
        public virtual tProduct tProduct { get; set; }
    }
}
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication2
{
    using System;
    using System.Collections.Generic;
    
    public partial class tCheck
    {
        public int fID { get; set; }
        public int fCustomerID { get; set; }
        public string fCheckNumber { get; set; }
        public decimal fCheckMoney { get; set; }
        public System.DateTime fPaymentDay { get; set; }
        public System.DateTime fTicktedDay { get; set; }
        public bool fPaymentStatus { get; set; }
        public bool fDeleteStatus { get; set; }
        public bool fPayByCash { get; set; }
        public int fOderID { get; set; }
        public string fPaymentPeriod { get; set; }
    
        public virtual tCustomer tCustomer { get; set; }
        public virtual tOrder tOrder { get; set; }
    }
}

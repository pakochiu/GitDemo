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
    
    public partial class tNew
    {
        public int fNewsID { get; set; }
        public string fSubject { get; set; }
        public string fContent { get; set; }
        public Nullable<System.DateTime> fData { get; set; }
        public Nullable<int> fCustomerID { get; set; }
    
        public virtual tCustomer tCustomer { get; set; }
    }
}
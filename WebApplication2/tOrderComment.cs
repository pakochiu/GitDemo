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
    
    public partial class tOrderComment
    {
        public int fID { get; set; }
        public Nullable<int> fOrderID { get; set; }
        public Nullable<int> fEmployeeID { get; set; }
        public Nullable<int> fCustomerID { get; set; }
        public string fComment { get; set; }
        public System.DateTime fUpdateDate { get; set; }
    
        public virtual tEmployee tEmployee { get; set; }
        public virtual tOrder tOrder { get; set; }
    }
}
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
    
    public partial class tFaceDetection
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tFaceDetection()
        {
            this.tWorkingTimes = new HashSet<tWorkingTime>();
        }
    
        public int fFaceDetectID { get; set; }
        public string fUserFaceID { get; set; }
        public Nullable<int> fEmployeeID { get; set; }
    
        public virtual tEmployee tEmployee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tWorkingTime> tWorkingTimes { get; set; }
    }
}

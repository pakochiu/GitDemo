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
    using System.ComponentModel.DataAnnotations;

    public partial class tWorkingTime
    {
        public int fWorkingID { get; set; }
        public int fDateID { get; set; }
        public Nullable<int> fFaceDetectID { get; set; }
        [DisplayFormat(DataFormatString = "{0:t}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> fStartWorkingTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:t}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> fOffWorkingTime { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public virtual tDate tDate { get; set; }
        public virtual tFaceDetection tFaceDetection { get; set; }
    }
}
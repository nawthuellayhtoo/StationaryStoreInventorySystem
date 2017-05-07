//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class Adjustment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Adjustment()
        {
            this.AdjustmentDetails = new HashSet<AdjustmentDetail>();
        }
    
        public string VoucherID { get; set; }
        public string EmpID { get; set; }
        public string AdjustmentStatus { get; set; }
        public Nullable<System.DateTime> VoucherDate { get; set; }
        public string AuthorisedBySupervisor { get; set; }
        public string AuthorisedByManager { get; set; }
        public Nullable<decimal> TotalPrice { get; set; }
        public Nullable<System.DateTime> ApprovalDate { get; set; }
    
        public virtual Employee Employee { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AdjustmentDetail> AdjustmentDetails { get; set; }
    }
}

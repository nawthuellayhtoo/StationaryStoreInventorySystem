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
    
    public partial class AdjustmentDetail
    {
        public string AdjustmentDetailsID { get; set; }
        public string VoucherID { get; set; }
        public string ItemNumber { get; set; }
        public Nullable<int> QuantityAdjustment { get; set; }
        public string SupplierID { get; set; }
        public string Reason { get; set; }
    
        public virtual Adjustment Adjustment { get; set; }
        public virtual InventoryStock InventoryStock { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
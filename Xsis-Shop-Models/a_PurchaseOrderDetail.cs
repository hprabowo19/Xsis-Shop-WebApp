//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Xsis_Shop_Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class a_PurchaseOrderDetail
    {
        public string ID { get; set; }
        public string PurchaseOrderID { get; set; }
        public string ProductID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<double> UnitPrice { get; set; }
    
        public virtual a_Product a_Product { get; set; }
        public virtual a_PurchaseOrder a_PurchaseOrder { get; set; }
    }
}

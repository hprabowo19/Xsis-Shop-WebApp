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
    
    public partial class a_PurchaseOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public a_PurchaseOrder()
        {
            this.a_PurchaseOrderDetail = new HashSet<a_PurchaseOrderDetail>();
        }
    
        public string ID { get; set; }
        public string Code { get; set; }
        public Nullable<System.DateTime> PurchaseDate { get; set; }
        public string SupplierID { get; set; }
        public string Remarks { get; set; }
    
        public virtual a_Supplier a_Supplier { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<a_PurchaseOrderDetail> a_PurchaseOrderDetail { get; set; }
    }
}

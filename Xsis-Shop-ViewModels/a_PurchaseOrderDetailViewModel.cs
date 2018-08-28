using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Xsis_Shop_ViewModels
{
    public class a_PurchaseOrderDetailViewModel
    {
        [Display(Name = "PurchaseOrderDetail ID")]
        [StringLength(50, ErrorMessage = "Maksimal Karakter 50")]
        public string ID { get; set; }

        [Display(Name = "PurchaseOrderDetail PurchaseOrderID")]
        [StringLength(50, ErrorMessage = "Maksimal Karakter 50")]
        public string PurchaseOrderID { get; set; }

        [Display(Name = "PurchaseOrderDetail ProductCode")]
        [StringLength(50, ErrorMessage = "Maksimal Karakter 50")]
        public string Code { get; set; }

        [Display(Name = "PurchaseOrderDetail ProductID")]
        [StringLength(50, ErrorMessage = "Maksimal Karakter 50")]
        public string ProductID { get; set; }

        [Display(Name = "PurchaseOrderDetail ProductName")]
        [StringLength(50, ErrorMessage = "Maksimal Karakter 50")]
        public string Name { get; set; }

        [Display(Name = "PurchaseOrderDetail Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "PurchaseOrderDetail UnitPrice")]
        public float UnitPrice { get; set; }
    }
}

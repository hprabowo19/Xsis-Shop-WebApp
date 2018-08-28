using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Xsis_Shop_ViewModels
{
    public class a_PurchaseOrderViewModel
    {
        [Display(Name = "PurchaseOrder ID")]
        [StringLength(50, ErrorMessage = "Maksimal Karakter 50")]
        public string ID { get; set; }

        [Required]
        [Display(Name = "PurchaseOrder Code")]
        [StringLength(10, ErrorMessage = "Maksimal Karakter 10")]
        public string Code { get; set; }

        [Display(Name = "PurchaseOrder Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime PurchaseDate { get; set; }

        [Display(Name = "PurchaseOrder SupplierID")]
        [StringLength(50, ErrorMessage = "Maksimal Karakter 50")]
        public string SupplierID { get; set; }

        [Display(Name = "PurchaseOrder Name")]
        [StringLength(50, ErrorMessage = "Maksimal Karakter 50")]
        public string Name { get; set; }

        [Display(Name = "PurchaseOrder Name")]
        public string Remarks { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Xsis_Shop_ViewModels
{
    public class a_SupplierViewModel
    {
        [Display(Name = "Supplier ID")]
        [StringLength(50, ErrorMessage = "Maksimal Karakter 50")]
        public string ID { get; set; }

        [Display(Name = "Supplier Code")]
        [StringLength(10, ErrorMessage = "Maksimal Karakter 10")]
        public string Code { get; set; }

        [Required]
        [Display(Name = "Supplier Name")]
        [StringLength(100, ErrorMessage = "Maksimal Karakter 100")]
        public string Name { get; set; }

        [Display(Name = "Supplier City")]
        [StringLength(100, ErrorMessage = "Maksimal Karakter 100")]
        public string City { get; set; }
    }
}

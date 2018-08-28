using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Xsis_Shop_ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nama Produk")]
        [Required(ErrorMessage = "Nama Produk harus diisi")]
        [StringLength(50, ErrorMessage = "Maksimal 50 karakter")]
        public string ProductName { get; set; }

        [Display(Name = "Nama Supplier")]
        [Required(ErrorMessage = "Nama Supplier harus dipilih")]
        public int SupplierId { get; set; }

        [Display(Name = "Nama Supplier")]
        public string SupplierName { get; set; }

        [Display(Name = "Harga Satuan")]
        [RegularExpression(@"^\d+.?\d{0,2}$", ErrorMessage = "Maksimal 2 digit dibelakang koma")]
        [Range(0, 9999999999.99, ErrorMessage = "Maksimal 12 digit")]
        public Nullable<decimal> UnitPrice { get; set; }

        [Display(Name = "Paket Produk")]
        [StringLength(30, ErrorMessage = "Maksimal 30 karakter")]
        public string Package { get; set; }

        [Display(Name = "Produksi Dihentikan?")]
        [Required]
        public bool IsDiscontinued { get; set; }
    }
}

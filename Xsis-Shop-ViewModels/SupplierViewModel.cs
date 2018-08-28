using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Xsis_Shop_ViewModels
{
    public class SupplierViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nama Perusahaan")]
        [Required(ErrorMessage = "Nama Perusahaan harus diisi")]
        [StringLength(40, ErrorMessage = "Maksimal 40 karakter")]
        public string CompanyName { get; set; }

        [Display(Name = "Nama Kontak")]
        [StringLength(50, ErrorMessage = "Maksimal 50 karakter")]
        public string ContactName { get; set; }

        [Display(Name = "Title")]
        [StringLength(40, ErrorMessage = "Maksimal 40 karakter")]
        public string ContactTitle { get; set; }

        [Display(Name = "Kota")]
        [StringLength(40, ErrorMessage = "Maksimal 40 karakter")]
        public string City { get; set; }

        [Display(Name = "Negara")]
        [StringLength(40, ErrorMessage = "Maksimal 40 karakter")]
        public string Country { get; set; }

        [Display(Name = "Telepon")]
        [StringLength(30, ErrorMessage = "Maksimal 30 karakter")]
        public string Phone { get; set; }
        
        [StringLength(30, ErrorMessage = "Maksimal 30 karakter")]
        public string Fax { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }
}

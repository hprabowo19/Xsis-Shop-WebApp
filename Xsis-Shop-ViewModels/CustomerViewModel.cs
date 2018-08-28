using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Xsis_Shop_ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nama Depan")]
        [Required(ErrorMessage = "Nama Depan harus diisi")]
        [StringLength(40, ErrorMessage = "Maksimal 40 karakter")]
        public string FirstName { get; set; }

        [Display(Name = "Nama Belakang")]
        [Required(ErrorMessage = "Nama Belakang harus diisi")]
        [StringLength(40, ErrorMessage = "Maksimal 40 karakter")]
        public string LastName { get; set; }

        [Display(Name = "Kota")]
        [StringLength(40, ErrorMessage = "Maksimal 40 karakter")]
        public string City { get; set; }

        [Display(Name = "Negara")]
        [StringLength(40, ErrorMessage = "Maksimal 40 karakter")]
        public string Country { get; set; }

        [Display(Name = "Telepon")]
        [StringLength(40, ErrorMessage = "Maksimal 20 karakter")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-.● ]?([0-9]{3})[-.● ]?([0-9]{4})$", ErrorMessage = "Nomor Telepon tidak valid")]
        //[RegularExpression("^[ +-()0-9]*$", ErrorMessage = "Hanya menggunakan angka dan karakter '(', ')' dan '-'")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Email tidak sesuai")]
        [StringLength(35, ErrorMessage = "Maksimal 35 karakter")]
        public string Email { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsis_Shop_ViewModels
{
    public class OrderViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Tanggal Order")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "No. Order")]
        public string OrderNumber { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Display(Name = "Nama Customer")]
        public string CustomerName { get; set; }

        [Display(Name = "Total Harga")]
        public decimal? TotalAmount { get; set; }
        
        public List<OrderItemViewModel> OrderItem { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xsis_Shop_ViewModels
{
    public class OrderItemViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }

        [Display(Name = "Nama Produk")]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Harga Satuan")]
        public decimal UnitPrice { get; set; }

        [Required]
        [Display(Name = "Jumlah")]
        public int Quantity { get; set; }

        [Display(Name = "Harga")]
        public decimal Price { get { return UnitPrice * Quantity; } }
    }

    public class OrderItemObject
    {
        public int Id { get; set; }
        public List<OrderItemViewModel> ListOrderItem { get; set; }
        public OrderItemViewModel OrderItem { get; set; }
    }
}

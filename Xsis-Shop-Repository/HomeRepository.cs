using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xsis_Shop_Models;
using Xsis_Shop_ViewModels;

namespace Xsis_Shop_Repository
{
    public class HomeRepository
    {
        public int[] GetTotals()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                int[] arrayTotal = { db.Customer.Count(), db.Supplier.Count(), db.Product.Count(), db.Order.Count() };
                return arrayTotal;
            }
        }

        public List<ProductViewModel> GetRecentProducts(int max)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var result = (from p in db.Product orderby p.Id descending select p).Take(max).ToList();
                List<ProductViewModel> ListProduct = new List<ProductViewModel>();
                foreach (var item in result)
                {
                    ProductViewModel product = new ProductViewModel();
                    product.Id = item.Id;
                    product.ProductName = item.ProductName;
                    product.UnitPrice = item.UnitPrice;

                    ListProduct.Add(product);
                }
                return ListProduct;
            }
        }

        public List<OrderViewModel> GetRecentOrders(int max)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var result = (from p in db.Order orderby p.Id descending select p).Take(max).ToList();
                List<OrderViewModel> ListOrder = new List<OrderViewModel>();
                foreach (var item in result)
                {
                    OrderViewModel order = new OrderViewModel();
                    order.Id = item.Id;
                    order.OrderNumber = item.OrderNumber;
                    order.CustomerName = GetCustomerName(item.CustomerId);
                    order.TotalAmount = item.TotalAmount;

                    ListOrder.Add(order);
                }
                return ListOrder;
            }
        }

        public List<object> GetTopCustomers(int top)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var result = (from p in db.Order
                              group p by p.CustomerId into grp
                              orderby grp.Count() descending
                              select new
                              {
                                  CustomerId = grp.Key,
                                  CustomerName = (from p in db.Customer where p.Id.Equals(grp.Key) select p.FirstName + " " + p.LastName).FirstOrDefault(),
                                  OrderTimes = grp.Count(),
                                  TotalAmount = grp.Sum(p => p.TotalAmount),
                              }).Take(top);
                return result.ToList<object>();
            }
        }

        public List<object> GetTopProducts(int top)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var result = (from p in db.OrderItem
                              group p by p.ProductId into gp
                              orderby gp.Sum(p => p.Quantity) descending
                              select new
                              {
                                  ProductId = gp.Key,
                                  ProductName = (from p in db.Product where p.Id.Equals(gp.Key) select p.ProductName).FirstOrDefault(),
                                  ProductSold = gp.Sum(p => p.Quantity),
                              }).Take(top);
                return result.ToList<object>();
            }
        }

        public string GetCustomerName(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var result = db.Customer.Find(id);
                return result.FirstName + " " + result.LastName;
            }
        }
    }
}

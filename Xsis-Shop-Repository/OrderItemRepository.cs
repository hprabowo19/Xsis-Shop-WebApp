using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xsis_Shop_Models;
using Xsis_Shop_ViewModels;

namespace Xsis_Shop_Repository
{
    public class OrderItemRepository
    {
        public int GetLastOrderItemId()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var result = (from p in db.OrderItem orderby p.Id descending select p.Id).FirstOrDefault() + 1;
                return result;
            }
        }

        public List<OrderItemViewModel> GetProductList()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var ListProduct = db.Product.ToList();
                List<OrderItemViewModel> ListView = new List<OrderItemViewModel>();
                foreach (var item in ListProduct)
                {
                    OrderItemViewModel Model = new OrderItemViewModel();
                    Model.ProductId = item.Id;
                    Model.ProductName = item.ProductName;

                    ListView.Add(Model);
                }
                return ListView;
            }
        }

        public ProductViewModel GetProductById(int Id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var product = (from p in db.Product where p.Id.Equals(Id) select p).FirstOrDefault();
                var result = new ProductViewModel();
                result.ProductName = product.ProductName;
                result.UnitPrice = product.UnitPrice;
                return result;
            }
        }

        public List<OrderItemViewModel> UpdateList(OrderItemObject Obj)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                if (Obj.ListOrderItem.Any(x => x.ProductId.Equals(Obj.OrderItem.ProductId)))
                {
                    Obj.ListOrderItem.FirstOrDefault(x => x.ProductId.Equals(Obj.OrderItem.ProductId)).Quantity += Obj.OrderItem.Quantity;
                }
                else
                {
                    OrderItemViewModel Model = new OrderItemViewModel();
                    var product = GetProductById(Obj.OrderItem.ProductId);
                    Model.Id = Obj.OrderItem.Id + Obj.ListOrderItem.Count;
                    Model.ProductId = Obj.OrderItem.ProductId;
                    Model.UnitPrice = (decimal)product.UnitPrice;
                    Model.ProductName = product.ProductName;
                    Model.Quantity = Obj.OrderItem.Quantity;

                    Obj.ListOrderItem.Add(Model);
                }
                return Obj.ListOrderItem;
            }
        }

        public List<OrderItemViewModel> Remove(OrderItemObject Obj)
        {
            var minId = Obj.ListOrderItem.Select(x => x.Id).FirstOrDefault();
            Obj.ListOrderItem.RemoveAll(x => x.Id.Equals(Obj.Id));
            foreach (var item in Obj.ListOrderItem)
            {
                item.Id = minId++;
            }
            return Obj.ListOrderItem;
        }
    }
}

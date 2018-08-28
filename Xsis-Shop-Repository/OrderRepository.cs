using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xsis_Shop_Models;
using Xsis_Shop_ViewModels;

namespace Xsis_Shop_Repository
{
    public class OrderRepository
    {
        public List<OrderViewModel> GetAllOrder(string id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                // Split parameters to three variable
                string[] parameter = id.Split('|');
                string OrderNumber = parameter[0];
                string tempOrderDate = parameter[1];
                string tempCustomerId = parameter[2];

                // Add boolean variable for conditional statements
                bool NullOrderNumber = string.IsNullOrWhiteSpace(OrderNumber);
                bool NullOrderDate = string.IsNullOrWhiteSpace(tempOrderDate);
                bool NullCustomerId = string.IsNullOrWhiteSpace(tempCustomerId);

                // Convert string to DateTime for comparing in Linq statements
                DateTime? OrderDate = NullOrderDate ? (DateTime?) null :
                    DateTime.Parse(tempOrderDate);

                // string to int too
                int? CustomerId = NullCustomerId ? (int?) null :
                    int.Parse(tempCustomerId);

                List<Order> ListOrder;

                if (NullOrderNumber && NullOrderDate && NullCustomerId)
                {
                    ListOrder = (from p in db.Order orderby p.Id descending select p).ToList();
                }
                else if (NullOrderDate && NullCustomerId)
                {
                    ListOrder = (from p in db.Order where p.OrderNumber.Equals(OrderNumber) orderby p.Id descending select p).ToList();
                }
                else if (NullOrderNumber && NullCustomerId)
                {
                    ListOrder = (from p in db.Order where (DateTime.Compare(p.OrderDate, (DateTime)OrderDate) == 0) orderby p.Id descending select p).ToList();
                }
                else if(NullOrderNumber && NullOrderDate)
                {
                    ListOrder = (from p in db.Order where p.CustomerId.Equals((int)CustomerId) orderby p.Id descending select p).ToList();
                }
                else if (NullCustomerId)
                {
                    ListOrder = (from p in db.Order where p.OrderNumber.Equals(OrderNumber) && (DateTime.Compare(p.OrderDate, (DateTime)OrderDate) == 0) orderby p.Id descending select p).ToList();
                }
                else if (NullOrderDate)
                {
                    ListOrder = (from p in db.Order where p.OrderNumber.Equals(OrderNumber) && p.CustomerId.Equals((int)CustomerId) orderby p.Id descending select p).ToList();
                }
                else if (NullOrderNumber)
                {
                    ListOrder = (from p in db.Order where (DateTime.Compare(p.OrderDate, (DateTime)OrderDate) == 0) && p.CustomerId.Equals((int)CustomerId) orderby p.Id descending select p).ToList();
                }
                else
                {
                    ListOrder = (from p in db.Order where p.OrderNumber.Equals(OrderNumber) && (DateTime.Compare(p.OrderDate, (DateTime)OrderDate) == 0) && p.CustomerId.Equals((int)CustomerId) orderby p.Id descending select p).ToList();
                }

                List<OrderViewModel> ListView = new List<OrderViewModel>();
                foreach (var item in ListOrder)
                {
                    OrderViewModel Model = new OrderViewModel();
                    Model.Id = item.Id;
                    Model.OrderNumber = item.OrderNumber;
                    Model.OrderDate = item.OrderDate;
                    Model.CustomerId = item.CustomerId;
                    Model.CustomerName = new HomeRepository().GetCustomerName(item.CustomerId);
                    Model.TotalAmount = item.TotalAmount;

                    ListView.Add(Model);
                }
                return ListView;
            }
        }
        
        public void CreateNewOrder(OrderViewModel model)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                Order order = new Order();
                order.OrderDate = model.OrderDate;
                order.OrderNumber = model.OrderNumber;
                order.CustomerId = model.CustomerId;
                order.TotalAmount = model.TotalAmount;

                db.Order.Add(order);
                db.SaveChanges();

                foreach (var item in model.OrderItem)
                {
                    OrderItem orderItem = new OrderItem();
                    orderItem.OrderId = (from p in db.Order orderby p.Id descending select p.Id).First();
                    orderItem.ProductId = item.ProductId;
                    orderItem.UnitPrice = item.UnitPrice;
                    orderItem.Quantity = item.Quantity;
                    db.OrderItem.Add(orderItem);
                    db.SaveChanges();
                }
            }
        }

        public List<OrderViewModel> GetCustomerList()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var ListCustomer = db.Customer.ToList();
                List<OrderViewModel> ListView = new List<OrderViewModel>();
                foreach (var item in ListCustomer)
                {
                    OrderViewModel Model = new OrderViewModel();
                    Model.CustomerId = item.Id;
                    Model.CustomerName = item.FirstName + " " + item.LastName;

                    ListView.Add(Model);
                }
                return ListView;
            }
        }

        public OrderViewModel GetOrderById(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var Order = db.Order.Find(id);
                OrderViewModel Model = new OrderViewModel();
                Model.Id = Order.Id;
                Model.OrderDate = Order.OrderDate;
                Model.OrderNumber = Order.OrderNumber;
                Model.CustomerId = Order.CustomerId;
                Model.CustomerName = Order.Customer.FirstName + " " + Order.Customer.LastName;
                Model.TotalAmount = Order.TotalAmount;
                Model.OrderItem = GetOrderItem(id);
                return Model;
            }
        }

        public List<OrderItemViewModel> GetOrderItem(int id)
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var ListOrderItem = (from p in db.OrderItem where p.OrderId.Equals(id) select p).ToList();
                List<OrderItemViewModel> ListView = new List<OrderItemViewModel>();
                foreach (var orderItem in ListOrderItem)
                {
                    OrderItemViewModel Model = new OrderItemViewModel();
                    Model.Id = orderItem.Id;
                    Model.OrderId = orderItem.OrderId;
                    Model.ProductId = orderItem.ProductId;
                    Model.ProductName = orderItem.Product.ProductName;
                    Model.UnitPrice = orderItem.UnitPrice;
                    Model.Quantity = orderItem.Quantity;
                    ListView.Add(Model);
                }
                return ListView;
            }
        }

        public int GetLastOrderId()
        {
            using (ShopDBEntities db = new ShopDBEntities())
            {
                var result = (from p in db.Order orderby p.Id descending select p.Id).FirstOrDefault() + 1;
                return result;
            }
        }
    }
}

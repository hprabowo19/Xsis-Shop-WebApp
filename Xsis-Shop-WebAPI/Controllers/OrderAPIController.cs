using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Xsis_Shop_Repository;
using Xsis_Shop_ViewModels;

namespace Xsis_Shop_WebAPI.Controllers
{
    public class OrderAPIController : ApiController
    {
        private OrderRepository service = new OrderRepository();

        [Route("api/OrderAPI/{id}")]
        [HttpGet]
        public List<OrderViewModel> Get(string id)
        {
            var result = service.GetAllOrder(id);
            return result;
        }

        [Route("api/OrderAPI/GetCustomerList")]
        [HttpGet]
        public List<OrderViewModel> GetCustomerList()
        {
            var result = service.GetCustomerList();
            return result;
        }

        [Route("api/OrderAPI/GetOrderById/{id}")]
        [HttpGet]
        public OrderViewModel GetOrderById(int id)
        {
            var result = service.GetOrderById(id);
            return result;
        }

        [Route("api/OrderAPI/GetLastOrderId")]
        [HttpGet]
        public int GetLastOrderId()
        {
            var result = service.GetLastOrderId();
            return result;
        }

        [Route("api/OrderAPI/")]
        [HttpPost]
        public bool Post(OrderViewModel order)
        {
            try
            {
                service.CreateNewOrder(order);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
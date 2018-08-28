using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Xsis_Shop_Repository;
using Xsis_Shop_ViewModels;
using Xsis_Shop_Models;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Xsis_Shop_WebAPI.Controllers
{
    public class OrderItemAPIController : ApiController
    {
        private OrderItemRepository service = new OrderItemRepository();

        [Route("api/OrderItemAPI/GetProductList")]
        [HttpGet]
        public List<OrderItemViewModel> GetProductList()
        {
            var result = service.GetProductList();
            return result;
        }

        [Route("api/OrderItemAPI/GetLastOrderItemId")]
        [HttpGet]
        public int GetLastOrderItemId()
        {
            var result = service.GetLastOrderItemId();
            return result;
        }

        [Route("api/OrderItemAPI/GetProductById/{Id}")]
        [HttpGet]
        public ProductViewModel GetProductById(int Id)
        {
            var result = service.GetProductById(Id);
            return result;
        }

        [Route("api/OrderItemAPI/UpdateList/")]
        [HttpPost]
        public List<OrderItemViewModel> UpdateList(OrderItemObject Id)
        {
            var result = service.UpdateList(Id);
            return result;
        }

        [Route("api/OrderItemAPI/Remove/")]
        [HttpPost]
        public List<OrderItemViewModel> Remove(OrderItemObject Id)
        {
            var result = service.Remove(Id);
            return result;
        }
    }
}

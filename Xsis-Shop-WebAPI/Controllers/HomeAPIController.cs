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
    public class HomeAPIController : ApiController
    {
        private HomeRepository service = new HomeRepository();

        [HttpGet]
        public int[] GetTotals()
        {
            return service.GetTotals();
        }

        [HttpGet]
        public List<ProductViewModel> GetRecentProducts(int id)
        {
            return service.GetRecentProducts(id);
        }

        [HttpGet]
        public List<OrderViewModel> GetRecentOrders(int id)
        {
            return service.GetRecentOrders(id);
        }

        [HttpGet]
        public List<object> GetTopCustomers(int id)
        {
            return service.GetTopCustomers(id);
        }

        [HttpGet]
        public List<object> GetTopProducts(int id)
        {
            return service.GetTopProducts(id);
        }
    }
}

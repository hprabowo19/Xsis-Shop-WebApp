using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Xsis_Shop_Repository;
using Xsis_Shop_ViewModels;
using System.Web.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Xsis_Shop_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly string API_URL = WebConfigurationManager.AppSettings["Xsis_Shop_WebAPI"];

        public ActionResult Index(FormCollection input)
        {
            string API_END_POINT, result;
            HttpClient client = new HttpClient();
            HttpResponseMessage response;

            API_END_POINT = API_URL + "api/HomeAPI/";
            response = client.GetAsync(API_END_POINT).Result;
            result = response.Content.ReadAsStringAsync().Result.ToString();

            int[] Totals = JsonConvert.DeserializeObject<int[]>(result);
            ViewBag.TotalCustomers = Totals[0];
            ViewBag.TotalSuppliers = Totals[1];
            ViewBag.TotalProducts = Totals[2];
            ViewBag.TotalOrders = Totals[3];

            int max_recent_product = int.Parse(string.IsNullOrWhiteSpace(input["max_recent_product"]) ? "5" : input["max_recent_product"]);
            API_END_POINT = API_URL + "api/HomeAPI/GetRecentProducts/" + (max_recent_product);
            response = client.GetAsync(API_END_POINT).Result;
            result = response.Content.ReadAsStringAsync().Result.ToString();
            ViewBag.RecentProducts = JsonConvert.DeserializeObject<List<ProductViewModel>>(result);
            ViewBag.MaxRecentProduct = max_recent_product;

            int max_recent_order = int.Parse(string.IsNullOrWhiteSpace(input["max_recent_order"]) ? "5" : input["max_recent_order"]);
            API_END_POINT = API_URL + "api/HomeAPI/GetRecentOrders/" + (max_recent_order);
            response = client.GetAsync(API_END_POINT).Result;
            result = response.Content.ReadAsStringAsync().Result.ToString();
            ViewBag.RecentOrders = JsonConvert.DeserializeObject<List<OrderViewModel>>(result);
            ViewBag.MaxRecentOrder = max_recent_order;

            int max_top_customer = int.Parse(string.IsNullOrWhiteSpace(input["max_top_customer"]) ? "5" : input["max_top_customer"]);
            API_END_POINT = API_URL + "api/HomeAPI/GetTopCustomers/" + (max_top_customer);
            response = client.GetAsync(API_END_POINT).Result;
            result = response.Content.ReadAsStringAsync().Result.ToString();
            ViewBag.TopCustomers = JsonConvert.DeserializeObject<List<object>>(result);
            ViewBag.MaxTopCustomer = max_top_customer;

            int max_top_product = int.Parse(string.IsNullOrWhiteSpace(input["max_top_product"]) ? "5" : input["max_top_product"]);
            API_END_POINT = API_URL + "api/HomeAPI/GetTopProducts/" + (max_top_product);
            response = client.GetAsync(API_END_POINT).Result;
            result = response.Content.ReadAsStringAsync().Result.ToString();
            ViewBag.TopProducts = JsonConvert.DeserializeObject<List<object>>(result);
            ViewBag.MaxTopProduct = max_top_product;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}
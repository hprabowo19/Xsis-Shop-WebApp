using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Xsis_Shop_Models;
using Xsis_Shop_ViewModels;

namespace Xsis_Shop_WebApp.Controllers
{
    public class OrdersController : Controller
    {
        private string API_URL = WebConfigurationManager.AppSettings["Xsis_Shop_WebAPI"];

        // GET: Orders
        public ActionResult Index(FormCollection input)
        {
            string API_END_POINT, result;
            HttpClient client = new HttpClient();
            HttpResponseMessage response;

            API_END_POINT = API_URL + "api/OrderAPI/GetCustomerList";
            response = client.GetAsync(API_END_POINT).Result;
            result = response.Content.ReadAsStringAsync().Result.ToString();
            var ListCustomer = JsonConvert.DeserializeObject<List<OrderViewModel>>(result);

            ViewBag.OrderNumber = input["OrderNumber"];
            ViewBag.OrderDate = input["OrderDate"];
            ViewBag.SelectedCustomer = input["CustomerId"];
            ViewBag.CustomerId = new SelectList(ListCustomer, "CustomerId", "CustomerName", ViewBag.SelectedCustomer);
            string date = string.IsNullOrWhiteSpace(input["OrderDate"]) ? string.Empty : input["OrderDate"].Replace("/", "-");
            string id = (input["OrderNumber"] + '|' + date + '|' + input["CustomerId"]);

            API_END_POINT = API_URL + "api/OrderAPI/" + id;
            response = client.GetAsync(API_END_POINT).Result;
            result = response.Content.ReadAsStringAsync().Result.ToString();
            var ListOrder = JsonConvert.DeserializeObject<List<OrderViewModel>>(result);

            return View(ListOrder);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string API_END_POINT, result;
            HttpClient client = new HttpClient();
            HttpResponseMessage response;

            API_END_POINT = API_URL + "api/OrderAPI/GetOrderById/" + id;
            response = client.GetAsync(API_END_POINT).Result;
            result = response.Content.ReadAsStringAsync().Result.ToString();

            var order = JsonConvert.DeserializeObject<OrderViewModel>(result);

            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            string API_END_POINT, result;
            HttpClient client = new HttpClient();
            HttpResponseMessage response;

            API_END_POINT = API_URL + "api/OrderAPI/GetCustomerList";
            response = client.GetAsync(API_END_POINT).Result;
            result = response.Content.ReadAsStringAsync().Result.ToString();
            var ListCustomer = JsonConvert.DeserializeObject<List<OrderViewModel>>(result);

            API_END_POINT = API_URL + "api/OrderAPI/GetLastOrderId";
            response = client.GetAsync(API_END_POINT).Result;
            result = response.Content.ReadAsStringAsync().Result.ToString();
            var LastOrderId = JsonConvert.DeserializeObject<int>(result);

            ViewBag.OrderNumber = "ORD" + DateTime.Now.ToString("yyMM") + LastOrderId.ToString("000");
            ViewBag.CustomerId = new SelectList(ListCustomer, "CustomerId", "CustomerName");
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrderViewModel model)
        {
            string API_END_POINT, result;
            HttpClient client = new HttpClient();
            HttpResponseMessage response;

            if (ModelState.IsValid)
            {
                model.OrderItem = (List<OrderItemViewModel>)TempData["ListOrderItem"];

                string orderJSON = JsonConvert.SerializeObject(model);
                var buffer = Encoding.UTF8.GetBytes(orderJSON);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                API_END_POINT = API_URL + "api/OrderAPI/";
                response = client.PostAsync(API_END_POINT, byteContent).Result;

                result = response.Content.ReadAsStringAsync().Result.ToString();
                bool success = bool.Parse(result);

                if (success)
                {
                    TempData.Clear();
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData.Clear();
                    ModelState.AddModelError(string.Empty, "Something was happened.");
                    return View(model);
                }
            }
            API_END_POINT = API_URL + "api/OrderAPI/GetCustomerList";
            response = client.GetAsync(API_END_POINT).Result;
            result = response.Content.ReadAsStringAsync().Result.ToString();
            var ListCustomer = JsonConvert.DeserializeObject<List<OrderViewModel>>(result);

            API_END_POINT = API_URL + "api/OrderAPI/GetLastOrderId";
            response = client.GetAsync(API_END_POINT).Result;
            result = response.Content.ReadAsStringAsync().Result.ToString();
            var LastOrderId = JsonConvert.DeserializeObject<int>(result);

            ViewBag.OrderNumber = "ORD" + DateTime.Now.ToString("yyMM") + LastOrderId.ToString("000");
            ViewBag.CustomerId = new SelectList(ListCustomer, "CustomerId", "CustomerName", model.CustomerId);
            return View(model);
        }
    }
}

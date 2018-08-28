using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
    public class OrderItemController : Controller
    {
        private string API_URL = WebConfigurationManager.AppSettings["Xsis_Shop_WebAPI"];

        public ActionResult Create(string Id)
        {
            string API_END_POINT, result;
            HttpClient client = new HttpClient();
            HttpResponseMessage response;

            API_END_POINT = API_URL + "api/OrderItemAPI/GetProductList";
            response = client.GetAsync(API_END_POINT).Result;
            result = response.Content.ReadAsStringAsync().Result.ToString();
            var ListProduct = JsonConvert.DeserializeObject<List<OrderItemViewModel>>(result);

            ViewBag.ProductId = new SelectList(ListProduct, "ProductId", "ProductName");
            ViewBag.OrderNumber = Id;
            return PartialView();
        }

        [HttpPost]
        public JsonResult Add(OrderItemViewModel OrderItem)
        {
            string API_END_POINT, result;
            HttpClient client = new HttpClient();
            HttpResponseMessage response;

            API_END_POINT = API_URL + "api/OrderItemAPI/GetLastOrderItemId";
            response = client.GetAsync(API_END_POINT).Result;
            result = response.Content.ReadAsStringAsync().Result.ToString();
            var LastOrderItemId = JsonConvert.DeserializeObject<int>(result);

            OrderItem.Id = LastOrderItemId;
            TempData["OrderItem"] = OrderItem;
            return Json(OrderItem, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get()
        {
            List<OrderItemViewModel> ListOrderItem = TempData["ListOrderItem"] != null ?
                TempData["ListOrderItem"] as List<OrderItemViewModel> : new List<OrderItemViewModel>();

            if (TempData["OrderItem"] != null)
            {
                OrderItemViewModel OrderItem = TempData["OrderItem"] as OrderItemViewModel;

                OrderItemObject Obj = new OrderItemObject();
                Obj.ListOrderItem = ListOrderItem;
                Obj.OrderItem = OrderItem;

                string json = JsonConvert.SerializeObject(Obj);
                var buffer = Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string API_END_POINT = API_URL + "api/OrderItemAPI/UpdateList/";
                HttpClient client = new HttpClient();

                HttpResponseMessage response = client.PostAsync(API_END_POINT, byteContent).Result;

                string result = response.Content.ReadAsStringAsync().Result.ToString();
                ListOrderItem = JsonConvert.DeserializeObject<List<OrderItemViewModel>>(result);
            }

            TempData["ListOrderItem"] = ListOrderItem;
            TempData.Keep("ListOrderItem");
            return Json(ListOrderItem, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RemoveItem(int Id)
        {
            List<OrderItemViewModel> ListOrderItem = (List<OrderItemViewModel>)TempData["ListOrderItem"];

            OrderItemObject Obj = new OrderItemObject();
            Obj.ListOrderItem = ListOrderItem;
            Obj.Id = Id;

            string json = JsonConvert.SerializeObject(Obj);
            var buffer = Encoding.UTF8.GetBytes(json);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string API_END_POINT = API_URL + "api/OrderItemAPI/Remove/";
            HttpClient client = new HttpClient();

            HttpResponseMessage response = client.PostAsync(API_END_POINT, byteContent).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            ListOrderItem = JsonConvert.DeserializeObject<List<OrderItemViewModel>>(result);


            //var minId = ListOrderItem.Select(x => x.Id).FirstOrDefault();
            //ListOrderItem.RemoveAll(x => x.Id.Equals(Id));
            //foreach (var item in ListOrderItem)
            //{
            //    item.Id = minId++;
            //}

            TempData["ListOrderItem"] = ListOrderItem;
            return Json(ListOrderItem, JsonRequestBehavior.AllowGet);
        }
    }
}
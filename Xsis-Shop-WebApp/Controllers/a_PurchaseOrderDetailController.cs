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
    public class a_PurchaseOrderDetailController : Controller
    {
        private string API_URL = WebConfigurationManager.AppSettings["Xsis_Shop_WebAPI"];
        private a_PurchaseOrderDetailRepository service = new a_PurchaseOrderDetailRepository();

        // GET: a_PurchaseOrderDetails
        public ActionResult Index()
        {
            string API_END_POINT = API_URL + "api/a_PurchaseOrderDetailAPI/";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            var Lista_PurchaseOrderDetail = JsonConvert.DeserializeObject<List<a_PurchaseOrderDetailViewModel>>(result);
            return View(Lista_PurchaseOrderDetail.ToList());
        }

        // GET: a_PurchaseOrderDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string API_END_POINT = API_URL + "api/a_PurchaseOrderDetailAPI/Get/" + (id ?? 0);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            a_PurchaseOrderDetailViewModel Model = JsonConvert.DeserializeObject<a_PurchaseOrderDetailViewModel>(result);
            if (Model == null)
            {
                return HttpNotFound();
            }

            return View(Model);
        }

        // GET: a_PurchaseOrderDetails/Create
        public ActionResult Create()
        {
            string API_END_POINT = API_URL + "api/a_PurchaseOrderDetailAPI/Geta_PurchaseOrderList/" + ("get");
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            var Lista_PurchaseOrder = JsonConvert.DeserializeObject<List<a_PurchaseOrderViewModel>>(result);
            var Lista_Product = JsonConvert.DeserializeObject<List<a_ProductViewModel>>(result);

            ViewBag.PurchaseOrderID = new SelectList(Lista_PurchaseOrder, "Id", "Remarks");
            ViewBag.ProductID = new SelectList(Lista_Product, "Id", "Name");
            return View();
        }

        // POST: a_PurchaseOrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(a_PurchaseOrderDetailViewModel a_PurchaseOrderDetail)
        {
            string API_END_POINT, result;
            HttpClient client = new HttpClient();
            HttpResponseMessage response;

            if (ModelState.IsValid)
            {
                string a_PurchaseOrderDetailJSON = JsonConvert.SerializeObject(a_PurchaseOrderDetail);
                var buffer = Encoding.UTF8.GetBytes(a_PurchaseOrderDetailJSON);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                API_END_POINT = API_URL + "api/a_PurchaseOrderDetailAPI/";

                response = client.PostAsync(API_END_POINT, byteContent).Result;

                result = response.Content.ReadAsStringAsync().Result.ToString();
                bool success = bool.Parse(result);

                if (success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Something was happened.");
                    return View(a_PurchaseOrderDetail);
                }
            }
            API_END_POINT = API_URL + "api/a_PurchaseOrderDetailAPI/Geta_PurchaseOrderList/" + ("get");
            response = client.GetAsync(API_END_POINT).Result;

            result = response.Content.ReadAsStringAsync().Result.ToString();
            var Lista_PurchaseOrder = JsonConvert.DeserializeObject<List<a_PurchaseOrderViewModel>>(result);
            var Lista_Product = JsonConvert.DeserializeObject<List<a_ProductViewModel>>(result);

            ViewBag.PurchaseOrderID = new SelectList(Lista_PurchaseOrder, "Id", "Remarks", a_PurchaseOrderDetail.PurchaseOrderID);
            ViewBag.ProductID = new SelectList(Lista_Product, "Id", "Name", a_PurchaseOrderDetail.ProductID);
            return View(a_PurchaseOrderDetail);
        }

        // GET: a_PurchaseOrderDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string API_END_POINT = API_URL + "api/a_PurchaseOrderDetailAPI/Get/" + (id ?? 0);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            a_PurchaseOrderDetailViewModel Model = JsonConvert.DeserializeObject<a_PurchaseOrderDetailViewModel>(result);
            if (Model == null)
            {
                return HttpNotFound();
            }

            ViewBag.PurchaseOrderID = new SelectList(service.Geta_PurchaseOrderList(), "Id", "Remarks", Model.PurchaseOrderID);
            ViewBag.ProductID = new SelectList(service.Geta_ProductList(), "Id", "Name", Model.ProductID);
            return View(Model);
        }

        // POST: a_PurchaseOrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(a_PurchaseOrderDetailViewModel a_PurchaseOrderDetail)
        {
            string API_END_POINT, result;
            HttpClient client = new HttpClient();
            HttpResponseMessage response;
            if (ModelState.IsValid)
            {
                string a_PurchaseOrderDetailJSON = JsonConvert.SerializeObject(a_PurchaseOrderDetail);
                var buffer = Encoding.UTF8.GetBytes(a_PurchaseOrderDetailJSON);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                API_END_POINT = API_URL + "api/a_PurchaseOrderDetailAPI/";

                response = client.PutAsync(API_END_POINT, byteContent).Result;

                result = response.Content.ReadAsStringAsync().Result.ToString();
                bool success = bool.Parse(result);

                if (success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Something was happened.");
                    return View(a_PurchaseOrderDetail);
                }
            }
            API_END_POINT = API_URL + "api/a_PurchaseOrderDetailAPI/Geta_PurchaseOrderList/" + ("get");
            response = client.GetAsync(API_END_POINT).Result;

            result = response.Content.ReadAsStringAsync().Result.ToString();
            var Lista_PurchaseOrder = JsonConvert.DeserializeObject<List<a_PurchaseOrderViewModel>>(result);
            var Lista_Product = JsonConvert.DeserializeObject<List<a_ProductViewModel>>(result);

            ViewBag.PurchaseOrderID = new SelectList(Lista_PurchaseOrder, "Id", "Remarks", a_PurchaseOrderDetail.PurchaseOrderID);
            ViewBag.ProductID = new SelectList(Lista_Product, "Id", "Name", a_PurchaseOrderDetail.ProductID);
            return View(a_PurchaseOrderDetail);
        }

        // GET: a_PurchaseOrderDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string API_END_POINT = API_URL + "api/a_PurchaseOrderDetailAPI/Get/" + (id ?? 0);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            a_PurchaseOrderDetailViewModel Model = JsonConvert.DeserializeObject<a_PurchaseOrderDetailViewModel>(result);
            if (Model == null)
            {
                return HttpNotFound();
            }
            return View(Model);
        }

        // POST: a_PurchaseOrderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string API_END_POINT = API_URL + "api/a_PurchaseOrderDetailAPI/" + (id);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.DeleteAsync(API_END_POINT).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            bool success = bool.Parse(result);

            if (success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Something was happened.");
                return HttpNotFound();
            }
        }
    }
}

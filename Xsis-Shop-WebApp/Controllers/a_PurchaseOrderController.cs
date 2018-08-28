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
    public class a_PurchaseOrderController : Controller
    {
        private string API_URL = WebConfigurationManager.AppSettings["Xsis_Shop_WebAPI"];
        private a_PurchaseOrderRepository service = new a_PurchaseOrderRepository();

        // GET: a_PurchaseOrders
        public ActionResult Index()
        {
            string API_END_POINT = API_URL + "api/a_PurchaseOrderAPI/";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            var Lista_PurchaseOrder = JsonConvert.DeserializeObject<List<a_PurchaseOrderViewModel>>(result);
            return View(Lista_PurchaseOrder.ToList());
        }

        // GET: a_PurchaseOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string API_END_POINT = API_URL + "api/a_PurchaseOrderAPI/Get/" + (id ?? 0);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            a_PurchaseOrderViewModel Model = JsonConvert.DeserializeObject<a_PurchaseOrderViewModel>(result);
            if (Model == null)
            {
                return HttpNotFound();
            }

            return View(Model);
        }

        // GET: a_PurchaseOrders/Create
        public ActionResult Create()
        {
            string API_END_POINT = API_URL + "api/a_PurchaseOrderAPI/Geta_SupplierList/" + ("get");
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            var Lista_Supplier = JsonConvert.DeserializeObject<List<a_SupplierViewModel>>(result);

            ViewBag.SupplierID = new SelectList(Lista_Supplier, "Id", "Name");
            return View();
        }

        // POST: a_PurchaseOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(a_PurchaseOrderViewModel a_PurchaseOrder)
        {
            string API_END_POINT, result;
            HttpClient client = new HttpClient();
            HttpResponseMessage response;

            if (ModelState.IsValid)
            {
                string a_PurchaseOrderJSON = JsonConvert.SerializeObject(a_PurchaseOrder);
                var buffer = Encoding.UTF8.GetBytes(a_PurchaseOrderJSON);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                API_END_POINT = API_URL + "api/a_PurchaseOrderAPI/";

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
                    return View(a_PurchaseOrder);
                }
            }
            API_END_POINT = API_URL + "api/a_PurchaseOrderAPI/Geta_SupplierList/" + ("get");
            response = client.GetAsync(API_END_POINT).Result;

            result = response.Content.ReadAsStringAsync().Result.ToString();
            var Lista_Supplier = JsonConvert.DeserializeObject<List<a_SupplierViewModel>>(result);

            ViewBag.SupplierID = new SelectList(Lista_Supplier, "Id", "Name", a_PurchaseOrder.SupplierID);
            return View(a_PurchaseOrder);
        }

        // GET: a_PurchaseOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string API_END_POINT = API_URL + "api/a_PurchaseOrderAPI/Get/" + (id ?? 0);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            a_PurchaseOrderViewModel Model = JsonConvert.DeserializeObject<a_PurchaseOrderViewModel>(result);
            if (Model == null)
            {
                return HttpNotFound();
            }

            ViewBag.SupplierID = new SelectList(service.Geta_SupplierList(), "Id", "Name", Model.SupplierID);
            return View(Model);
        }

        // POST: a_PurchaseOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(a_PurchaseOrderViewModel a_PurchaseOrder)
        {
            string API_END_POINT, result;
            HttpClient client = new HttpClient();
            HttpResponseMessage response;
            if (ModelState.IsValid)
            {
                string a_PurchaseOrderJSON = JsonConvert.SerializeObject(a_PurchaseOrder);
                var buffer = Encoding.UTF8.GetBytes(a_PurchaseOrderJSON);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                API_END_POINT = API_URL + "api/a_PurchaseOrderAPI/";

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
                    return View(a_PurchaseOrder);
                }
            }
            API_END_POINT = API_URL + "api/a_PurchaseOrderAPI/Geta_SupplierList/" + ("get");
            response = client.GetAsync(API_END_POINT).Result;

            result = response.Content.ReadAsStringAsync().Result.ToString();
            var Lista_Supplier = JsonConvert.DeserializeObject<List<a_SupplierViewModel>>(result);

            ViewBag.SupplierID = new SelectList(Lista_Supplier, "Id", "Name", a_PurchaseOrder.SupplierID);
            return View(a_PurchaseOrder);
        }

        // GET: a_PurchaseOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            string API_END_POINT = API_URL + "api/a_PurchaseOrderAPI/Get/" + (id ?? 0);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            a_PurchaseOrderViewModel Model = JsonConvert.DeserializeObject<a_PurchaseOrderViewModel>(result);
            if (Model == null)
            {
                return HttpNotFound();
            }
            return View(Model);
        }

        // POST: a_PurchaseOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            string API_END_POINT = API_URL + "api/a_PurchaseOrderAPI/" + (id);
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

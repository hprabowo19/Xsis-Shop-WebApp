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
    public class a_SupplierController : Controller
    {
        private string API_URL = WebConfigurationManager.AppSettings["Xsis_Shop_WebAPI"];
        private a_SupplierRepository service = new a_SupplierRepository();

        // GET: a_Supplier
        public ActionResult Index()
        {
            string API_END_POINT = API_URL + "api/a_SupplierAPI/";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            var Lista_Supplier = JsonConvert.DeserializeObject<List<a_SupplierViewModel>>(result);

            return View(Lista_Supplier.ToList());
        }

        [HttpPost]
        public ActionResult Index(FormCollection input)
        {
            string API_END_POINT = API_URL + "api/a_SupplierAPI/Search/" + (input["Name"] + '|' + input["City"]);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            List<a_SupplierViewModel> Lista_Supplier = JsonConvert.DeserializeObject<List<a_SupplierViewModel>>(result);

            ViewBag.Name = input["Name"];
            ViewBag.City = input["City"];

            return View(Lista_Supplier.ToList());
        }

        // GET: a_Suppliers/Details/{id}
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string API_END_POINT = API_URL + "api/a_SupplierAPI/Get/" + (id ?? 0);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            a_SupplierViewModel Model = JsonConvert.DeserializeObject<a_SupplierViewModel>(result);

            if (Model == null)
            {
                return HttpNotFound();
            }
            return View(Model);
        }

        // GET: a_Suppliers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: a_Suppliers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(a_SupplierViewModel a_Supplier)
        {
            if (ModelState.IsValid)
            {
                string a_SupplierJSON = JsonConvert.SerializeObject(a_Supplier);
                var buffer = Encoding.UTF8.GetBytes(a_SupplierJSON);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string API_END_POINT = API_URL + "api/a_SupplierAPI/";
                HttpClient client = new HttpClient();

                HttpResponseMessage response = client.PostAsync(API_END_POINT, byteContent).Result;

                string result = response.Content.ReadAsStringAsync().Result.ToString();
                bool success = bool.Parse(result);

                if (success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Something was happened.");
                    return View(a_Supplier);
                }
            }
            return View(a_Supplier);
        }

        // GET: a_Suppliers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string API_END_POINT = API_URL + "api/a_SupplierAPI/Get/" + (id ?? 0);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            a_SupplierViewModel Model = JsonConvert.DeserializeObject<a_SupplierViewModel>(result);

            if (Model == null)
            {
                return HttpNotFound();
            }
            return View(Model);
        }

        // POST: a_Suppliers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(a_SupplierViewModel a_Supplier)
        {
            if (ModelState.IsValid)
            {
                string a_SupplierJSON = JsonConvert.SerializeObject(a_Supplier);
                var buffer = Encoding.UTF8.GetBytes(a_SupplierJSON);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string API_END_POINT = API_URL + "api/a_SupplierAPI/";
                HttpClient client = new HttpClient();

                HttpResponseMessage response = client.PutAsync(API_END_POINT, byteContent).Result;

                string result = response.Content.ReadAsStringAsync().Result.ToString();
                bool success = bool.Parse(result);

                if (success)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Something was happened.");
                    return View(a_Supplier);
                }
            }
            return View(a_Supplier);
        }

        // GET: a_Suppliers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string API_END_POINT = API_URL + "api/a_SupplierAPI/Get/" + (id ?? 0);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            a_SupplierViewModel Model = JsonConvert.DeserializeObject<a_SupplierViewModel>(result);

            if (Model == null)
            {
                return HttpNotFound();
            }
            return View(Model);
        }

        // POST: a_Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            string API_END_POINT = API_URL + "api/a_SupplierAPI/" + (id);
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

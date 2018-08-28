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
    public class a_ProductController : Controller
    {
        private string API_URL = WebConfigurationManager.AppSettings["Xsis_Shop_WebAPI"];
        private a_ProductRepository service = new a_ProductRepository();

        // GET: a_Product
        public ActionResult Index()
        {
            string API_END_POINT = API_URL + "api/a_ProductAPI/";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            var Lista_Product = JsonConvert.DeserializeObject<List<a_ProductViewModel>>(result);

            return View(Lista_Product.ToList());
        }

        [HttpPost]
        public ActionResult Index(FormCollection input)
        {
            string API_END_POINT = API_URL + "api/a_ProductAPI/Search/" + (input["Name"]);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            List<a_ProductViewModel> Lista_Product = JsonConvert.DeserializeObject<List<a_ProductViewModel>>(result);

            ViewBag.Name = input["Name"];

            return View(Lista_Product.ToList());
        }

        // GET: a_Products/Details/{id}
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string API_END_POINT = API_URL + "api/a_ProductAPI/Get/" + (id ?? 0);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            a_ProductViewModel Model = JsonConvert.DeserializeObject<a_ProductViewModel>(result);

            if (Model == null)
            {
                return HttpNotFound();
            }
            return View(Model);
        }

        // GET: a_Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: a_Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(a_ProductViewModel a_Product)
        {
            if (ModelState.IsValid)
            {
                string a_ProductJSON = JsonConvert.SerializeObject(a_Product);
                var buffer = Encoding.UTF8.GetBytes(a_ProductJSON);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string API_END_POINT = API_URL + "api/a_ProductAPI/";
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
                    return View(a_Product);
                }
            }
            return View(a_Product);
        }

        // GET: a_Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string API_END_POINT = API_URL + "api/a_ProductAPI/Get/" + (id ?? 0);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            a_ProductViewModel Model = JsonConvert.DeserializeObject<a_ProductViewModel>(result);

            if (Model == null)
            {
                return HttpNotFound();
            }
            return View(Model);
        }

        // POST: a_Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(a_ProductViewModel a_Product)
        {
            if (ModelState.IsValid)
            {
                string a_ProductJSON = JsonConvert.SerializeObject(a_Product);
                var buffer = Encoding.UTF8.GetBytes(a_ProductJSON);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string API_END_POINT = API_URL + "api/a_ProductAPI/";
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
                    return View(a_Product);
                }
            }
            return View(a_Product);
        }

        // GET: a_Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string API_END_POINT = API_URL + "api/a_ProductAPI/Get/" + (id ?? 0);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            a_ProductViewModel Model = JsonConvert.DeserializeObject<a_ProductViewModel>(result);

            if (Model == null)
            {
                return HttpNotFound();
            }
            return View(Model);
        }

        // POST: a_Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            string API_END_POINT = API_URL + "api/a_ProductAPI/" + (id);
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

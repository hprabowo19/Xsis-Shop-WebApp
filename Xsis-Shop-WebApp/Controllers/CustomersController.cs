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
    public class CustomersController : Controller
    {
        private string API_URL = WebConfigurationManager.AppSettings["Xsis_Shop_WebAPI"];
        private CustomerRepository service = new CustomerRepository();
        
        // GET: Customers
        public ActionResult Index()
        {
            string API_END_POINT = API_URL + "api/CustomerAPI/";
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            var ListCustomer = JsonConvert.DeserializeObject<List<CustomerViewModel>>(result);
            
            return View(ListCustomer.ToList());
        }

        [HttpPost]
        public ActionResult Index(FormCollection input)
        {
            //List<CustomerViewModel> ListCustomer = null;

            string API_END_POINT = API_URL + "api/CustomerAPI/Search/" + (input["FullName"] + '|' + input["Place"] + '|' + input["Email"]);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            List<CustomerViewModel> ListCustomer = JsonConvert.DeserializeObject<List<CustomerViewModel>>(result);

            ViewBag.fullname = input["FullName"];
            ViewBag.place = input["Place"];
            ViewBag.email = input["Email"];

            return View(ListCustomer.ToList());
        }

        // GET: Customers/Details/{id}
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            string API_END_POINT = API_URL + "api/CustomerAPI/Get/" + (id ?? 0);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            CustomerViewModel Model = JsonConvert.DeserializeObject<CustomerViewModel>(result);
            
            if (Model == null)
            {
                return HttpNotFound();
            }
            return View(Model);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel customer)
        {
            if (ModelState.IsValid)
            {
                string API_CEK_NAMA = API_URL + "api/CustomerAPI/CekNama/" + customer.FirstName + "/" + customer.LastName;
                HttpClient cekNamaClient = new HttpClient();
                HttpResponseMessage cekNamaResponse = cekNamaClient.GetAsync(API_CEK_NAMA).Result;
                string result = cekNamaResponse.Content.ReadAsStringAsync().Result.ToString();
                bool NameAlreadyExists = bool.Parse(result);

                string API_CEK_EMAIL = API_URL + "api/CustomerAPI/CekEmail/" + customer.Email;
                HttpClient cekEmailClient = new HttpClient();
                HttpResponseMessage cekEmailResponse = cekEmailClient.GetAsync(API_CEK_EMAIL).Result;
                result = string.IsNullOrWhiteSpace(customer.Email) ? "false": cekEmailResponse.Content.ReadAsStringAsync().Result.ToString();
                bool EmailAlreadyExists = bool.Parse(result);
                
                if (NameAlreadyExists)
                    ModelState.AddModelError(string.Empty, "Customer Name already exists.");

                if (EmailAlreadyExists)
                    ModelState.AddModelError(string.Empty, "Customer Email already exists.");

                if (NameAlreadyExists || EmailAlreadyExists)
                    return View(customer);
                else
                {
                    string customerJSON = JsonConvert.SerializeObject(customer);
                    var buffer = Encoding.UTF8.GetBytes(customerJSON);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    string API_END_POINT = API_URL + "api/CustomerAPI/";
                    HttpClient client = new HttpClient();

                    HttpResponseMessage response = client.PostAsync(API_END_POINT, byteContent).Result;

                    result = response.Content.ReadAsStringAsync().Result.ToString();
                    bool success = bool.Parse(result);
                    
                    if (success)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Something was happened.");
                        return View(customer);
                    }
                }
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string API_END_POINT = API_URL + "api/CustomerAPI/Get/" + (id ?? 0);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            CustomerViewModel Model = JsonConvert.DeserializeObject<CustomerViewModel>(result);

            if (Model == null)
            {
                return HttpNotFound();
            }
            return View(Model);
        }

        // POST: Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerViewModel customer)
        {
            if (ModelState.IsValid)
            {
                string customerJSON = JsonConvert.SerializeObject(customer);
                var buffer = Encoding.UTF8.GetBytes(customerJSON);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string API_END_POINT = API_URL + "api/CustomerAPI/";
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
                    return View(customer);
                }
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string API_END_POINT = API_URL + "api/CustomerAPI/Get/" + (id ?? 0);
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(API_END_POINT).Result;

            string result = response.Content.ReadAsStringAsync().Result.ToString();
            CustomerViewModel Model = JsonConvert.DeserializeObject<CustomerViewModel>(result);

            if (Model == null)
            {
                return HttpNotFound();
            }
            return View(Model);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            string API_END_POINT = API_URL + "api/CustomerAPI/" + (id);
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

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
    public class a_SupplierAPIController : ApiController
    {
        private a_SupplierRepository service = new a_SupplierRepository();

        [HttpGet]
        public List<a_SupplierViewModel> Get()
        {
            var result = service.GetAlla_Supplier();
            return result;
        }

        [HttpGet]
        public a_SupplierViewModel Get(int id)
        {
            var result = service.Geta_SupplierById(id);
            return result;
        }

        [HttpGet]
        public List<a_SupplierViewModel> Search(string id)
        {
            string[] ID = id.Split('|');

            string Name = ID[0];
            string City = ID[1];

            var result = service.GetAlla_Supplier(Name, City);
            return result;
        }
        
        [HttpPost]
        public bool Post(a_SupplierViewModel a_Supplier)
        {
            try
            {
                service.CreateNewa_Supplier(a_Supplier);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        [HttpPut]
        public bool Put(a_SupplierViewModel a_Supplier)
        {
            try
            {
                service.Updatea_Supplier(a_Supplier);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            try
            {
                service.Deletea_Supplier(id);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
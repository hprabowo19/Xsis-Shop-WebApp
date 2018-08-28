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
    public class SupplierAPIController : ApiController
    {
        private SupplierRepository service = new SupplierRepository();

        [HttpGet]
        public List<SupplierViewModel> Get()
        {
            var result = service.GetAllSupplier();
            return result;
        }

        [HttpGet]
        public SupplierViewModel Get(int id)
        {
            var result = service.GetSupplierById(id);
            result.Products = service.GetProduct(id);
            return result;
        }

        [HttpPost]
        public bool Post(SupplierViewModel supplier)
        {
            try
            {
                service.CreateNewSupplier(supplier);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        [HttpPut]
        public bool Put(SupplierViewModel supplier)
        {
            try
            {
                service.UpdateSupplier(supplier);
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
                service.DeleteSupplier(id);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
